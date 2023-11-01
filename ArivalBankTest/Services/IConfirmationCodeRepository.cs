using ArivalBankTest.Models;

namespace ArivalBankTest.Services
{
    public interface IConfirmationCodeRepository
    {
        Task AddCodeAsync(ConfirmationCode code);
        Task<ConfirmationCode> GetCodeAsync(string phoneNumber, string code);
        Task<bool> HasMaxConcurrentCodesAsync(string phoneNumber, int maxConcurrentCodes);
    }
}
