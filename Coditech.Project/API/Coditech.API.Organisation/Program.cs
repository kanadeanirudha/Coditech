using Coditech.API.Data;
using Coditech.API.Mapper;
using Coditech.API.Service;
using Coditech.Common.Logger;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddDbContext<GeneralDepartmentMasterDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoditechDatabase"));
});

// Add Dependency 
builder.Services.AddScoped<IOrganisationService, OrganisationService>();
builder.Services.AddScoped<IGeneralDepartmentMasterService, GeneralDepartmentMasterService>();
builder.Services.AddScoped<ICoditechLogging, CoditechLogging>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
