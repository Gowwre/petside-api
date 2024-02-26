using Microsoft.EntityFrameworkCore;
using PetHealthCare.Model;

namespace PetHealthCare.AppDatabaseContext;

public class PetDbContext : DbContext
{
    public PetDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Users> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Pets> Pets { get; set; }
    public DbSet<Offerings> Offerings { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Providers> Providers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<MemberUser> MemberUsers { get; set; }

    public DbSet<UsersRole> UsersRoles { get; set; }
    public DbSet<OfferAppointment> OfferAppointments { get; set; }
    public DbSet<OfferProviders> OfferProviders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(s => s.RoleName).HasConversion<string>();
            //entity.Navigation(r => r.UsersRoles).AutoInclude();
        });
        modelBuilder.Entity<Users>(entity =>
        {
            entity.Property(s => s.Status).HasConversion<string>();
            entity.Navigation(u => u.UsersRoles).AutoInclude();
            entity.HasIndex(u => u.Email).IsUnique();
            // entity.Navigation(u => u.Pets).AutoInclude();
            // entity.HasIndex(u => u.Email).IsUnique(); 
        });
        modelBuilder.Entity<Pets>(entity =>
        {
            entity.Property(s => s.Gender).HasConversion<string>();
            // entity.Navigation(p => p.Users).AutoInclude();
        });
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(s => s.AppointmentStatus).HasConversion<string>();
            entity.Property(a => a.AppointmentFee).HasColumnType("decimal(18,2)");
        });
        modelBuilder.Entity<Payment>(entity => { entity.Property(s => s.Status).HasConversion<string>(); });
        modelBuilder.Entity<Membership>(entity => { entity.Property(s => s.Status).HasConversion<string>(); });
        modelBuilder.Entity<Offerings>(entity => { entity.Property(a => a.Price).HasColumnType("decimal(18,2)"); });

        modelBuilder.Entity<OfferAppointment>(entity =>
        {
            entity.HasKey(oa => new { oa.AppointmentId, oa.OfferingsId });
        });
        modelBuilder.Entity<OfferProviders>(entity =>
        {
            entity.HasKey(op => new { op.OfferingsId, op.ProvidersId });
        });

        modelBuilder.Entity<UsersRole>(entity => { entity.HasKey(ur => new { ur.RoleId, ur.UsersId }); });

        modelBuilder.Entity<Appointment>()
            .HasMany(a => a.Pets)
            .WithOne(a => a.Appointment)
            .HasForeignKey(a => a.AppointmentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Users>()
            .HasMany(u => u.Pets)
            .WithOne(u => u.Users)
            .HasForeignKey(u => u.UsersId)
            .OnDelete(DeleteBehavior.NoAction); // NoAction
        modelBuilder.Entity<Users>()
            .HasMany(u => u.Appointments)
            .WithOne(u => u.Users)
            .HasForeignKey(u => u.UsersId)
            .OnDelete(DeleteBehavior.NoAction); // chua test 
    }
}