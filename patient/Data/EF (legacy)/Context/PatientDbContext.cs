//using Microsoft.EntityFrameworkCore;
//using PatientMs.Domain;
//
//namespace PatientMs.Data.Context
//{
//    public class PatientDbContext : DbContext
//    {
//        public DbSet<Patient> patients { get; set; }
//
//        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
//        {
//
//        }
//
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            base.OnConfiguring(optionsBuilder);
//        }
//
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Patient>()
//                .HasMany(patient => patient.InsurancePolicies)
//                .WithOne(policy => policy.Patient)
//                .OnDelete(DeleteBehavior.Cascade);
//        }
//    }
//}
//