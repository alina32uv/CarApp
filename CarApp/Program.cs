using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarApp.Data;
using Microsoft.AspNetCore.Identity;
using CarApp.Interfaces;
using CarApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using CarApp.Entities;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CarAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarAppContext") ?? throw new InvalidOperationException("Connection string 'CarAppContext' not found.")));
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CarAppContext>();



builder.Services.AddScoped<ICarBrand, BrandRepo>();
builder.Services.AddScoped<ITransmission, TransmissionRepo>();
builder.Services.AddScoped<IStatus, StatusRepo>();
builder.Services.AddScoped<IVehicle, VehicleRepo>();
builder.Services.AddScoped<IDrive, DriveRepo>();
builder.Services.AddScoped<IFuel, FuelRepo>();
builder.Services.AddScoped<ICarView, CarViewRepo>();
builder.Services.AddScoped<IInsurance, InsuranceRepo>();

builder.Services.AddScoped<IRentInfo, RentInfoRepo>();

builder.Services.AddScoped<IBody, CarBodyTypeRepo>();
builder.Services.AddScoped<ICar, CarRepo>();
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());




builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Clear();

    options.ViewLocationFormats.Add("/Pages/Shared/{0}" + RazorViewEngine.ViewExtension);

    options.ViewLocationFormats.Add("/Pages/Fuels/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Pages/Body/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Pages/Brands/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Pages/Drives/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Pages/Status/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Pages/Transmissions/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Pages/Vehicle/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Pages/Car/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
   // options.ViewLocationFormats.Add("/Pages/Car/Views/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Pages/Home/View/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Pages/RentInfo/View/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Pages/Insurances/{1}/{0}" + RazorViewEngine.ViewExtension);

    options.ViewLocationFormats.Add("/Pages/{1}/{0}" + RazorViewEngine.ViewExtension);

    options.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);



});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Manager", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

    }
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();


    // Adăugați ruta pentru controlerul HomeController din directorul Pages/Home
    endpoints.MapControllerRoute(
        name: "home",
        pattern: "Pages/Home/Home/{action=Index}/{id?}",
        defaults: new { controller = "Home" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.Run();
