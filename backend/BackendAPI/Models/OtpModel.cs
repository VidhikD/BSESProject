using System;
using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models
{
    public class OtpModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid mobile number.")]
        public string MobileNumber { get; set; } = string.Empty; // ✅ Ensures it's not null

        public string? OtpCode { get; set; }  // ✅ Make it nullable

        public DateTime ExpiryTime { get; set; }
    }
}


