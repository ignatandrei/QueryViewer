﻿// See https://aka.ms/new-console-template for more information
using Generated;
var x = false;
var y = x.ToString();
var z= bool.Parse(y);
Console.WriteLine("Hello, World!");
var context = new ApplicationDbContext();
//await SearchAdvanced(SearchCriteria.Contains, eauthorsColumns.au_fname, "Ann", 2);
//await SearchAdvanced(SearchCriteria.Equal, eauthorsColumns.au_lname, "Ringer", 2);
//await SearchAdvanced(SearchCriteria.Different, eauthorsColumns.state, "Oakland", 18);
await SearchAdvanced(SearchCriteria.Equal, eauthorsColumns.contract, "False", 4);
async Task SearchAdvanced(SearchCriteria sc, eauthorsColumns col, string val, int nrRecs)
{

    var data = context.authorsSimpleSearch(sc, col, val);
    var q= await data.ToArrayAsync();

}
