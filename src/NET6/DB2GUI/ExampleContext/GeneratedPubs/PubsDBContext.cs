using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Generated
{
    public partial class PubsDBContext : DbContext
    {
        public PubsDBContext()
        {
        }

        public PubsDBContext(DbContextOptions<PubsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<authors> authors { get; set; } = null!;
        public virtual DbSet<discounts> discounts { get; set; } = null!;
        public virtual DbSet<employee> employee { get; set; } = null!;
        public virtual DbSet<jobs> jobs { get; set; } = null!;
        public virtual DbSet<pub_info> pub_info { get; set; } = null!;
        public virtual DbSet<publishers> publishers { get; set; } = null!;
        public virtual DbSet<roysched> roysched { get; set; } = null!;
        public virtual DbSet<sales> sales { get; set; } = null!;
        public virtual DbSet<stores> stores { get; set; } = null!;
        public virtual DbSet<titleauthor> titleauthor { get; set; } = null!;
        public virtual DbSet<titles> titles { get; set; } = null!;
        public virtual DbSet<titleview> titleview { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=pubs;UId=sa;pwd=<YourStrong@Passw0rd>");
                optionsBuilder.UseSqlite(@"DataSource=C:\Users\Surface1\Documents\GitHub\QueryViewer\src\NET6\DB2GUI\pubs.db");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<authors>(entity =>
            {
                entity.HasKey(e => e.au_id)
                    .HasName("UPKCL_auidind");

                entity.Property(e => e.phone)
                    .HasDefaultValueSql("('UNKNOWN')")
                    .IsFixedLength();

                entity.Property(e => e.state).IsFixedLength();

                entity.Property(e => e.zip).IsFixedLength();
            });

            modelBuilder.Entity<discounts>(entity =>
            {
                entity.Property(e => e.stor_id).IsFixedLength();

                entity.HasOne(d => d.stor)
                    .WithMany()
                    .HasForeignKey(d => d.stor_id)
                    .HasConstraintName("FK__discounts__stor___7482D2A6");
            });

            modelBuilder.Entity<employee>(entity =>
            {
                entity.HasKey(e => e.emp_id)
                    .HasName("PK_emp_id")
                    .IsClustered(false);

                entity.HasIndex(e => new { e.lname, e.fname, e.minit }, "employee_ind")
                    .IsClustered();

                entity.Property(e => e.emp_id).IsFixedLength();

                entity.Property(e => e.hire_date).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.job_id).HasDefaultValueSql("((1))");

                entity.Property(e => e.job_lvl).HasDefaultValueSql("((10))");

                entity.Property(e => e.minit).IsFixedLength();

                entity.Property(e => e.pub_id)
                    .HasDefaultValueSql("('9952')")
                    .IsFixedLength();

                entity.HasOne(d => d.job)
                    .WithMany(p => p.employee)
                    .HasForeignKey(d => d.job_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__employee__job_id__00E8A98B");

                entity.HasOne(d => d.pub)
                    .WithMany(p => p.employee)
                    .HasForeignKey(d => d.pub_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__employee__pub_id__03C51636");
            });

            modelBuilder.Entity<jobs>(entity =>
            {
                entity.HasKey(e => e.job_id)
                    .HasName("PK__jobs__6E32B6A5CFE3FA3A");

                entity.Property(e => e.job_desc).HasDefaultValueSql("('New Position - title not formalized yet')");
            });

            modelBuilder.Entity<pub_info>(entity =>
            {
                entity.HasKey(e => e.pub_id)
                    .HasName("UPKCL_pubinfo");

                entity.Property(e => e.pub_id).IsFixedLength();

                entity.HasOne(d => d.pub)
                    .WithOne(p => p.pub_info)
                    .HasForeignKey<pub_info>(d => d.pub_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__pub_info__pub_id__7C23F46E");
            });

            modelBuilder.Entity<publishers>(entity =>
            {
                entity.HasKey(e => e.pub_id)
                    .HasName("UPKCL_pubind");

                entity.Property(e => e.pub_id).IsFixedLength();

                entity.Property(e => e.country).HasDefaultValueSql("('USA')");

                entity.Property(e => e.state).IsFixedLength();
            });

            modelBuilder.Entity<roysched>(entity =>
            {
                entity.HasOne(d => d.title)
                    .WithMany()
                    .HasForeignKey(d => d.title_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__roysched__title___729A8A34");
            });

            modelBuilder.Entity<sales>(entity =>
            {
                entity.HasKey(e => new { e.stor_id, e.ord_num, e.title_id })
                    .HasName("UPKCL_sales");

                entity.Property(e => e.stor_id).IsFixedLength();

                entity.HasOne(d => d.stor)
                    .WithMany(p => p.sales)
                    .HasForeignKey(d => d.stor_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__sales__stor_id__6FBE1D89");

                entity.HasOne(d => d.title)
                    .WithMany(p => p.sales)
                    .HasForeignKey(d => d.title_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__sales__title_id__70B241C2");
            });

            modelBuilder.Entity<stores>(entity =>
            {
                entity.HasKey(e => e.stor_id)
                    .HasName("UPK_storeid");

                entity.Property(e => e.stor_id).IsFixedLength();

                entity.Property(e => e.state).IsFixedLength();

                entity.Property(e => e.zip).IsFixedLength();
            });

            modelBuilder.Entity<titleauthor>(entity =>
            {
                entity.HasKey(e => new { e.au_id, e.title_id })
                    .HasName("UPKCL_taind");

                entity.HasOne(d => d.au)
                    .WithMany(p => p.titleauthor)
                    .HasForeignKey(d => d.au_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__titleauth__au_id__6A054433");

                entity.HasOne(d => d.title)
                    .WithMany(p => p.titleauthor)
                    .HasForeignKey(d => d.title_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__titleauth__title__6AF9686C");
            });

            modelBuilder.Entity<titles>(entity =>
            {
                entity.HasKey(e => e.title_id)
                    .HasName("UPKCL_titleidind");

                entity.Property(e => e.pub_id).IsFixedLength();

                entity.Property(e => e.pubdate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.type)
                    .HasDefaultValueSql("('UNDECIDED')")
                    .IsFixedLength();

                entity.HasOne(d => d.pub)
                    .WithMany(p => p.titles)
                    .HasForeignKey(d => d.pub_id)
                    .HasConstraintName("FK__titles__pub_id__6634B34F");
            });

            modelBuilder.Entity<titleview>(entity =>
            {
                entity.ToView("titleview");

                entity.Property(e => e.pub_id).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
