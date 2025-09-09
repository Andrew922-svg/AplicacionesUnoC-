using Microsoft.EntityFrameworkCore;
using Universidad.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UniversidadDosContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conneccion")));
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login";
    });

builder.Services.AddAuthorization();
var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")

    {
        context.Response.Redirect("/swagger/index.html", permanent: false);
        return;
    }
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.UseAuthorization();
app.UseCors("AllowReactApp");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();