// See https://aka.ms/new-console-template for more information
using Generated;
var x = false;
var y = x.ToString();
var z= bool.Parse(y);
Console.WriteLine("Hello, World!");
var context = new ApplicationDbContext();
//await SearchAdvanced1(SearchCriteria.Contains, eauthorsColumns.au_fname, "Ann", 2);
//await SearchAdvanced1(SearchCriteria.Equal, eauthorsColumns.au_lname, "Ringer", 2);
//await SearchAdvanced1(SearchCriteria.Different, eauthorsColumns.state, "Oakland", 18);
//await SearchAdvanced1(SearchCriteria.Equal, eauthorsColumns.contract, "False", 4);
//async Task SearchAdvanced1(SearchCriteria sc, eauthorsColumns col, string val, int nrRecs)
//{

//    var data = context.authorsSimpleSearch(sc, col, val);
//    var q= await data.ToArrayAsync();

//}
await SearchAdvanced2(SearchCriteria.Equal, ediscountsColumns.stor_id, null, 2);
async Task SearchAdvanced2(SearchCriteria sc, ediscountsColumns col, string val, int nrRecs)
{

    var data = context.discountsSimpleSearch(sc, col, val);
    var q= await data.ToArrayAsync();

}
