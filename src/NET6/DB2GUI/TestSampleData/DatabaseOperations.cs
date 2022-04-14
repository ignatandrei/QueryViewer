namespace TestSampleData;

 internal class DatabaseOperations
{
    public static void Restore(DbContext cnt, string nameDB)
    {
        string file = Path.Combine(@"C:\Users\Surface1\Documents\GitHub\QueryViewer\src\NET6\DB2GUI", "Backup" + nameDB + ".db");
        if (File.Exists(file))
        {

            File.Copy(file, file.Replace("Backup",""),true);

        }
        var file = File.ReadAllText($"Insert{nameDB}.sql").Split("GO", StringSplitOptions.RemoveEmptyEntries);
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
}