namespace GeneratorFromDB;

public class OrderBy<TColumn>
    where TColumn : System.Enum
{
    public TColumn? FieldName { get; set; }
    public bool Asc { get; set; }
}