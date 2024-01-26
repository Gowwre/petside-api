using Microsoft.EntityFrameworkCore;
using PetHealthCare.Model;
using PetHealthCare.Model.Enums;
using PetHealthCare.Utils;

namespace PetHealthCare.AppDatabaseContext;

public class PetDbContext : DbContext
{
    public PetDbContext(DbContextOptions options) : base(options)
    {
        // commit lại lúc tạo database hoặc file Migrations
        //Initialize();
    }

    public DbSet<Users> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Pets> Pets { get; set; }
    public DbSet<Offerings> Offerings { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Providers> Providers { get; set; }

    public DbSet<UsersRole> UsersRoles { get; set; }
    public DbSet<OfferAppointment> OfferAppointments { get; set; }

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
            // entity.HasIndex(u => u.Email).IsUnique(); tôi có thể bắt lỗi gì khi save vào database
        });
        modelBuilder.Entity<Pets>(entity =>
        {
            entity.Property(s => s.Gender).HasConversion<string>();
            // entity.Navigation(p => p.Users).AutoInclude();
        });
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(s => s.AppointmentStatus).HasConversion<string>();
        });

        modelBuilder.Entity<OfferAppointment>(entity =>
        {
            entity.HasKey(oa => new { oa.AppointmentId, oa.OfferingsId });
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
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Users>()
            .HasMany(u => u.Appointments)
            .WithOne(u => u.Users)
            .HasForeignKey(u => u.UsersId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public async void Initialize()
    {
        if (!Roles.Any())
        {
            Roles.Add(new Role
            {
                RoleName = RoleName.ADMIN,
                CreateDateTime = DateTime.Now
            });

            Roles.Add(new Role
            {
                RoleName = RoleName.OWNER,
                CreateDateTime = DateTime.Now
            });

            Roles.Add(new Role
            {
                RoleName = RoleName.CARETAKER,
                CreateDateTime = DateTime.Now
            });

            Roles.Add(new Role
            {
                RoleName = RoleName.MEMBER,
                CreateDateTime = DateTime.Now
            });
            Roles.Add(new Role
            {
                RoleName = RoleName.MODERATOR,
                CreateDateTime = DateTime.Now
            });
            SaveChanges();
        }

        if (!Users.Any())
        {
            PasswordHashUtils.CreatePasswordHash("SuperAdmin", out var passwordHash, out var passwordSalt);
            var adminRole = Roles.FirstOrDefault(r => r.RoleName == RoleName.ADMIN);
            var user = Users.Add(new Users
            {
                Avatar = "https://indotel.com.vn/wp-content/uploads/2022/07/hinh-anh-du-lich-ha-long8.jpg",
                Email = "Admin@gmail.com",
                DateOfBirth = "09/09/2001",
                FullName = "Admin",
                Address = "Admin",
                Status = UserStatus.INACTIVE,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PhoneNumber = "sd",
                UsersRoles = new List<UsersRole> { new() { Role = adminRole } }
            });
            SaveChanges();
        }
    }
}