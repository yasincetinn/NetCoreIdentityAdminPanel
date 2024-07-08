using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NetCoreIdentityAdminPanel.Models.ContextClasses;
using NetCoreIdentityAdminPanel.Models.Entities;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x=> x.RegisterValidatorsFromAssemblyContaining<Program>()); //FluentValidation'u entegre ettik


builder.Services.AddIdentity<AppUser,AppRole>(x=>
{
    x.Password.RequiredLength = 3; //Þifre uzunluðu 
    x.Password.RequireDigit = false; //Parolanýn en az bir rakam içermesi gerekip gerekmediðini belirler.
    x.Password.RequireUppercase = false; //Parolanýn en az bir büyük harf içermesi gerekip gerekmediðini belirler.
    x.Password.RequireLowercase = false; //Parolanýn en az bir küçük harf içermesi gerekip gerekmediðini belirler.
    x.Password.RequireNonAlphanumeric = false; //Parolanýn en az bir özel karakter (non-alphanumeric character) içermesi gerekip gerekmediðini belirler.
    x.Lockout.MaxFailedAccessAttempts = 5;//Bu ayar, kullanýcýlarýn uygulamaya baþarýsýz giriþ denemeleri yaptýktan sonra hesaplarýnýn kilitlenmesi için bir sýnýr belirler.
    x.User.RequireUniqueEmail = true; //Bu ayar, kullanýcýlarýn e-posta adreslerinin uygulama içinde benzersiz olmasýný zorunlu kýlar.
    //x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //Bu satýr, hesap kilitlenme süresini belirler.  

}).AddEntityFrameworkStores<MyContext>(); 


builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie.HttpOnly = true; //Bu ayar, tarayýcýya gönderilen çerezlerin yalnýzca HTTP istekleri aracýlýðýyla eriþilebilir olup olmayacaðýný belirler. true olarak ayarlandýðýnda, JavaScript gibi istemci tarafý kodlarý tarafýndan çereze eriþilemez.

    x.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; //Bu ayar, çerezlerin güvenli (secure) iletim politikasýný belirler. CookieSecurePolicy.SameAsRequest deðeri, çerezlerin HTTPS üzerinden (güvenli baðlantý) veya HTTP üzerinden (güvensiz baðlantý) gönderilip gönderilmeyeceðini istemci isteðine göre belirler.

    x.Cookie.Name = "Cetin"; //Bu ayar, oluþturulan çerezin adýný belirler. 

    x.ExpireTimeSpan = TimeSpan.FromDays(1); //Bu ayar, çerezin geçerlilik süresini belirler.

    x.Cookie.SameSite = SameSiteMode.Strict;//Bu ayar, çerezin gönderildiði istekin kaynak (origin) ile ayný site içinde olup olmadýðýný belirler. SameSiteMode.Strict deðeri, çerezin sadece ayný site içinde gönderilmesini saðlar.

    x.LoginPath = new PathString("/Home/SignIn"); //Bu ayar, kimlik doðrulamasý gereken bir kullanýcýnýn yönlendirileceði giriþ sayfasýnýn yolunu belirler. Bu durum, kullanýcýnýn kimlik doðrulamasý gerektiren bir iþlemi gerçekleþtirmeye çalýþtýðýnda uygulamanýn yönlendireceði sayfayý ifade eder.

    x.AccessDeniedPath = new PathString("/Home/AccessDenied");//Bu ayar, kullanýcýnýn yetkilendirilmemiþ bir iþlemi gerçekleþtirmeye çalýþtýðýnda yönlendirileceði eriþim reddedildi sayfasýnýn yolunu belirler.
});


builder.Services.AddDbContextPool<MyContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies());


WebApplication app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Register}/{id?}");

app.Run();
