using Microsoft.EntityFrameworkCore;
using ParkIn.Components;
using ParkIn.Data;
using Azure.Identity;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure Azure AD authentication
var connectionStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"))
{
    Authentication = SqlAuthenticationMethod.ActiveDirectoryDefault
};

// Remove the password from the connection string
connectionStringBuilder.Remove("User ID");
connectionStringBuilder.Remove("Password");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionStringBuilder.ConnectionString,
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

builder.Services.AddAuthorization(); // Add authorization services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
