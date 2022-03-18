﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;
using WebApplication11.ViewModels;

namespace WebApplication11.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountController : Controller
    {
  
        private readonly IAccountViewModel _AccountRepository;

        public AccountController( IAccountViewModel AccountRepository)
        {
        _AccountRepository = AccountRepository;
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
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _AccountRepository.AdminRegistrtion(model);

                if (result == 1)
                {
                    return RedirectToAction("Login", "Account");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        // Login Action

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel user)
            
        {
            if (ModelState.IsValid)
            {
                var result = await _AccountRepository.AdminLogin(user);

                if (result == 1)
                {
                    

                    return RedirectToAction("Index", "ProductAdmin");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }
        // Logout Action
        public async Task<IActionResult> Logout()
        {
            await _AccountRepository.AdminLogout();

            return RedirectToAction("Login");
        }





        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new IdentityUser
        //        {
        //            UserName = model.Email,
        //            Email = model.Email,
        //        };

        //        var result = await _userManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded)
        //        {
        //            await _signInManager.SignInAsync(user, isPersistent: false);

        //            return RedirectToAction("Login", "Account");
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }

        //        ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

        //    }
        //    return View(model);
        //}


        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(LoginViewModel user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "ProductAdmin");
        //        }

        //        ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

        //    }
        //    return View(user);
        //}

        // Logout Action

        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();

        //    return RedirectToAction("Login");
        //}



    }
}
