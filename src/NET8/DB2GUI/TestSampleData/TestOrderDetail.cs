using Generated;

namespace TestSampleData;

public class TestOrderDetail
{
    static TestOrderDetail()
    {
        CreateDb();
    }
    static void CreateDb()
    {
        DatabaseOperations.Restore(context, "northwind");

        //var cnt = context;
        //var file = File.ReadAllText("instnwnd.sql").Split("GO", StringSplitOptions.RemoveEmptyEntries);
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
    static NorthwindDBContext context
    {
        get
        {
            var ext = extensions.GetOptions<NorthwindDBContext>();
            return new NorthwindDBContext(ext);

            

        }
    }

    [Theory] 
    [InlineData(SearchCriteria.Equal, eOrder_DetailsColumns.Quantity, "8042", 0)]
    [InlineData(SearchCriteria.Equal, eOrder_DetailsColumns.OrderID, "8042", 0)]
    [InlineData(SearchCriteria.Equal, eOrder_DetailsColumns.Discount, "8042", 0)]
    [InlineData(SearchCriteria.Different, eOrder_DetailsColumns.Discount, "8042", 2155)]
    [InlineData(SearchCriteria.Equal, eOrder_DetailsColumns.Discount, "0", 1317)]
    [InlineData(SearchCriteria.Different, eOrder_DetailsColumns.Discount, "0", 838)]
    [InlineData(SearchCriteria.Greater, eOrder_DetailsColumns.Discount, "0", 838)]
    [InlineData(SearchCriteria.Less, eOrder_DetailsColumns.Discount, "0.15", 1840)]
    [InlineData(SearchCriteria.LessOrEqual, eOrder_DetailsColumns.Discount, "0.15", 1840)]
    [InlineData(SearchCriteria.InArray, eOrder_DetailsColumns.Quantity, "10,12", 273)]
    [InlineData(SearchCriteria.NotInArray, eOrder_DetailsColumns.Quantity, "10,12", 1882)]
    [InlineData(SearchCriteria.Between, eOrder_DetailsColumns.Quantity, "10,12", 275)]
    [InlineData(SearchCriteria.NotBetween, eOrder_DetailsColumns.Quantity, "10,12", 1880)]

    public async Task SearchAdvanced(SearchCriteria sc, eOrder_DetailsColumns col, string val, int nrRecs)
    {
        CreateDb();
        var data = await context.Order_DetailsSimpleSearch(sc, col, val).ToArrayAsync();
        Assert.Equal(nrRecs, data.Length);
    }

}