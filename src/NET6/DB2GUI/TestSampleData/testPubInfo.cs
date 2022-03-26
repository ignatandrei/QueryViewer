﻿namespace TestSampleData;

public class testPubInfo
{
    static testPubInfo()
    {
        CreateDb();
    }
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
    static ApplicationDbContext context
    {
        get
        {
            return new ApplicationDbContext();

        }
    }
    //[Theory] 
    //[InlineData(SearchCriteria.Contains, epub_infoColumns.pr_info, "sample text", 3)]
    //public async Task SearchAdvanced(SearchCriteria sc, epub_infoColumns col, string val, int nrRecs)
    //{
    //    var data = await context.pub_infoSimpleSearch(sc, col, val).ToArrayAsync();
    //    Assert.Equal(nrRecs, data.Length);
    //}
}
