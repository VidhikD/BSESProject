using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Models
{
    public class Complaint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComplaintId { get; set; }

        public string? FullName { get; set; }  // ✅ Optional

        [Required]
        public string? CANumber { get; set; }

        [Required]
        public string? Division { get; set; }

        [Required]
        public string? ComplainantName { get; set; }

        [Required]
        [Phone]
        public string? ComplainantMobileNo { get; set; }

        [Required]
        public string? RelationWithCustomer { get; set; }

        [Required]
        public string? ComplaintType { get; set; }

        [Required]
        public string? ComplaintAgainst { get; set; }

        [Required]
        public string? ComplaintBrief { get; set; }

        public string? DepartmentName { get; set; }  // ✅ Optional

        public string? RequestNo { get; set; }  // ✅ Optional

        public bool BsesConnected { get; set; } = false;

        public string? Remarks { get; set; }  // ✅ Optional

        public string? UploadDocument { get; set; }  // ✅ File path (optional)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
