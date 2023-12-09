using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Drawing;
using NetCore2BlocklyNew;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Generated;
using System.Text.Json;
using ExampleWebAPI;
using System.Xml.Linq;

class Program
{
    //to register all contexts
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var assControllers = typeof(UtilsControllers).Assembly;

        builder.Services.AddControllers()
            .AddJsonOptions(c =>
            {
                c.JsonSerializerOptions.PropertyNamingPolicy = new LowerCaseNamingPolicy();
            })
              .PartManager.ApplicationParts.Add(new AssemblyPart(assControllers)); ;
        ;
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        List<Type> typesContext = new();
        //this line register contexts
        foreach (IRegisterContext item in UtilsControllers.registerContexts)
        {
           typesContext.Add( item.AddServices(builder.Services, builder.Configuration));
        }
        builder.Services.AddCors(sa => sa.AddPolicy("default", b =>
                b
                .SetIsOriginAllowed(it => true)
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
        ));
        builder.Services.AddTransient((ctx) =>
        {
            Func<string, DbContext?> a = (string dbName) =>
            {
                var t = typesContext.First(it => it.Name == dbName);
                
                var req = ctx.GetRequiredService(t);
                if (req == null) return null;
                return req as DbContext;
            };
            return a;
        });



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

