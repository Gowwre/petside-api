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
        // Initialize();
    }

    public DbSet<Users> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Pets> Pets { get; set; }

    public DbSet<UsersRole> UsersRoles { get; set; }

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
        });
        modelBuilder.Entity<Pets>(entity =>
        {
            entity.Property(s => s.Gender).HasConversion<string>();
            // entity.Navigation(p => p.Users).AutoInclude();
        });

        modelBuilder.Entity<UsersRole>(entity => { entity.HasKey(ur => new { ur.RoleId, ur.UsersId }); });
    }

    public void Initialize()
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
            Users.Add(new Users
            {
                Avatar = "https://indotel.com.vn/wp-content/uploads/2022/07/hinh-anh-du-lich-ha-long8.jpg",
                Email = "Admin@gmail.com",
                DateOfBirth = "09/09/2001",
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