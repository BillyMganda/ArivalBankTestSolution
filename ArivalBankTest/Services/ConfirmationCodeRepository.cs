using ArivalBankTest.Data;
using ArivalBankTest.Models;
using ArivalBankTest.Requests;
using Microsoft.EntityFrameworkCore;

namespace ArivalBankTest.Services
{
    public class ConfirmationCodeRepository : IConfirmationCodeRepository
    {
        private readonly ApplicationDbContext _context;
        public ConfirmationCodeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GenerateCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task AddConfirmationCodeToDbAsync(SendCodeRequestModel requestModel)
        {
            var newConfirmationCode = new ConfirmationCode
            {
                PhoneNumber = requestModel.PhoneNumber,
                Code = GenerateCode(),
                ExpirationTime = DateTime.UtcNow,
            };

            _context.ConfirmationCodes.Add(newConfirmationCode);
            await _context.SaveChangesAsync();
        }

        public async Task<ConfirmationCode> GetCodeAsync(CheckCodeRequestModel requestModel)
        {
            var response = await _context.ConfirmationCodes
                .FirstOrDefaultAsync(c => c.PhoneNumber == requestModel.PhoneNumber && c.Code == requestModel.Code);

            return response!;
        }

        public async Task<bool> HasMaxConcurrentCodesAsync(string phoneNumber, int maxConcurrentCodes)
        {
            var codeCount = await _context.ConfirmationCodes
                .CountAsync(c => c.PhoneNumber == phoneNumber);

            return codeCount >= maxConcurrentCodes;
        }
    }
}
