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
// required to upload photo
using Microsoft.AspNetCore.Http; //to read the photo data from httpPost(IformFile)
using Microsoft.AspNetCore.Hosting; //give us the service to some info about the hosting enviroment 
using System.IO; // help to create folders in the file system and files

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelGrande.Controllers
{
    
    public class TravelPackageController : Controller
    {
        private IRepository<TravelPackage> _packageRepo;
        private UserManager<MyUser> _userManager;
        private IRepository<ProviderProfile> _providerProfileRepo;
        private IHostingEnvironment _environment;
        private IRepository<Feedback> _feedbackRepo;


        public TravelPackageController(IRepository<TravelPackage> packageRepo, UserManager<MyUser> userManager, IRepository<ProviderProfile> providerProfileRepo, IHostingEnvironment environment, IRepository<Feedback> feedbackRepo)
        {
            _packageRepo = packageRepo;
            _userManager = userManager;
            _providerProfileRepo = providerProfileRepo;
            _environment = environment;
            _feedbackRepo = feedbackRepo;


        }
        //----------------------DISPLAY QUERY----------------------//
        //----------------------GET
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(GetTravelPackagesVM vm)
        {
            var list = _packageRepo.Query(s=>s.Discontinued == false);


            if (vm.LocationQuery != null && vm.LocationQuery != "")
            {              
                list = list.Where(p => p.Location.Contains(vm.LocationQuery));
            }
            if (vm.MinPrice != null)
            {                
                list = list.Where(p => p.Price >= Double.Parse(vm.MinPrice));
            }
            if (vm.MaxPrice != null)
            {                
                list = list.Where(p => p.Price <= Double.Parse(vm.MaxPrice));
            }

            vm.Total = list.Count();
            vm.TravelPackages = list;

            if (vm.Total == 0)
            {
                vm.ResultMessage = "Sorry, no packages were found...";
            }


            return View(vm);
        }

        //----------------------DISPLAY SINGLE----------------------//
        //----------------------GET
        [HttpGet]
        public IActionResult Display(int id)
        {
            //var user = await _userManager.FindByNameAsync(User.Identity.Name); ////errrrrroooooooooooooo
            //get the single category by id
            TravelPackage package = _packageRepo.GetSingle(p => p.TravelPackageId == id);
            IEnumerable<Feedback> feedbacks = _feedbackRepo.Query(f => f.TravelPackageId == id);

            //create vm
            DisplaySingleTravelPackageVM vm = new DisplaySingleTravelPackageVM
            {
                
                Name = package.Name,
                PackageId = package.TravelPackageId,
                Description = package.Description,
                Price = package.Price,
                Discontinued = package.Discontinued,
                Location = package.Location,
                State = package.State,
                ProviderName = package.ProviderName,
                Feedbacks = feedbacks, //FEEDBACK
                PackPhotoPath = package.PhotoPath
                
                
                
                
                
            };
            return View(vm);

        }

        //----------------------CREATE PACKAGE----------------------//
        //----------------------GET
        [HttpGet]
        [Authorize(Roles = "Provider")]
        public IActionResult Create()
        {
            return View();
        }
        //----------------------POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Create(CreateTravelPackageVM vm, IFormFile photoPath)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                IEnumerable<TravelPackage> list = _packageRepo.Query(l => l.UserId == user.Id && !l.Discontinued);
                if (list != null)
                {
                    if (list.Any(n => n.Name == vm.Name))
                    {
                        ModelState.AddModelError("PackageName", "Please Choose a Different Package Name");
                        return View(vm);
                    }
                }

                //map vm to model
                TravelPackage pack = new TravelPackage
                {
                    Name = vm.Name,
                    Price = vm.Price,
                    Location = vm.Location,
                    State = vm.State,
                    Description = vm.Description,
                    UserId = user.Id,
                    ProviderName = user.UserName
                };
                //upload photo
                ////
                if (photoPath != null)
                {
                    //1 -) create directory
                    string uploadPath = Path.Combine(_environment.WebRootPath, "Uploads\\TravelPackages");
                    uploadPath = Path.Combine(uploadPath, User.Identity.Name);
                    Directory.CreateDirectory(Path.Combine(uploadPath, pack.Name));
                    //2 -) get the file name
                    string FileName = Path.GetFileName(photoPath.FileName);

                    //3 -) 
                    using (FileStream fs = new FileStream(Path.Combine(uploadPath, pack.Name, FileName), FileMode.Create))
                    {
                        photoPath.CopyTo(fs);
                    }
                    //4 -) change the pack photoPath
                    pack.PhotoPath = Path.Combine(User.Identity.Name, pack.Name, FileName);

                }
                else
                {
                    pack.PhotoPath = "";
                }

                //save to db
                _packageRepo.Create(pack);
                //go home/index
                return RedirectToAction("Index", "TravelPackage");
            }
            
            return View(vm);
        }

        //----------------------Update PACKAGE----------------------//
        //----------------------GET
        [HttpGet]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Update(int id)
        {
            //get the selected travel package by its ID
            TravelPackage package = _packageRepo.GetSingle(s => s.TravelPackageId == id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user.UserName == package.ProviderName)
            {
                if (package != null)
                {
                    UpdateTravelPackageVM vm = new UpdateTravelPackageVM
                    {
                        Name = package.Name,
                        Description = package.Description,
                        Location = package.Location,
                        State = package.State,
                        Price = package.Price,
                        Discontinued = package.Discontinued
                    };
                    return View(vm);
                }
            }
            return RedirectToAction("Denied", "Account");
        }

        //----------------------POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Provider")]
        public IActionResult Update(int id, UpdateTravelPackageVM vm)
        {
            TravelPackage p = _packageRepo.GetSingle(c => c.TravelPackageId == id);
            if (ModelState.IsValid && p != null)
            {
                p.Name = vm.Name;
                p.Description = vm.Description;
                p.Location = vm.Location;
                p.State = vm.State;
                p.Price = vm.Price;
                p.Discontinued = vm.Discontinued;

                //call data service
                _packageRepo.Update(p);
                //go to details page
                return RedirectToAction("Display", new { id = p.TravelPackageId });
            }
            //if invalid
            return View(vm);
        }

        //----------------------Discontinue PACKAGE----------------------//
        //----------------------GET
        //[HttpGet]
        //[Authorize(Roles = "Provider, Admin")]
        //public async Task<IActionResult> Discontinue(int id)
        //{
        //    TravelPackage tp = _packageRepo.GetSingle(t => t.TravelPackageId == id);
        //    var userName = await _userManager.FindByNameAsync(User.Identity.Name);
        //    //check if It is Their Own Travel Package
        //    if (tp != null && (tp.UserId == userName.Id || User.IsInRole("Admin")))
        //    {
        //        tp.Discontinued = true;

        //    }
        //    _packageRepo.Update(tp);
        //    return RedirectToAction("Index");
        //}

        //-------DISPLAY THE PACKAGES CREATED BY THE LOGGED PROVIDER------//
        //----------------------GET
        [HttpGet]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> DisplayProviderPackages(DisplayAllPackagesPerProviderVM vm)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var list = _packageRepo.Query(s => s.ProviderName == user.UserName);

            vm.ProviderName = user.UserName;
            vm.Packages = list;


            return View(vm);
        }

    }
}
