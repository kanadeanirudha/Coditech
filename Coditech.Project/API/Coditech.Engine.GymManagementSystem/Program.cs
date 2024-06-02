using Coditech.API.Common;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 104857600;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = 104857600;
    options.MultipartBodyLengthLimit = 104857600;
    options.MultipartHeadersLengthLimit = 104857600;
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 104857600;
});

/// <summary>
/// Registers common services.
/// </summary>
builder.RegisterCommonServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/// <summary>
/// Registers application services with the specified builder.
/// </summary>
app.RegisterApplicationServices(builder);

app.Run();
