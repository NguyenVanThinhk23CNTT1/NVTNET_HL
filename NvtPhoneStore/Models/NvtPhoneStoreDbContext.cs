using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NvtPhoneStore.Models;

public partial class NvtPhoneStoreDbContext : DbContext
{
    public NvtPhoneStoreDbContext()
    {
    }

    public NvtPhoneStoreDbContext(DbContextOptions<NvtPhoneStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NvtProduct> NvtProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOPOFBANH;Database=NvtPhoneStoreDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NvtProduct>(entity =>
        {
            entity.HasKey(e => e.NvtProductId).HasName("PK__NvtProdu__AD7D5C0EF3880B77");

            entity.ToTable("NvtProduct");

            entity.Property(e => e.NvtCategory).HasMaxLength(100);
            entity.Property(e => e.NvtDescription).HasMaxLength(500);
            entity.Property(e => e.NvtImage)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NvtIsActive).HasDefaultValue(true);
            entity.Property(e => e.NvtPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NvtProductName).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
