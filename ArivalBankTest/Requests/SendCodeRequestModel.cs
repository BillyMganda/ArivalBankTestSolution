using System.ComponentModel.DataAnnotations;

namespace ArivalBankTest.Requests
{
    public class SendCodeRequestModel
    {
        [Required]
        [RegularExpression(@"^\+\d{10,15}$")] // Validate phone number format
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
