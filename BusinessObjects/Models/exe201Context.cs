using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Models
{
    public partial class exe201Context : IdentityDbContext<Account>
    {
        public exe201Context()
        {
        }

        public exe201Context(DbContextOptions<exe201Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountProfile> AccountProfiles { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Vehicle> Vehicles { get; set; } = null!;
        public virtual DbSet<VehicleType> VehicleTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
            var strConn = config["ConnectionStrings:DB"];

            return strConn;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
                // Các cấu hình khác nếu cần
            });
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password")
                    .IsFixedLength();

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Status");
            });

            modelBuilder.Entity<AccountProfile>(entity =>
            {
                entity.ToTable("Account_Profile");

                entity.HasIndex(e => e.AccountId, "UQ__Account___46A222CCAB80AB61")
                    .IsUnique();

                entity.Property(e => e.AccountProfileId).HasColumnName("account_profile_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("address")
                    .IsFixedLength();

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("country")
                    .IsFixedLength();

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.AccountProfile)
                    .HasForeignKey<AccountProfile>(d => d.AccountId)
                    .HasConstraintName("FK_AccountProfile_Account");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.ImagesId)
                    .HasName("PK__Images__FA2651F7718498B6");

                entity.Property(e => e.ImagesId).HasColumnName("images_id");

                entity.Property(e => e.ImagesLink)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("images_link")
                    .IsFixedLength();

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK_Images_Review");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Images_Vehicle");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.Property(e => e.ReservationId).HasColumnName("reservation_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BabySeat).HasColumnName("baby_seat");

                entity.Property(e => e.BreakdownAssistance).HasColumnName("breakdown_assistance");

                entity.Property(e => e.ComprehensiveInsurance).HasColumnName("comprehensive_insurance");

                entity.Property(e => e.PickupDate)
                    .HasColumnType("datetime")
                    .HasColumnName("pickup_date");

                entity.Property(e => e.PickupLocation)
                    .HasMaxLength(500)
                    .HasColumnName("pickup_location");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("datetime")
                    .HasColumnName("return_date");

                entity.Property(e => e.ReturnLocation)
                    .HasMaxLength(500)
                    .HasColumnName("return_location");

                entity.Property(e => e.RoadTax).HasColumnName("road_tax");

                entity.Property(e => e.SecurityDeposit).HasColumnName("security_deposit");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");

                entity.Property(e => e.UnlimitedMileage).HasColumnName("unlimited_mileage");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Account");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Point).HasColumnName("point");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Account");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Status");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Vehicle");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status_name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Deposit).HasColumnName("deposit");

                entity.Property(e => e.Doors).HasColumnName("doors");

                entity.Property(e => e.Engine)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("engine")
                    .IsFixedLength();

                entity.Property(e => e.Fueltype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fueltype")
                    .IsFixedLength();

                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("license_plate")
                    .IsFixedLength();

                entity.Property(e => e.Options)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("options")
                    .IsFixedLength();

                entity.Property(e => e.Passengers).HasColumnName("passengers");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReservationId).HasColumnName("reservation_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Suitcase)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("suitcase")
                    .IsFixedLength();

                entity.Property(e => e.VehicleName)
                    .HasMaxLength(100)
                    .HasColumnName("vehicle_name");

                entity.Property(e => e.VehicleTypeId).HasColumnName("vehicle_type_id");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.ReservationId)
                    .HasConstraintName("FK_Vehicle_Reservation");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicle_Status");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicle_VehicleType");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("Vehicle_Type");

                entity.Property(e => e.VehicleTypeId).HasColumnName("vehicle_type_id");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type_name")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
