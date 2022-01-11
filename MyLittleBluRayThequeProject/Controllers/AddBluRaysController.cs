using Microsoft.AspNetCore.Mvc;
using MyLittleBluRayThequeProject.Business;
using MyLittleBluRayThequeProject.DTOs;
using MyLittleBluRayThequeProject.Models;
using MyLittleBluRayThequeProject.Repositories;

namespace MyLittleBluRayThequeProject.Controllers
{

    public class AddBluRaysController : Controller
    {
        private readonly ILogger<AddBluRaysController> _logger;

        private readonly BluRayRepository brRepository;
        private readonly BluRayBusiness brBusiness;

        public AddBluRaysController(ILogger<AddBluRaysController> logger)
        {
            _logger = logger;
            brRepository = new BluRayRepository();
            brBusiness = new BluRayBusiness();
        }
        public IActionResult AjouterBluRay()
        {
            AddBluRayBodyViewModel model = new AddBluRayBodyViewModel();
            return View(model);
        }
        public ObjectResult EmprunterBluRay([FromRoute] IdBluRayRoute route)
        {
            return new CreatedResult($"{route.IdBluray}", null);
        }

        [HttpPost]
        public IActionResult CreateBluRay(AddBluRayBodyViewModel model)
        {

            brBusiness.CreateBluRay(model);


            return RedirectToAction("Index", "Home", new { area =""});

        }


    }
}
