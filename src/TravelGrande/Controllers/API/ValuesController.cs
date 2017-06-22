using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//
using TravelGrande.Models;
using TravelGrande.ViewModels;
using TravelGrande.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelGrande.Controllers.API
{
    
    public class ValuesController : Controller
    {
        private IRepository<TravelPackage> _packagesRepo;
        
        public ValuesController(IRepository<TravelPackage> packageRepo)
        {
            _packagesRepo = packageRepo;
        }

        //this is API method to return all packages
        [HttpGet("api/GetAllPackages")]
        public JsonResult GetAll()
        {
            var list = _packagesRepo.GetAll();

            return Json(list);
        }

        //return packages by location
        [HttpGet("api/GetPackagesByLocation")]
        public JsonResult GetByLocation(string location)
        {
            var list = _packagesRepo.Query(s => s.Location.Contains(location));

            return Json(list);
        }

        //this is API method to post data to the API - create package as example
        [HttpPost("api/CreatePackages")]
        public JsonResult PostPackage(CreateTravelPackageVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TravelPackage pack = new TravelPackage
                    {
                        Name = vm.Name,
                        Description = vm.Description,
                        Price = vm.Price,
                        Location = vm.Location
                    };
                    _packagesRepo.Create(pack);
                    //success
                    Response.StatusCode = (int)HttpStatusCode.Created;

                    return Json(true);
                }
            }
            catch (Exception ex)
            {

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }
            //case it is invalid
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed to upload details =(", ModelState = ModelState });

            //{
            //    "travelPackageId": 1,
            //    "name": "Asasaasa2",
            //    "location": "a2",
            //    "description": "a2",
            //    "price": 1112,
            //    "active": false,
            //    "providerCompany": "",
            //    "userId": "Provider4"
            //   }
        }
    }
}
