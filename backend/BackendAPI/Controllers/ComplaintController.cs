using BackendAPI.Data;
using BackendAPI.DTOs;  // ✅ Added DTOs reference
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComplaintController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET all complaints
        [HttpGet("all")]
        [SwaggerOperation(Summary = "Get all complaints", Description = "Fetches all complaints from the database.")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaints()
        {
            var complaints = await _context.Complaints.ToListAsync();
            return Ok(complaints);
        }

        // ✅ GET a specific complaint by ID
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a complaint by ID", Description = "Fetches a specific complaint by its ID.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Complaint>> GetComplaint(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);

            if (complaint == null)
            {
                return NotFound(new { message = "Complaint not found" });
            }

            return Ok(complaint);
        }

        // ✅ CA Verification
        [HttpGet("verify-ca/{caNumber}")]
        [SwaggerOperation(Summary = "Verify CA number", Description = "Checks if a CA number exists in the database.")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> VerifyCA(string caNumber)
        {
            var complaintExists = await _context.Complaints
                .AnyAsync(c => c.CANumber == caNumber);

            if (complaintExists)
            {
                return Ok(new { exists = true, message = "CA Number exists." });
            }

            return NotFound(new { exists = false, message = "CA Number not registered. Please go to the new user section." });
        }

        // ✅ Submit a complaint
        [HttpPost("submit")]
        [SwaggerOperation(Summary = "Submit a new complaint", Description = "Saves a new complaint in the database.")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SubmitComplaint([FromForm] ComplaintDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filePath = await SaveFile(dto.UploadDocument); // ✅ Save file if provided

            var complaint = new Complaint
            {
                FullName = dto.FullName,
                CANumber = dto.CANumber,
                Division = dto.Division,
                ComplainantName = dto.ComplainantName,
                ComplainantMobileNo = dto.ComplainantMobileNo,
                RelationWithCustomer = dto.RelationWithCustomer,
                ComplaintType = dto.ComplaintType,
                ComplaintAgainst = dto.ComplaintAgainst,
                ComplaintBrief = dto.ComplaintBrief,
                DepartmentName = dto.DepartmentName,
                RequestNo = dto.RequestNo,  // ✅ Now matches Complaint.cs, DTO, and database
                BsesConnected = dto.BsesConnected,
                Remarks = dto.BsesConnected ? dto.Remarks : "No remarks given", // ✅ Auto-set for 'No'
                UploadDocument = filePath // ✅ Store file path
            };

            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComplaint), new { id = complaint.ComplaintId }, complaint);
        }

        // ✅ Helper method to save uploaded file
        private async Task<string?> SaveFile(IFormFile? file)
        {
            if (file == null) return null; // No file uploaded

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder); // Ensure directory exists

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{fileName}"; // Return relative file path
        }
    }
}