namespace DBToGuiRSCG;

internal class EmbedReader
{
    static Assembly assembly;
    static EmbedReader()
    {
        assembly = Assembly.GetExecutingAssembly();

    }
    public static string ContentFile(string name)
    {
        var resourceName = name;

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        using (StreamReader reader = new StreamReader(stream))
        {
            string result = reader.ReadToEnd();
            return result;
        }
    }
}


