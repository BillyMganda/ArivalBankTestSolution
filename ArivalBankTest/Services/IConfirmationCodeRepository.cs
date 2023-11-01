using ArivalBankTest.Models;
using ArivalBankTest.Requests;

namespace ArivalBankTest.Services
{
    public interface IConfirmationCodeRepository
    {
        Task AddConfirmationCodeToDbAsync(SendCodeRequestModel requestModel);
        Task<ConfirmationCode> GetCodeAsync(string phoneNumber, string code);
        Task<bool> HasMaxConcurrentCodesAsync(string phoneNumber, int maxConcurrentCodes);
    }
}
