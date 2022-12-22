using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Generated;

public partial class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Department { get; set; }

    public virtual DbSet<Employee> Employee { get; set; }

    public virtual DbSet<test> test { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasOne(d => d.IDDepartmentNavigation).WithMany(p => p.Employee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Department");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

//added new
public partial class ApplicationDBContext : DbContext
{
    //oneKey    
    public Task<Department?> DepartmentGetSingle(long id){
        return this.Department.FirstOrDefaultAsync(e => e.IDDepartment == id);
    }
    

    public IAsyncEnumerable<Department> DepartmentFind_AsyncEnumerable(SearchDepartment? search){
        IQueryable<Department> data= this.Department ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    //oneKey    
    public Task<Employee?> EmployeeGetSingle(long id){
        return this.Employee.FirstOrDefaultAsync(e => e.IDEmployee == id);
    }
    

    public IAsyncEnumerable<Employee> EmployeeFind_AsyncEnumerable(SearchEmployee? search){
        IQueryable<Employee> data= this.Employee ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }
    

    public IAsyncEnumerable<test> testFind_AsyncEnumerable(Searchtest? search){
        IQueryable<test> data= this.test ;
        if(search == null){
            return data.AsAsyncEnumerable();
        }
        data = search.TransformToWhere(data);        
        var ret= search.TransformToOrder(data).Skip((search.PageNumber-1)*search.PageSize).Take(search.PageSize);
        return ret.AsAsyncEnumerable();
    
    }

}
public interface I_InsertDataApplicationDBContext{
        Task<Department_Table?> InsertDepartment(Department_Table value);
        Task<Department_Table[]> InsertDepartments(params Department_Table[] values);

        Task<Employee_Table?> InsertEmployee(Employee_Table value);
        Task<Employee_Table[]> InsertEmployees(params Employee_Table[] values);

        Task<test_Table?> Inserttest(test_Table value);
        Task<test_Table[]> Inserttests(params test_Table[] values);

    }

public class InsertDataApplicationDBContext: I_InsertDataApplicationDBContext{

        private ApplicationDBContext _context;
        public InsertDataApplicationDBContext(ApplicationDBContext context){
            _context=context;
        }
        public async Task<Department_Table?> InsertDepartment(Department_Table value){
            if (value == null)
                return null;

            Department val = (Department)value!;
            _context.Department.Add(val);
            await _context.SaveChangesAsync();
            return (Department_Table)val! ;

        }
        public async Task<Department_Table[]> InsertDepartments(params Department_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Department_Table[0];

        Department[] vals = values.Select(it=>(Department)it!).ToArray();
        _context.Department.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Department_Table)it!  ).ToArray();
    }

        public async Task<Employee_Table?> InsertEmployee(Employee_Table value){
            if (value == null)
                return null;

            Employee val = (Employee)value!;
            _context.Employee.Add(val);
            await _context.SaveChangesAsync();
            return (Employee_Table)val! ;

        }
        public async Task<Employee_Table[]> InsertEmployees(params Employee_Table[] values){
        
        if (values == null || values.Length == 0)
            return new Employee_Table[0];

        Employee[] vals = values.Select(it=>(Employee)it!).ToArray();
        _context.Employee.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (Employee_Table)it!  ).ToArray();
    }

        public async Task<test_Table?> Inserttest(test_Table value){
            if (value == null)
                return null;

            test val = (test)value!;
            _context.test.Add(val);
            await _context.SaveChangesAsync();
            return (test_Table)val! ;

        }
        public async Task<test_Table[]> Inserttests(params test_Table[] values){
        
        if (values == null || values.Length == 0)
            return new test_Table[0];

        test[] vals = values.Select(it=>(test)it!).ToArray();
        _context.test.AddRange(vals);
        await _context.SaveChangesAsync();
        return vals.Select(it => (test_Table)it!  ).ToArray();
    }

    

   public interface ISearchDataDepartment {
        IAsyncEnumerable<Department> DepartmentFind_AsyncEnumerable(SearchDepartment? search);
        //oneKey    
    public Task<Department?> DepartmentGetSingle(long id);
    
    
    public  IAsyncEnumerable<Department> DepartmentSimpleSearch_IDDepartment(GeneratorFromDB.SearchCriteria sc,  long value);
    public  IAsyncEnumerable<Department> DepartmentSimpleSearchNull_IDDepartment(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Department> DepartmentSimpleSearch_Name(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Department> DepartmentSimpleSearchNull_Name(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataDepartment: ISearchDataDepartment{
        private ApplicationDBContext context;
        public SearchDataDepartment (ApplicationDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Department> DepartmentFind_AsyncEnumerable(SearchDepartment? search){
            return context.DepartmentFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<Department?> DepartmentGetSingle(long id){
            return context.DepartmentGetSingle(id);
    }
    public  IAsyncEnumerable<Department> DepartmentSimpleSearch(GeneratorFromDB.SearchCriteria sc, eDepartmentColumns colToSearch, string? value){

            var search = new SearchDepartment();
            var orderBy = new GeneratorFromDB.OrderBy<eDepartmentColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eDepartmentColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.DepartmentFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Department> DepartmentSimpleSearch_IDDepartment(GeneratorFromDB.SearchCriteria sc,  long value){
         return DepartmentSimpleSearch(sc,eDepartmentColumns.IDDepartment,value.ToString());

    
    }
    public  IAsyncEnumerable<Department> DepartmentSimpleSearchNull_IDDepartment(GeneratorFromDB.SearchCriteria sc){
        return DepartmentSimpleSearch(sc,eDepartmentColumns.IDDepartment,null);

    }


        //False
    public  IAsyncEnumerable<Department> DepartmentSimpleSearch_Name(GeneratorFromDB.SearchCriteria sc,  string value){
         return DepartmentSimpleSearch(sc,eDepartmentColumns.Name,value.ToString());

    
    }
    public  IAsyncEnumerable<Department> DepartmentSimpleSearchNull_Name(GeneratorFromDB.SearchCriteria sc){
        return DepartmentSimpleSearch(sc,eDepartmentColumns.Name,null);

    }


        } //class searchdata




    
   public interface ISearchDataEmployee {
        IAsyncEnumerable<Employee> EmployeeFind_AsyncEnumerable(SearchEmployee? search);
        //oneKey    
    public Task<Employee?> EmployeeGetSingle(long id);
    
    
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearch_IDEmployee(GeneratorFromDB.SearchCriteria sc,  long value);
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearchNull_IDEmployee(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearch_Name(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearchNull_Name(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearch_IDDepartment(GeneratorFromDB.SearchCriteria sc,  long value);
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearchNull_IDDepartment(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearch_Salary(GeneratorFromDB.SearchCriteria sc,  long value);
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearchNull_Salary(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDataEmployee: ISearchDataEmployee{
        private ApplicationDBContext context;
        public SearchDataEmployee (ApplicationDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<Employee> EmployeeFind_AsyncEnumerable(SearchEmployee? search){
            return context.EmployeeFind_AsyncEnumerable(search);
        }
        //oneKey    
    public Task<Employee?> EmployeeGetSingle(long id){
            return context.EmployeeGetSingle(id);
    }
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearch(GeneratorFromDB.SearchCriteria sc, eEmployeeColumns colToSearch, string? value){

            var search = new SearchEmployee();
            var orderBy = new GeneratorFromDB.OrderBy<eEmployeeColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<eEmployeeColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.EmployeeFind_AsyncEnumerable(search);
            return data;
        }

    
        //False
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearch_IDEmployee(GeneratorFromDB.SearchCriteria sc,  long value){
         return EmployeeSimpleSearch(sc,eEmployeeColumns.IDEmployee,value.ToString());

    
    }
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearchNull_IDEmployee(GeneratorFromDB.SearchCriteria sc){
        return EmployeeSimpleSearch(sc,eEmployeeColumns.IDEmployee,null);

    }


        //False
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearch_Name(GeneratorFromDB.SearchCriteria sc,  string value){
         return EmployeeSimpleSearch(sc,eEmployeeColumns.Name,value.ToString());

    
    }
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearchNull_Name(GeneratorFromDB.SearchCriteria sc){
        return EmployeeSimpleSearch(sc,eEmployeeColumns.Name,null);

    }


        //False
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearch_IDDepartment(GeneratorFromDB.SearchCriteria sc,  long value){
         return EmployeeSimpleSearch(sc,eEmployeeColumns.IDDepartment,value.ToString());

    
    }
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearchNull_IDDepartment(GeneratorFromDB.SearchCriteria sc){
        return EmployeeSimpleSearch(sc,eEmployeeColumns.IDDepartment,null);

    }


        //False
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearch_Salary(GeneratorFromDB.SearchCriteria sc,  long value){
         return EmployeeSimpleSearch(sc,eEmployeeColumns.Salary,value.ToString());

    
    }
    public  IAsyncEnumerable<Employee> EmployeeSimpleSearchNull_Salary(GeneratorFromDB.SearchCriteria sc){
        return EmployeeSimpleSearch(sc,eEmployeeColumns.Salary,null);

    }


        } //class searchdata




    
   public interface ISearchDatatest {
        IAsyncEnumerable<test> testFind_AsyncEnumerable(Searchtest? search);
        
    
    public  IAsyncEnumerable<test> testSimpleSearch_id(GeneratorFromDB.SearchCriteria sc,  int? value);
    public  IAsyncEnumerable<test> testSimpleSearchNull_id(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<test> testSimpleSearch_name(GeneratorFromDB.SearchCriteria sc,  string value);
    public  IAsyncEnumerable<test> testSimpleSearchNull_name(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<test> testSimpleSearch_ASDASD(GeneratorFromDB.SearchCriteria sc,  int? value);
    public  IAsyncEnumerable<test> testSimpleSearchNull_ASDASD(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<test> testSimpleSearch_dob(GeneratorFromDB.SearchCriteria sc,  DateTime value);
    public  IAsyncEnumerable<test> testSimpleSearchNull_dob(GeneratorFromDB.SearchCriteria sc);
    
    public  IAsyncEnumerable<test> testSimpleSearch_dob2(GeneratorFromDB.SearchCriteria sc,  DateTime? value);
    public  IAsyncEnumerable<test> testSimpleSearchNull_dob2(GeneratorFromDB.SearchCriteria sc);
        } //interface searchdata   

   public class SearchDatatest: ISearchDatatest{
        private ApplicationDBContext context;
        public SearchDatatest (ApplicationDBContext context) {
            this.context=context;
        }
   
        
        public IAsyncEnumerable<test> testFind_AsyncEnumerable(Searchtest? search){
            return context.testFind_AsyncEnumerable(search);
        }
        public  IAsyncEnumerable<test> testSimpleSearch(GeneratorFromDB.SearchCriteria sc, etestColumns colToSearch, string? value){

            var search = new Searchtest();
            var orderBy = new GeneratorFromDB.OrderBy<etestColumns>();
            orderBy.FieldName = colToSearch;
            orderBy.Asc = true;
            search.OrderBys = new[] { orderBy };
            search.PageNumber = 1;
            search.PageSize = int.MaxValue;
            var s = new GeneratorFromDB.SearchField<etestColumns>();
            s.Criteria = sc;
            s.FieldName = colToSearch;
            s.Value = value;
            search.SearchFields = new[] { s };
            var data = this.testFind_AsyncEnumerable(search);
            return data;
        }

    
        //True
    public  IAsyncEnumerable<test> testSimpleSearch_id(GeneratorFromDB.SearchCriteria sc,  int? value){
         return testSimpleSearch(sc,etestColumns.id,value?.ToString());

    
    }
    public  IAsyncEnumerable<test> testSimpleSearchNull_id(GeneratorFromDB.SearchCriteria sc){
        return testSimpleSearch(sc,etestColumns.id,null);

    }


        //True
    public  IAsyncEnumerable<test> testSimpleSearch_name(GeneratorFromDB.SearchCriteria sc,  string value){
         return testSimpleSearch(sc,etestColumns.name,value?.ToString());

    
    }
    public  IAsyncEnumerable<test> testSimpleSearchNull_name(GeneratorFromDB.SearchCriteria sc){
        return testSimpleSearch(sc,etestColumns.name,null);

    }


        //True
    public  IAsyncEnumerable<test> testSimpleSearch_ASDASD(GeneratorFromDB.SearchCriteria sc,  int? value){
         return testSimpleSearch(sc,etestColumns.ASDASD,value?.ToString());

    
    }
    public  IAsyncEnumerable<test> testSimpleSearchNull_ASDASD(GeneratorFromDB.SearchCriteria sc){
        return testSimpleSearch(sc,etestColumns.ASDASD,null);

    }


        //False
    public  IAsyncEnumerable<test> testSimpleSearch_dob(GeneratorFromDB.SearchCriteria sc,  DateTime value){
         return testSimpleSearch(sc,etestColumns.dob,value.ToString());

    
    }
    public  IAsyncEnumerable<test> testSimpleSearchNull_dob(GeneratorFromDB.SearchCriteria sc){
        return testSimpleSearch(sc,etestColumns.dob,null);

    }


        //True
    public  IAsyncEnumerable<test> testSimpleSearch_dob2(GeneratorFromDB.SearchCriteria sc,  DateTime? value){
         return testSimpleSearch(sc,etestColumns.dob2,value?.ToString());

    
    }
    public  IAsyncEnumerable<test> testSimpleSearchNull_dob2(GeneratorFromDB.SearchCriteria sc){
        return testSimpleSearch(sc,etestColumns.dob2,null);

    }


        } //class searchdata




    
   


//end added new




}