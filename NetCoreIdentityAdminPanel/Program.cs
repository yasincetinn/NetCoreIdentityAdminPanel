using Microsoft.EntityFrameworkCore;
using NetCoreIdentityAdminPanel.Models.ContextClasses;
using NetCoreIdentityAdminPanel.Models.Entities;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddIdentity<AppUser,AppRole>(x=>
{
    x.Password.RequiredLength = 3; //�ifre uzunlu�u 
    x.Password.RequireDigit = false; //Parolan�n en az bir rakam i�ermesi gerekip gerekmedi�ini belirler.
    x.Password.RequireUppercase = false; //Parolan�n en az bir b�y�k harf i�ermesi gerekip gerekmedi�ini belirler.
    x.Password.RequireLowercase = false; //Parolan�n en az bir k���k harf i�ermesi gerekip gerekmedi�ini belirler.
    x.Password.RequireNonAlphanumeric = false; //Parolan�n en az bir �zel karakter (non-alphanumeric character) i�ermesi gerekip gerekmedi�ini belirler.
    x.Lockout.MaxFailedAccessAttempts = 5;//Bu ayar, kullan�c�lar�n uygulamaya ba�ar�s�z giri� denemeleri yapt�ktan sonra hesaplar�n�n kilitlenmesi i�in bir s�n�r belirler.
    x.User.RequireUniqueEmail = true; //Bu ayar, kullan�c�lar�n e-posta adreslerinin uygulama i�inde benzersiz olmas�n� zorunlu k�lar.
    //x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //Bu sat�r, hesap kilitlenme s�resini belirler.  

}).AddEntityFrameworkStores<MyContext>(); 


builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie.HttpOnly = true; //Bu ayar, taray�c�ya g�nderilen �erezlerin yaln�zca HTTP istekleri arac�l���yla eri�ilebilir olup olmayaca��n� belirler. true olarak ayarland���nda, JavaScript gibi istemci taraf� kodlar� taraf�ndan �ereze eri�ilemez.

    x.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; //Bu ayar, �erezlerin g�venli (secure) iletim politikas�n� belirler. CookieSecurePolicy.SameAsRequest de�eri, �erezlerin HTTPS �zerinden (g�venli ba�lant�) veya HTTP �zerinden (g�vensiz ba�lant�) g�nderilip g�nderilmeyece�ini istemci iste�ine g�re belirler.

    x.Cookie.Name = "Cetin"; //Bu ayar, olu�turulan �erezin ad�n� belirler. 

    x.ExpireTimeSpan = TimeSpan.FromDays(1); //Bu ayar, �erezin ge�erlilik s�resini belirler.

    x.Cookie.SameSite = SameSiteMode.Strict;//Bu ayar, �erezin g�nderildi�i istekin kaynak (origin) ile ayn� site i�inde olup olmad���n� belirler. SameSiteMode.Strict de�eri, �erezin sadece ayn� site i�inde g�nderilmesini sa�lar.

    x.LoginPath = new PathString("/Home/SignIn"); //Bu ayar, kimlik do�rulamas� gereken bir kullan�c�n�n y�nlendirilece�i giri� sayfas�n�n yolunu belirler. Bu durum, kullan�c�n�n kimlik do�rulamas� gerektiren bir i�lemi ger�ekle�tirmeye �al��t���nda uygulaman�n y�nlendirece�i sayfay� ifade eder.

    x.AccessDeniedPath = new PathString("/Home/AccessDenied");//Bu ayar, kullan�c�n�n yetkilendirilmemi� bir i�lemi ger�ekle�tirmeye �al��t���nda y�nlendirilece�i eri�im reddedildi sayfas�n�n yolunu belirler.
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
