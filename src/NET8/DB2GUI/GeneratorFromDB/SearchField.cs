namespace GeneratorFromDB;

public class SearchField<TColumn>
    where TColumn : System.Enum
{
    public TColumn? FieldName { get; set; }
    public string? Value { get; set; }
    public SearchCriteria Criteria { get; set; }

    //public string? CriteriaString { get; set; }

}