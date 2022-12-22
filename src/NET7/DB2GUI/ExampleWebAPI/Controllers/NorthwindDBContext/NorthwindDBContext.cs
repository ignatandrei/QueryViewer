using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Generated;

public partial class NorthwindDBContext : DbContext
{
    public NorthwindDBContext(DbContextOptions<NorthwindDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alphabetical_list_of_products> Alphabetical_list_of_products { get; set; }

    public virtual DbSet<Categories> Categories { get; set; }

    public virtual DbSet<Category_Sales_for_1997> Category_Sales_for_1997 { get; set; }

    public virtual DbSet<Current_Product_List> Current_Product_List { get; set; }

    public virtual DbSet<CustomerDemographics> CustomerDemographics { get; set; }

    public virtual DbSet<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_City { get; set; }

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<Employees> Employees { get; set; }

    public virtual DbSet<Invoices> Invoices { get; set; }

    public virtual DbSet<Order_Details> Order_Details { get; set; }

    public virtual DbSet<Order_Details_Extended> Order_Details_Extended { get; set; }

    public virtual DbSet<Order_Subtotals> Order_Subtotals { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<Orders_Qry> Orders_Qry { get; set; }

    public virtual DbSet<Product_Sales_for_1997> Product_Sales_for_1997 { get; set; }

    public virtual DbSet<Products> Products { get; set; }

    public virtual DbSet<Products_Above_Average_Price> Products_Above_Average_Price { get; set; }

    public virtual DbSet<Products_by_Category> Products_by_Category { get; set; }

    public virtual DbSet<Quarterly_Orders> Quarterly_Orders { get; set; }

    public virtual DbSet<Region> Region { get; set; }

    public virtual DbSet<Sales_Totals_by_Amount> Sales_Totals_by_Amount { get; set; }

    public virtual DbSet<Sales_by_Category> Sales_by_Category { get; set; }

    public virtual DbSet<Shippers> Shippers { get; set; }

    public virtual DbSet<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_Quarter { get; set; }

    public virtual DbSet<Summary_of_Sales_by_Year> Summary_of_Sales_by_Year { get; set; }

    public virtual DbSet<Suppliers> Suppliers { get; set; }

    public virtual DbSet<Territories> Territories { get; set; }

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
            entity.HasKey(e => e.CustomerTypeID).IsClustered(false);

            entity.Property(e => e.CustomerTypeID).IsFixedLength();
        });

        modelBuilder.Entity<Customer_and_Suppliers_by_City>(entity =>
        {
            entity.ToView("Customer and Suppliers by City");
        });

        modelBuilder.Entity<Customers>(entity =>
        {
            entity.Property(e => e.CustomerID).IsFixedLength();

            entity.HasMany(d => d.CustomerType).WithMany(p => p.Customer)
                .UsingEntity<Dictionary<string, object>>(
                    "CustomerCustomerDemo",
                    r => r.HasOne<CustomerDemographics>().WithMany()
                        .HasForeignKey("CustomerTypeID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CustomerCustomerDemo"),
                    l => l.HasOne<Customers>().WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CustomerCustomerDemo_Customers"),
                    j =>
                    {
                        j.HasKey("CustomerID", "CustomerTypeID").IsClustered(false);
                    });
        });

