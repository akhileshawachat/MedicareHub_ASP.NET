using Microsoft.EntityFrameworkCore;
using WebAppDemoStudent.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebAppDemoStudent.DTOs;

namespace WebAppDemoStudent.Data
{
    public class MyDbContext : IdentityDbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<ApplicationUser> users { get; set; }
        public DbSet<WebAppDemoStudent.DTOs.RoleStore>? RoleStore { get; set; }

        public DbSet<Booking> Bookings { get; set; }
    }
}
