
class Program
{
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
        var h = builder.Services.AddHealthChecks();                        
        builder.Services
            .AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("All", "/healthz");
                setup.MaximumHistoryEntriesPerEndpoint(100);
                setup.SetEvaluationTimeInSeconds(60);
            })
            .AddInMemoryStorage();
        var connectionStringsSection = builder.Configuration.GetSection("ConnectionStrings");

        if (connectionStringsSection.Exists())
        {
            var connectionStrings = connectionStringsSection.AsEnumerable();

            foreach (var connectionString in connectionStrings)
            {
                var key = connectionString.Key;
                var value = connectionString.Value;
                if(value!=null)
                    h.AddSqlServer(connectionString:value,name: key);
                
            }
        }


        
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
        //this line register DB contexts
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
        app.UseAMS();
        app.UseBlocklyAutomation();
        app
            .UseRouting()
            .UseEndpoints(config => {
                config.MapHealthChecks("/healthz", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                config.MapHealthChecksUI();
                });

        app.Run();

    }
}

