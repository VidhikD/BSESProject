using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BackendAPI.DTOs
{
    public class ComplaintDTO
    {
        [Required]
        public string CANumber { get; set; }

        [Required]
        public string Division { get; set; }

        [Required]
        public string ComplainantName { get; set; }

        [Required]
        [Phone]
        public string ComplainantMobileNo { get; set; }

        [Required]
        public string RelationWithCustomer { get; set; }

        [Required]
        public string ComplaintType { get; set; }

        [Required]
        public string ComplaintAgainst { get; set; }

        [Required]
        public string ComplaintBrief { get; set; }

        public bool BsesConnected { get; set; } = false;

        public string? Remarks { get; set; }  // ❌ Not required

        public string? FullName { get; set; } // ✅ Added

        public string? DepartmentName { get; set; } // ✅ Added

        public string? RequestNo { get; set; } // ✅ Added

        [MaxFileSize(25 * 1024 * 1024)] // ✅ 25MB limit
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".pdf", ".mp4", ".avi", ".mp3" })]
        public IFormFile? UploadDocument { get; set; }
    }
}
