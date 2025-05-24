using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBlogApp.Data.Abstractt;
using NewBlogApp.Entitiy;
using NewBlogApp.Models;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace NewBlogApp.Controllers
{
    public class UsersController:Controller
    {

        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated) 
            {
              return RedirectToAction("Index", "Posts");
            }
            return View();
        }

        public IActionResult Register()
        {
         
          return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterView model)
        {
            if (ModelState.IsValid)
            {
                var user= await _userRepository.Users.FirstOrDefaultAsync(x => x.Email == model.Email || x.UserName==model.UserName);
                if (user==null)
                {
                    _userRepository.createUser(new User
                    {
                        UserName = model.UserName,
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Image = "baha.jpg"

                    });

                    return RedirectToAction("Login");
                }

                else
                {
                    ModelState.AddModelError("", "Bu kullanıcı adı veya email zaten kayıtlı");

                }
                
            }

            return View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View("Login");
        }
        [HttpPost]
        public async  Task<IActionResult> Login(LoginView model)
        {
            if (ModelState.IsValid) 
            {

                var isUser=_userRepository.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

                if (isUser != null)
                {


                    var userClaims= new List<Claim>();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? " "));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? ""));

                    if (isUser.Email== "ali@mail.com")
                    {

                        userClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    }
                    
                    var claimsIdentity= new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var autohProperties= new AuthenticationProperties()
                    {
                        IsPersistent = true,
             
                    };

                   

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity), autohProperties);

                }
                else
                {

                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                }

                return RedirectToAction("Index", "Posts");
            }
            return View(model);
        }



        public IActionResult Profile(string? username)
        {
            if(string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var user = _userRepository.Users.Include(x=>x.Comments).ThenInclude(x=>x.Post).Include(x=>x.Posts).FirstOrDefault(c=>c.UserName==username);

            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }

    }
}
