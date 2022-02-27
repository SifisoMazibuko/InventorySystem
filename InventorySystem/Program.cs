using Infrastructure.Data;
using InventorySystem;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add framework services.
builder.Services.AddDbContext<InventoryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventorySystemConnection"),
         sqlServerOptionsAction: sqlOptions =>
         {
             //Adding Connection Resiliency for connection failer
             sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
             maxRetryDelay: TimeSpan.FromSeconds(30),
             errorNumbersToAdd: null);
         });
});

builder.Services.AddDistributedMemoryCache();
// Adding Session service.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(3);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

ConfigurationManager configuration = builder.Configuration;
configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

var username = configuration.GetSection("EmailConfiguration").GetSection("Username").Value;
var pw = configuration.GetSection("EmailConfiguration").GetSection("Password").Value;

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
