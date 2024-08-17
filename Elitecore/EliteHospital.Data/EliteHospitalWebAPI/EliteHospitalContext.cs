using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EliteHospitalWebAPI
{
    public partial class EliteHospitalContext : DbContext
    {
        public EliteHospitalContext()
        {
        }

        public EliteHospitalContext(DbContextOptions<EliteHospitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PatientVerification> PatientVerifications { get; set; }

        //public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<CovidBanner> CovidBanners { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<SpecialOffer> SpecialOffers { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
        public virtual DbSet<Insurance> Insurance { get; set; }
        public virtual DbSet<DailyOffer> DailyOffer { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EliteHospital;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<PatientVerification>(entity =>
            {
                entity.ToTable("PatientVerification");

                entity.HasIndex(e => e.MobileNo, "IX_PatientVerification")
                    .IsUnique();

                entity.Property(e => e.CreatedOnUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedOnUTC");

                entity.Property(e => e.ExpiringOnUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("ExpiringOnUTC");

                entity.Property(e => e.LastVerifiedOnUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("LastVerifiedOnUTC");

                entity.Property(e => e.MobileNo)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("Banner")
                .HasKey(p=> p.Id);

                entity.Property(e => e.BannerTitle)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BannerTitleArabic)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(e => e.BannerSubTitle)
                    .HasMaxLength(50);

                entity.Property(e => e.BannerSubTitleArabic)
                    .IsUnicode(true)
                    .HasMaxLength(50);               
            });

            modelBuilder.Entity<CovidBanner>(entity =>
            {
                entity.ToTable("CovidBanner")
                .HasKey(p => p.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department")
                .HasKey(p => p.DepartmentName);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DepartmentNameArabic)
                    .IsUnicode(true)
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .HasMaxLength(250);

                entity.Property(e => e.DescriptionArabic)
                    .IsUnicode(true)
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor")
                .HasKey(p => p.Id);

                entity.Property(e => e.DoctorId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DoctorName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SpecialOffer>(entity =>
            {
                entity.ToTable("SpecialOffer")
                .HasKey(p => p.Id);
            });

            modelBuilder.Entity<ContactU>(entity =>
            {
                entity.ToTable("ContactUs")
                .HasKey(p => p.Id);
            });

            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.ToTable("Insurance")
                .HasKey(p => p.Id);
            });

            modelBuilder.Entity<DailyOffer>(entity =>
            {
                entity.ToTable("DailyOffers")
                .HasKey(p => p.Id);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
