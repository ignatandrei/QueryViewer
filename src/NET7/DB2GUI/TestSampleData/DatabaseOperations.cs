namespace TestSampleData;

 internal class DatabaseOperations
{
    public static void Restore(DbContext cnt, string nameDB)
    {
        string file = Path.Combine(@"C:\Users\Surface1\Documents\GitHub\QueryViewer\src\NET7\DB2GUI", "Backup" + nameDB + ".db");
        if (File.Exists(file))
        {

            File.Copy(file, file.Replace("Backup",""),true);
            return;

        }
        var fileText = File.ReadAllText($"Insert{nameDB}.sql").Split("GO", StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in fileText)
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