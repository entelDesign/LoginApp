using System.Security.Claims;
using LoginApp.Data;
using LoginApp.Data.Abstract;
using LoginApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginApp.Controllers;

public class UserController : Controller
{

    private readonly IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult fail()
    {
        return View();
    }


    public IActionResult Index()
    {
        /* System.Console.WriteLine("Talep geldi.");
         var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
         var role = User.FindFirstValue(ClaimTypes.Role);

         var user = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserId == userId);
         if (user == null)
         {

             return RedirectToAction("fail", "User");
         }*/
        return View();
    }



    public IActionResult admin()
    {
        /* var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
         var role = User.FindFirstValue(ClaimTypes.Role);

         var user = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserId == userId);
         if (user == null)
         {

             return RedirectToAction("fail", "User");
         }
         else if (user.RoleId != 100 && role != "admin")
         {
             return RedirectToAction("fail", "User");
         }*/
        return View();
    }

    public IActionResult Login()
    {
        if (User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName || x.UserName == model.UserName);
            if (user == null)
            {
                _userRepository.CreateUser(new User
                {
                    UserName = model.UserName,
                    AdSoyad = model.AdSoyad,
                    Password = model.Password,
                    RoleId = 0
                });

                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Username ya da Email kullanımda!");
            }

        }
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var isUser = _userRepository.Users.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

            if (isUser != null)
            {
                var userClaims = new List<Claim>();
                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));     // User id bilgisi
                userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));                  // Kullanıcı adı
                userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.AdSoyad ?? ""));                 // Ad ve soyad bilgisi

                if (isUser.RoleId == 100)
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    System.Console.WriteLine("Admin kullanıcısı geldi.");
                    // beklenen eposta adresiyse admin rolü atansın
                }
                else
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, "kullanici"));
                    System.Console.WriteLine("Normal kullanıcı geldi.");

                }

                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);  // Middleware olarak eklenmişti.
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true     // kullanıcı beni hatırlayı seçmişse true yapılacak, varsayılan true yaptık.
                };
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                );

                if (isUser.RoleId == 100) return RedirectToAction("admin", "User");
                return RedirectToAction("Index", "User");

            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            }
        }
        return View(model);
    }


}