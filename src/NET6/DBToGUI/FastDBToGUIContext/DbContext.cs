namespace Generated;

public partial class ApplicationDbContext : DbContext
{
    public long x()
    {
        return this.Department.LongCount();
    }
}
