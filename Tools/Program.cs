
using Academy.Extensions;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMyContexts();
builder.Services.AddMyMapper();
builder.Services.AddMyModels();
//https://learn.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-7.0&viewFallbackFrom=aspnetcore-3.0#add-newtonsoftjson-based-json-format-support
//https://khalidabuhakmeh.com/aspnet-core-6-mvc-upgrade-systemtextjson-serialization-issues
builder.Services.Configure<JsonOptions>(options => { 

    options.SerializerOptions.PropertyNameCaseInsensitive = true;
    options.SerializerOptions.AllowTrailingCommas = true;
    //options.SerializerOptions.Converters;
});


//builder.Services.AddControllers()
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters();


//});
// =============================================================================================================

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller}/{action}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

