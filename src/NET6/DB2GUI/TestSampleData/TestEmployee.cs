﻿namespace TestSampleData;

public class TestEmployee
{
    static void CreateDb()
    {
        var context1 = context;
        var file = File.ReadAllText("insertPubs.sql").Split("GO", StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in file)
        {
            try
            {
                context1.Database.ExecuteSqlRaw(item);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(item, ex);

            }
        }

    }
    static ApplicationDbContext context
    {
        get
        {
            return new ApplicationDbContext();

        }
    }
    static TestEmployee()
    {
        //CreateDb();
    }
    [Theory] 
    [InlineData(SearchCriteria.Equal, eemployeeColumns.minit, "M", 3)]
    [InlineData(SearchCriteria.Less, eemployeeColumns.minit, "M", 29)]
    [InlineData(SearchCriteria.Greater, eemployeeColumns.minit, "M", 11)]
    [InlineData(SearchCriteria.Like, eemployeeColumns.emp_id, "%1%", 14)]
    [InlineData(SearchCriteria.Like, eemployeeColumns.emp_id, "K%J%", 2)]
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

    public async Task SearchAdvanced(SearchCriteria sc, eemployeeColumns col, string val, int nrRecs)
    {
        var data = await context.employeeSimpleSearch(sc, col, val).ToArrayAsync();
        Assert.Equal(nrRecs, data.Length);
    }

}