using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreIdentityAdminPanel.Models;
using NetCoreIdentityAdminPanel.Models.Entities;
using NetCoreIdentityAdminPanel.Models.ViewModels.AppUsers.PureVMs;
using System.Diagnostics;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace NetCoreIdentityAdminPanel.Controllers
{

    [AutoValidateAntiforgeryToken] //Get ile gelen sayfada verilen �zel bir token sayesinde Post'un bu tokensiz yap�lamamas�n� saglar...PostMan gibi third part software'lerinden g�zlemlediginizde direkt Post taraf�na ulasamad�g�n�z� g�receksiniz...

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;
        readonly SignInManager<AppUser> _signInManager;


        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            if (ModelState.IsValid) 
            {
                AppUser appUser = new()
                {
                    UserName = model.UserName,
                    Email = model.Email
                };
                
                IdentityResult result = await _userManager.CreateAsync(appUser,model.Password);

                if (result.Succeeded)
                {
                    #region Admin

                    //AppRole role = await _roleManager.FindByNameAsync("Admin");

                    //if (role == null) await _roleManager.CreateAsync(new(){Name = "Admin"});

                    //await _userManager.AddToRoleAsync(appUser, "Admin");

                    #endregion

                    #region Member
                    
                    AppRole role = await _roleManager.FindByNameAsync("Member");

                    if (role == null) await _roleManager.CreateAsync(new(){Name = "Member"});

                    await _userManager.AddToRoleAsync(appUser, "Member");

                    #endregion

                    return RedirectToAction("Register");   
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }         
            }

            return View(model);
        }

        public IActionResult SignIn(string returnUrl)
        {
            UserSignInRequestModel usModel = new()
            {
                ReturnUrl = returnUrl
            };
            return View(usModel);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInRequestModel model)
        {
            if (ModelState.IsValid)
            {
               AppUser appUser = await _userManager.FindByNameAsync(model.UserName);

                SignInResult result = await _signInManager.PasswordSignInAsync(appUser, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                   IList<string> roles = await _userManager.GetRolesAsync(appUser);

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("AdminPanel");//Eger gitmek istediginiz sayfa bir baska Area'da ise routeData parametresine Anonymus type olarak arg�man vererek g�nderirsiniz return RedirectToAction("AdminPanel",new {Area = "Admin"})
                    }

                    else if (roles.Contains("Member"))
                    {
                        return RedirectToAction("MemberPanel");
                    }

                    return RedirectToAction("Panel"); 
                }

                else if (result.IsLockedOut)
                {
                    DateTimeOffset? lockOutEndDate = await _userManager.GetLockoutEndDateAsync(appUser);

                    ModelState.AddModelError("", $"Hesab�n�z {(lockOutEndDate.Value.UtcDateTime - DateTime.UtcNow).Minutes} dakika s�reyle ask�ya al�nm��t�r");
                }

                else
                {
                    string message = "";

                    if (appUser != null)
                    {
                        int maxFailedAttempts = _userManager.Options.Lockout.MaxFailedAccessAttempts;
                        message = $"E�er {maxFailedAttempts - await _userManager.GetAccessFailedCountAsync(appUser)} kez daha yanl�� giri� yaparsan�z hesab�n�z ge�ici olarak ask�ya al�nacakt�r.";

                    }
                    else
                    {
                        message = "Kullan�c� bulunamad�";
                    }

                    ModelState.AddModelError("", message);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Roles = "Member")]
        public IActionResult MemberPanel()
        {
            return View();
        }

        public IActionResult Panel()
        {
            return View();  
        }

    }
}
