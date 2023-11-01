using System.ComponentModel.DataAnnotations;

namespace ArivalBankTest.Requests
{
    public class SendCodeRequestModel
    {
        [Required]
        [RegularExpression(@"^\+\d{10,15}$")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