        modelBuilder.Entity<Employees>(entity =>
        {
            entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation).HasConstraintName("FK_Employees_Employees");

            entity.HasMany(d => d.Territory).WithMany(p => p.Employee)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeTerritories",
                    r => r.HasOne<Territories>().WithMany()
                        .HasForeignKey("TerritoryID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EmployeeTerritories_Territories"),
                    l => l.HasOne<Employees>().WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_EmployeeTerritories_Employees"),
                    j =>
                    {
                        j.HasKey("EmployeeID", "TerritoryID").IsClustered(false);
                    });
        });

        modelBuilder.Entity<Invoices>(entity =>
        {
            entity.ToView("Invoices");

            entity.Property(e => e.CustomerID).IsFixedLength();
        });

        modelBuilder.Entity<Order_Details>(entity =>
        {
            entity.HasKey(e => new { e.OrderID, e.ProductID }).HasName("PK_Order_Details");

            entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Order).WithMany(p => p.Order_Details)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.Order_Details)
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

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders).HasConstraintName("FK_Orders_Employees");

            entity.HasOne(d => d.ShipViaNavigation).WithMany(p => p.Orders).HasConstraintName("FK_Orders_Shippers");
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

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products).HasConstraintName("FK_Products_Suppliers");
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
            entity.HasKey(e => e.RegionID).IsClustered(false);

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
            entity.HasKey(e => e.TerritoryID).IsClustered(false);

            entity.Property(e => e.TerritoryDescription).IsFixedLength();

            entity.HasOne(d => d.Region).WithMany(p => p.Territories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Territories_Region");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

//added new
public partial class NorthwindDBContext : DbContext
{
    

    public IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsFind_AsyncEnumerable(SearchAlphabetical_list_of_products? search){
        IQueryable<Alphabetical_list_of_products> data= this.Alphabetical_list_of_products ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    //oneKey    
    public Task<Categories?> CategoriesGetSingle(int id){
        return this.Categories.FirstOrDefaultAsync(e => e.CategoryID == id);
    }
    

    public IAsyncEnumerable<Categories> CategoriesFind_AsyncEnumerable(SearchCategories? search){
        IQueryable<Categories> data= this.Categories ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997Find_AsyncEnumerable(SearchCategory_Sales_for_1997? search){
        IQueryable<Category_Sales_for_1997> data= this.Category_Sales_for_1997 ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Current_Product_List> Current_Product_ListFind_AsyncEnumerable(SearchCurrent_Product_List? search){
        IQueryable<Current_Product_List> data= this.Current_Product_List ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    //oneKey    
    public Task<CustomerDemographics?> CustomerDemographicsGetSingle(string id){
        return this.CustomerDemographics.FirstOrDefaultAsync(e => e.CustomerTypeID == id);
    }
    

    public IAsyncEnumerable<CustomerDemographics> CustomerDemographicsFind_AsyncEnumerable(SearchCustomerDemographics? search){
        IQueryable<CustomerDemographics> data= this.CustomerDemographics ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CityFind_AsyncEnumerable(SearchCustomer_and_Suppliers_by_City? search){
        IQueryable<Customer_and_Suppliers_by_City> data= this.Customer_and_Suppliers_by_City ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    //oneKey    
    public Task<Customers?> CustomersGetSingle(string id){
        return this.Customers.FirstOrDefaultAsync(e => e.CustomerID == id);
    }
    

    public IAsyncEnumerable<Customers> CustomersFind_AsyncEnumerable(SearchCustomers? search){
        IQueryable<Customers> data= this.Customers ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    //oneKey    
    public Task<Employees?> EmployeesGetSingle(int id){
        return this.Employees.FirstOrDefaultAsync(e => e.EmployeeID == id);
    }
    

    public IAsyncEnumerable<Employees> EmployeesFind_AsyncEnumerable(SearchEmployees? search){
        IQueryable<Employees> data= this.Employees ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Invoices> InvoicesFind_AsyncEnumerable(SearchInvoices? search){
        IQueryable<Invoices> data= this.Invoices ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Order_Details> Order_DetailsFind_AsyncEnumerable(SearchOrder_Details? search){
        IQueryable<Order_Details> data= this.Order_Details ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedFind_AsyncEnumerable(SearchOrder_Details_Extended? search){
        IQueryable<Order_Details_Extended> data= this.Order_Details_Extended ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Order_Subtotals> Order_SubtotalsFind_AsyncEnumerable(SearchOrder_Subtotals? search){
        IQueryable<Order_Subtotals> data= this.Order_Subtotals ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    //oneKey    
    public Task<Orders?> OrdersGetSingle(int id){
        return this.Orders.FirstOrDefaultAsync(e => e.OrderID == id);
    }
    

    public IAsyncEnumerable<Orders> OrdersFind_AsyncEnumerable(SearchOrders? search){
        IQueryable<Orders> data= this.Orders ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Orders_Qry> Orders_QryFind_AsyncEnumerable(SearchOrders_Qry? search){
        IQueryable<Orders_Qry> data= this.Orders_Qry ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997Find_AsyncEnumerable(SearchProduct_Sales_for_1997? search){
        IQueryable<Product_Sales_for_1997> data= this.Product_Sales_for_1997 ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    //oneKey    
    public Task<Products?> ProductsGetSingle(int id){
        return this.Products.FirstOrDefaultAsync(e => e.ProductID == id);
    }
    

    public IAsyncEnumerable<Products> ProductsFind_AsyncEnumerable(SearchProducts? search){
        IQueryable<Products> data= this.Products ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceFind_AsyncEnumerable(SearchProducts_Above_Average_Price? search){
        IQueryable<Products_Above_Average_Price> data= this.Products_Above_Average_Price ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Products_by_Category> Products_by_CategoryFind_AsyncEnumerable(SearchProducts_by_Category? search){
        IQueryable<Products_by_Category> data= this.Products_by_Category ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersFind_AsyncEnumerable(SearchQuarterly_Orders? search){
        IQueryable<Quarterly_Orders> data= this.Quarterly_Orders ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    //oneKey    
    public Task<Region?> RegionGetSingle(int id){
        return this.Region.FirstOrDefaultAsync(e => e.RegionID == id);
    }
    

    public IAsyncEnumerable<Region> RegionFind_AsyncEnumerable(SearchRegion? search){
        IQueryable<Region> data= this.Region ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountFind_AsyncEnumerable(SearchSales_Totals_by_Amount? search){
        IQueryable<Sales_Totals_by_Amount> data= this.Sales_Totals_by_Amount ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Sales_by_Category> Sales_by_CategoryFind_AsyncEnumerable(SearchSales_by_Category? search){
        IQueryable<Sales_by_Category> data= this.Sales_by_Category ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    //oneKey    
    public Task<Shippers?> ShippersGetSingle(int id){
        return this.Shippers.FirstOrDefaultAsync(e => e.ShipperID == id);
    }
    

    public IAsyncEnumerable<Shippers> ShippersFind_AsyncEnumerable(SearchShippers? search){
        IQueryable<Shippers> data= this.Shippers ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterFind_AsyncEnumerable(SearchSummary_of_Sales_by_Quarter? search){
        IQueryable<Summary_of_Sales_by_Quarter> data= this.Summary_of_Sales_by_Quarter ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearFind_AsyncEnumerable(SearchSummary_of_Sales_by_Year? search){
        IQueryable<Summary_of_Sales_by_Year> data= this.Summary_of_Sales_by_Year ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    //oneKey    
    public Task<Suppliers?> SuppliersGetSingle(int id){
        return this.Suppliers.FirstOrDefaultAsync(e => e.SupplierID == id);
    }
    

    public IAsyncEnumerable<Suppliers> SuppliersFind_AsyncEnumerable(SearchSuppliers? search){
        IQueryable<Suppliers> data= this.Suppliers ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    //oneKey    
    public Task<Territories?> TerritoriesGetSingle(string id){
        return this.Territories.FirstOrDefaultAsync(e => e.TerritoryID == id);
    }
    

    public IAsyncEnumerable<Territories> TerritoriesFind_AsyncEnumerable(SearchTerritories? search){
        IQueryable<Territories> data= this.Territories ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }

}
public interface I_InsertDataNorthwindDBContext{
        Task<Alphabetical_list_of_products_Table?> InsertAlphabetical_list_of_products(Alphabetical_list_of_products_Table value);
        Task<Alphabetical_list_of_products_Table[]> InsertAlphabetical_list_of_productss(params Alphabetical_list_of_products_Table[] values);

        Task<Categories_Table?> InsertCategories(Categories_Table value);
        Task<Categories_Table[]> InsertCategoriess(params Categories_Table[] values);

        Task<Category_Sales_for_1997_Table?> InsertCategory_Sales_for_1997(Category_Sales_for_1997_Table value);
        Task<Category_Sales_for_1997_Table[]> InsertCategory_Sales_for_1997s(params Category_Sales_for_1997_Table[] values);

        Task<Current_Product_List_Table?> InsertCurrent_Product_List(Current_Product_List_Table value);
        Task<Current_Product_List_Table[]> InsertCurrent_Product_Lists(params Current_Product_List_Table[] values);

        Task<CustomerDemographics_Table?> InsertCustomerDemographics(CustomerDemographics_Table value);
        Task<CustomerDemographics_Table[]> InsertCustomerDemographicss(params CustomerDemographics_Table[] values);

        Task<Customer_and_Suppliers_by_City_Table?> InsertCustomer_and_Suppliers_by_City(Customer_and_Suppliers_by_City_Table value);
        Task<Customer_and_Suppliers_by_City_Table[]> InsertCustomer_and_Suppliers_by_Citys(params Customer_and_Suppliers_by_City_Table[] values);

        Task<Customers_Table?> InsertCustomers(Customers_Table value);
        Task<Customers_Table[]> InsertCustomerss(params Customers_Table[] values);

        Task<Employees_Table?> InsertEmployees(Employees_Table value);
        Task<Employees_Table[]> InsertEmployeess(params Employees_Table[] values);

        Task<Invoices_Table?> InsertInvoices(Invoices_Table value);
        Task<Invoices_Table[]> InsertInvoicess(params Invoices_Table[] values);

        Task<Order_Details_Table?> InsertOrder_Details(Order_Details_Table value);
        Task<Order_Details_Table[]> InsertOrder_Detailss(params Order_Details_Table[] values);

        Task<Order_Details_Extended_Table?> InsertOrder_Details_Extended(Order_Details_Extended_Table value);
        Task<Order_Details_Extended_Table[]> InsertOrder_Details_Extendeds(params Order_Details_Extended_Table[] values);

        Task<Order_Subtotals_Table?> InsertOrder_Subtotals(Order_Subtotals_Table value);
        Task<Order_Subtotals_Table[]> InsertOrder_Subtotalss(params Order_Subtotals_Table[] values);

        Task<Orders_Table?> InsertOrders(Orders_Table value);
        Task<Orders_Table[]> InsertOrderss(params Orders_Table[] values);

        Task<Orders_Qry_Table?> InsertOrders_Qry(Orders_Qry_Table value);
        Task<Orders_Qry_Table[]> InsertOrders_Qrys(params Orders_Qry_Table[] values);

        Task<Product_Sales_for_1997_Table?> InsertProduct_Sales_for_1997(Product_Sales_for_1997_Table value);
        Task<Product_Sales_for_1997_Table[]> InsertProduct_Sales_for_1997s(params Product_Sales_for_1997_Table[] values);

        Task<Products_Table?> InsertProducts(Products_Table value);
        Task<Products_Table[]> InsertProductss(params Products_Table[] values);

        Task<Products_Above_Average_Price_Table?> InsertProducts_Above_Average_Price(Products_Above_Average_Price_Table value);
        Task<Products_Above_Average_Price_Table[]> InsertProducts_Above_Average_Prices(params Products_Above_Average_Price_Table[] values);

        Task<Products_by_Category_Table?> InsertProducts_by_Category(Products_by_Category_Table value);
        Task<Products_by_Category_Table[]> InsertProducts_by_Categorys(params Products_by_Category_Table[] values);

        Task<Quarterly_Orders_Table?> InsertQuarterly_Orders(Quarterly_Orders_Table value);
        Task<Quarterly_Orders_Table[]> InsertQuarterly_Orderss(params Quarterly_Orders_Table[] values);

        Task<Region_Table?> InsertRegion(Region_Table value);
        Task<Region_Table[]> InsertRegions(params Region_Table[] values);

        Task<Sales_Totals_by_Amount_Table?> InsertSales_Totals_by_Amount(Sales_Totals_by_Amount_Table value);
        Task<Sales_Totals_by_Amount_Table[]> InsertSales_Totals_by_Amounts(params Sales_Totals_by_Amount_Table[] values);

        Task<Sales_by_Category_Table?> InsertSales_by_Category(Sales_by_Category_Table value);
        Task<Sales_by_Category_Table[]> InsertSales_by_Categorys(params Sales_by_Category_Table[] values);

        Task<Shippers_Table?> InsertShippers(Shippers_Table value);
        Task<Shippers_Table[]> InsertShipperss(params Shippers_Table[] values);

        Task<Summary_of_Sales_by_Quarter_Table?> InsertSummary_of_Sales_by_Quarter(Summary_of_Sales_by_Quarter_Table value);
        Task<Summary_of_Sales_by_Quarter_Table[]> InsertSummary_of_Sales_by_Quarters(params Summary_of_Sales_by_Quarter_Table[] values);

        Task<Summary_of_Sales_by_Year_Table?> InsertSummary_of_Sales_by_Year(Summary_of_Sales_by_Year_Table value);
        Task<Summary_of_Sales_by_Year_Table[]> InsertSummary_of_Sales_by_Years(params Summary_of_Sales_by_Year_Table[] values);

        Task<Suppliers_Table?> InsertSuppliers(Suppliers_Table value);
        Task<Suppliers_Table[]> InsertSupplierss(params Suppliers_Table[] values);

        Task<Territories_Table?> InsertTerritories(Territories_Table value);
        Task<Territories_Table[]> InsertTerritoriess(params Territories_Table[] values);

    }

public class InsertDataNorthwindDBContext: I_InsertDataNorthwindDBContext{

        private NorthwindDBContext _context;
        public InsertDataNorthwindDBContext(NorthwindDBContext context){
            _context=context;
        }
        public async Task<Alphabetical_list_of_products_Table?> InsertAlphabetical_list_of_products(Alphabetical_list_of_products_Table value){
            if (value == null)
                return null;

            Alphabetical_list_of_products val = (Alphabetical_list_of_products)value!;
            _context.Alphabetical_list_of_products.Add(val);
            await _context.SaveChangesAsync();
            return (Alphabetical_list_of_products_Table)val! ;

        }
        public async Task<Alphabetical_list_of_products_Table[]> InsertAlphabetical_list_of_productss(params Alphabetical_list_of_products_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Alphabetical_list_of_products_Table[0];

        Alphabetical_list_of_products[] vals = values.Select(it=>(Alphabetical_list_of_products)it!).ToArray();
        _context.Alphabetical_list_of_products.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Alphabetical_list_of_products_Table)it!  ).ToArray();
    }

        public async Task<Categories_Table?> InsertCategories(Categories_Table value){
            if (value == null)
                return null;

            Categories val = (Categories)value!;
            _context.Categories.Add(val);
            await _context.SaveChangesAsync();
            return (Categories_Table)val! ;

        }
        public async Task<Categories_Table[]> InsertCategoriess(params Categories_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Categories_Table[0];

        Categories[] vals = values.Select(it=>(Categories)it!).ToArray();
        _context.Categories.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Categories_Table)it!  ).ToArray();
    }

        public async Task<Category_Sales_for_1997_Table?> InsertCategory_Sales_for_1997(Category_Sales_for_1997_Table value){
            if (value == null)
                return null;

            Category_Sales_for_1997 val = (Category_Sales_for_1997)value!;
            _context.Category_Sales_for_1997.Add(val);
            await _context.SaveChangesAsync();
            return (Category_Sales_for_1997_Table)val! ;

        }
        public async Task<Category_Sales_for_1997_Table[]> InsertCategory_Sales_for_1997s(params Category_Sales_for_1997_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Category_Sales_for_1997_Table[0];

        Category_Sales_for_1997[] vals = values.Select(it=>(Category_Sales_for_1997)it!).ToArray();
        _context.Category_Sales_for_1997.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Category_Sales_for_1997_Table)it!  ).ToArray();
    }

        public async Task<Current_Product_List_Table?> InsertCurrent_Product_List(Current_Product_List_Table value){
            if (value == null)
                return null;

            Current_Product_List val = (Current_Product_List)value!;
            _context.Current_Product_List.Add(val);
            await _context.SaveChangesAsync();
            return (Current_Product_List_Table)val! ;

        }
        public async Task<Current_Product_List_Table[]> InsertCurrent_Product_Lists(params Current_Product_List_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Current_Product_List_Table[0];

        Current_Product_List[] vals = values.Select(it=>(Current_Product_List)it!).ToArray();
        _context.Current_Product_List.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Current_Product_List_Table)it!  ).ToArray();
    }

        public async Task<CustomerDemographics_Table?> InsertCustomerDemographics(CustomerDemographics_Table value){
            if (value == null)
                return null;

            CustomerDemographics val = (CustomerDemographics)value!;
            _context.CustomerDemographics.Add(val);
            await _context.SaveChangesAsync();
            return (CustomerDemographics_Table)val! ;

        }
        public async Task<CustomerDemographics_Table[]> InsertCustomerDemographicss(params CustomerDemographics_Table[] values){
        
        if (values == null || values.Length == 0)
            return new CustomerDemographics_Table[0];

        CustomerDemographics[] vals = values.Select(it=>(CustomerDemographics)it!).ToArray();
        _context.CustomerDemographics.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (CustomerDemographics_Table)it!  ).ToArray();
    }

        public async Task<Customer_and_Suppliers_by_City_Table?> InsertCustomer_and_Suppliers_by_City(Customer_and_Suppliers_by_City_Table value){
            if (value == null)
                return null;

            Customer_and_Suppliers_by_City val = (Customer_and_Suppliers_by_City)value!;
            _context.Customer_and_Suppliers_by_City.Add(val);
            await _context.SaveChangesAsync();
            return (Customer_and_Suppliers_by_City_Table)val! ;

        }
        public async Task<Customer_and_Suppliers_by_City_Table[]> InsertCustomer_and_Suppliers_by_Citys(params Customer_and_Suppliers_by_City_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Customer_and_Suppliers_by_City_Table[0];

        Customer_and_Suppliers_by_City[] vals = values.Select(it=>(Customer_and_Suppliers_by_City)it!).ToArray();
        _context.Customer_and_Suppliers_by_City.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Customer_and_Suppliers_by_City_Table)it!  ).ToArray();
    }

        public async Task<Customers_Table?> InsertCustomers(Customers_Table value){
            if (value == null)
                return null;

            Customers val = (Customers)value!;
            _context.Customers.Add(val);
            await _context.SaveChangesAsync();
            return (Customers_Table)val! ;

        }
        public async Task<Customers_Table[]> InsertCustomerss(params Customers_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Customers_Table[0];

        Customers[] vals = values.Select(it=>(Customers)it!).ToArray();
        _context.Customers.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Customers_Table)it!  ).ToArray();
    }

        public async Task<Employees_Table?> InsertEmployees(Employees_Table value){
            if (value == null)
                return null;

            Employees val = (Employees)value!;
            _context.Employees.Add(val);
            await _context.SaveChangesAsync();
            return (Employees_Table)val! ;

        }
        public async Task<Employees_Table[]> InsertEmployeess(params Employees_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Employees_Table[0];

        Employees[] vals = values.Select(it=>(Employees)it!).ToArray();
        _context.Employees.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Employees_Table)it!  ).ToArray();
    }

        public async Task<Invoices_Table?> InsertInvoices(Invoices_Table value){
            if (value == null)
                return null;

            Invoices val = (Invoices)value!;
            _context.Invoices.Add(val);
            await _context.SaveChangesAsync();
            return (Invoices_Table)val! ;

        }
        public async Task<Invoices_Table[]> InsertInvoicess(params Invoices_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Invoices_Table[0];

        Invoices[] vals = values.Select(it=>(Invoices)it!).ToArray();
        _context.Invoices.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Invoices_Table)it!  ).ToArray();
    }

        public async Task<Order_Details_Table?> InsertOrder_Details(Order_Details_Table value){
            if (value == null)
                return null;

            Order_Details val = (Order_Details)value!;
            _context.Order_Details.Add(val);
            await _context.SaveChangesAsync();
            return (Order_Details_Table)val! ;

        }
        public async Task<Order_Details_Table[]> InsertOrder_Detailss(params Order_Details_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Order_Details_Table[0];

        Order_Details[] vals = values.Select(it=>(Order_Details)it!).ToArray();
        _context.Order_Details.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Order_Details_Table)it!  ).ToArray();
    }

        public async Task<Order_Details_Extended_Table?> InsertOrder_Details_Extended(Order_Details_Extended_Table value){
            if (value == null)
                return null;

            Order_Details_Extended val = (Order_Details_Extended)value!;
            _context.Order_Details_Extended.Add(val);
            await _context.SaveChangesAsync();
            return (Order_Details_Extended_Table)val! ;

        }
        public async Task<Order_Details_Extended_Table[]> InsertOrder_Details_Extendeds(params Order_Details_Extended_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Order_Details_Extended_Table[0];

        Order_Details_Extended[] vals = values.Select(it=>(Order_Details_Extended)it!).ToArray();
        _context.Order_Details_Extended.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Order_Details_Extended_Table)it!  ).ToArray();
    }

        public async Task<Order_Subtotals_Table?> InsertOrder_Subtotals(Order_Subtotals_Table value){
            if (value == null)
                return null;

            Order_Subtotals val = (Order_Subtotals)value!;
            _context.Order_Subtotals.Add(val);
            await _context.SaveChangesAsync();
            return (Order_Subtotals_Table)val! ;

        }
        public async Task<Order_Subtotals_Table[]> InsertOrder_Subtotalss(params Order_Subtotals_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Order_Subtotals_Table[0];

        Order_Subtotals[] vals = values.Select(it=>(Order_Subtotals)it!).ToArray();
        _context.Order_Subtotals.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Order_Subtotals_Table)it!  ).ToArray();
    }

        public async Task<Orders_Table?> InsertOrders(Orders_Table value){
            if (value == null)
                return null;

            Orders val = (Orders)value!;
            _context.Orders.Add(val);
            await _context.SaveChangesAsync();
            return (Orders_Table)val! ;

        }
        public async Task<Orders_Table[]> InsertOrderss(params Orders_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Orders_Table[0];

        Orders[] vals = values.Select(it=>(Orders)it!).ToArray();
        _context.Orders.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Orders_Table)it!  ).ToArray();
    }

        public async Task<Orders_Qry_Table?> InsertOrders_Qry(Orders_Qry_Table value){
            if (value == null)
                return null;

            Orders_Qry val = (Orders_Qry)value!;
            _context.Orders_Qry.Add(val);
            await _context.SaveChangesAsync();
            return (Orders_Qry_Table)val! ;

        }
        public async Task<Orders_Qry_Table[]> InsertOrders_Qrys(params Orders_Qry_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Orders_Qry_Table[0];

        Orders_Qry[] vals = values.Select(it=>(Orders_Qry)it!).ToArray();
        _context.Orders_Qry.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Orders_Qry_Table)it!  ).ToArray();
    }

        public async Task<Product_Sales_for_1997_Table?> InsertProduct_Sales_for_1997(Product_Sales_for_1997_Table value){
            if (value == null)
                return null;

            Product_Sales_for_1997 val = (Product_Sales_for_1997)value!;
            _context.Product_Sales_for_1997.Add(val);
            await _context.SaveChangesAsync();
            return (Product_Sales_for_1997_Table)val! ;

        }
        public async Task<Product_Sales_for_1997_Table[]> InsertProduct_Sales_for_1997s(params Product_Sales_for_1997_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Product_Sales_for_1997_Table[0];

        Product_Sales_for_1997[] vals = values.Select(it=>(Product_Sales_for_1997)it!).ToArray();
        _context.Product_Sales_for_1997.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Product_Sales_for_1997_Table)it!  ).ToArray();
    }

        public async Task<Products_Table?> InsertProducts(Products_Table value){
            if (value == null)
                return null;

            Products val = (Products)value!;
            _context.Products.Add(val);
            await _context.SaveChangesAsync();
            return (Products_Table)val! ;

        }
        public async Task<Products_Table[]> InsertProductss(params Products_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Products_Table[0];

        Products[] vals = values.Select(it=>(Products)it!).ToArray();
        _context.Products.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Products_Table)it!  ).ToArray();
    }

        public async Task<Products_Above_Average_Price_Table?> InsertProducts_Above_Average_Price(Products_Above_Average_Price_Table value){
            if (value == null)
                return null;

            Products_Above_Average_Price val = (Products_Above_Average_Price)value!;
            _context.Products_Above_Average_Price.Add(val);
            await _context.SaveChangesAsync();
            return (Products_Above_Average_Price_Table)val! ;

        }
        public async Task<Products_Above_Average_Price_Table[]> InsertProducts_Above_Average_Prices(params Products_Above_Average_Price_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Products_Above_Average_Price_Table[0];

        Products_Above_Average_Price[] vals = values.Select(it=>(Products_Above_Average_Price)it!).ToArray();
        _context.Products_Above_Average_Price.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Products_Above_Average_Price_Table)it!  ).ToArray();
    }

        public async Task<Products_by_Category_Table?> InsertProducts_by_Category(Products_by_Category_Table value){
            if (value == null)
                return null;

            Products_by_Category val = (Products_by_Category)value!;
            _context.Products_by_Category.Add(val);
            await _context.SaveChangesAsync();
            return (Products_by_Category_Table)val! ;

        }
        public async Task<Products_by_Category_Table[]> InsertProducts_by_Categorys(params Products_by_Category_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Products_by_Category_Table[0];

        Products_by_Category[] vals = values.Select(it=>(Products_by_Category)it!).ToArray();
        _context.Products_by_Category.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Products_by_Category_Table)it!  ).ToArray();
    }

        public async Task<Quarterly_Orders_Table?> InsertQuarterly_Orders(Quarterly_Orders_Table value){
            if (value == null)
                return null;

            Quarterly_Orders val = (Quarterly_Orders)value!;
            _context.Quarterly_Orders.Add(val);
            await _context.SaveChangesAsync();
            return (Quarterly_Orders_Table)val! ;

        }
        public async Task<Quarterly_Orders_Table[]> InsertQuarterly_Orderss(params Quarterly_Orders_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Quarterly_Orders_Table[0];

        Quarterly_Orders[] vals = values.Select(it=>(Quarterly_Orders)it!).ToArray();
        _context.Quarterly_Orders.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Quarterly_Orders_Table)it!  ).ToArray();
    }

        public async Task<Region_Table?> InsertRegion(Region_Table value){
            if (value == null)
                return null;

            Region val = (Region)value!;
            _context.Region.Add(val);
            await _context.SaveChangesAsync();
            return (Region_Table)val! ;

        }
        public async Task<Region_Table[]> InsertRegions(params Region_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Region_Table[0];

        Region[] vals = values.Select(it=>(Region)it!).ToArray();
        _context.Region.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Region_Table)it!  ).ToArray();
    }

        public async Task<Sales_Totals_by_Amount_Table?> InsertSales_Totals_by_Amount(Sales_Totals_by_Amount_Table value){
            if (value == null)
                return null;

            Sales_Totals_by_Amount val = (Sales_Totals_by_Amount)value!;
            _context.Sales_Totals_by_Amount.Add(val);
            await _context.SaveChangesAsync();
            return (Sales_Totals_by_Amount_Table)val! ;

        }
        public async Task<Sales_Totals_by_Amount_Table[]> InsertSales_Totals_by_Amounts(params Sales_Totals_by_Amount_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Sales_Totals_by_Amount_Table[0];

        Sales_Totals_by_Amount[] vals = values.Select(it=>(Sales_Totals_by_Amount)it!).ToArray();
        _context.Sales_Totals_by_Amount.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Sales_Totals_by_Amount_Table)it!  ).ToArray();
    }

        public async Task<Sales_by_Category_Table?> InsertSales_by_Category(Sales_by_Category_Table value){
            if (value == null)
                return null;

            Sales_by_Category val = (Sales_by_Category)value!;
            _context.Sales_by_Category.Add(val);
            await _context.SaveChangesAsync();
            return (Sales_by_Category_Table)val! ;

        }
        public async Task<Sales_by_Category_Table[]> InsertSales_by_Categorys(params Sales_by_Category_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Sales_by_Category_Table[0];

        Sales_by_Category[] vals = values.Select(it=>(Sales_by_Category)it!).ToArray();
        _context.Sales_by_Category.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Sales_by_Category_Table)it!  ).ToArray();
    }

        public async Task<Shippers_Table?> InsertShippers(Shippers_Table value){
            if (value == null)
                return null;

            Shippers val = (Shippers)value!;
            _context.Shippers.Add(val);
            await _context.SaveChangesAsync();
            return (Shippers_Table)val! ;

        }
        public async Task<Shippers_Table[]> InsertShipperss(params Shippers_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Shippers_Table[0];

        Shippers[] vals = values.Select(it=>(Shippers)it!).ToArray();
        _context.Shippers.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Shippers_Table)it!  ).ToArray();
    }

        public async Task<Summary_of_Sales_by_Quarter_Table?> InsertSummary_of_Sales_by_Quarter(Summary_of_Sales_by_Quarter_Table value){
            if (value == null)
                return null;

            Summary_of_Sales_by_Quarter val = (Summary_of_Sales_by_Quarter)value!;
            _context.Summary_of_Sales_by_Quarter.Add(val);
            await _context.SaveChangesAsync();
            return (Summary_of_Sales_by_Quarter_Table)val! ;

        }
        public async Task<Summary_of_Sales_by_Quarter_Table[]> InsertSummary_of_Sales_by_Quarters(params Summary_of_Sales_by_Quarter_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Summary_of_Sales_by_Quarter_Table[0];

        Summary_of_Sales_by_Quarter[] vals = values.Select(it=>(Summary_of_Sales_by_Quarter)it!).ToArray();
        _context.Summary_of_Sales_by_Quarter.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Summary_of_Sales_by_Quarter_Table)it!  ).ToArray();
    }

        public async Task<Summary_of_Sales_by_Year_Table?> InsertSummary_of_Sales_by_Year(Summary_of_Sales_by_Year_Table value){
            if (value == null)
                return null;

            Summary_of_Sales_by_Year val = (Summary_of_Sales_by_Year)value!;
            _context.Summary_of_Sales_by_Year.Add(val);
            await _context.SaveChangesAsync();
            return (Summary_of_Sales_by_Year_Table)val! ;

        }
        public async Task<Summary_of_Sales_by_Year_Table[]> InsertSummary_of_Sales_by_Years(params Summary_of_Sales_by_Year_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Summary_of_Sales_by_Year_Table[0];

        Summary_of_Sales_by_Year[] vals = values.Select(it=>(Summary_of_Sales_by_Year)it!).ToArray();
        _context.Summary_of_Sales_by_Year.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Summary_of_Sales_by_Year_Table)it!  ).ToArray();
    }

        public async Task<Suppliers_Table?> InsertSuppliers(Suppliers_Table value){
            if (value == null)
                return null;

            Suppliers val = (Suppliers)value!;
            _context.Suppliers.Add(val);
            await _context.SaveChangesAsync();
            return (Suppliers_Table)val! ;

        }
        public async Task<Suppliers_Table[]> InsertSupplierss(params Suppliers_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Suppliers_Table[0];

        Suppliers[] vals = values.Select(it=>(Suppliers)it!).ToArray();
        _context.Suppliers.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Suppliers_Table)it!  ).ToArray();
    }

        public async Task<Territories_Table?> InsertTerritories(Territories_Table value){
            if (value == null)
                return null;

            Territories val = (Territories)value!;
            _context.Territories.Add(val);
            await _context.SaveChangesAsync();
            return (Territories_Table)val! ;

        }
        public async Task<Territories_Table[]> InsertTerritoriess(params Territories_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Territories_Table[0];

        Territories[] vals = values.Select(it=>(Territories)it!).ToArray();
        _context.Territories.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Territories_Table)it!  ).ToArray();
    }

    

   public interface ISearchDataAlphabetical_list_of_products {
        IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsFind_AsyncEnumerable(SearchAlphabetical_list_of_products? search);
        
    
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_SupplierID(GeneratorFromDB.SearchCriteria sc,  int? value);
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_SupplierID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_CategoryID(GeneratorFromDB.SearchCriteria sc,  int? value);
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_CategoryID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_UnitsInStock(GeneratorFromDB.SearchCriteria sc,  short? value);
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_UnitsInStock(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_UnitsOnOrder(GeneratorFromDB.SearchCriteria sc,  short? value);
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_UnitsOnOrder(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_ReorderLevel(GeneratorFromDB.SearchCriteria sc,  short? value);
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_ReorderLevel(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_Discontinued(GeneratorFromDB.SearchCriteria sc,  bool value);
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_Discontinued(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataAlphabetical_list_of_products: ISearchDataAlphabetical_list_of_products{
        private NorthwindDBContext context;
        public SearchDataAlphabetical_list_of_products (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsFind_AsyncEnumerable(SearchAlphabetical_list_of_products? search){
            return context.Alphabetical_list_of_productsFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch(GeneratorFromDB.SearchCriteria sc, eAlphabetical_list_of_productsColumns colToSearch, string? value){

            var search = new SearchAlphabetical_list_of_products();
            var orderBy = new GeneratorFromDB.OrderBy<eAlphabetical_list_of_productsColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eAlphabetical_list_of_productsColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Alphabetical_list_of_productsFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.ProductID,value.ToString());

    
    }
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc){
        return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.ProductID,null);

    }


        //False
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.ProductName,value.ToString());

    
    }
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc){
        return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.ProductName,null);

    }


        //True
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_SupplierID(GeneratorFromDB.SearchCriteria sc,  int? value){
         return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.SupplierID,value?.ToString());

    
    }
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_SupplierID(GeneratorFromDB.SearchCriteria sc){
        return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.SupplierID,null);

    }


        //True
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_CategoryID(GeneratorFromDB.SearchCriteria sc,  int? value){
         return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.CategoryID,value?.ToString());

    
    }
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_CategoryID(GeneratorFromDB.SearchCriteria sc){
        return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.CategoryID,null);

    }


        //True
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc,  string value){
         return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.QuantityPerUnit,value?.ToString());

    
    }
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc){
        return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.QuantityPerUnit,null);

    }


        //True
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.UnitPrice,value?.ToString());

    
    }
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc){
        return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.UnitPrice,null);

    }


        //True
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_UnitsInStock(GeneratorFromDB.SearchCriteria sc,  short? value){
         return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.UnitsInStock,value?.ToString());

    
    }
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_UnitsInStock(GeneratorFromDB.SearchCriteria sc){
        return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.UnitsInStock,null);

    }


        //True
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_UnitsOnOrder(GeneratorFromDB.SearchCriteria sc,  short? value){
         return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.UnitsOnOrder,value?.ToString());

    
    }
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_UnitsOnOrder(GeneratorFromDB.SearchCriteria sc){
        return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.UnitsOnOrder,null);

    }


        //True
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_ReorderLevel(GeneratorFromDB.SearchCriteria sc,  short? value){
         return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.ReorderLevel,value?.ToString());

    
    }
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_ReorderLevel(GeneratorFromDB.SearchCriteria sc){
        return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.ReorderLevel,null);

    }


        //False
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_Discontinued(GeneratorFromDB.SearchCriteria sc,  bool value){
         return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.Discontinued,value.ToString());

    
    }
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_Discontinued(GeneratorFromDB.SearchCriteria sc){
        return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.Discontinued,null);

    }


        //False
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.CategoryName,value.ToString());

    
    }
    public  IAsyncEnumerable<Alphabetical_list_of_products> Alphabetical_list_of_productsSimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc){
        return Alphabetical_list_of_productsSimpleSearch(sc,eAlphabetical_list_of_productsColumns.CategoryName,null);

    }


        } //class searchdata




    
   public interface ISearchDataCategories {
        IAsyncEnumerable<Categories> CategoriesFind_AsyncEnumerable(SearchCategories? search);
        //oneKey    
    public Task<Categories?> CategoriesGetSingle(int id);
    
    
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearch_CategoryID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearchNull_CategoryID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearch_Description(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearchNull_Description(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearch_Picture(GeneratorFromDB.SearchCriteria sc,  byte[] value);
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearchNull_Picture(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataCategories: ISearchDataCategories{
        private NorthwindDBContext context;
        public SearchDataCategories (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Categories> CategoriesFind_AsyncEnumerable(SearchCategories? search){
            return context.CategoriesFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<Categories?> CategoriesGetSingle(int id){
            return context.CategoriesGetSingle(id);
    }
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearch(GeneratorFromDB.SearchCriteria sc, eCategoriesColumns colToSearch, string? value){

            var search = new SearchCategories();
            var orderBy = new GeneratorFromDB.OrderBy<eCategoriesColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eCategoriesColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.CategoriesFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearch_CategoryID(GeneratorFromDB.SearchCriteria sc,  int value){
         return CategoriesSimpleSearch(sc,eCategoriesColumns.CategoryID,value.ToString());

    
    }
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearchNull_CategoryID(GeneratorFromDB.SearchCriteria sc){
        return CategoriesSimpleSearch(sc,eCategoriesColumns.CategoryID,null);

    }


        //False
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value){
         return CategoriesSimpleSearch(sc,eCategoriesColumns.CategoryName,value.ToString());

    
    }
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc){
        return CategoriesSimpleSearch(sc,eCategoriesColumns.CategoryName,null);

    }


        //True
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearch_Description(GeneratorFromDB.SearchCriteria sc,  string value){
         return CategoriesSimpleSearch(sc,eCategoriesColumns.Description,value?.ToString());

    
    }
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearchNull_Description(GeneratorFromDB.SearchCriteria sc){
        return CategoriesSimpleSearch(sc,eCategoriesColumns.Description,null);

    }


        //True
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearch_Picture(GeneratorFromDB.SearchCriteria sc,  byte[] value){
         return CategoriesSimpleSearch(sc,eCategoriesColumns.Picture,value?.ToString());

    
    }
    public  IAsyncEnumerable<Categories> CategoriesSimpleSearchNull_Picture(GeneratorFromDB.SearchCriteria sc){
        return CategoriesSimpleSearch(sc,eCategoriesColumns.Picture,null);

    }


        } //class searchdata




    
   public interface ISearchDataCategory_Sales_for_1997 {
        IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997Find_AsyncEnumerable(SearchCategory_Sales_for_1997? search);
        
    
    public  IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997SimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997SimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997SimpleSearch_CategorySales(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997SimpleSearchNull_CategorySales(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataCategory_Sales_for_1997: ISearchDataCategory_Sales_for_1997{
        private NorthwindDBContext context;
        public SearchDataCategory_Sales_for_1997 (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997Find_AsyncEnumerable(SearchCategory_Sales_for_1997? search){
            return context.Category_Sales_for_1997Find_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997SimpleSearch(GeneratorFromDB.SearchCriteria sc, eCategory_Sales_for_1997Columns colToSearch, string? value){

            var search = new SearchCategory_Sales_for_1997();
            var orderBy = new GeneratorFromDB.OrderBy<eCategory_Sales_for_1997Columns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eCategory_Sales_for_1997Columns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Category_Sales_for_1997Find_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997SimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Category_Sales_for_1997SimpleSearch(sc,eCategory_Sales_for_1997Columns.CategoryName,value.ToString());

    
    }
    public  IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997SimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc){
        return Category_Sales_for_1997SimpleSearch(sc,eCategory_Sales_for_1997Columns.CategoryName,null);

    }


        //True
    public  IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997SimpleSearch_CategorySales(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return Category_Sales_for_1997SimpleSearch(sc,eCategory_Sales_for_1997Columns.CategorySales,value?.ToString());

    
    }
    public  IAsyncEnumerable<Category_Sales_for_1997> Category_Sales_for_1997SimpleSearchNull_CategorySales(GeneratorFromDB.SearchCriteria sc){
        return Category_Sales_for_1997SimpleSearch(sc,eCategory_Sales_for_1997Columns.CategorySales,null);

    }


        } //class searchdata




    
   public interface ISearchDataCurrent_Product_List {
        IAsyncEnumerable<Current_Product_List> Current_Product_ListFind_AsyncEnumerable(SearchCurrent_Product_List? search);
        
    
    public  IAsyncEnumerable<Current_Product_List> Current_Product_ListSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Current_Product_List> Current_Product_ListSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Current_Product_List> Current_Product_ListSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Current_Product_List> Current_Product_ListSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataCurrent_Product_List: ISearchDataCurrent_Product_List{
        private NorthwindDBContext context;
        public SearchDataCurrent_Product_List (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Current_Product_List> Current_Product_ListFind_AsyncEnumerable(SearchCurrent_Product_List? search){
            return context.Current_Product_ListFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Current_Product_List> Current_Product_ListSimpleSearch(GeneratorFromDB.SearchCriteria sc, eCurrent_Product_ListColumns colToSearch, string? value){

            var search = new SearchCurrent_Product_List();
            var orderBy = new GeneratorFromDB.OrderBy<eCurrent_Product_ListColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eCurrent_Product_ListColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Current_Product_ListFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Current_Product_List> Current_Product_ListSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Current_Product_ListSimpleSearch(sc,eCurrent_Product_ListColumns.ProductID,value.ToString());

    
    }
    public  IAsyncEnumerable<Current_Product_List> Current_Product_ListSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc){
        return Current_Product_ListSimpleSearch(sc,eCurrent_Product_ListColumns.ProductID,null);

    }


        //False
    public  IAsyncEnumerable<Current_Product_List> Current_Product_ListSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Current_Product_ListSimpleSearch(sc,eCurrent_Product_ListColumns.ProductName,value.ToString());

    
    }
    public  IAsyncEnumerable<Current_Product_List> Current_Product_ListSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc){
        return Current_Product_ListSimpleSearch(sc,eCurrent_Product_ListColumns.ProductName,null);

    }


        } //class searchdata




    
   public interface ISearchDataCustomerDemographics {
        IAsyncEnumerable<CustomerDemographics> CustomerDemographicsFind_AsyncEnumerable(SearchCustomerDemographics? search);
        //oneKey    
    public Task<CustomerDemographics?> CustomerDemographicsGetSingle(string id);
    
    
    public  IAsyncEnumerable<CustomerDemographics> CustomerDemographicsSimpleSearch_CustomerTypeID(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<CustomerDemographics> CustomerDemographicsSimpleSearchNull_CustomerTypeID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<CustomerDemographics> CustomerDemographicsSimpleSearch_CustomerDesc(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<CustomerDemographics> CustomerDemographicsSimpleSearchNull_CustomerDesc(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataCustomerDemographics: ISearchDataCustomerDemographics{
        private NorthwindDBContext context;
        public SearchDataCustomerDemographics (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<CustomerDemographics> CustomerDemographicsFind_AsyncEnumerable(SearchCustomerDemographics? search){
            return context.CustomerDemographicsFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<CustomerDemographics?> CustomerDemographicsGetSingle(string id){
            return context.CustomerDemographicsGetSingle(id);
    }
    public  IAsyncEnumerable<CustomerDemographics> CustomerDemographicsSimpleSearch(GeneratorFromDB.SearchCriteria sc, eCustomerDemographicsColumns colToSearch, string? value){

            var search = new SearchCustomerDemographics();
            var orderBy = new GeneratorFromDB.OrderBy<eCustomerDemographicsColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eCustomerDemographicsColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.CustomerDemographicsFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<CustomerDemographics> CustomerDemographicsSimpleSearch_CustomerTypeID(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomerDemographicsSimpleSearch(sc,eCustomerDemographicsColumns.CustomerTypeID,value.ToString());

    
    }
    public  IAsyncEnumerable<CustomerDemographics> CustomerDemographicsSimpleSearchNull_CustomerTypeID(GeneratorFromDB.SearchCriteria sc){
        return CustomerDemographicsSimpleSearch(sc,eCustomerDemographicsColumns.CustomerTypeID,null);

    }


        //True
    public  IAsyncEnumerable<CustomerDemographics> CustomerDemographicsSimpleSearch_CustomerDesc(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomerDemographicsSimpleSearch(sc,eCustomerDemographicsColumns.CustomerDesc,value?.ToString());

    
    }
    public  IAsyncEnumerable<CustomerDemographics> CustomerDemographicsSimpleSearchNull_CustomerDesc(GeneratorFromDB.SearchCriteria sc){
        return CustomerDemographicsSimpleSearch(sc,eCustomerDemographicsColumns.CustomerDesc,null);

    }


        } //class searchdata




    
   public interface ISearchDataCustomer_and_Suppliers_by_City {
        IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CityFind_AsyncEnumerable(SearchCustomer_and_Suppliers_by_City? search);
        
    
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearch_ContactName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearchNull_ContactName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearch_Relationship(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearchNull_Relationship(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataCustomer_and_Suppliers_by_City: ISearchDataCustomer_and_Suppliers_by_City{
        private NorthwindDBContext context;
        public SearchDataCustomer_and_Suppliers_by_City (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CityFind_AsyncEnumerable(SearchCustomer_and_Suppliers_by_City? search){
            return context.Customer_and_Suppliers_by_CityFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearch(GeneratorFromDB.SearchCriteria sc, eCustomer_and_Suppliers_by_CityColumns colToSearch, string? value){

            var search = new SearchCustomer_and_Suppliers_by_City();
            var orderBy = new GeneratorFromDB.OrderBy<eCustomer_and_Suppliers_by_CityColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eCustomer_and_Suppliers_by_CityColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Customer_and_Suppliers_by_CityFind_AsyncEnumerable(search);
            return data;
        }

    
        //True
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value){
         return Customer_and_Suppliers_by_CitySimpleSearch(sc,eCustomer_and_Suppliers_by_CityColumns.City,value?.ToString());

    
    }
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc){
        return Customer_and_Suppliers_by_CitySimpleSearch(sc,eCustomer_and_Suppliers_by_CityColumns.City,null);

    }


        //False
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Customer_and_Suppliers_by_CitySimpleSearch(sc,eCustomer_and_Suppliers_by_CityColumns.CompanyName,value.ToString());

    
    }
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc){
        return Customer_and_Suppliers_by_CitySimpleSearch(sc,eCustomer_and_Suppliers_by_CityColumns.CompanyName,null);

    }


        //True
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearch_ContactName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Customer_and_Suppliers_by_CitySimpleSearch(sc,eCustomer_and_Suppliers_by_CityColumns.ContactName,value?.ToString());

    
    }
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearchNull_ContactName(GeneratorFromDB.SearchCriteria sc){
        return Customer_and_Suppliers_by_CitySimpleSearch(sc,eCustomer_and_Suppliers_by_CityColumns.ContactName,null);

    }


        //False
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearch_Relationship(GeneratorFromDB.SearchCriteria sc,  string value){
         return Customer_and_Suppliers_by_CitySimpleSearch(sc,eCustomer_and_Suppliers_by_CityColumns.Relationship,value.ToString());

    
    }
    public  IAsyncEnumerable<Customer_and_Suppliers_by_City> Customer_and_Suppliers_by_CitySimpleSearchNull_Relationship(GeneratorFromDB.SearchCriteria sc){
        return Customer_and_Suppliers_by_CitySimpleSearch(sc,eCustomer_and_Suppliers_by_CityColumns.Relationship,null);

    }


        } //class searchdata




    
   public interface ISearchDataCustomers {
        IAsyncEnumerable<Customers> CustomersFind_AsyncEnumerable(SearchCustomers? search);
        //oneKey    
    public Task<Customers?> CustomersGetSingle(string id);
    
    
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_CustomerID(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_CustomerID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_ContactName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_ContactName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_ContactTitle(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_ContactTitle(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_Address(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_Address(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_Region(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_Region(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_PostalCode(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_PostalCode(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_Phone(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_Phone(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_Fax(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_Fax(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataCustomers: ISearchDataCustomers{
        private NorthwindDBContext context;
        public SearchDataCustomers (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Customers> CustomersFind_AsyncEnumerable(SearchCustomers? search){
            return context.CustomersFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<Customers?> CustomersGetSingle(string id){
            return context.CustomersGetSingle(id);
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch(GeneratorFromDB.SearchCriteria sc, eCustomersColumns colToSearch, string? value){

            var search = new SearchCustomers();
            var orderBy = new GeneratorFromDB.OrderBy<eCustomersColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eCustomersColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.CustomersFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_CustomerID(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomersSimpleSearch(sc,eCustomersColumns.CustomerID,value.ToString());

    
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_CustomerID(GeneratorFromDB.SearchCriteria sc){
        return CustomersSimpleSearch(sc,eCustomersColumns.CustomerID,null);

    }


        //False
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomersSimpleSearch(sc,eCustomersColumns.CompanyName,value.ToString());

    
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc){
        return CustomersSimpleSearch(sc,eCustomersColumns.CompanyName,null);

    }


        //True
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_ContactName(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomersSimpleSearch(sc,eCustomersColumns.ContactName,value?.ToString());

    
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_ContactName(GeneratorFromDB.SearchCriteria sc){
        return CustomersSimpleSearch(sc,eCustomersColumns.ContactName,null);

    }


        //True
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_ContactTitle(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomersSimpleSearch(sc,eCustomersColumns.ContactTitle,value?.ToString());

    
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_ContactTitle(GeneratorFromDB.SearchCriteria sc){
        return CustomersSimpleSearch(sc,eCustomersColumns.ContactTitle,null);

    }


        //True
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_Address(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomersSimpleSearch(sc,eCustomersColumns.Address,value?.ToString());

    
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_Address(GeneratorFromDB.SearchCriteria sc){
        return CustomersSimpleSearch(sc,eCustomersColumns.Address,null);

    }


        //True
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomersSimpleSearch(sc,eCustomersColumns.City,value?.ToString());

    
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc){
        return CustomersSimpleSearch(sc,eCustomersColumns.City,null);

    }


        //True
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_Region(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomersSimpleSearch(sc,eCustomersColumns.Region,value?.ToString());

    
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_Region(GeneratorFromDB.SearchCriteria sc){
        return CustomersSimpleSearch(sc,eCustomersColumns.Region,null);

    }


        //True
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_PostalCode(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomersSimpleSearch(sc,eCustomersColumns.PostalCode,value?.ToString());

    
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_PostalCode(GeneratorFromDB.SearchCriteria sc){
        return CustomersSimpleSearch(sc,eCustomersColumns.PostalCode,null);

    }


        //True
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomersSimpleSearch(sc,eCustomersColumns.Country,value?.ToString());

    
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc){
        return CustomersSimpleSearch(sc,eCustomersColumns.Country,null);

    }


        //True
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_Phone(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomersSimpleSearch(sc,eCustomersColumns.Phone,value?.ToString());

    
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_Phone(GeneratorFromDB.SearchCriteria sc){
        return CustomersSimpleSearch(sc,eCustomersColumns.Phone,null);

    }


        //True
    public  IAsyncEnumerable<Customers> CustomersSimpleSearch_Fax(GeneratorFromDB.SearchCriteria sc,  string value){
         return CustomersSimpleSearch(sc,eCustomersColumns.Fax,value?.ToString());

    
    }
    public  IAsyncEnumerable<Customers> CustomersSimpleSearchNull_Fax(GeneratorFromDB.SearchCriteria sc){
        return CustomersSimpleSearch(sc,eCustomersColumns.Fax,null);

    }


        } //class searchdata




    
   public interface ISearchDataEmployees {
        IAsyncEnumerable<Employees> EmployeesFind_AsyncEnumerable(SearchEmployees? search);
        //oneKey    
    public Task<Employees?> EmployeesGetSingle(int id);
    
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_EmployeeID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_EmployeeID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_LastName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_LastName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_FirstName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_FirstName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Title(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Title(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_TitleOfCourtesy(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_TitleOfCourtesy(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_BirthDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_BirthDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_HireDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_HireDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Address(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Address(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Region(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Region(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_PostalCode(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_PostalCode(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_HomePhone(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_HomePhone(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Extension(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Extension(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Photo(GeneratorFromDB.SearchCriteria sc,  byte[] value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Photo(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Notes(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Notes(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_ReportsTo(GeneratorFromDB.SearchCriteria sc,  int? value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_ReportsTo(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_PhotoPath(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_PhotoPath(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataEmployees: ISearchDataEmployees{
        private NorthwindDBContext context;
        public SearchDataEmployees (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Employees> EmployeesFind_AsyncEnumerable(SearchEmployees? search){
            return context.EmployeesFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<Employees?> EmployeesGetSingle(int id){
            return context.EmployeesGetSingle(id);
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch(GeneratorFromDB.SearchCriteria sc, eEmployeesColumns colToSearch, string? value){

            var search = new SearchEmployees();
            var orderBy = new GeneratorFromDB.OrderBy<eEmployeesColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eEmployeesColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.EmployeesFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_EmployeeID(GeneratorFromDB.SearchCriteria sc,  int value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.EmployeeID,value.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_EmployeeID(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.EmployeeID,null);

    }


        //False
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_LastName(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.LastName,value.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_LastName(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.LastName,null);

    }


        //False
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_FirstName(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.FirstName,value.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_FirstName(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.FirstName,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Title(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.Title,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Title(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.Title,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_TitleOfCourtesy(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.TitleOfCourtesy,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_TitleOfCourtesy(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.TitleOfCourtesy,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_BirthDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.BirthDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_BirthDate(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.BirthDate,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_HireDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.HireDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_HireDate(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.HireDate,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Address(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.Address,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Address(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.Address,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.City,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.City,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Region(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.Region,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Region(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.Region,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_PostalCode(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.PostalCode,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_PostalCode(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.PostalCode,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.Country,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.Country,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_HomePhone(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.HomePhone,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_HomePhone(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.HomePhone,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Extension(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.Extension,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Extension(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.Extension,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Photo(GeneratorFromDB.SearchCriteria sc,  byte[] value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.Photo,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Photo(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.Photo,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_Notes(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.Notes,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_Notes(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.Notes,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_ReportsTo(GeneratorFromDB.SearchCriteria sc,  int? value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.ReportsTo,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_ReportsTo(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.ReportsTo,null);

    }


        //True
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearch_PhotoPath(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeesSimpleSearch(sc,eEmployeesColumns.PhotoPath,value?.ToString());

    
    }
    public  IAsyncEnumerable<Employees> EmployeesSimpleSearchNull_PhotoPath(GeneratorFromDB.SearchCriteria sc){
        return EmployeesSimpleSearch(sc,eEmployeesColumns.PhotoPath,null);

    }


        } //class searchdata




    
   public interface ISearchDataInvoices {
        IAsyncEnumerable<Invoices> InvoicesFind_AsyncEnumerable(SearchInvoices? search);
        
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipAddress(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipAddress(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipCity(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipCity(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipRegion(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipRegion(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipPostalCode(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipPostalCode(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipCountry(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipCountry(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_CustomerID(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_CustomerID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_CustomerName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_CustomerName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Address(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Address(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Region(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Region(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_PostalCode(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_PostalCode(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Salesperson(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Salesperson(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_OrderDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_OrderDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_RequiredDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_RequiredDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipperName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipperName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Quantity(GeneratorFromDB.SearchCriteria sc,  short value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Quantity(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Discount(GeneratorFromDB.SearchCriteria sc,  float value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Discount(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ExtendedPrice(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ExtendedPrice(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Freight(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Freight(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataInvoices: ISearchDataInvoices{
        private NorthwindDBContext context;
        public SearchDataInvoices (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Invoices> InvoicesFind_AsyncEnumerable(SearchInvoices? search){
            return context.InvoicesFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch(GeneratorFromDB.SearchCriteria sc, eInvoicesColumns colToSearch, string? value){

            var search = new SearchInvoices();
            var orderBy = new GeneratorFromDB.OrderBy<eInvoicesColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eInvoicesColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.InvoicesFind_AsyncEnumerable(search);
            return data;
        }

    
        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipName(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipName,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipName(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipName,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipAddress(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipAddress,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipAddress(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipAddress,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipCity(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipCity,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipCity(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipCity,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipRegion(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipRegion,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipRegion(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipRegion,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipPostalCode(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipPostalCode,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipPostalCode(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipPostalCode,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipCountry(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipCountry,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipCountry(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipCountry,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_CustomerID(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.CustomerID,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_CustomerID(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.CustomerID,null);

    }


        //False
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_CustomerName(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.CustomerName,value.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_CustomerName(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.CustomerName,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Address(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.Address,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Address(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.Address,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.City,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.City,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Region(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.Region,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Region(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.Region,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_PostalCode(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.PostalCode,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_PostalCode(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.PostalCode,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.Country,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.Country,null);

    }


        //False
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Salesperson(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.Salesperson,value.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Salesperson(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.Salesperson,null);

    }


        //False
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.OrderID,value.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.OrderID,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_OrderDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.OrderDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_OrderDate(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.OrderDate,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_RequiredDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.RequiredDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_RequiredDate(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.RequiredDate,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.ShippedDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.ShippedDate,null);

    }


        //False
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ShipperName(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipperName,value.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ShipperName(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.ShipperName,null);

    }


        //False
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.ProductID,value.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.ProductID,null);

    }


        //False
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.ProductName,value.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.ProductName,null);

    }


        //False
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.UnitPrice,value.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.UnitPrice,null);

    }


        //False
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Quantity(GeneratorFromDB.SearchCriteria sc,  short value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.Quantity,value.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Quantity(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.Quantity,null);

    }


        //False
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Discount(GeneratorFromDB.SearchCriteria sc,  float value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.Discount,value.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Discount(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.Discount,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_ExtendedPrice(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.ExtendedPrice,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_ExtendedPrice(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.ExtendedPrice,null);

    }


        //True
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearch_Freight(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return InvoicesSimpleSearch(sc,eInvoicesColumns.Freight,value?.ToString());

    
    }
    public  IAsyncEnumerable<Invoices> InvoicesSimpleSearchNull_Freight(GeneratorFromDB.SearchCriteria sc){
        return InvoicesSimpleSearch(sc,eInvoicesColumns.Freight,null);

    }


        } //class searchdata




    
   public interface ISearchDataOrder_Details {
        IAsyncEnumerable<Order_Details> Order_DetailsFind_AsyncEnumerable(SearchOrder_Details? search);
        
    
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal value);
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearch_Quantity(GeneratorFromDB.SearchCriteria sc,  short value);
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearchNull_Quantity(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearch_Discount(GeneratorFromDB.SearchCriteria sc,  float value);
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearchNull_Discount(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataOrder_Details: ISearchDataOrder_Details{
        private NorthwindDBContext context;
        public SearchDataOrder_Details (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Order_Details> Order_DetailsFind_AsyncEnumerable(SearchOrder_Details? search){
            return context.Order_DetailsFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearch(GeneratorFromDB.SearchCriteria sc, eOrder_DetailsColumns colToSearch, string? value){

            var search = new SearchOrder_Details();
            var orderBy = new GeneratorFromDB.OrderBy<eOrder_DetailsColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eOrder_DetailsColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Order_DetailsFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Order_DetailsSimpleSearch(sc,eOrder_DetailsColumns.OrderID,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc){
        return Order_DetailsSimpleSearch(sc,eOrder_DetailsColumns.OrderID,null);

    }


        //False
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Order_DetailsSimpleSearch(sc,eOrder_DetailsColumns.ProductID,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc){
        return Order_DetailsSimpleSearch(sc,eOrder_DetailsColumns.ProductID,null);

    }


        //False
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal value){
         return Order_DetailsSimpleSearch(sc,eOrder_DetailsColumns.UnitPrice,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc){
        return Order_DetailsSimpleSearch(sc,eOrder_DetailsColumns.UnitPrice,null);

    }


        //False
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearch_Quantity(GeneratorFromDB.SearchCriteria sc,  short value){
         return Order_DetailsSimpleSearch(sc,eOrder_DetailsColumns.Quantity,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearchNull_Quantity(GeneratorFromDB.SearchCriteria sc){
        return Order_DetailsSimpleSearch(sc,eOrder_DetailsColumns.Quantity,null);

    }


        //False
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearch_Discount(GeneratorFromDB.SearchCriteria sc,  float value){
         return Order_DetailsSimpleSearch(sc,eOrder_DetailsColumns.Discount,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details> Order_DetailsSimpleSearchNull_Discount(GeneratorFromDB.SearchCriteria sc){
        return Order_DetailsSimpleSearch(sc,eOrder_DetailsColumns.Discount,null);

    }


        } //class searchdata




    
   public interface ISearchDataOrder_Details_Extended {
        IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedFind_AsyncEnumerable(SearchOrder_Details_Extended? search);
        
    
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal value);
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_Quantity(GeneratorFromDB.SearchCriteria sc,  short value);
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_Quantity(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_Discount(GeneratorFromDB.SearchCriteria sc,  float value);
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_Discount(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_ExtendedPrice(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_ExtendedPrice(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataOrder_Details_Extended: ISearchDataOrder_Details_Extended{
        private NorthwindDBContext context;
        public SearchDataOrder_Details_Extended (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedFind_AsyncEnumerable(SearchOrder_Details_Extended? search){
            return context.Order_Details_ExtendedFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch(GeneratorFromDB.SearchCriteria sc, eOrder_Details_ExtendedColumns colToSearch, string? value){

            var search = new SearchOrder_Details_Extended();
            var orderBy = new GeneratorFromDB.OrderBy<eOrder_Details_ExtendedColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eOrder_Details_ExtendedColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Order_Details_ExtendedFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.OrderID,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc){
        return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.OrderID,null);

    }


        //False
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.ProductID,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc){
        return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.ProductID,null);

    }


        //False
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.ProductName,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc){
        return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.ProductName,null);

    }


        //False
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal value){
         return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.UnitPrice,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc){
        return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.UnitPrice,null);

    }


        //False
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_Quantity(GeneratorFromDB.SearchCriteria sc,  short value){
         return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.Quantity,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_Quantity(GeneratorFromDB.SearchCriteria sc){
        return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.Quantity,null);

    }


        //False
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_Discount(GeneratorFromDB.SearchCriteria sc,  float value){
         return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.Discount,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_Discount(GeneratorFromDB.SearchCriteria sc){
        return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.Discount,null);

    }


        //True
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearch_ExtendedPrice(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.ExtendedPrice,value?.ToString());

    
    }
    public  IAsyncEnumerable<Order_Details_Extended> Order_Details_ExtendedSimpleSearchNull_ExtendedPrice(GeneratorFromDB.SearchCriteria sc){
        return Order_Details_ExtendedSimpleSearch(sc,eOrder_Details_ExtendedColumns.ExtendedPrice,null);

    }


        } //class searchdata




    
   public interface ISearchDataOrder_Subtotals {
        IAsyncEnumerable<Order_Subtotals> Order_SubtotalsFind_AsyncEnumerable(SearchOrder_Subtotals? search);
        
    
    public  IAsyncEnumerable<Order_Subtotals> Order_SubtotalsSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Order_Subtotals> Order_SubtotalsSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Order_Subtotals> Order_SubtotalsSimpleSearch_Subtotal(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Order_Subtotals> Order_SubtotalsSimpleSearchNull_Subtotal(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataOrder_Subtotals: ISearchDataOrder_Subtotals{
        private NorthwindDBContext context;
        public SearchDataOrder_Subtotals (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Order_Subtotals> Order_SubtotalsFind_AsyncEnumerable(SearchOrder_Subtotals? search){
            return context.Order_SubtotalsFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Order_Subtotals> Order_SubtotalsSimpleSearch(GeneratorFromDB.SearchCriteria sc, eOrder_SubtotalsColumns colToSearch, string? value){

            var search = new SearchOrder_Subtotals();
            var orderBy = new GeneratorFromDB.OrderBy<eOrder_SubtotalsColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eOrder_SubtotalsColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Order_SubtotalsFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Order_Subtotals> Order_SubtotalsSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Order_SubtotalsSimpleSearch(sc,eOrder_SubtotalsColumns.OrderID,value.ToString());

    
    }
    public  IAsyncEnumerable<Order_Subtotals> Order_SubtotalsSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc){
        return Order_SubtotalsSimpleSearch(sc,eOrder_SubtotalsColumns.OrderID,null);

    }


        //True
    public  IAsyncEnumerable<Order_Subtotals> Order_SubtotalsSimpleSearch_Subtotal(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return Order_SubtotalsSimpleSearch(sc,eOrder_SubtotalsColumns.Subtotal,value?.ToString());

    
    }
    public  IAsyncEnumerable<Order_Subtotals> Order_SubtotalsSimpleSearchNull_Subtotal(GeneratorFromDB.SearchCriteria sc){
        return Order_SubtotalsSimpleSearch(sc,eOrder_SubtotalsColumns.Subtotal,null);

    }


        } //class searchdata




    
   public interface ISearchDataOrders {
        IAsyncEnumerable<Orders> OrdersFind_AsyncEnumerable(SearchOrders? search);
        //oneKey    
    public Task<Orders?> OrdersGetSingle(int id);
    
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_CustomerID(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_CustomerID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_EmployeeID(GeneratorFromDB.SearchCriteria sc,  int? value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_EmployeeID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_OrderDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_OrderDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_RequiredDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_RequiredDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipVia(GeneratorFromDB.SearchCriteria sc,  int? value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipVia(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_Freight(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_Freight(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipAddress(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipAddress(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipCity(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipCity(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipRegion(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipRegion(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipPostalCode(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipPostalCode(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipCountry(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipCountry(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataOrders: ISearchDataOrders{
        private NorthwindDBContext context;
        public SearchDataOrders (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Orders> OrdersFind_AsyncEnumerable(SearchOrders? search){
            return context.OrdersFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<Orders?> OrdersGetSingle(int id){
            return context.OrdersGetSingle(id);
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch(GeneratorFromDB.SearchCriteria sc, eOrdersColumns colToSearch, string? value){

            var search = new SearchOrders();
            var orderBy = new GeneratorFromDB.OrderBy<eOrdersColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eOrdersColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.OrdersFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value){
         return OrdersSimpleSearch(sc,eOrdersColumns.OrderID,value.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.OrderID,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_CustomerID(GeneratorFromDB.SearchCriteria sc,  string value){
         return OrdersSimpleSearch(sc,eOrdersColumns.CustomerID,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_CustomerID(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.CustomerID,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_EmployeeID(GeneratorFromDB.SearchCriteria sc,  int? value){
         return OrdersSimpleSearch(sc,eOrdersColumns.EmployeeID,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_EmployeeID(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.EmployeeID,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_OrderDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return OrdersSimpleSearch(sc,eOrdersColumns.OrderDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_OrderDate(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.OrderDate,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_RequiredDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return OrdersSimpleSearch(sc,eOrdersColumns.RequiredDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_RequiredDate(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.RequiredDate,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return OrdersSimpleSearch(sc,eOrdersColumns.ShippedDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.ShippedDate,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipVia(GeneratorFromDB.SearchCriteria sc,  int? value){
         return OrdersSimpleSearch(sc,eOrdersColumns.ShipVia,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipVia(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.ShipVia,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_Freight(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return OrdersSimpleSearch(sc,eOrdersColumns.Freight,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_Freight(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.Freight,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipName(GeneratorFromDB.SearchCriteria sc,  string value){
         return OrdersSimpleSearch(sc,eOrdersColumns.ShipName,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipName(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.ShipName,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipAddress(GeneratorFromDB.SearchCriteria sc,  string value){
         return OrdersSimpleSearch(sc,eOrdersColumns.ShipAddress,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipAddress(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.ShipAddress,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipCity(GeneratorFromDB.SearchCriteria sc,  string value){
         return OrdersSimpleSearch(sc,eOrdersColumns.ShipCity,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipCity(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.ShipCity,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipRegion(GeneratorFromDB.SearchCriteria sc,  string value){
         return OrdersSimpleSearch(sc,eOrdersColumns.ShipRegion,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipRegion(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.ShipRegion,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipPostalCode(GeneratorFromDB.SearchCriteria sc,  string value){
         return OrdersSimpleSearch(sc,eOrdersColumns.ShipPostalCode,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipPostalCode(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.ShipPostalCode,null);

    }


        //True
    public  IAsyncEnumerable<Orders> OrdersSimpleSearch_ShipCountry(GeneratorFromDB.SearchCriteria sc,  string value){
         return OrdersSimpleSearch(sc,eOrdersColumns.ShipCountry,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders> OrdersSimpleSearchNull_ShipCountry(GeneratorFromDB.SearchCriteria sc){
        return OrdersSimpleSearch(sc,eOrdersColumns.ShipCountry,null);

    }


        } //class searchdata




    
   public interface ISearchDataOrders_Qry {
        IAsyncEnumerable<Orders_Qry> Orders_QryFind_AsyncEnumerable(SearchOrders_Qry? search);
        
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_CustomerID(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_CustomerID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_EmployeeID(GeneratorFromDB.SearchCriteria sc,  int? value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_EmployeeID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_OrderDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_OrderDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_RequiredDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_RequiredDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipVia(GeneratorFromDB.SearchCriteria sc,  int? value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipVia(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_Freight(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_Freight(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipAddress(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipAddress(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipCity(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipCity(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipRegion(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipRegion(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipPostalCode(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipPostalCode(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipCountry(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipCountry(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_Address(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_Address(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_Region(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_Region(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_PostalCode(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_PostalCode(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataOrders_Qry: ISearchDataOrders_Qry{
        private NorthwindDBContext context;
        public SearchDataOrders_Qry (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Orders_Qry> Orders_QryFind_AsyncEnumerable(SearchOrders_Qry? search){
            return context.Orders_QryFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch(GeneratorFromDB.SearchCriteria sc, eOrders_QryColumns colToSearch, string? value){

            var search = new SearchOrders_Qry();
            var orderBy = new GeneratorFromDB.OrderBy<eOrders_QryColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eOrders_QryColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Orders_QryFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.OrderID,value.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.OrderID,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_CustomerID(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.CustomerID,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_CustomerID(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.CustomerID,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_EmployeeID(GeneratorFromDB.SearchCriteria sc,  int? value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.EmployeeID,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_EmployeeID(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.EmployeeID,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_OrderDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.OrderDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_OrderDate(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.OrderDate,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_RequiredDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.RequiredDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_RequiredDate(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.RequiredDate,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShippedDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShippedDate,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipVia(GeneratorFromDB.SearchCriteria sc,  int? value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipVia,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipVia(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipVia,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_Freight(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.Freight,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_Freight(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.Freight,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipName,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipName(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipName,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipAddress(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipAddress,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipAddress(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipAddress,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipCity(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipCity,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipCity(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipCity,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipRegion(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipRegion,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipRegion(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipRegion,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipPostalCode(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipPostalCode,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipPostalCode(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipPostalCode,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_ShipCountry(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipCountry,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_ShipCountry(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.ShipCountry,null);

    }


        //False
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.CompanyName,value.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.CompanyName,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_Address(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.Address,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_Address(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.Address,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.City,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.City,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_Region(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.Region,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_Region(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.Region,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_PostalCode(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.PostalCode,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_PostalCode(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.PostalCode,null);

    }


        //True
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value){
         return Orders_QrySimpleSearch(sc,eOrders_QryColumns.Country,value?.ToString());

    
    }
    public  IAsyncEnumerable<Orders_Qry> Orders_QrySimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc){
        return Orders_QrySimpleSearch(sc,eOrders_QryColumns.Country,null);

    }


        } //class searchdata




    
   public interface ISearchDataProduct_Sales_for_1997 {
        IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997Find_AsyncEnumerable(SearchProduct_Sales_for_1997? search);
        
    
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearch_ProductSales(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearchNull_ProductSales(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataProduct_Sales_for_1997: ISearchDataProduct_Sales_for_1997{
        private NorthwindDBContext context;
        public SearchDataProduct_Sales_for_1997 (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997Find_AsyncEnumerable(SearchProduct_Sales_for_1997? search){
            return context.Product_Sales_for_1997Find_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearch(GeneratorFromDB.SearchCriteria sc, eProduct_Sales_for_1997Columns colToSearch, string? value){

            var search = new SearchProduct_Sales_for_1997();
            var orderBy = new GeneratorFromDB.OrderBy<eProduct_Sales_for_1997Columns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eProduct_Sales_for_1997Columns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Product_Sales_for_1997Find_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Product_Sales_for_1997SimpleSearch(sc,eProduct_Sales_for_1997Columns.CategoryName,value.ToString());

    
    }
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc){
        return Product_Sales_for_1997SimpleSearch(sc,eProduct_Sales_for_1997Columns.CategoryName,null);

    }


        //False
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Product_Sales_for_1997SimpleSearch(sc,eProduct_Sales_for_1997Columns.ProductName,value.ToString());

    
    }
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc){
        return Product_Sales_for_1997SimpleSearch(sc,eProduct_Sales_for_1997Columns.ProductName,null);

    }


        //True
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearch_ProductSales(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return Product_Sales_for_1997SimpleSearch(sc,eProduct_Sales_for_1997Columns.ProductSales,value?.ToString());

    
    }
    public  IAsyncEnumerable<Product_Sales_for_1997> Product_Sales_for_1997SimpleSearchNull_ProductSales(GeneratorFromDB.SearchCriteria sc){
        return Product_Sales_for_1997SimpleSearch(sc,eProduct_Sales_for_1997Columns.ProductSales,null);

    }


        } //class searchdata




    
   public interface ISearchDataProducts {
        IAsyncEnumerable<Products> ProductsFind_AsyncEnumerable(SearchProducts? search);
        //oneKey    
    public Task<Products?> ProductsGetSingle(int id);
    
    
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_SupplierID(GeneratorFromDB.SearchCriteria sc,  int? value);
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_SupplierID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_CategoryID(GeneratorFromDB.SearchCriteria sc,  int? value);
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_CategoryID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_UnitsInStock(GeneratorFromDB.SearchCriteria sc,  short? value);
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_UnitsInStock(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_UnitsOnOrder(GeneratorFromDB.SearchCriteria sc,  short? value);
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_UnitsOnOrder(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_ReorderLevel(GeneratorFromDB.SearchCriteria sc,  short? value);
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_ReorderLevel(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_Discontinued(GeneratorFromDB.SearchCriteria sc,  bool value);
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_Discontinued(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataProducts: ISearchDataProducts{
        private NorthwindDBContext context;
        public SearchDataProducts (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Products> ProductsFind_AsyncEnumerable(SearchProducts? search){
            return context.ProductsFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<Products?> ProductsGetSingle(int id){
            return context.ProductsGetSingle(id);
    }
    public  IAsyncEnumerable<Products> ProductsSimpleSearch(GeneratorFromDB.SearchCriteria sc, eProductsColumns colToSearch, string? value){

            var search = new SearchProducts();
            var orderBy = new GeneratorFromDB.OrderBy<eProductsColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eProductsColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.ProductsFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_ProductID(GeneratorFromDB.SearchCriteria sc,  int value){
         return ProductsSimpleSearch(sc,eProductsColumns.ProductID,value.ToString());

    
    }
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_ProductID(GeneratorFromDB.SearchCriteria sc){
        return ProductsSimpleSearch(sc,eProductsColumns.ProductID,null);

    }


        //False
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value){
         return ProductsSimpleSearch(sc,eProductsColumns.ProductName,value.ToString());

    
    }
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc){
        return ProductsSimpleSearch(sc,eProductsColumns.ProductName,null);

    }


        //True
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_SupplierID(GeneratorFromDB.SearchCriteria sc,  int? value){
         return ProductsSimpleSearch(sc,eProductsColumns.SupplierID,value?.ToString());

    
    }
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_SupplierID(GeneratorFromDB.SearchCriteria sc){
        return ProductsSimpleSearch(sc,eProductsColumns.SupplierID,null);

    }


        //True
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_CategoryID(GeneratorFromDB.SearchCriteria sc,  int? value){
         return ProductsSimpleSearch(sc,eProductsColumns.CategoryID,value?.ToString());

    
    }
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_CategoryID(GeneratorFromDB.SearchCriteria sc){
        return ProductsSimpleSearch(sc,eProductsColumns.CategoryID,null);

    }


        //True
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc,  string value){
         return ProductsSimpleSearch(sc,eProductsColumns.QuantityPerUnit,value?.ToString());

    
    }
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc){
        return ProductsSimpleSearch(sc,eProductsColumns.QuantityPerUnit,null);

    }


        //True
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return ProductsSimpleSearch(sc,eProductsColumns.UnitPrice,value?.ToString());

    
    }
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc){
        return ProductsSimpleSearch(sc,eProductsColumns.UnitPrice,null);

    }


        //True
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_UnitsInStock(GeneratorFromDB.SearchCriteria sc,  short? value){
         return ProductsSimpleSearch(sc,eProductsColumns.UnitsInStock,value?.ToString());

    
    }
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_UnitsInStock(GeneratorFromDB.SearchCriteria sc){
        return ProductsSimpleSearch(sc,eProductsColumns.UnitsInStock,null);

    }


        //True
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_UnitsOnOrder(GeneratorFromDB.SearchCriteria sc,  short? value){
         return ProductsSimpleSearch(sc,eProductsColumns.UnitsOnOrder,value?.ToString());

    
    }
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_UnitsOnOrder(GeneratorFromDB.SearchCriteria sc){
        return ProductsSimpleSearch(sc,eProductsColumns.UnitsOnOrder,null);

    }


        //True
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_ReorderLevel(GeneratorFromDB.SearchCriteria sc,  short? value){
         return ProductsSimpleSearch(sc,eProductsColumns.ReorderLevel,value?.ToString());

    
    }
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_ReorderLevel(GeneratorFromDB.SearchCriteria sc){
        return ProductsSimpleSearch(sc,eProductsColumns.ReorderLevel,null);

    }


        //False
    public  IAsyncEnumerable<Products> ProductsSimpleSearch_Discontinued(GeneratorFromDB.SearchCriteria sc,  bool value){
         return ProductsSimpleSearch(sc,eProductsColumns.Discontinued,value.ToString());

    
    }
    public  IAsyncEnumerable<Products> ProductsSimpleSearchNull_Discontinued(GeneratorFromDB.SearchCriteria sc){
        return ProductsSimpleSearch(sc,eProductsColumns.Discontinued,null);

    }


        } //class searchdata




    
   public interface ISearchDataProducts_Above_Average_Price {
        IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceFind_AsyncEnumerable(SearchProducts_Above_Average_Price? search);
        
    
    public  IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataProducts_Above_Average_Price: ISearchDataProducts_Above_Average_Price{
        private NorthwindDBContext context;
        public SearchDataProducts_Above_Average_Price (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceFind_AsyncEnumerable(SearchProducts_Above_Average_Price? search){
            return context.Products_Above_Average_PriceFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceSimpleSearch(GeneratorFromDB.SearchCriteria sc, eProducts_Above_Average_PriceColumns colToSearch, string? value){

            var search = new SearchProducts_Above_Average_Price();
            var orderBy = new GeneratorFromDB.OrderBy<eProducts_Above_Average_PriceColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eProducts_Above_Average_PriceColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Products_Above_Average_PriceFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceSimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Products_Above_Average_PriceSimpleSearch(sc,eProducts_Above_Average_PriceColumns.ProductName,value.ToString());

    
    }
    public  IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceSimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc){
        return Products_Above_Average_PriceSimpleSearch(sc,eProducts_Above_Average_PriceColumns.ProductName,null);

    }


        //True
    public  IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceSimpleSearch_UnitPrice(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return Products_Above_Average_PriceSimpleSearch(sc,eProducts_Above_Average_PriceColumns.UnitPrice,value?.ToString());

    
    }
    public  IAsyncEnumerable<Products_Above_Average_Price> Products_Above_Average_PriceSimpleSearchNull_UnitPrice(GeneratorFromDB.SearchCriteria sc){
        return Products_Above_Average_PriceSimpleSearch(sc,eProducts_Above_Average_PriceColumns.UnitPrice,null);

    }


        } //class searchdata




    
   public interface ISearchDataProducts_by_Category {
        IAsyncEnumerable<Products_by_Category> Products_by_CategoryFind_AsyncEnumerable(SearchProducts_by_Category? search);
        
    
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearch_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearchNull_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearch_UnitsInStock(GeneratorFromDB.SearchCriteria sc,  short? value);
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearchNull_UnitsInStock(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearch_Discontinued(GeneratorFromDB.SearchCriteria sc,  bool value);
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearchNull_Discontinued(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataProducts_by_Category: ISearchDataProducts_by_Category{
        private NorthwindDBContext context;
        public SearchDataProducts_by_Category (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Products_by_Category> Products_by_CategoryFind_AsyncEnumerable(SearchProducts_by_Category? search){
            return context.Products_by_CategoryFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearch(GeneratorFromDB.SearchCriteria sc, eProducts_by_CategoryColumns colToSearch, string? value){

            var search = new SearchProducts_by_Category();
            var orderBy = new GeneratorFromDB.OrderBy<eProducts_by_CategoryColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eProducts_by_CategoryColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Products_by_CategoryFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Products_by_CategorySimpleSearch(sc,eProducts_by_CategoryColumns.CategoryName,value.ToString());

    
    }
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc){
        return Products_by_CategorySimpleSearch(sc,eProducts_by_CategoryColumns.CategoryName,null);

    }


        //False
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Products_by_CategorySimpleSearch(sc,eProducts_by_CategoryColumns.ProductName,value.ToString());

    
    }
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc){
        return Products_by_CategorySimpleSearch(sc,eProducts_by_CategoryColumns.ProductName,null);

    }


        //True
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearch_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc,  string value){
         return Products_by_CategorySimpleSearch(sc,eProducts_by_CategoryColumns.QuantityPerUnit,value?.ToString());

    
    }
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearchNull_QuantityPerUnit(GeneratorFromDB.SearchCriteria sc){
        return Products_by_CategorySimpleSearch(sc,eProducts_by_CategoryColumns.QuantityPerUnit,null);

    }


        //True
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearch_UnitsInStock(GeneratorFromDB.SearchCriteria sc,  short? value){
         return Products_by_CategorySimpleSearch(sc,eProducts_by_CategoryColumns.UnitsInStock,value?.ToString());

    
    }
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearchNull_UnitsInStock(GeneratorFromDB.SearchCriteria sc){
        return Products_by_CategorySimpleSearch(sc,eProducts_by_CategoryColumns.UnitsInStock,null);

    }


        //False
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearch_Discontinued(GeneratorFromDB.SearchCriteria sc,  bool value){
         return Products_by_CategorySimpleSearch(sc,eProducts_by_CategoryColumns.Discontinued,value.ToString());

    
    }
    public  IAsyncEnumerable<Products_by_Category> Products_by_CategorySimpleSearchNull_Discontinued(GeneratorFromDB.SearchCriteria sc){
        return Products_by_CategorySimpleSearch(sc,eProducts_by_CategoryColumns.Discontinued,null);

    }


        } //class searchdata




    
   public interface ISearchDataQuarterly_Orders {
        IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersFind_AsyncEnumerable(SearchQuarterly_Orders? search);
        
    
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearch_CustomerID(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearchNull_CustomerID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataQuarterly_Orders: ISearchDataQuarterly_Orders{
        private NorthwindDBContext context;
        public SearchDataQuarterly_Orders (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersFind_AsyncEnumerable(SearchQuarterly_Orders? search){
            return context.Quarterly_OrdersFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearch(GeneratorFromDB.SearchCriteria sc, eQuarterly_OrdersColumns colToSearch, string? value){

            var search = new SearchQuarterly_Orders();
            var orderBy = new GeneratorFromDB.OrderBy<eQuarterly_OrdersColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eQuarterly_OrdersColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Quarterly_OrdersFind_AsyncEnumerable(search);
            return data;
        }

    
        //True
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearch_CustomerID(GeneratorFromDB.SearchCriteria sc,  string value){
         return Quarterly_OrdersSimpleSearch(sc,eQuarterly_OrdersColumns.CustomerID,value?.ToString());

    
    }
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearchNull_CustomerID(GeneratorFromDB.SearchCriteria sc){
        return Quarterly_OrdersSimpleSearch(sc,eQuarterly_OrdersColumns.CustomerID,null);

    }


        //True
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Quarterly_OrdersSimpleSearch(sc,eQuarterly_OrdersColumns.CompanyName,value?.ToString());

    
    }
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc){
        return Quarterly_OrdersSimpleSearch(sc,eQuarterly_OrdersColumns.CompanyName,null);

    }


        //True
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value){
         return Quarterly_OrdersSimpleSearch(sc,eQuarterly_OrdersColumns.City,value?.ToString());

    
    }
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc){
        return Quarterly_OrdersSimpleSearch(sc,eQuarterly_OrdersColumns.City,null);

    }


        //True
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value){
         return Quarterly_OrdersSimpleSearch(sc,eQuarterly_OrdersColumns.Country,value?.ToString());

    
    }
    public  IAsyncEnumerable<Quarterly_Orders> Quarterly_OrdersSimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc){
        return Quarterly_OrdersSimpleSearch(sc,eQuarterly_OrdersColumns.Country,null);

    }


        } //class searchdata




    
   public interface ISearchDataRegion {
        IAsyncEnumerable<Region> RegionFind_AsyncEnumerable(SearchRegion? search);
        //oneKey    
    public Task<Region?> RegionGetSingle(int id);
    
    
    public  IAsyncEnumerable<Region> RegionSimpleSearch_RegionID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Region> RegionSimpleSearchNull_RegionID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Region> RegionSimpleSearch_RegionDescription(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Region> RegionSimpleSearchNull_RegionDescription(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataRegion: ISearchDataRegion{
        private NorthwindDBContext context;
        public SearchDataRegion (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Region> RegionFind_AsyncEnumerable(SearchRegion? search){
            return context.RegionFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<Region?> RegionGetSingle(int id){
            return context.RegionGetSingle(id);
    }
    public  IAsyncEnumerable<Region> RegionSimpleSearch(GeneratorFromDB.SearchCriteria sc, eRegionColumns colToSearch, string? value){

            var search = new SearchRegion();
            var orderBy = new GeneratorFromDB.OrderBy<eRegionColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eRegionColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.RegionFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Region> RegionSimpleSearch_RegionID(GeneratorFromDB.SearchCriteria sc,  int value){
         return RegionSimpleSearch(sc,eRegionColumns.RegionID,value.ToString());

    
    }
    public  IAsyncEnumerable<Region> RegionSimpleSearchNull_RegionID(GeneratorFromDB.SearchCriteria sc){
        return RegionSimpleSearch(sc,eRegionColumns.RegionID,null);

    }


        //False
    public  IAsyncEnumerable<Region> RegionSimpleSearch_RegionDescription(GeneratorFromDB.SearchCriteria sc,  string value){
         return RegionSimpleSearch(sc,eRegionColumns.RegionDescription,value.ToString());

    
    }
    public  IAsyncEnumerable<Region> RegionSimpleSearchNull_RegionDescription(GeneratorFromDB.SearchCriteria sc){
        return RegionSimpleSearch(sc,eRegionColumns.RegionDescription,null);

    }


        } //class searchdata




    
   public interface ISearchDataSales_Totals_by_Amount {
        IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountFind_AsyncEnumerable(SearchSales_Totals_by_Amount? search);
        
    
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearch_SaleAmount(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearchNull_SaleAmount(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataSales_Totals_by_Amount: ISearchDataSales_Totals_by_Amount{
        private NorthwindDBContext context;
        public SearchDataSales_Totals_by_Amount (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountFind_AsyncEnumerable(SearchSales_Totals_by_Amount? search){
            return context.Sales_Totals_by_AmountFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearch(GeneratorFromDB.SearchCriteria sc, eSales_Totals_by_AmountColumns colToSearch, string? value){

            var search = new SearchSales_Totals_by_Amount();
            var orderBy = new GeneratorFromDB.OrderBy<eSales_Totals_by_AmountColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eSales_Totals_by_AmountColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Sales_Totals_by_AmountFind_AsyncEnumerable(search);
            return data;
        }

    
        //True
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearch_SaleAmount(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return Sales_Totals_by_AmountSimpleSearch(sc,eSales_Totals_by_AmountColumns.SaleAmount,value?.ToString());

    
    }
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearchNull_SaleAmount(GeneratorFromDB.SearchCriteria sc){
        return Sales_Totals_by_AmountSimpleSearch(sc,eSales_Totals_by_AmountColumns.SaleAmount,null);

    }


        //False
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Sales_Totals_by_AmountSimpleSearch(sc,eSales_Totals_by_AmountColumns.OrderID,value.ToString());

    
    }
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc){
        return Sales_Totals_by_AmountSimpleSearch(sc,eSales_Totals_by_AmountColumns.OrderID,null);

    }


        //False
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Sales_Totals_by_AmountSimpleSearch(sc,eSales_Totals_by_AmountColumns.CompanyName,value.ToString());

    
    }
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc){
        return Sales_Totals_by_AmountSimpleSearch(sc,eSales_Totals_by_AmountColumns.CompanyName,null);

    }


        //True
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return Sales_Totals_by_AmountSimpleSearch(sc,eSales_Totals_by_AmountColumns.ShippedDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Sales_Totals_by_Amount> Sales_Totals_by_AmountSimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc){
        return Sales_Totals_by_AmountSimpleSearch(sc,eSales_Totals_by_AmountColumns.ShippedDate,null);

    }


        } //class searchdata




    
   public interface ISearchDataSales_by_Category {
        IAsyncEnumerable<Sales_by_Category> Sales_by_CategoryFind_AsyncEnumerable(SearchSales_by_Category? search);
        
    
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearch_CategoryID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearchNull_CategoryID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearch_ProductSales(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearchNull_ProductSales(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataSales_by_Category: ISearchDataSales_by_Category{
        private NorthwindDBContext context;
        public SearchDataSales_by_Category (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Sales_by_Category> Sales_by_CategoryFind_AsyncEnumerable(SearchSales_by_Category? search){
            return context.Sales_by_CategoryFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearch(GeneratorFromDB.SearchCriteria sc, eSales_by_CategoryColumns colToSearch, string? value){

            var search = new SearchSales_by_Category();
            var orderBy = new GeneratorFromDB.OrderBy<eSales_by_CategoryColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eSales_by_CategoryColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Sales_by_CategoryFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearch_CategoryID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Sales_by_CategorySimpleSearch(sc,eSales_by_CategoryColumns.CategoryID,value.ToString());

    
    }
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearchNull_CategoryID(GeneratorFromDB.SearchCriteria sc){
        return Sales_by_CategorySimpleSearch(sc,eSales_by_CategoryColumns.CategoryID,null);

    }


        //False
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearch_CategoryName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Sales_by_CategorySimpleSearch(sc,eSales_by_CategoryColumns.CategoryName,value.ToString());

    
    }
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearchNull_CategoryName(GeneratorFromDB.SearchCriteria sc){
        return Sales_by_CategorySimpleSearch(sc,eSales_by_CategoryColumns.CategoryName,null);

    }


        //False
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearch_ProductName(GeneratorFromDB.SearchCriteria sc,  string value){
         return Sales_by_CategorySimpleSearch(sc,eSales_by_CategoryColumns.ProductName,value.ToString());

    
    }
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearchNull_ProductName(GeneratorFromDB.SearchCriteria sc){
        return Sales_by_CategorySimpleSearch(sc,eSales_by_CategoryColumns.ProductName,null);

    }


        //True
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearch_ProductSales(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return Sales_by_CategorySimpleSearch(sc,eSales_by_CategoryColumns.ProductSales,value?.ToString());

    
    }
    public  IAsyncEnumerable<Sales_by_Category> Sales_by_CategorySimpleSearchNull_ProductSales(GeneratorFromDB.SearchCriteria sc){
        return Sales_by_CategorySimpleSearch(sc,eSales_by_CategoryColumns.ProductSales,null);

    }


        } //class searchdata




    
   public interface ISearchDataShippers {
        IAsyncEnumerable<Shippers> ShippersFind_AsyncEnumerable(SearchShippers? search);
        //oneKey    
    public Task<Shippers?> ShippersGetSingle(int id);
    
    
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearch_ShipperID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearchNull_ShipperID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearch_Phone(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearchNull_Phone(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataShippers: ISearchDataShippers{
        private NorthwindDBContext context;
        public SearchDataShippers (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Shippers> ShippersFind_AsyncEnumerable(SearchShippers? search){
            return context.ShippersFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<Shippers?> ShippersGetSingle(int id){
            return context.ShippersGetSingle(id);
    }
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearch(GeneratorFromDB.SearchCriteria sc, eShippersColumns colToSearch, string? value){

            var search = new SearchShippers();
            var orderBy = new GeneratorFromDB.OrderBy<eShippersColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eShippersColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.ShippersFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearch_ShipperID(GeneratorFromDB.SearchCriteria sc,  int value){
         return ShippersSimpleSearch(sc,eShippersColumns.ShipperID,value.ToString());

    
    }
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearchNull_ShipperID(GeneratorFromDB.SearchCriteria sc){
        return ShippersSimpleSearch(sc,eShippersColumns.ShipperID,null);

    }


        //False
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value){
         return ShippersSimpleSearch(sc,eShippersColumns.CompanyName,value.ToString());

    
    }
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc){
        return ShippersSimpleSearch(sc,eShippersColumns.CompanyName,null);

    }


        //True
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearch_Phone(GeneratorFromDB.SearchCriteria sc,  string value){
         return ShippersSimpleSearch(sc,eShippersColumns.Phone,value?.ToString());

    
    }
    public  IAsyncEnumerable<Shippers> ShippersSimpleSearchNull_Phone(GeneratorFromDB.SearchCriteria sc){
        return ShippersSimpleSearch(sc,eShippersColumns.Phone,null);

    }


        } //class searchdata




    
   public interface ISearchDataSummary_of_Sales_by_Quarter {
        IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterFind_AsyncEnumerable(SearchSummary_of_Sales_by_Quarter? search);
        
    
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearch_Subtotal(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearchNull_Subtotal(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataSummary_of_Sales_by_Quarter: ISearchDataSummary_of_Sales_by_Quarter{
        private NorthwindDBContext context;
        public SearchDataSummary_of_Sales_by_Quarter (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterFind_AsyncEnumerable(SearchSummary_of_Sales_by_Quarter? search){
            return context.Summary_of_Sales_by_QuarterFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearch(GeneratorFromDB.SearchCriteria sc, eSummary_of_Sales_by_QuarterColumns colToSearch, string? value){

            var search = new SearchSummary_of_Sales_by_Quarter();
            var orderBy = new GeneratorFromDB.OrderBy<eSummary_of_Sales_by_QuarterColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eSummary_of_Sales_by_QuarterColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Summary_of_Sales_by_QuarterFind_AsyncEnumerable(search);
            return data;
        }

    
        //True
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return Summary_of_Sales_by_QuarterSimpleSearch(sc,eSummary_of_Sales_by_QuarterColumns.ShippedDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc){
        return Summary_of_Sales_by_QuarterSimpleSearch(sc,eSummary_of_Sales_by_QuarterColumns.ShippedDate,null);

    }


        //False
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Summary_of_Sales_by_QuarterSimpleSearch(sc,eSummary_of_Sales_by_QuarterColumns.OrderID,value.ToString());

    
    }
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc){
        return Summary_of_Sales_by_QuarterSimpleSearch(sc,eSummary_of_Sales_by_QuarterColumns.OrderID,null);

    }


        //True
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearch_Subtotal(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return Summary_of_Sales_by_QuarterSimpleSearch(sc,eSummary_of_Sales_by_QuarterColumns.Subtotal,value?.ToString());

    
    }
    public  IAsyncEnumerable<Summary_of_Sales_by_Quarter> Summary_of_Sales_by_QuarterSimpleSearchNull_Subtotal(GeneratorFromDB.SearchCriteria sc){
        return Summary_of_Sales_by_QuarterSimpleSearch(sc,eSummary_of_Sales_by_QuarterColumns.Subtotal,null);

    }


        } //class searchdata




    
   public interface ISearchDataSummary_of_Sales_by_Year {
        IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearFind_AsyncEnumerable(SearchSummary_of_Sales_by_Year? search);
        
    
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearch_Subtotal(GeneratorFromDB.SearchCriteria sc,  decimal? value);
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearchNull_Subtotal(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataSummary_of_Sales_by_Year: ISearchDataSummary_of_Sales_by_Year{
        private NorthwindDBContext context;
        public SearchDataSummary_of_Sales_by_Year (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearFind_AsyncEnumerable(SearchSummary_of_Sales_by_Year? search){
            return context.Summary_of_Sales_by_YearFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearch(GeneratorFromDB.SearchCriteria sc, eSummary_of_Sales_by_YearColumns colToSearch, string? value){

            var search = new SearchSummary_of_Sales_by_Year();
            var orderBy = new GeneratorFromDB.OrderBy<eSummary_of_Sales_by_YearColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eSummary_of_Sales_by_YearColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.Summary_of_Sales_by_YearFind_AsyncEnumerable(search);
            return data;
        }

    
        //True
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearch_ShippedDate(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return Summary_of_Sales_by_YearSimpleSearch(sc,eSummary_of_Sales_by_YearColumns.ShippedDate,value?.ToString());

    
    }
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearchNull_ShippedDate(GeneratorFromDB.SearchCriteria sc){
        return Summary_of_Sales_by_YearSimpleSearch(sc,eSummary_of_Sales_by_YearColumns.ShippedDate,null);

    }


        //False
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearch_OrderID(GeneratorFromDB.SearchCriteria sc,  int value){
         return Summary_of_Sales_by_YearSimpleSearch(sc,eSummary_of_Sales_by_YearColumns.OrderID,value.ToString());

    
    }
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearchNull_OrderID(GeneratorFromDB.SearchCriteria sc){
        return Summary_of_Sales_by_YearSimpleSearch(sc,eSummary_of_Sales_by_YearColumns.OrderID,null);

    }


        //True
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearch_Subtotal(GeneratorFromDB.SearchCriteria sc,  decimal? value){
         return Summary_of_Sales_by_YearSimpleSearch(sc,eSummary_of_Sales_by_YearColumns.Subtotal,value?.ToString());

    
    }
    public  IAsyncEnumerable<Summary_of_Sales_by_Year> Summary_of_Sales_by_YearSimpleSearchNull_Subtotal(GeneratorFromDB.SearchCriteria sc){
        return Summary_of_Sales_by_YearSimpleSearch(sc,eSummary_of_Sales_by_YearColumns.Subtotal,null);

    }


        } //class searchdata




    
   public interface ISearchDataSuppliers {
        IAsyncEnumerable<Suppliers> SuppliersFind_AsyncEnumerable(SearchSuppliers? search);
        //oneKey    
    public Task<Suppliers?> SuppliersGetSingle(int id);
    
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_SupplierID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_SupplierID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_ContactName(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_ContactName(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_ContactTitle(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_ContactTitle(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_Address(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_Address(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_Region(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_Region(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_PostalCode(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_PostalCode(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_Phone(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_Phone(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_Fax(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_Fax(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_HomePage(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_HomePage(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataSuppliers: ISearchDataSuppliers{
        private NorthwindDBContext context;
        public SearchDataSuppliers (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Suppliers> SuppliersFind_AsyncEnumerable(SearchSuppliers? search){
            return context.SuppliersFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<Suppliers?> SuppliersGetSingle(int id){
            return context.SuppliersGetSingle(id);
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch(GeneratorFromDB.SearchCriteria sc, eSuppliersColumns colToSearch, string? value){

            var search = new SearchSuppliers();
            var orderBy = new GeneratorFromDB.OrderBy<eSuppliersColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eSuppliersColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.SuppliersFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_SupplierID(GeneratorFromDB.SearchCriteria sc,  int value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.SupplierID,value.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_SupplierID(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.SupplierID,null);

    }


        //False
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_CompanyName(GeneratorFromDB.SearchCriteria sc,  string value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.CompanyName,value.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_CompanyName(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.CompanyName,null);

    }


        //True
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_ContactName(GeneratorFromDB.SearchCriteria sc,  string value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.ContactName,value?.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_ContactName(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.ContactName,null);

    }


        //True
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_ContactTitle(GeneratorFromDB.SearchCriteria sc,  string value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.ContactTitle,value?.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_ContactTitle(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.ContactTitle,null);

    }


        //True
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_Address(GeneratorFromDB.SearchCriteria sc,  string value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.Address,value?.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_Address(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.Address,null);

    }


        //True
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_City(GeneratorFromDB.SearchCriteria sc,  string value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.City,value?.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_City(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.City,null);

    }


        //True
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_Region(GeneratorFromDB.SearchCriteria sc,  string value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.Region,value?.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_Region(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.Region,null);

    }


        //True
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_PostalCode(GeneratorFromDB.SearchCriteria sc,  string value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.PostalCode,value?.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_PostalCode(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.PostalCode,null);

    }


        //True
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_Country(GeneratorFromDB.SearchCriteria sc,  string value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.Country,value?.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_Country(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.Country,null);

    }


        //True
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_Phone(GeneratorFromDB.SearchCriteria sc,  string value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.Phone,value?.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_Phone(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.Phone,null);

    }


        //True
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_Fax(GeneratorFromDB.SearchCriteria sc,  string value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.Fax,value?.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_Fax(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.Fax,null);

    }


        //True
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearch_HomePage(GeneratorFromDB.SearchCriteria sc,  string value){
         return SuppliersSimpleSearch(sc,eSuppliersColumns.HomePage,value?.ToString());

    
    }
    public  IAsyncEnumerable<Suppliers> SuppliersSimpleSearchNull_HomePage(GeneratorFromDB.SearchCriteria sc){
        return SuppliersSimpleSearch(sc,eSuppliersColumns.HomePage,null);

    }


        } //class searchdata




    
   public interface ISearchDataTerritories {
        IAsyncEnumerable<Territories> TerritoriesFind_AsyncEnumerable(SearchTerritories? search);
        //oneKey    
    public Task<Territories?> TerritoriesGetSingle(string id);
    
    
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearch_TerritoryID(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearchNull_TerritoryID(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearch_TerritoryDescription(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearchNull_TerritoryDescription(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearch_RegionID(GeneratorFromDB.SearchCriteria sc,  int value);
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearchNull_RegionID(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataTerritories: ISearchDataTerritories{
        private NorthwindDBContext context;
        public SearchDataTerritories (NorthwindDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Territories> TerritoriesFind_AsyncEnumerable(SearchTerritories? search){
            return context.TerritoriesFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<Territories?> TerritoriesGetSingle(string id){
            return context.TerritoriesGetSingle(id);
    }
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearch(GeneratorFromDB.SearchCriteria sc, eTerritoriesColumns colToSearch, string? value){

            var search = new SearchTerritories();
            var orderBy = new GeneratorFromDB.OrderBy<eTerritoriesColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eTerritoriesColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.TerritoriesFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearch_TerritoryID(GeneratorFromDB.SearchCriteria sc,  string value){
         return TerritoriesSimpleSearch(sc,eTerritoriesColumns.TerritoryID,value.ToString());

    
    }
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearchNull_TerritoryID(GeneratorFromDB.SearchCriteria sc){
        return TerritoriesSimpleSearch(sc,eTerritoriesColumns.TerritoryID,null);

    }


        //False
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearch_TerritoryDescription(GeneratorFromDB.SearchCriteria sc,  string value){
         return TerritoriesSimpleSearch(sc,eTerritoriesColumns.TerritoryDescription,value.ToString());

    
    }
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearchNull_TerritoryDescription(GeneratorFromDB.SearchCriteria sc){
        return TerritoriesSimpleSearch(sc,eTerritoriesColumns.TerritoryDescription,null);

    }


        //False
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearch_RegionID(GeneratorFromDB.SearchCriteria sc,  int value){
         return TerritoriesSimpleSearch(sc,eTerritoriesColumns.RegionID,value.ToString());

    
    }
    public  IAsyncEnumerable<Territories> TerritoriesSimpleSearchNull_RegionID(GeneratorFromDB.SearchCriteria sc){
        return TerritoriesSimpleSearch(sc,eTerritoriesColumns.RegionID,null);

    }


        } //class searchdata




    
   


//end added new




}