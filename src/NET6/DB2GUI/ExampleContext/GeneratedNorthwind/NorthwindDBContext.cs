using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Generated
{
    public partial class NorthwindDBContext : DbContext
    {
        public NorthwindDBContext()
        {
        }

        public NorthwindDBContext(DbContextOptions<NorthwindDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alphabetical_list_of_products> Alphabetical_list_of_products { get; set; } = null!;
        public virtual DbSet<Categories> Categories { get; set; } = null!;
        public virtual DbSet<Category_Sales_for_1997> Category_Sales_for_1997 { get; set; } = null!;
        public virtual DbSet<Current_Product_List> Current_Product_List { get; set; } = null!;
        public virtual DbSet<CustomerDemographics> CustomerDemographics { get; set; } = null!;
        public virtual DbSet<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_City { get; set; } = null!;
        public virtual DbSet<Customers> Customers { get; set; } = null!;
        public virtual DbSet<Employees> Employees { get; set; } = null!;
        public virtual DbSet<Invoices> Invoices { get; set; } = null!;
        public virtual DbSet<Order_Details> Order_Details { get; set; } = null!;
        public virtual DbSet<Order_Details_Extended> Order_Details_Extended { get; set; } = null!;
        public virtual DbSet<Order_Subtotals> Order_Subtotals { get; set; } = null!;
        public virtual DbSet<Orders> Orders { get; set; } = null!;
        public virtual DbSet<Orders_Qry> Orders_Qry { get; set; } = null!;
        public virtual DbSet<Product_Sales_for_1997> Product_Sales_for_1997 { get; set; } = null!;
        public virtual DbSet<Products> Products { get; set; } = null!;
        public virtual DbSet<Products_Above_Average_Price> Products_Above_Average_Price { get; set; } = null!;
        public virtual DbSet<Products_by_Category> Products_by_Category { get; set; } = null!;
        public virtual DbSet<Quarterly_Orders> Quarterly_Orders { get; set; } = null!;
        public virtual DbSet<Region> Region { get; set; } = null!;
        public virtual DbSet<Sales_Totals_by_Amount> Sales_Totals_by_Amount { get; set; } = null!;
        public virtual DbSet<Sales_by_Category> Sales_by_Category { get; set; } = null!;
        public virtual DbSet<Shippers> Shippers { get; set; } = null!;
        public virtual DbSet<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_Quarter { get; set; } = null!;
        public virtual DbSet<Summary_of_Sales_by_Year> Summary_of_Sales_by_Year { get; set; } = null!;
        public virtual DbSet<Suppliers> Suppliers { get; set; } = null!;
        public virtual DbSet<Territories> Territories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Northwind;UId=sa;pwd=<YourStrong@Passw0rd>");
                optionsBuilder.UseSqlite(@"DataSource=C:\Users\Surface1\Documents\GitHub\QueryViewer\src\NET6\DB2GUI\northwind.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alphabetical_list_of_products>(entity =>
            {
                entity.ToView("Alphabetical list of products");
            });

            modelBuilder.Entity<Category_Sales_for_1997>(entity =>
            {
                entity.ToView("Category Sales for 1997");
            });

            modelBuilder.Entity<Current_Product_List>(entity =>
            {
                entity.ToView("Current Product List");

                entity.Property(e => e.ProductID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CustomerDemographics>(entity =>
            {
                entity.HasKey(e => e.CustomerTypeID)
                    .IsClustered(false);

                entity.Property(e => e.CustomerTypeID).IsFixedLength();
            });

            modelBuilder.Entity<Customer_and_Suppliers_by_City>(entity =>
            {
                entity.ToView("Customer and Suppliers by City");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.CustomerID).IsFixedLength();

                entity.HasMany(d => d.CustomerType)
                    .WithMany(p => p.Customer)
                    .UsingEntity<Dictionary<string, object>>(
                        "CustomerCustomerDemo",
                        l => l.HasOne<CustomerDemographics>().WithMany().HasForeignKey("CustomerTypeID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CustomerCustomerDemo"),
                        r => r.HasOne<Customers>().WithMany().HasForeignKey("CustomerID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CustomerCustomerDemo_Customers"),
                        j =>
                        {
                            j.HasKey("CustomerID", "CustomerTypeID").IsClustered(false);

                            j.ToTable("CustomerCustomerDemo");

                            j.IndexerProperty<string>("CustomerID").HasMaxLength(5).IsFixedLength();

                            j.IndexerProperty<string>("CustomerTypeID").HasMaxLength(10).IsFixedLength();
                        });
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("FK_Employees_Employees");

                entity.HasMany(d => d.Territory)
                    .WithMany(p => p.Employee)
                    .UsingEntity<Dictionary<string, object>>(
                        "EmployeeTerritories",
                        l => l.HasOne<Territories>().WithMany().HasForeignKey("TerritoryID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_EmployeeTerritories_Territories"),
                        r => r.HasOne<Employees>().WithMany().HasForeignKey("EmployeeID").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_EmployeeTerritories_Employees"),
                        j =>
                        {
                            j.HasKey("EmployeeID", "TerritoryID").IsClustered(false);

                            j.ToTable("EmployeeTerritories");

                            j.IndexerProperty<string>("TerritoryID").HasMaxLength(20);
                        });
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.ToView("Invoices");

                entity.Property(e => e.CustomerID).IsFixedLength();
            });

            modelBuilder.Entity<Order_Details>(entity =>
            {
                entity.HasKey(e => new { e.OrderID, e.ProductID })
                    .HasName("PK_Order_Details");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Order_Details)
                    .HasForeignKey(d => d.OrderID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Order_Details)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Products");
            });

            modelBuilder.Entity<Order_Details_Extended>(entity =>
            {
                entity.ToView("Order Details Extended");
            });

            modelBuilder.Entity<Order_Subtotals>(entity =>
            {
                entity.ToView("Order Subtotals");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.CustomerID).IsFixedLength();

                entity.Property(e => e.Freight).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeID)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.ShipViaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipVia)
                    .HasConstraintName("FK_Orders_Shippers");
            });

            modelBuilder.Entity<Orders_Qry>(entity =>
            {
                entity.ToView("Orders Qry");

                entity.Property(e => e.CustomerID).IsFixedLength();
            });

            modelBuilder.Entity<Product_Sales_for_1997>(entity =>
            {
                entity.ToView("Product Sales for 1997");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierID)
                    .HasConstraintName("FK_Products_Suppliers");
            });

            modelBuilder.Entity<Products_Above_Average_Price>(entity =>
            {
                entity.ToView("Products Above Average Price");
            });

            modelBuilder.Entity<Products_by_Category>(entity =>
            {
                entity.ToView("Products by Category");
            });

            modelBuilder.Entity<Quarterly_Orders>(entity =>
            {
                entity.ToView("Quarterly Orders");

                entity.Property(e => e.CustomerID).IsFixedLength();
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionID)
                    .IsClustered(false);

                entity.Property(e => e.RegionID).ValueGeneratedNever();

                entity.Property(e => e.RegionDescription).IsFixedLength();
            });

            modelBuilder.Entity<Sales_Totals_by_Amount>(entity =>
            {
                entity.ToView("Sales Totals by Amount");
            });

            modelBuilder.Entity<Sales_by_Category>(entity =>
            {
                entity.ToView("Sales by Category");
            });

            modelBuilder.Entity<Summary_of_Sales_by_Quarter>(entity =>
            {
                entity.ToView("Summary of Sales by Quarter");
            });

            modelBuilder.Entity<Summary_of_Sales_by_Year>(entity =>
            {
                entity.ToView("Summary of Sales by Year");
            });

            modelBuilder.Entity<Territories>(entity =>
            {
                entity.HasKey(e => e.TerritoryID)
                    .IsClustered(false);

                entity.Property(e => e.TerritoryDescription).IsFixedLength();

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Territories)
                    .HasForeignKey(d => d.RegionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Territories_Region");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
