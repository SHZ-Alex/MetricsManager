using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager.Converters;
using MetricsManager.Models;
using MetricsManager.Services.Client;
using MetricsManager.Services.Client.Impl;
using MetricsManager.Services.Repositorys;
using MetricsManager.Services.Repositorys.Impl;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Polly;
using System.Data.SQLite;

namespace MetricsManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Automapper

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new
                MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            #endregion

            #region Configure Options

            builder.Services.Configure<DatabaseOptions>(options =>
            {
                builder.Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
            });

            #endregion

            #region Configure Database

            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(rb =>
                rb.AddSQLite()
                .WithGlobalConnectionString(builder.Configuration["Settings:DatabaseOptions:ConnectionString"].ToString())
                .ScanIn(typeof(Program).Assembly).For.Migrations()
                ).AddLogging(lb => lb.AddFluentMigratorConsole());

            #endregion

            #region Configure Repository

            builder.Services.AddScoped<IAgentMetricsManagerRepository, AgentMetricsRepository>();

            #endregion

            #region Configure logging

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();

            }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            #endregion

            #region Configure HTTP and Polly

            builder.Services.AddHttpClient();

            builder.Services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(retryCount: 3,
                sleepDurationProvider: (attemptCount) => TimeSpan.FromSeconds(attemptCount * 2),
                onRetry: (response, sleepDuration, attemptCount, context) => {

                    var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();
                        logger.LogError(response.Exception != null ? response.Exception :
                            new Exception($"\n{response.Result.StatusCode}: {response.Result.RequestMessage}"),
                            $"(attempt: {attemptCount}) request exception.");
            }
            ));

            #endregion

            builder.Services.AddControllers()
              .AddJsonOptions(options =>
                  options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsManager", Version = "v1" });

                // Поддержка TimeSpan
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00:00")
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseHttpLogging();

            app.MapControllers();

            #region Configure migration

            var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using (IServiceScope serviceScope = serviceScopeFactory.CreateScope())
            {
                var migrationRunner = serviceScope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                migrationRunner.MigrateUp();
            }

            #endregion

            app.Run();
        }
    }
}