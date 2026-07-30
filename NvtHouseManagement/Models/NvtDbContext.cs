using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NvtHouseManagement.Models;

public partial class NvtDbContext : DbContext
{
    public NvtDbContext()
    {
    }

    public NvtDbContext(DbContextOptions<NvtDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NvtBaoCaoSuCo> NvtBaoCaoSuCos { get; set; }

    public virtual DbSet<NvtChiTietDichVu> NvtChiTietDichVus { get; set; }

    public virtual DbSet<NvtChotDienNuoc> NvtChotDienNuocs { get; set; }

    public virtual DbSet<NvtDayTro> NvtDayTros { get; set; }

    public virtual DbSet<NvtDichVu> NvtDichVus { get; set; }

    public virtual DbSet<NvtHoaDon> NvtHoaDons { get; set; }

    public virtual DbSet<NvtHopDong> NvtHopDongs { get; set; }

    public virtual DbSet<NvtKhachThue> NvtKhachThues { get; set; }

    public virtual DbSet<NvtLoaiPhong> NvtLoaiPhongs { get; set; }

    public virtual DbSet<NvtPhong> NvtPhongs { get; set; }

    public virtual DbSet<NvtThanhVienPhong> NvtThanhVienPhongs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=nvt_mvc;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NvtBaoCaoSuCo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NvtBaoCa__3214EC0767DC370B");

            entity.ToTable("NvtBaoCaoSuCo");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TieuDe).HasMaxLength(200);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Mới tiếp nhận");

            entity.HasOne(d => d.KhachThue).WithMany(p => p.NvtBaoCaoSuCos)
                .HasForeignKey(d => d.KhachThueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NvtBaoCaoSuCo_NvtKhachThue");

            entity.HasOne(d => d.Phong).WithMany(p => p.NvtBaoCaoSuCos)
                .HasForeignKey(d => d.PhongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NvtBaoCaoSuCo_NvtPhong");
        });

        modelBuilder.Entity<NvtChiTietDichVu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NvtChiTi__3214EC07358CAEE4");

            entity.ToTable("NvtChiTietDichVu");

            entity.Property(e => e.SoLuong).HasDefaultValue(1);

            entity.HasOne(d => d.DichVu).WithMany(p => p.NvtChiTietDichVus)
                .HasForeignKey(d => d.DichVuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NvtChiTietDichVu_NvtDichVu");

            entity.HasOne(d => d.HopDong).WithMany(p => p.NvtChiTietDichVus)
                .HasForeignKey(d => d.HopDongId)
                .HasConstraintName("FK_NvtChiTietDichVu_NvtHopDong");
        });

        modelBuilder.Entity<NvtChotDienNuoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NvtChotD__3214EC076713A30E");

            entity.ToTable("NvtChotDienNuoc");

            entity.Property(e => e.NgayChot).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Phong).WithMany(p => p.NvtChotDienNuocs)
                .HasForeignKey(d => d.PhongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NvtChotDienNuoc_NvtPhong");
        });

        modelBuilder.Entity<NvtDayTro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NvtDayTr__3214EC07AC734CDB");

            entity.ToTable("NvtDayTro");

            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.TenDayTro).HasMaxLength(100);
        });

        modelBuilder.Entity<NvtDichVu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NvtDichV__3214EC07108BECB1");

            entity.ToTable("NvtDichVu");

            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DonViTinh).HasMaxLength(30);
            entity.Property(e => e.TenDichVu).HasMaxLength(100);
        });

        modelBuilder.Entity<NvtHoaDon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NvtHoaDo__3214EC077196FFA2");

            entity.ToTable("NvtHoaDon");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TienDichVuKhac)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TienDien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TienNuoc).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TienPhong).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Chưa thanh toán");

            entity.HasOne(d => d.HopDong).WithMany(p => p.NvtHoaDons)
                .HasForeignKey(d => d.HopDongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NvtHoaDon_NvtHopDong");
        });

        modelBuilder.Entity<NvtHopDong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NvtHopDo__3214EC07F4456609");

            entity.ToTable("NvtHopDong");

            entity.Property(e => e.GiaThueThucTe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TienCoc).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Hiệu lực");

            entity.HasOne(d => d.KhachThue).WithMany(p => p.NvtHopDongs)
                .HasForeignKey(d => d.KhachThueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NvtHopDong_NvtKhachThue");

            entity.HasOne(d => d.Phong).WithMany(p => p.NvtHopDongs)
                .HasForeignKey(d => d.PhongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NvtHopDong_NvtPhong");
        });

        modelBuilder.Entity<NvtKhachThue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NvtKhach__3214EC078707C710");

            entity.ToTable("NvtKhachThue");

            entity.Property(e => e.Cccd).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.QueQuan).HasMaxLength(255);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<NvtLoaiPhong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NvtLoaiP__3214EC078FF01BD3");

            entity.ToTable("NvtLoaiPhong");

            entity.Property(e => e.GiaCoBan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoNguoiToiDa).HasDefaultValue(2);
            entity.Property(e => e.TenLoaiPhong).HasMaxLength(100);
        });

        modelBuilder.Entity<NvtPhong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NvtPhong__3214EC07BAE42B57");

            entity.ToTable("NvtPhong");

            entity.Property(e => e.SoPhong).HasMaxLength(20);
            entity.Property(e => e.Tang).HasDefaultValue(1);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(30)
                .HasDefaultValue("Trống");

            entity.HasOne(d => d.DayTro).WithMany(p => p.NvtPhongs)
                .HasForeignKey(d => d.DayTroId)
                .HasConstraintName("FK_NvtPhong_NvtDayTro");

            entity.HasOne(d => d.LoaiPhong).WithMany(p => p.NvtPhongs)
                .HasForeignKey(d => d.LoaiPhongId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NvtPhong_NvtLoaiPhong");
        });

        modelBuilder.Entity<NvtThanhVienPhong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NvtThanh__3214EC0749F92FCC");

            entity.ToTable("NvtThanhVienPhong");

            entity.Property(e => e.Cccd).HasMaxLength(20);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.QuanHeVoiChuHo).HasMaxLength(50);
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);

            entity.HasOne(d => d.HopDong).WithMany(p => p.NvtThanhVienPhongs)
                .HasForeignKey(d => d.HopDongId)
                .HasConstraintName("FK_NvtThanhVienPhong_NvtHopDong");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
