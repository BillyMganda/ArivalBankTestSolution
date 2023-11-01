using ArivalBankTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArivalBankTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwoFactorAuthenticationController : ControllerBase
    {
        private readonly IConfirmationCodeRepository _codeRepository;
        public TwoFactorAuthenticationController(IConfirmationCodeRepository codeRepository)
        {
            _codeRepository = codeRepository;
        }
    }
}
