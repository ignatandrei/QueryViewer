
namespace TestSampleData;

public class TestAuthors
{
    static void CreateDb()
    {
        var cnt = context;
        var file = File.ReadAllText("insertPubs.sql").Split("GO", StringSplitOptions.RemoveEmptyEntries);
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
    static PubsContext context
    {
        get
        {
            return new PubsContext();

        }
    }
    //int nrRecs = 23;
    const string newAuID = "174-34-1176";
    static TestAuthors()
    {
        CreateDb();
    }

    [Theory]
    [InlineData(SearchCriteria.Equal, eauthorsColumns.au_id, "asd", 0)]
    [InlineData(SearchCriteria.Equal, eauthorsColumns.au_lname, "Ringer", 2)]
    [InlineData(SearchCriteria.Contains, eauthorsColumns.au_fname, "Ann", 2)]
    [InlineData(SearchCriteria.StartsWith, eauthorsColumns.au_fname, "Ann", 2)]
    [InlineData(SearchCriteria.EndsWith, eauthorsColumns.au_fname, "nne", 1)]
    [InlineData(SearchCriteria.EndsWith, eauthorsColumns.au_fname, "e", 3)]
    [InlineData(SearchCriteria.Different, eauthorsColumns.city, "Oakland", 18)]
    [InlineData(SearchCriteria.Different, eauthorsColumns.contract, "False", 19)]
    [InlineData(SearchCriteria.Equal, eauthorsColumns.contract, "False", 4)]
    [InlineData(SearchCriteria.Different, eauthorsColumns.contract, "True", 4)]
    [InlineData(SearchCriteria.Equal, eauthorsColumns.contract, "True", 19)]
    [InlineData(SearchCriteria.Different, eauthorsColumns.address, null, 23)]
    [InlineData(SearchCriteria.Equal, eauthorsColumns.address, null, 0)]
    [InlineData(SearchCriteria.Contains, eauthorsColumns.address, "20", 3)]
    [InlineData(SearchCriteria.StartsWith, eauthorsColumns.address, "3", 6)]
    [InlineData(SearchCriteria.EndsWith, eauthorsColumns.address, "Av.", 5)]
    public async Task SearchAdvanced(SearchCriteria sc, eauthorsColumns col, string val, int nrRecs)
    {
        var data= await context.authorsSimpleSearch(sc, col, val).ToArrayAsync();
        Assert.Equal(nrRecs,data.Length);
    }
    [Fact]
    public async Task SearchData()
    {
        
        var search = new Searchauthors();
        var orderBy = new Generated.OrderBy<eauthorsColumns>();
        orderBy.FieldName = eauthorsColumns.au_id;
        orderBy.Asc = true;
        search.OrderBys = new[] { orderBy };
        search.PageNumber = 1;
        search.PageSize = int.MaxValue;
        var s = new SearchField<eauthorsColumns>();
        s.Criteria = SearchCriteria.Equal;
        s.FieldName = eauthorsColumns.au_id;
        s.Value = "q2e";
        search.SearchFields = new[] { s };

        var data = await context.authorsFind_Array(search);
        Assert.Empty(data);
    }
    
}
