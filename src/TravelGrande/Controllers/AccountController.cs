using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//...
using TravelGrande.Models;
using TravelGrande.Services;
using TravelGrande.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelGrande.Controllers
{
    public class AccountController : Controller
    {
        
        private UserManager<MyUser> _userManagerService;
        private SignInManager<MyUser> _signinUserManagerService;
        private IRepository<ProviderProfile> _providerProfileRepo;

        public AccountController(UserManager<MyUser> userManger, SignInManager<MyUser> signInUserManager, IRepository<ProviderProfile> providerProfileRepo)
        {
            _userManagerService = userManger;
            _signinUserManagerService = signInUserManager;
            _providerProfileRepo = providerProfileRepo;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserVM vm)
        {
            if (ModelState.IsValid)
            {
                //map 
                MyUser tempUser = new MyUser
                {
                    UserName = vm.Username,
                    Email = vm.Email
                    
                };

                //add user to database
                var result = await _userManagerService.CreateAsync(tempUser, vm.Password);

                if (result.Succeeded)
                {
                    //add user to role CUSTOMER
                    await _userManagerService.AddToRoleAsync(tempUser, "Customer");

                    //go home
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }

            //error input validation
            return View(vm);
        }


        [HttpGet]
        public IActionResult RegisterProvider()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterProvider(RegisterProviderVM vm)
        {
            if (ModelState.IsValid)
            {
                //map 
                MyUser newProvider = new MyUser
                {
                    UserName = vm.Username,
                    Email = vm.Email,                    
                    PhoneNumber = vm.PhoneNumber 
                };

                //add user to database
                var result = await _userManagerService.CreateAsync(newProvider, vm.Password);

                if (result.Succeeded)
                {
                    //add user to role PROVIDER
                    await _userManagerService.AddToRoleAsync(newProvider, "PROVIDER");
                    //add profile

                    ProviderProfile newProfile = new ProviderProfile
                    {
                        UserId = newProvider.Id,
                        PhoneNumber = newProvider.PhoneNumber,
                        Email = newProvider.Email
                        
                        
                        

                    };
                 
                    //save the new profile to database
                    _providerProfileRepo.Create(newProfile);
                    //go home
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }

            //error input validation
            return View(vm);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            LoginVM vm = new LoginVM
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signinUserManagerService.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);

                if (result.Succeeded)
                {
                    //redirect 
                    if (!string.IsNullOrEmpty(vm.ReturnUrl))
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Username or Password incorrect");
            //invalid input
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signinUserManagerService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Denied()
        {
            return View();
        }

    }
}
