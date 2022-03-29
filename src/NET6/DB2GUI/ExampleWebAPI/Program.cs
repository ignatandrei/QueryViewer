using AMSWebAPI;
using Generated;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using NetCore2BlocklyNew;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<ApplicationDBContext>(

        options =>
        {
            var cn = builder.Configuration.GetConnectionString("PubsConnection");
            options.UseSqlServer(cn);
        }
     )
   ;
//builder.Services.AddDbContextFactory<PubsContext>(

//        options =>
//        {
//            var cn = builder.Configuration.GetConnectionString("PubsConnection");
//            options.UseSqlServer(cn);
//        }
//     )
//   ;
//builder.Services.AddDbContext<NorthwindContext>(

//        options =>
//        {
//            var cn = builder.Configuration.GetConnectionString("NorthwindConnection");
//            options.UseSqlServer(cn);
//        }
//     )
//   ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseBlocklyUI(app.Environment);
app.UseAuthorization();

app.MapControllers();
app.UseAMS();
app.UseBlocklyAutomation();
app.Run();
