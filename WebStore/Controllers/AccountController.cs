using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.DomainCore.Entities.Identity;
using WebStore.Infastrature.Interfaces;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

         

            var resultSignIn = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (!resultSignIn.Succeeded)
            {
                ModelState.AddModelError("", "Неверный догин или пароль");
                return View(model);
            }

            if (Url.IsLocalUrl(model.ReturnURL))
                return Redirect(model.ReturnURL);
            else
                return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            
            if (!ModelState.IsValid)
                return View(registerViewModel);
            var user = new User()
            {
                UserName = registerViewModel.UserName,
            };

            var resultRegister = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (resultRegister.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.User).ConfigureAwait(false);

                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var e in resultRegister.Errors)
                {
                    ModelState.AddModelError("", e.Description);
                }
                return View(registerViewModel);
            }
        }


        public IActionResult Profile(string userName, [FromServices] IOrderService orderService)
        {

            return View(orderService.GetOrdersByUserName(userName));
        }
        
    }

}
