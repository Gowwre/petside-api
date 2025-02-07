﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetHealthCare.AppDatabaseContext;

#nullable disable

namespace PetHealthCare.Migrations
{
    [DbContext(typeof(PetDbContext))]
    [Migration("20240307154554_UpdateEntityField")]
    partial class UpdateEntityField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PetHealthCare.Model.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("AppointmentFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("AppointmentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProvidersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VisitType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProvidersId");

                    b.HasIndex("UsersId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("PetHealthCare.Model.MemberUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiredDay")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("MembershipId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<Guid?>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MembershipId");

                    b.HasIndex("UsersId");

                    b.ToTable("MemberUsers");
                });

            modelBuilder.Entity("PetHealthCare.Model.Membership", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.HasKey("Id");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("PetHealthCare.Model.Notifications", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<DateTime>("DateRemind")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<int>("Dusage")
                        .HasColumnType("int");

                    b.Property<string>("NameMedicine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PetsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TimeRemind")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.Property<Guid?>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PetsId");

                    b.HasIndex("UsersId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("PetHealthCare.Model.OfferAppointment", b =>
                {
                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OfferingsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AppointmentId", "OfferingsId");

                    b.HasIndex("OfferingsId");

                    b.ToTable("OfferAppointments");
                });

            modelBuilder.Entity("PetHealthCare.Model.OfferProviders", b =>
                {
                    b.Property<Guid>("OfferingsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProvidersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OfferingsId", "ProvidersId");

                    b.HasIndex("ProvidersId");

                    b.ToTable("OfferProviders");
                });

            modelBuilder.Entity("PetHealthCare.Model.Offerings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.HasKey("Id");

                    b.ToTable("Offerings");
                });

            modelBuilder.Entity("PetHealthCare.Model.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<Guid?>("AppointmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.Property<Guid?>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId")
                        .IsUnique()
                        .HasFilter("[AppointmentId] IS NOT NULL");

                    b.HasIndex("UsersId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("PetHealthCare.Model.Pets", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid?>("AppointmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Height")
                        .HasColumnType("float");

                    b.Property<string>("IdentifyingFeatures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Species")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UsersId");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.HasIndex("UsersId");

                    b.ToTable("Pets", "dbo");
                });

            modelBuilder.Entity("PetHealthCare.Model.Providers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Availability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProviderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("ServiceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("PetHealthCare.Model.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.HasKey("Id");

                    b.ToTable("Role", "dbo");
                });

            modelBuilder.Entity("PetHealthCare.Model.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsUpgrade")
                        .HasColumnType("bit");

                    b.Property<long?>("OtpEmail")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordHash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordSalt");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.Property<DateTime>("UpgradeDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", "dbo");
                });

            modelBuilder.Entity("PetHealthCare.Model.UsersRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UsersRoles");
                });

            modelBuilder.Entity("PetHealthCare.Model.Appointment", b =>
                {
                    b.HasOne("PetHealthCare.Model.Providers", "Providers")
                        .WithMany("Appointments")
                        .HasForeignKey("ProvidersId");

                    b.HasOne("PetHealthCare.Model.Users", "Users")
                        .WithMany("Appointments")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Providers");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PetHealthCare.Model.MemberUser", b =>
                {
                    b.HasOne("PetHealthCare.Model.Membership", "Membership")
                        .WithMany("MemberUsers")
                        .HasForeignKey("MembershipId");

                    b.HasOne("PetHealthCare.Model.Users", "Users")
                        .WithMany("MemberUsers")
                        .HasForeignKey("UsersId");

                    b.Navigation("Membership");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PetHealthCare.Model.Notifications", b =>
                {
                    b.HasOne("PetHealthCare.Model.Pets", "Pets")
                        .WithMany("Notifications")
                        .HasForeignKey("PetsId");

                    b.HasOne("PetHealthCare.Model.Users", "Users")
                        .WithMany("Notifications")
                        .HasForeignKey("UsersId");

                    b.Navigation("Pets");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PetHealthCare.Model.OfferAppointment", b =>
                {
                    b.HasOne("PetHealthCare.Model.Appointment", "Appointment")
                        .WithMany("OfferAppointments")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHealthCare.Model.Offerings", "Offerings")
                        .WithMany("OfferAppointments")
                        .HasForeignKey("OfferingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Offerings");
                });

            modelBuilder.Entity("PetHealthCare.Model.OfferProviders", b =>
                {
                    b.HasOne("PetHealthCare.Model.Offerings", "Offerings")
                        .WithMany("OfferProviders")
                        .HasForeignKey("OfferingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHealthCare.Model.Providers", "Providers")
                        .WithMany("OfferProviders")
                        .HasForeignKey("ProvidersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offerings");

                    b.Navigation("Providers");
                });

            modelBuilder.Entity("PetHealthCare.Model.Payment", b =>
                {
                    b.HasOne("PetHealthCare.Model.Appointment", "Appointment")
                        .WithOne("Payment")
                        .HasForeignKey("PetHealthCare.Model.Payment", "AppointmentId");

                    b.HasOne("PetHealthCare.Model.Users", "Users")
                        .WithMany("Payments")
                        .HasForeignKey("UsersId");

                    b.Navigation("Appointment");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PetHealthCare.Model.Pets", b =>
                {
                    b.HasOne("PetHealthCare.Model.Appointment", "Appointment")
                        .WithMany("Pets")
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("PetHealthCare.Model.Users", "Users")
                        .WithMany("Pets")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PetHealthCare.Model.UsersRole", b =>
                {
                    b.HasOne("PetHealthCare.Model.Role", "Role")
                        .WithMany("UsersRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetHealthCare.Model.Users", "Users")
                        .WithMany("UsersRoles")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PetHealthCare.Model.Appointment", b =>
                {
                    b.Navigation("OfferAppointments");

                    b.Navigation("Payment");

                    b.Navigation("Pets");
                });

            modelBuilder.Entity("PetHealthCare.Model.Membership", b =>
                {
                    b.Navigation("MemberUsers");
                });

            modelBuilder.Entity("PetHealthCare.Model.Offerings", b =>
                {
                    b.Navigation("OfferAppointments");

                    b.Navigation("OfferProviders");
                });

            modelBuilder.Entity("PetHealthCare.Model.Pets", b =>
                {
                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("PetHealthCare.Model.Providers", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("OfferProviders");
                });

            modelBuilder.Entity("PetHealthCare.Model.Role", b =>
                {
                    b.Navigation("UsersRoles");
                });

            modelBuilder.Entity("PetHealthCare.Model.Users", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("MemberUsers");

                    b.Navigation("Notifications");

                    b.Navigation("Payments");

                    b.Navigation("Pets");

                    b.Navigation("UsersRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
