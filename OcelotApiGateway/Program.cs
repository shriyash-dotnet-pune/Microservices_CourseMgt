using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add configuration for ocelot.json
builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
       .AddEnvironmentVariables();

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Ocelot
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// (optional) normal middleware pipeline additions
app.UseRouting();
app.UseAuthorization();

// Start Ocelot middleware and catch errors with full logging
try
{
    // Important: await UseOcelot() — this wires the gateway middleware
    var ocelotTask = app.UseOcelot();
    ocelotTask.Wait(); // synchronously wait so we get exceptions here in console
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error starting Ocelot");
    Console.WriteLine($"Error starting Ocelot: {ex.Message}");
    // rethrow if you want the host to stop
    throw;
}

app.MapControllers(); // optional if you have local controllers in the gateway

app.Run();
