using Microsoft.EntityFrameworkCore;
using BackendAPI.Models;

namespace BackendAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<OtpModel> Otps { get; set; }         // Existing table
    public DbSet<Complaint> Complaints { get; set; }  // New Complaints table
}
