using Microsoft.EntityFrameworkCore;
using ParkIn.Components;
using ParkIn.Data;
using Azure.Identity;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure database connection
var connectionStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"))
{
    TrustServerCertificate = true // Add this line to trust the server certificate
};

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionStringBuilder.ConnectionString,
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

builder.Services.AddAuthorization(); // Add authorization services

// Register EmailService
builder.Services.AddSingleton<EmailService>();

// Retrieve the encryption key from configuration
var encryptionKey = builder.Configuration["TokenService:EncryptionKey"];
if (string.IsNullOrEmpty(encryptionKey) || encryptionKey.Length != 32)
{
    throw new InvalidOperationException("Encryption key must be 32 characters long.");
}

// Register TokenService with the encryption key
builder.Services.AddSingleton(new TokenService(encryptionKey));

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
