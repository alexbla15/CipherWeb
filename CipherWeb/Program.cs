using CipherData.ApiMode;
using CipherData.Interfaces;
using QuestPDF.Infrastructure;
using Radzen;

using System.Globalization;
using static CipherWeb.Shared.Components.Buttons.CipherPDFButton;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("he-IL");

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args); 

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddRadzenComponents();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<ISqlDataAcess, SqlDataAcess>();
builder.Services.AddTransient<ICipherInfo, CipherInfo>(); 
builder.Services.AddScoped<NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseMiddleware<TempFileMiddleware>(); // Add custom middleware for /tempfiles

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
