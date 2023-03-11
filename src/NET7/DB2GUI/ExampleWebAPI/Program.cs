using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Drawing;
using NetCore2BlocklyNew;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using ExampleControllers;

class Program
{
    //to register all contexts
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var assControllers = typeof(UtilsControllers).Assembly;

        builder.Services.AddControllers()
              .PartManager.ApplicationParts.Add(new AssemblyPart(assControllers)); ;
        ;
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //this line register contexts
        foreach (var item in UtilsControllers.registerContexts)
        {
            item.AddServices(builder.Services, builder.Configuration);
        }
        builder.Services.AddCors(sa => sa.AddPolicy("default", b =>
                b
                .SetIsOriginAllowed(it => true)
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
        ));
        var app = builder.Build();
        app.UseCors("default");
        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(s=>s.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None)) ;
            app.UseBlocklyUI(app.Environment);

        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapControllers();
        app.UseBlocklyAutomation();

        app.Run();

    }
}

