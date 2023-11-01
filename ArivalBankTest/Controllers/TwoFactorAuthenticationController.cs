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

        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode([FromBody] SendCodeRequestModel model)
        {           
            return Ok(new { CodeSent = true });
        }

        [HttpPost("check-code")]
        public async Task<IActionResult> CheckCode([FromBody] CheckCodeRequestModel model)
        {            
            return Ok(new { CodeValid = true });
        }
    }
}
