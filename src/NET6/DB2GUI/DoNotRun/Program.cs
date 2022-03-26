// See https://aka.ms/new-console-template for more information
using Generated;

Console.WriteLine("Hello, World!");
var context = new ApplicationDbContext();
//await SearchAdvanced(SearchCriteria.Contains, eauthorsColumns.au_fname, "Ann", 2);
await SearchAdvanced(SearchCriteria.Equal, eauthorsColumns.au_lname, "Ringer", 2);
async Task SearchAdvanced(SearchCriteria sc, eauthorsColumns col, string val, int nrRecs)
{

    var data = context.authorsSimpleSearch(sc, col, val);
    var q= await data.ToArrayAsync();

}
