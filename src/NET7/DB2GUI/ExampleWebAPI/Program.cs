using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Drawing;
using NetCore2BlocklyNew;

class Program
{
    //to register all contexts
    public static List<IRegisterContext> registerContexts = new();
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //this line register contexts
        foreach (var item in registerContexts)
        {
            item.AddServices(builder.Services, builder.Configuration);
        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseBlocklyUI(app.Environment);

        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseBlocklyAutomation();

        app.Run();

    }
}

