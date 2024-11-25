using Microsoft.EntityFrameworkCore;

namespace JKLHealthcareSystem.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Caregiver> Caregivers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}