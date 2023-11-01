using ArivalBankTest.Models;
using ArivalBankTest.Requests;

namespace ArivalBankTest.Services
{
    public interface IConfirmationCodeRepository
    {
        string GenerateRandomCode();
        Task AddConfirmationCodeToDbAsync(SendCodeRequestModel requestModel);
        Task<ConfirmationCode> GetCodeAsync(CheckCodeRequestModel requestModel);
        Task<bool> HasMaxConcurrentCodesAsync(string phoneNumber);
    }
}
