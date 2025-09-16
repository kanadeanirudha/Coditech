using Coditech.Admin;
using Coditech.Admin.Middleware;
var builder = WebApplication.CreateBuilder(args);

builder.RegisterCommonServices();

var app = builder.Build();

// Middleware
app.UseMiddleware<EncryptedQueryMiddleware>();
app.UseMiddleware<RequestMiddleware>();

app.UseRouting();
app.UseAuthorization();


app.RegisterApplicationServices(builder);

app.Run();
