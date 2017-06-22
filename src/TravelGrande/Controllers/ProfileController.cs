using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//
using TravelGrande.Models;
using TravelGrande.Services;
using TravelGrande.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelGrande.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<MyUser> _userManager;
        private IRepository<ProviderProfile> _providerProfileRepo;
        private IRepository<CustomerProfile> _customerProfileRepo;

        public ProfileController(UserManager<MyUser> userManager, IRepository<ProviderProfile> providerProfileRepo, IRepository<CustomerProfile> customerProfileRepo)
        {
            _userManager = userManager;
            _providerProfileRepo = providerProfileRepo;
            _customerProfileRepo = customerProfileRepo;
        }

        [HttpGet]
        [Authorize(Roles = "Provider, Admin")]
        public async Task<IActionResult> UpdateProviderProfile()
        {
            var loggedUser = await _userManager.FindByNameAsync(User.Identity.Name);
            ProviderProfile loggedProfile = _providerProfileRepo.GetSingle(p => p.UserId == loggedUser.Id);
            
            UpdateProviderProfileVM vm = new UpdateProviderProfileVM();
            if (loggedProfile != null)
            {
                vm.PhoneNumber = loggedProfile.PhoneNumber;
                
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Provider, Admin")]
        public async Task<IActionResult> UpdateProviderProfile(UpdateProviderProfileVM vm)
        {
            if (ModelState.IsValid)
            {
                //get logged user info
                var loggedUser = await _userManager.FindByNameAsync(User.Identity.Name);

                //check if the profile already exist
                ProviderProfile loggedProfile = _providerProfileRepo.GetSingle(p => p.UserId == loggedUser.Id);

                //create or update the profile
                if (loggedProfile != null)
                {

                    loggedProfile.PhoneNumber = vm.PhoneNumber;
                    loggedUser.PhoneNumber = vm.PhoneNumber;

                    //save changes to the database
                    _providerProfileRepo.Update(loggedProfile);
                    await _userManager.UpdateAsync(loggedUser);
                    ////update existing Packages For Name Changes
                    //IEnumerable<TravelPackage> list = _travelPackageRepo.Query(l => l.MyUserId == loggedUser.Id).ToList();
                    //foreach (var item in list)
                    //{
                    //    item.ProviderName = vm.CompanyName;
                    //    _travelPackageRepo.Update(item);
                    //}
                }
                else
                {
                    //create new profile
                    loggedProfile = new ProviderProfile
                    {
                        UserId = loggedUser.Id,
                        PhoneNumber = loggedUser.PhoneNumber,
                    };
                    //save the new profile to database
                    _providerProfileRepo.Create(loggedProfile);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }


        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerProfile()
        {
            var loggedUser = await _userManager.FindByNameAsync(User.Identity.Name);
            CustomerProfile loggedProfile = _customerProfileRepo.GetSingle(p => p.UserId == loggedUser.Id);
            UpdateCustomerProfileVM vm = new UpdateCustomerProfileVM();
            vm.Email = loggedUser.Email;
            if (loggedProfile != null)
            {

                vm.FirstName = loggedProfile.FirstName;
                vm.LastName = loggedProfile.LastName;
                vm.Email = loggedProfile.Email;
                vm.Phone = loggedProfile.PhoneNumber;

            }
            
            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> UpdateCustomerProfile(UpdateCustomerProfileVM vm)
        {
            if (ModelState.IsValid)
            {
                //who logged in
                var loggedUser = await _userManager.FindByNameAsync(User.Identity.Name);
                //check if there is a profile already
                CustomerProfile loggedProfile = _customerProfileRepo.GetSingle(p => p.UserId == loggedUser.Id);
                //create or update
                if (loggedProfile != null)
                {
                    //update the loggedProfile
                    loggedProfile.FirstName = vm.FirstName;
                    loggedProfile.LastName = vm.LastName;
                    loggedUser.Email = vm.Email;
                    loggedUser.PhoneNumber = vm.Phone;
                    //save the update to the database
                    _customerProfileRepo.Update(loggedProfile);
                    await _userManager.SetEmailAsync(loggedUser, vm.Email);
                }
                else
                {
                    //create new profile
                    loggedProfile = new CustomerProfile
                    {
                        UserId = loggedUser.Id,
                        FirstName = vm.FirstName,
                        LastName = vm.LastName,
                        Email = vm.Email,
                        PhoneNumber = loggedUser.PhoneNumber
                    };
                    //save the new profile to database
                    _customerProfileRepo.Create(loggedProfile);
                    await _userManager.SetEmailAsync(loggedUser, vm.Email);
                }
                return RedirectToAction("Index", "Home");
            }

            return View(vm);

        }

    }
}

