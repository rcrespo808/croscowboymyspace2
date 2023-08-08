using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using POS.API;
using POS.API.Helpers;
using POS.Domain;
using System;
using System.Linq;
using System.Transactions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<JobService>();

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var connectionString = builder.Configuration.GetConnectionString("DbConnectionString");
// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseStorage(new MySqlStorage(connectionString,
        new MySqlStorageOptions
        {
            TransactionIsolationLevel = IsolationLevel.ReadCommitted,
            QueuePollInterval = TimeSpan.FromSeconds(15),
            JobExpirationCheckInterval = TimeSpan.FromHours(1),
            CountersAggregateInterval = TimeSpan.FromMinutes(5),
            PrepareSchemaIfNecessary = true,
            DashboardJobListLimit = 50000,
            TransactionTimeout = TimeSpan.FromMinutes(1),
            TablesPrefix = "Hangfire"
        })));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
});

var app = builder.Build();

try
{
    using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
    {
        //var context = serviceScope.ServiceProvider.GetRequiredService<POSDbContext>();
        //context.Database.Migrate();
    }
}
catch (System.Exception)
{
    throw;
}


ILoggerFactory loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

startup.Configure(app, app.Environment, loggerFactory);

app.UseHangfireDashboard();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHangfireDashboard();
});

JobService jobService = app.Services.GetRequiredService<JobService>();
jobService.StartScheduler();
app.Run();
