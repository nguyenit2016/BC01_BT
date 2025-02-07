using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//http client
builder.Services.AddHttpClient();
// Khai bao service DI
builder.Services.AddScoped<CountService>();
builder.Services.AddScoped<CryptoServices>();
builder.Services.AddScoped<ProductServices>();
builder.Services.AddScoped<RoomServices>();

builder.Services.AddSignalR();

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

app.UseRouting();

app.MapHub<RoomHubs>("/room-hub");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
