using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LisansAPI.Models
{
    public partial class ercesa_terminalContext : DbContext
    {
        public ercesa_terminalContext()
        {
        }

        public ercesa_terminalContext(DbContextOptions<ercesa_terminalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApiAddress> ApiAddresses { get; set; } = null!;
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerBranch> CustomerBranches { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<DeviceLicence> DeviceLicences { get; set; } = null!;
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; } = null!;
        public virtual DbSet<SystemUser> SystemUsers { get; set; } = null!;
        public virtual DbSet<SystemUserRole> SystemUserRoles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserLogin> UserLogins { get; set; } = null!;
        public virtual DbSet<VersionInfo> VersionInfos { get; set; } = null!;
        public virtual DbSet<VersionProgram> VersionPrograms { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS01;Database=ercesa_terminal;Trusted_connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ApiAddress>(entity =>
            {
                entity.ToTable("ApiAddress");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CustomerBranch)
                    .WithMany(p => p.ApiAddresses)
                    .HasForeignKey(d => d.CustomerBranchId)
                    .HasConstraintName("FK_dbo.ApiAddress_dbo.CustomerBranch_CustomerBranchId");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("ApplicationUser");

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefreshTokenEndDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.Customer_dbo.User_User_Id");
            });

            modelBuilder.Entity<CustomerBranch>(entity =>
            {
                entity.ToTable("CustomerBranch");

                entity.Property(e => e.BranchCloseTime).HasDefaultValueSql("(N'04:00:00')");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerBranches)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.CustomerBranch_dbo.Customer_CustomerId");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("Device");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Uuid).HasColumnName("UUID");

                entity.HasOne(d => d.CustomerBranch)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.CustomerBranchId)
                    .HasConstraintName("FK_dbo.Device_dbo.CustomerBranch_CustomerBranchId");
            });

            modelBuilder.Entity<DeviceLicence>(entity =>
            {
                entity.ToTable("DeviceLicence");

                entity.Property(e => e.ConfirmedDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(N'2022-01-01')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.DeviceLicences)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("FK_dbo.DeviceLicence_dbo.Device_DeviceId");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.ToTable("SystemUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.RefreshToken).IsUnicode(false);

                entity.Property(e => e.RefreshTokenEndDate).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.SystemUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_SystemUser_SystemUserRole");
            });

            modelBuilder.Entity<SystemUserRole>(entity =>
            {
                entity.ToTable("SystemUserRole");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(N'2022-01-01')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("UserLogin");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<VersionInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("VersionInfo");

                entity.Property(e => e.AppliedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1024);
            });

            modelBuilder.Entity<VersionProgram>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("VersionProgram");

                entity.Property(e => e.Version).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
