namespace TestSampleData;

public class TestDiscounts
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
    [InlineData(SearchCriteria.Equal, ediscountsColumns.stor_id, "8042", 1)]
    [InlineData(SearchCriteria.Different, ediscountsColumns.stor_id, "8042", 2)]
    [InlineData(SearchCriteria.Equal, ediscountsColumns.stor_id, null, 2)]
    [InlineData(SearchCriteria.Different, ediscountsColumns.stor_id, null, 1)]
    public async Task SearchAdvanced(SearchCriteria sc, ediscountsColumns col, string val, int nrRecs)
    {
        CreateDb();
        var data = await context.discountsSimpleSearch(sc, col, val).ToArrayAsync();
        Assert.Equal(nrRecs, data.Length);
    }

}