namespace TestSampleData;

public class TestEmployee
{
    static void CreateDb()
    {
        DatabaseOperations.Restore(context, "Pubs");

        //var context1 = context;
        //var file = File.ReadAllText("insertPubs.sql").Split("GO", StringSplitOptions.RemoveEmptyEntries);
        //foreach (var item in file)
        //{
        //    try
        //    {
        //        context1.Database.ExecuteSqlRaw(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException(item, ex);

        //    }
        //}

    }
    static PubsDBContext  context
    {
        get
        {
            var ext = extensions.GetOptions<PubsDBContext>();
            return new PubsDBContext(ext);

        }
    }
    static TestEmployee()
    {
        CreateDb();
    }
    [Theory]
    [InlineData(SearchCriteria.Equal, eemployeeColumns.minit, "M", 3)]
    [InlineData(SearchCriteria.Less, eemployeeColumns.minit, "M", 29)]
    [InlineData(SearchCriteria.Greater, eemployeeColumns.minit, "M", 11)]
    [InlineData(SearchCriteria.Contains, eemployeeColumns.emp_id, "1", 14)]
    [InlineData(SearchCriteria.StartsWith, eemployeeColumns.emp_id, "K", 2)]
    [InlineData(SearchCriteria.Equal, eemployeeColumns.job_id, "4", 1)]
    [InlineData(SearchCriteria.Less, eemployeeColumns.job_id, "4", 2)]
    [InlineData(SearchCriteria.Greater, eemployeeColumns.job_id, "4", 40)]
    [InlineData(SearchCriteria.Equal, eemployeeColumns.job_lvl, "87", 1)]
    [InlineData(SearchCriteria.Less, eemployeeColumns.job_lvl, "87", 10)]
    [InlineData(SearchCriteria.Greater, eemployeeColumns.job_lvl, "87", 32)]
    [InlineData(SearchCriteria.Equal, eemployeeColumns.hire_date, "1993-08-19", 1)]
    [InlineData(SearchCriteria.Less, eemployeeColumns.hire_date, "1993-08-19", 37)]
    [InlineData(SearchCriteria.LessOrEqual, eemployeeColumns.hire_date, "1993-08-19", 38)]
    [InlineData(SearchCriteria.Greater, eemployeeColumns.hire_date, "1993-08-19", 5)]
    [InlineData(SearchCriteria.GreaterOrEqual, eemployeeColumns.hire_date, "1993-08-19", 6)]
    [InlineData(SearchCriteria.EqualYear, eemployeeColumns.hire_date, "1993-08-19", 7)]
    [InlineData(SearchCriteria.DifferentYear, eemployeeColumns.hire_date, "1993-08-19", 36)]
    [InlineData(SearchCriteria.GreaterYear, eemployeeColumns.hire_date, "1993-08-19", 3)]
    [InlineData(SearchCriteria.GreaterOrEqualYear, eemployeeColumns.hire_date, "1993-08-19", 10)]
    [InlineData(SearchCriteria.LessYear, eemployeeColumns.hire_date, "1993-08-19", 33)]
    [InlineData(SearchCriteria.LessOrEqualYear, eemployeeColumns.hire_date, "1993-08-19", 40)]
    //[InlineData(SearchCriteria.EqualMonthYear, eemployeeColumns.hire_date, "1993-08-19", 1)]
    //[InlineData(SearchCriteria.DifferentMonthYear, eemployeeColumns.hire_date, "1993-08-19", 42)]
    //[InlineData(SearchCriteria.GreaterMonthYear, eemployeeColumns.hire_date, "1992-08-27", 12)]
    //[InlineData(SearchCriteria.GreaterOrEqualMonthYear, eemployeeColumns.hire_date, "1992-08-27", 13)]
    //[InlineData(SearchCriteria.LessMonthYear, eemployeeColumns.hire_date, "1993-08-19", 37)]
    //[InlineData(SearchCriteria.LessOrEqualMonthYear, eemployeeColumns.hire_date, "1993-08-19", 38)]

    public async Task SearchAdvanced(SearchCriteria sc, eemployeeColumns col, string val, int nrRecs)
    {
        var data = await context.employeeSimpleSearch(sc, col, val).ToArrayAsync();
        Assert.Equal(nrRecs, data.Length);
    }

}