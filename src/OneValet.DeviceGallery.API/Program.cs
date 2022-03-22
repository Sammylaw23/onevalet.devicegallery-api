using Microsoft.AspNetCore.Authentication;
using OneValet.DeviceGallery.API.Extensions;
using OneValet.DeviceGallery.API.Middlewares;
using OneValet.DeviceGallery.Application;
using OneValet.DeviceGallery.Infrastructure;
using Serilog;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

    // Add services to the container.
    IConfiguration configuration = builder.Configuration;
    builder.Services.AddInfrastructure(configuration);
    builder.Services.AddApplicationLayer();
    builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.WithOrigins(configuration["AllowedUrl"]).AllowAnyHeader().AllowAnyMethod()));
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
    builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


    // Configure the HTTP request pipeline.
    var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.Logger.LogInformation("The application started");
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseApiErrorHandler();
    app.Run();
}
catch (Exception ex)
{
    //  Ignore StopTheHostException.
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Error(ex, "Stopped program because of exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}


