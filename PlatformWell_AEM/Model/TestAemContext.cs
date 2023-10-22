using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PlatformWell_AEM.Model;

public partial class TestAemContext : DbContext
{
    public TestAemContext()
    {
    }

    public TestAemContext(DbContextOptions<TestAemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PlatformTable> PlatformTables { get; set; }

    public virtual DbSet<PlatformWell> PlatformWells { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\Local;Initial Catalog=TestAEM;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlatformTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC0717A09D65");

            entity.ToTable("PlatformTable");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UniqueName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<PlatformWell>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platform__3214EC073A2F53AF");

            entity.ToTable("PlatformWell");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UniqueName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Platform).WithMany(p => p.Well)
                .HasForeignKey(d => d.PlatformId)
                .HasConstraintName("FK_PlatformWell_PlatformTable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
