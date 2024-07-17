using ProgiApp.Domain.Interfaces;
using ProgiApp.Infrastructure.Repositories;
using ProgiApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IAssociationFeeRepository, AssociationFeeRepository>();
builder.Services.AddScoped<IBuyerFeeRepository, BuyerFeeRepository>();
builder.Services.AddScoped<IOtherFeeRepository, OtherFeeRepository>();
builder.Services.AddScoped<ISellerFeeRepository, SellerFeeRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();


builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
options.WithOrigins("http://localhost:8080")
.AllowAnyMethod()
.AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
