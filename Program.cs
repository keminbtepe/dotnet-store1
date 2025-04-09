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
    //�ifre zorunluluk ayarlar�; hata verdirmek istersek bunlar� true yapabiliriz
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 2;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; //giri� yapmad�g�nda y�nlendirecegi sayfa
    options.LogoutPath = "/Admin/Logout"; //��k�� yapt�g�nda y�nlendirecegi sayfa
    options.AccessDeniedPath = "/Admin/AccessDenied"; //yetkisi olmayan sayfaya girmeye �al��t�g�nda y�nlendirecegi sayfa
    options.ExpireTimeSpan = TimeSpan.FromDays(30); //�erez s�resi 30 g�n giri� yapmas�na gerek kalmaz
    options.SlidingExpiration = true; //�erez s�resi dolmadan 30 g�n daha ekler hareket alg�larsa s�reyi uzat�r
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

//app.MapStaticAssets();    //Root klas�r�ne eri�memizi saglar ama s�k��t�rarak eri�tirir uygulamaya �al��t�r�p yeni bir dosya ekledigimizde bu i�lemin par�as� olmad�g�ndan dolay� uyar� verir.

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
