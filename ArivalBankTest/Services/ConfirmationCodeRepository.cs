using ArivalBankTest.Data;
using ArivalBankTest.Models;
using ArivalBankTest.Requests;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ArivalBankTest.Services
{
    public class ConfirmationCodeRepository : IConfirmationCodeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IRedisCacheProvider _redisCache;
        public ConfirmationCodeRepository(ApplicationDbContext context, IConfiguration configuration, IRedisCacheProvider redisCache)
        {
            _context = context;
            _configuration = configuration;
            _redisCache = redisCache;
        }

        public string GenerateRandomCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task AddConfirmationCodeToDbAsync(SendCodeRequestModel requestModel)
        {
            string code = GenerateRandomCode();
            double ExpirationTime = Convert.ToDouble(_configuration.GetSection("ServiceConfigs:CodeExpirationMinutes").Value);

            var newConfirmationCode = new ConfirmationCode
            {
                PhoneNumber = requestModel.PhoneNumber,
                Code = code,
                ExpirationTime = DateTime.UtcNow.AddMinutes(ExpirationTime),
            };

            Log.Information($"Generated code for {requestModel.PhoneNumber}: {code}");

            _context.ConfirmationCodes.Add(newConfirmationCode);
            await _context.SaveChangesAsync();
        }

        public async Task<ConfirmationCode> GetCodeAsync(CheckCodeRequestModel requestModel)
        {
            var cacheKey = $"ConfirmationCode:{requestModel.PhoneNumber}:{requestModel.Code}";
            var cachedCode = await _redisCache.GetAsync<ConfirmationCode>(cacheKey);

            if (cachedCode != null)
            {
                return cachedCode;
            }
            else
            {
                var response = await _context.ConfirmationCodes
                .FirstOrDefaultAsync(c => c.PhoneNumber == requestModel.PhoneNumber && c.Code == requestModel.Code);

                if (response != null)
                {
                    // Cache the result for future use
                    await _redisCache.SetAsync(cacheKey, response, TimeSpan.FromMinutes(5));

                    return response;
                }

                return null!;
            }            
        }

        public async Task<bool> HasMaxConcurrentCodesAsync(string phoneNumber)
        {
            int concurrentCodesPerPhone = Convert.ToInt32(_configuration.GetSection("ServiceConfigs:ConcurrentCodesPerPhone").Value);

            var codeCount = await _context.ConfirmationCodes
                .CountAsync(c => c.PhoneNumber == phoneNumber);

            return codeCount >= concurrentCodesPerPhone;
        }
    }
}
