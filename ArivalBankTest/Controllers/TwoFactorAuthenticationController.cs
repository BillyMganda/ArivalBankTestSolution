using ArivalBankTest.Requests;
using ArivalBankTest.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _codeRepository.HasMaxConcurrentCodesAsync(model.PhoneNumber))
            {
                return BadRequest("Maximum concurrent codes reached for this phone number.");
            }

            try
            {
                string randomCode = _codeRepository.GenerateRandomCode();

                Log.Information($"Generated code for {model.PhoneNumber}: {randomCode}");

                await _codeRepository.AddConfirmationCodeToDbAsync(new SendCodeRequestModel
                {
                    PhoneNumber = model.PhoneNumber
                });

                return Ok(new { CodeSent = true });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error sending code.");
                return StatusCode(500, "Error sending code.");                
            }
        }

        [HttpPost("check-code")]
        public async Task<IActionResult> CheckCode([FromBody] CheckCodeRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var code = await _codeRepository.GetCodeAsync(model);

                if (code == null)
                {
                    return Ok(new { CodeValid = false });
                }

                // Check if the code has expired
                if (code.ExpirationTime <= DateTime.UtcNow)
                {
                    return Ok(new { CodeValid = false });
                }

                return Ok(new { CodeValid = true });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error checking code.");
                return StatusCode(500, "Error checking code.");
            }
        }
    }
}
