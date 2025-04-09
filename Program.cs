using dotnet_store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlite(connectionString);
});

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    //þifre zorunluluk ayarlarý; hata verdirmek istersek bunlarý true yapabiliriz
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 2;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; //giriþ yapmadýgýnda yönlendirecegi sayfa
    options.LogoutPath = "/Admin/Logout"; //çýkýþ yaptýgýnda yönlendirecegi sayfa
    options.AccessDeniedPath = "/Admin/AccessDenied"; //yetkisi olmayan sayfaya girmeye çalýþtýgýnda yönlendirecegi sayfa
    options.ExpireTimeSpan = TimeSpan.FromDays(30); //çerez süresi 30 gün giriþ yapmasýna gerek kalmaz
    options.SlidingExpiration = true; //çerez süresi dolmadan 30 gün daha ekler hareket algýlarsa süreyi uzatýr
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
app.UseRouting();

app.UseAuthorization();

//app.MapStaticAssets();    //Root klasörüne eriþmemizi saglar ama sýkýþtýrarak eriþtirir uygulamaya çalýþtýrýp yeni bir dosya ekledigimizde bu iþlemin parçasý olmadýgýndan dolayý uyarý verir.

app.UseStaticFiles();

app.MapControllerRoute(
    name: "urunler_by_kategori",
    pattern: "urunler/{url?}",
    defaults: new { controller = "Urun", action = "List" }).WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
