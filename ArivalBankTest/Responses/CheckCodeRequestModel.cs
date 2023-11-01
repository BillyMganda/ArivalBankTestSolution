using System.ComponentModel.DataAnnotations;

namespace ArivalBankTest.Responses
{
    public class CheckCodeRequestModel
    {
        [Required]
        [RegularExpression(@"^\+\d{10,15}$")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Code { get; set; } = string.Empty;
    }
}
