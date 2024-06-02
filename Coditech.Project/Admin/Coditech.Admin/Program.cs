using Coditech.Admin;
using Microsoft.AspNetCore.Http.Features;

/// <summary>
/// Creates a WebApplication Builder with the given arguments.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // Set limit to 100MB
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 104857600; // Set limit to 100MB
});
/// <summary>
/// Registers common services.
/// </summary>
builder.RegisterCommonServices();

/// <summary>
/// Builds the application.
/// </summary>
var app = builder.Build();
/// <summary>
/// Registers application services with the specified builder.
/// </summary>
app.RegisterApplicationServices(builder);
/// <summary>
/// Executes the application's startup logic. 
/// </summary>
app.Run();


