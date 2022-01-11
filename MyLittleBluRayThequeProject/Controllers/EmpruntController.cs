using Microsoft.AspNetCore.Mvc;
using MyLittleBluRayThequeProject.Business;
using MyLittleBluRayThequeProject.DTOs;
using MyLittleBluRayThequeProject.Models;
using MyLittleBluRayThequeProject.Repositories;

namespace MyLittleBluRayThequeProject.Controllers
{
    public class EmpruntController : Controller
    {
       
        private readonly ILogger<EmpruntController> _logger;

        private readonly BluRayRepository brRepository;
        private readonly BluRayBusiness brBusiness;

        public EmpruntController(ILogger<EmpruntController> logger)
        {
            _logger = logger;
            brRepository = new BluRayRepository();
            brBusiness = new BluRayBusiness();
        }
        public IActionResult EmprunterBluRay(long? id)
        {
            EmprunterBluRayViewModel model = new EmprunterBluRayViewModel();
            var br = brBusiness.GetListBluRayEmprunter();
            model.BluRays = br;
            if (id != null)
            {
                model.SelectedBluRay = brBusiness.GetBluRay(id.Value);
            }
            return View(model);
        }

        public IActionResult RendreBluRay(long id)
        {
            EmprunterBluRayViewModel model = new EmprunterBluRayViewModel();
            var br = brBusiness.GetListBluRayEmprunter();
            if (id != default)
            {
                brBusiness.RendreBluRay(id);
            }
            return Redirect("https://localhost:7266/");
        }


    }
    
}
