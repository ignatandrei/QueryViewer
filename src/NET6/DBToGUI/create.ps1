dotnet tool restore
cd .\FastDBToGUI\
dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=TestData;UId=sa;pwd=<YourStrong@Passw0rd>" Microsoft.EntityFrameworkCore.SqlServer
cd ..