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
    public class FeedbackController : Controller
    {
        private IRepository<Booking> _bookingRepo;
        private UserManager<MyUser> _userManager;
        private IRepository<TravelPackage> _travelPackageManager;
        private IRepository<Feedback> _feedbackManager;

        public FeedbackController(IRepository<Booking> bookingRepo, UserManager<MyUser> userManager, IRepository<TravelPackage> travelPackageManager, IRepository<Feedback> feedbackManager)
        {
            _feedbackManager = feedbackManager;
            _userManager = userManager;
            _bookingRepo = bookingRepo;
            _travelPackageManager = travelPackageManager;
        }

        // GET: /<controller>/
        [HttpGet]
        [Authorize(Roles = "Customer,Admin")]
        public IActionResult Create(int id)
        {
            Booking booking = _bookingRepo.GetSingle(t => t.BookingId == id);
            //add check for security
            if (booking != null && !booking.LeftFeedback)
            {
                if (booking.UserId == _userManager.GetUserId(User))
                {
                    CreateFeedbackVM vm = new CreateFeedbackVM
                    {
                        TravelPackageId = booking.PackageId,
                        BookingId = booking.BookingId
                    };
                    return View(vm);
                }
            }

            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer,Admin")]
        public IActionResult Create(CreateFeedbackVM vm)
        {
            if (ModelState.IsValid)
            {
                Booking booking = _bookingRepo.GetSingle(b => b.BookingId == vm.BookingId);
                if (booking !=null && !booking.LeftFeedback)
                {
                    var userId = _userManager.GetUserId(User);
                    Feedback newFeedback = new Feedback
                    {
                        UserName = User.Identity.Name,
                        TravelPackageId = vm.TravelPackageId,
                        MyUserId = userId,
                        Comment = vm.Comment
                    };
                    _feedbackManager.Create(newFeedback);
                    booking.LeftFeedback = true;
                    _bookingRepo.Update(booking);

                    return RedirectToAction("Display", "TravelPackage", new { id = newFeedback.TravelPackageId });
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
            }
            return View();
        }
    }
}
