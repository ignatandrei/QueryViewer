var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(it=>it.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter() ));
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<ApplicationDbContext>(
    
        options =>
        {
            var cn = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(cn);
        }
     )
   ;
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
app.UseCors(it => it.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(a => true));
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseDefaultFiles();  
app.UseStaticFiles();
app.MapControllers();
app.MapFallbackToFile("BlocklyAutomation/{**slug}", "BlockyAutomation/index.html");
app.Run();
