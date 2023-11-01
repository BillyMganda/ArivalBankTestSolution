using ArivalBankTest.Data;
using ArivalBankTest.Models;
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

        public async Task AddCodeAsync(ConfirmationCode code)
        {
            _context.ConfirmationCodes.Add(code);
            await _context.SaveChangesAsync();
        }

        public async Task<ConfirmationCode> GetCodeAsync(string phoneNumber, string code)
        {
            var results = await _context.ConfirmationCodes
                .FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber && c.Code == code);

            return results!;
        }

        public async Task<bool> HasMaxConcurrentCodesAsync(string phoneNumber, int maxConcurrentCodes)
        {
            var codeCount = await _context.ConfirmationCodes
                .CountAsync(c => c.PhoneNumber == phoneNumber);

            return codeCount >= maxConcurrentCodes;
        }
    }
}
