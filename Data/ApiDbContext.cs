using Microsoft.EntityFrameworkCore;
using OfficeTrackApi.Entities;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<MaintenanceTask> MaintenanceTasks { get; set; }
    public DbSet<EquipmentMaintenance> EquipmentMaintenances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // EquipmentType
        modelBuilder.Entity<EquipmentType>()
            .Property(et => et.Description)
            .HasMaxLength(100)
            .IsRequired();

        // Equipment
        modelBuilder.Entity<Equipment>()
            .Property(e => e.Brand)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Equipment>()
            .Property(e => e.Model)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Equipment>()
            .Property(e => e.SerialNumber)
            .HasMaxLength(100);

        modelBuilder.Entity<Equipment>()
            .HasOne(e => e.EquipmentType)
            .WithMany(et => et.Equipment)
            .HasForeignKey(e => e.EquipmentTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        // MaintenanceTask
        modelBuilder.Entity<MaintenanceTask>()
            .Property(mt => mt.Description)
            .HasMaxLength(200)
            .IsRequired();

        // EquipmentMaintenance (Join Table)
        modelBuilder.Entity<EquipmentMaintenance>()
            .HasKey(em => new { em.EquipmentId, em.MaintenanceTaskId });

        modelBuilder.Entity<EquipmentMaintenance>()
            .HasOne(em => em.Equipment)
            .WithMany(e => e.EquipmentMaintenances)
            .HasForeignKey(em => em.EquipmentId);

        modelBuilder.Entity<EquipmentMaintenance>()
            .HasOne(em => em.MaintenanceTask)
            .WithMany(mt => mt.EquipmentMaintenances)
            .HasForeignKey(em => em.MaintenanceTaskId);
    }
}
