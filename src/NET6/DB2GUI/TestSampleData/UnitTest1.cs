
namespace TestSampleData;

public class TestAuthors
{
    ApplicationDbContext context;
    int nrRecs = 23;
    public TestAuthors()
    {
        context = new ApplicationDbContext();
        context.Database.EnsureCreated();

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
}
