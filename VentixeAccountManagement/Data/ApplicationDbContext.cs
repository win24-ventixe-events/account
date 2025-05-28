using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VentixeAccountManagement.Data.Entities;


namespace VentixeAccountManagement.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
}