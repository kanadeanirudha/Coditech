using Coditech.Admin;
/// <summary>
/// Creates a WebApplication Builder with the given arguments.
/// </summary>
var builder = WebApplication.CreateBuilder(args);
/// <summary>
/// Registers common services.
/// </summary>
builder.RegisterCommonServices();

/// <summary>
/// Builds the application.
/// </summary>
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}");

app.Run();
