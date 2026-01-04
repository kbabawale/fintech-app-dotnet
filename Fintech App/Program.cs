using Fintech_App.Middlewares;
using Fintech_App.Model.Db;
using Fintech_App.Model.ServiceInterfaces;
using Fintech_App.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
}).ConfigureApiBehaviorOptions(options =>
{
    // Disable the default [ApiController] validation so our filter takes over
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddOpenApi();

//Input Validations
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

//DB Context
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Business Layer Services
builder.Services.AddScoped<IAccount, AccountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

//Error handler
app.UseMiddleware<Errorhandler>();

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();