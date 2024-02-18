using Microsoft.EntityFrameworkCore;

namespace Company.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Company;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dependant>().HasKey(depnt => new { depnt.EmpSSN, depnt.DependantName });


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Dependant> Dependants { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Works_for> works_For { get; set; }


    }
}
