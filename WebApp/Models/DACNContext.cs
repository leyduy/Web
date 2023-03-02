using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApp.Models
{
    public partial class DACNContext : DbContext
    {
        public DACNContext()
        {
        }

        public DACNContext(DbContextOptions<DACNContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Dichvu> Dichvus { get; set; }
        public virtual DbSet<Duan> Duans { get; set; }
        public virtual DbSet<Khachhang> Khachhangs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-VC5IF5QK;Initial Catalog=DACN;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Salt)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Account_Roles");
            });

            modelBuilder.Entity<Dichvu>(entity =>
            {
                entity.HasKey(e => e.Madichvu);

                entity.ToTable("DICHVU");

                entity.Property(e => e.Madichvu)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MADICHVU");

                entity.Property(e => e.Loaidichvu)
                    .HasMaxLength(30)
                    .HasColumnName("LOAIDICHVU");
            });

            modelBuilder.Entity<Duan>(entity =>
            {
                entity.HasKey(e => e.Maduan);

                entity.ToTable("DUAN");

                entity.Property(e => e.Maduan).HasColumnName("MADUAN");

                entity.Property(e => e.Loaiduan)
                    .HasMaxLength(50)
                    .HasColumnName("LOAIDUAN");
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.Makh)
                    .HasName("PK__KHACHHAN__603F592C87C3787B");

                entity.ToTable("KHACHHANG");

                entity.Property(e => e.Makh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MAKH")
                    .HasDefaultValueSql("([DBO].[AUTO_IDKH]())");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(50)
                    .HasColumnName("DIACHI");

                entity.Property(e => e.Dt)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("DT");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(100)
                    .HasColumnName("GHICHU");

                entity.Property(e => e.Madichvu)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("MADICHVU");

                entity.Property(e => e.Maduan).HasColumnName("MADUAN");

                entity.Property(e => e.Tenkh)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("TENKH");

                entity.HasOne(d => d.MadichvuNavigation)
                    .WithMany(p => p.Khachhangs)
                    .HasForeignKey(d => d.Madichvu)
                    .HasConstraintName("FK_KHACHHANG_DICHVU1");

                entity.HasOne(d => d.MaduanNavigation)
                    .WithMany(p => p.Khachhangs)
                    .HasForeignKey(d => d.Maduan)
                    .HasConstraintName("FK_KHACHHANG_DUAN");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.RoleDescription).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
