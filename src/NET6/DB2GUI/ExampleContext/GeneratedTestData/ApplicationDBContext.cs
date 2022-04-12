using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Generated
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; } = null!;
        public virtual DbSet<EFCore> EFCore { get; set; } = null!;
        public virtual DbSet<Employee> Employee { get; set; } = null!;
        public virtual DbSet<MyApp> MyApp { get; set; } = null!;
        public virtual DbSet<NuGetPackages> NuGetPackages { get; set; } = null!;
        public virtual DbSet<Projects> Projects { get; set; } = null!;
        public virtual DbSet<Relation_MyApp_EFCore> Relation_MyApp_EFCore { get; set; } = null!;
        public virtual DbSet<Relation_NuGetPackages_NuGetPackages> Relation_NuGetPackages_NuGetPackages { get; set; } = null!;
        public virtual DbSet<Relation_Projects_NuGetPackages> Relation_Projects_NuGetPackages { get; set; } = null!;
        public virtual DbSet<vwDepEmp> vwDepEmp { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Owner).IsFixedLength();
            });

            modelBuilder.Entity<EFCore>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK__EFCore__0F5401351094CC38");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(d => d.IDDepartmentNavigation)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.IDDepartment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Department");
            });

            modelBuilder.Entity<MyApp>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK__MyApp__0F540135868454C4");
            });

            modelBuilder.Entity<NuGetPackages>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__NuGetPac__737584F7240F1E4D");
            });

            modelBuilder.Entity<vwDepEmp>(entity =>
            {
                entity.ToView("vwDepEmp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
