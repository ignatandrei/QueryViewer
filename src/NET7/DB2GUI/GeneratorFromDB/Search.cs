namespace GeneratorFromDB;

public abstract class Search<TColumn, TClass>
    where TColumn : System.Enum
    where TClass : class
{
    public Search()
    {
        PageSize = 10;
        PageNumber = 1;
    }
    public SearchField<TColumn>[]? SearchFields { get; set; }
    public OrderBy<TColumn>[]? OrderBys { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public abstract IQueryable<TClass> TransformToWhere(IQueryable<TClass> data);
    public abstract IOrderedQueryable<TClass> TransformToOrder(IQueryable<TClass> data);
    //public abstract IOrderedQueryable<TClass> TransformToPaging(IOrderedQueryable<TClass>  data);

}