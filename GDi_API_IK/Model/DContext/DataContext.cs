using GDi_API_IK.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GDi_API_IK.Model.DContext {
    public class DataContext : DbContext {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<AssignementLog> AssignementLog { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>().HasIndex(c => c.Registration).IsUnique();
            modelBuilder.Entity<Car>().HasIndex(c => c.DriverId).IsUnique();

            modelBuilder.Entity<Car>().HasOne(c => c.Driver).WithMany().HasForeignKey(c => c.DriverId).OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Driver>().HasOne(d => d.Car).WithOne(c => c.Driver)
            //    .HasForeignKey<Driver>(d => d.CarId).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
