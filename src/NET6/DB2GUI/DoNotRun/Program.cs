// See https://aka.ms/new-console-template for more information
using Generated;

Console.WriteLine("Hello, World!");
var context = new ApplicationDbContext();
await SearchAdvanced(SearchCriteria.Contains, eauthorsColumns.au_fname, "Ann", 2);

async Task SearchAdvanced(SearchCriteria sc, eauthorsColumns col, string val, int nrRecs)
{
    
    var data = await context.authorsSimpleSearch(SearchCriteria.Equal, col, val).ToArrayAsync();

}
