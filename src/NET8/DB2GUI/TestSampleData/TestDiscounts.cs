using Generated;

namespace TestSampleData;

public class TestDiscounts
{
    static TestDiscounts()
    {
        CreateDb();
    }
    static void CreateDb()
    {
        DatabaseOperations.Restore(context, "Pubs");

        //var cnt = context;
        //var file = File.ReadAllText("insertPubs.sql").Split("GO", StringSplitOptions.RemoveEmptyEntries);
        //foreach (var item in file)
        //{
        //    try
        //    {
        //        cnt.Database.ExecuteSqlRaw(item);
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
            var ext = Extensions.GetOptions<PubsDBContext>();
            return new PubsDBContext(ext);


        }
    }

    [Theory]
    [InlineData(SearchCriteria.Equal, ediscountsColumns.stor_id, "8042", 1)]
    [InlineData(SearchCriteria.Different, ediscountsColumns.stor_id, "8042", 2)]
    [InlineData(SearchCriteria.Equal, ediscountsColumns.stor_id, null, 2)]
    [InlineData(SearchCriteria.Different, ediscountsColumns.stor_id, null, 1)]
    //[InlineData(SearchCriteria.Equal, ediscountsColumns.discount, "5", 1)]
    [InlineData(SearchCriteria.Equal, ediscountsColumns.lowqty, "100", 1)]
    public async Task SearchAdvanced(SearchCriteria sc, ediscountsColumns col, string val, int nrRecs)
    {
        CreateDb();
        var data = await context.discountsSimpleSearch(sc, col, val).ToArrayAsync();
        Assert.Equal(nrRecs, data.Length);
    }

}