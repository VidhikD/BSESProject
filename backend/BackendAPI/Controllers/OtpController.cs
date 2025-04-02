using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendAPI.Data;
using BackendAPI.Models;

namespace BackendAPI.Controllers
{
    [Route("api/otp")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OtpController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendOtp([FromBody] OtpModel request)
        {
            if (string.IsNullOrEmpty(request.MobileNumber))
                return BadRequest(new { message = "Mobile number is required." });

            // Delete old OTPs for this number (Optional for security)
            _context.Otps.RemoveRange(_context.Otps.Where(o => o.MobileNumber == request.MobileNumber));
            await _context.SaveChangesAsync();

            // Generate new OTP
            var otpCode = new Random().Next(1000000, 9999999).ToString();
            var expiryTime = DateTime.UtcNow.AddMinutes(5);

            var otpEntry = new OtpModel
            {
                MobileNumber = request.MobileNumber,
                OtpCode = otpCode,
                ExpiryTime = expiryTime
            };

            _context.Otps.Add(otpEntry);
            await _context.SaveChangesAsync();

            Console.WriteLine($"OTP Sent to {request.MobileNumber}: {otpCode}");

            return Ok(new { message = "OTP sent successfully." });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpModel request)
        {
            if (string.IsNullOrEmpty(request.MobileNumber) || string.IsNullOrEmpty(request.OtpCode))
                return BadRequest(new { message = "Mobile number and OTP are required." });

            var otpRecord = await _context.Otps
                .Where(o => o.MobileNumber == request.MobileNumber && o.OtpCode == request.OtpCode)
                .FirstOrDefaultAsync();

            if (otpRecord == null || otpRecord.ExpiryTime < DateTime.UtcNow)
                return BadRequest(new { message = "Invalid or expired OTP." });

            // Delete OTP after successful verification
            _context.Otps.Remove(otpRecord);
            await _context.SaveChangesAsync();

            return Ok(new { message = "OTP verified successfully." });
        }
    }
}
