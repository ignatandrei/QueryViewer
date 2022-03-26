namespace TestSampleData;

public class TestEmployee
{
    void CreateDb()
    {
        var context = this.context;
        var file = File.ReadAllText("insertPubs.sql").Split("GO", StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in file)
        {
            try
            {
                context.Database.ExecuteSqlRaw(item);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(item, ex);

            }
        }

    }
    ApplicationDbContext context
    {
        get
        {
            return new ApplicationDbContext();

        }
    }

    [Theory] 
    [InlineData(SearchCriteria.Equal, eemployeeColumns.minit, "M", 3)]
    [InlineData(SearchCriteria.Less, eemployeeColumns.minit, "M", 29)]
    [InlineData(SearchCriteria.Greater, eemployeeColumns.minit, "M", 11)]
    public async Task SearchAdvanced(SearchCriteria sc, eemployeeColumns col, string val, int nrRecs)
    {
        //CreateDb();
        var data = await context.employeeSimpleSearch(sc, col, val).ToArrayAsync();
        Assert.Equal(nrRecs, data.Length);
    }

}