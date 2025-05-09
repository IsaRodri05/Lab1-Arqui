using Lab1App.Models.Entities;
using Lab1App.Models.Interfaces;
using Lab1App.Models.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ArqPerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AddDbContext"));
});

builder.Services.AddScoped<IPersona, PersonaRepository>();
builder.Services.AddScoped<IEstudio, EstudioRepository>();
builder.Services.AddScoped<IProfesion, ProfesionRepository>();
builder.Services.AddScoped<ITelefono, TelefonoRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
