using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelGrande.Models;
using TravelGrande.Services;
using TravelGrande.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelGrande.Controllers
{
    public class BookingController : Controller
    {
        private IRepository<TravelPackage> _packageRepo;
        private IRepository<Booking> _bookingRepo;
        private UserManager<MyUser> _userManager;

        public BookingController(IRepository<TravelPackage> packageRepo, IRepository<Booking> bookingRepo, UserManager<MyUser> userManager)
        {
            _bookingRepo = bookingRepo;
            _packageRepo = packageRepo;
            _userManager = userManager;
        }

        // GET: /<controller>/
        [HttpGet]
       
        [Authorize(Roles = "Customer, Admin")]
        public IActionResult Create(int id)
        {
            TravelPackage tp = _packageRepo.GetSingle(p => p.TravelPackageId == id);

            CreateBookingVM vm = new CreateBookingVM
            {
                TravelPackageName = tp.Name,
                PackageId = id,
                PackagePrice = tp.Price
            };
            return View(vm);
        }

        [HttpPost]
        
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer, Admin")]
        public IActionResult Create(CreateBookingVM vm)
        {
            if (ModelState.IsValid)
            {
                TravelPackage package = _packageRepo.GetSingle(p => p.TravelPackageId == vm.PackageId);

                //get logged user info
                var userId = _userManager.GetUserId(User);

                double totalCost = vm.NumberOfPeople * package.Price;

                Booking booking = new Booking
                {
                    DateBooked = DateTime.Now,
                    StartDate = vm.StartDate,
                    TotalPrice = totalCost,
                    NumberOfPeople = vm.NumberOfPeople,
                    PackageId = package.TravelPackageId,
                    UserId = userId,
                    PackageName = package.Name
                    
                };
                _bookingRepo.Create(booking);
                return RedirectToAction("Index", "TravelPackage", new { id = booking.PackageId });


            }
            return View(vm);
        }

        [HttpGet]
        
        [Authorize(Roles = "Customer, Admin")]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            IEnumerable<Booking> list = _bookingRepo.Query(b => b.UserId == userId && b.DateBooked > DateTime.Today);

            DisplayBookingVM vm = new DisplayBookingVM
            {
                listOfBooking = list

            };
            return View(vm);
        }
    }
}
