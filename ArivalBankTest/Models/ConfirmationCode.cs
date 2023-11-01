using System.ComponentModel.DataAnnotations;

namespace ArivalBankTest.Models
{
    public class ConfirmationCode
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Code { get; set; } = string.Empty;
        public DateTime ExpirationTime { get; set; }
    }
}
