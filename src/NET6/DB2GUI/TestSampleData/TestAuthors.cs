
namespace TestSampleData;

public class TestAuthors
{
    ApplicationDbContext context;
    int nrRecs = 23;
    public TestAuthors()
    {
        context = new ApplicationDbContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        var file = File.ReadAllText("insertPubs.sql").Split("GO",StringSplitOptions.RemoveEmptyEntries);
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

    [Fact]
    public async Task GetAll()
    {

        var all = await context.authorsGetAll().ToArrayAsync();
        Assert.Equal(nrRecs,all.Count());
        
        
    }
    [Fact]
    public async Task GetCount()
    {

        var all = await context.authorsCount(null);
        Assert.Equal(nrRecs, all);
    }
    [Fact]
    public async Task SaveNew()
    {

        var n = new authors();
        n.au_id = "174-34-1176";
        n.au_fname = "Andrei";
        n.au_lname = "Ignat";
        var all = context.authors.Add(n);
        await context.SaveChangesAsync();
        
    }


}
