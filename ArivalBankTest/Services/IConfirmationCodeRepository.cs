using ArivalBankTest.Models;
using ArivalBankTest.Requests;

namespace ArivalBankTest.Services
{
    public interface IConfirmationCodeRepository
    {
        Task AddConfirmationCodeToDbAsync(SendCodeRequestModel requestModel);
        Task<ConfirmationCode> GetCodeAsync(CheckCodeRequestModel requestModel);
        Task<bool> HasMaxConcurrentCodesAsync(string phoneNumber);
    }
}
