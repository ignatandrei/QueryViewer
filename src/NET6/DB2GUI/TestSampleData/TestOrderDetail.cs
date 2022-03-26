namespace TestSampleData;

public class TestOrderDetail
{
    static TestOrderDetail()
    {
        //CreateDb();
    }
    static void CreateDb()
    {
        var cnt = context;
        var file = File.ReadAllText("instnwnd.sql").Split("GO", StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in file)
        {
            try
            {
                cnt.Database.ExecuteSqlRaw(item);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(item, ex);

            }
        }

    }
    static NorthwindContext context
    {
        get
        {
            return new NorthwindContext();

        }
    }

    [Theory] 
    [InlineData(SearchCriteria.Equal, eOrder_DetailsColumns.Quantity, "8042", 0)]
    [InlineData(SearchCriteria.Equal, eOrder_DetailsColumns.UnitPrice, "8042", 0)]
    [InlineData(SearchCriteria.Equal, eOrder_DetailsColumns.Discount, "8042", 0)]
    [InlineData(SearchCriteria.Different, eOrder_DetailsColumns.Discount, "8042", 2155)]
    [InlineData(SearchCriteria.Equal, eOrder_DetailsColumns.Discount, "0", 1317)]
    [InlineData(SearchCriteria.Different, eOrder_DetailsColumns.Discount, "0", 838)]
    [InlineData(SearchCriteria.Greater, eOrder_DetailsColumns.Discount, "0", 838)]
    [InlineData(SearchCriteria.Less, eOrder_DetailsColumns.Discount, "0.15", 1683)]
    [InlineData(SearchCriteria.LessOrEqual, eOrder_DetailsColumns.Discount, "0.15", 1840)]
    [InlineData(SearchCriteria.InArray, eOrder_DetailsColumns.Quantity, "10,12", 273)]
    [InlineData(SearchCriteria.NotInArray, eOrder_DetailsColumns.Quantity, "10,12", 1882)]
    public async Task SearchAdvanced(SearchCriteria sc, eOrder_DetailsColumns col, string val, int nrRecs)
    {        
        var data = await context.Order_DetailsSimpleSearch(sc, col, val).ToArrayAsync();
        Assert.Equal(nrRecs, data.Length);
    }

}