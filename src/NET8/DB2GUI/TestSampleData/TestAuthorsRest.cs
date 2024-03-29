
using Generated;

namespace TestSampleData;

public class TestRest
{
    void CreateDb()
    {
        DatabaseOperations.Restore(context, "Pubs");
        //var context = this.context;
        //var file = File.ReadAllText("insertPubs.sql").Split("GO", StringSplitOptions.RemoveEmptyEntries);
        //foreach (var item in file)
        //{
        //    try
        //    {
        //        context.Database.ExecuteSqlRaw(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException(item, ex);

        //    }
        //}

    }
    PubsDBContext  context
    {
        get
        {
            var ext = Extensions.GetOptions<PubsDBContext>();
            return new PubsDBContext(ext);

        }
    }
    int nrRecs = 23;
    const string newAuID = "174-34-1176";
    public TestRest()
    {

    }

    [Fact]
    public async Task GetAll()
    {
        CreateDb();
        var all = await context.authorsGetAll().ToArrayAsync();
        Assert.Equal(nrRecs, all.Count());


    }
    [Fact]
    public async Task GetCount()
    {
        CreateDb();
        var all = await context.authorsCount(null);
        Assert.Equal(nrRecs, all);
    }
    [Fact]
    public async Task SaveNew()
    {
        CreateDb();
        var all = await Save();
        Assert.True(all);
    }
    private async Task<bool> Save()
    {

        var n = new authors();
        n.au_id = newAuID;
        n.au_fname = "Andrei";
        n.au_lname = "Ignat";
        var all = await context.authorsSave(n);
        return all != null;

    }
    [Fact]
    public async Task Delete()
    {
        CreateDb();
        var n = Save();
        var d = await context.authorsDelete(newAuID);
        Assert.True(d);
    }
    [Fact]
    public async Task Modify()
    {
        CreateDb();
        await SaveNew();
        var n = new authors();
        n.au_id = newAuID;
        n.au_fname = "Andrei1";
        n.au_lname = "Ignat1";
        n.phone = "test";
        var d = await context.authorsModify(n);
        Assert.True(d);
    }
    
}
