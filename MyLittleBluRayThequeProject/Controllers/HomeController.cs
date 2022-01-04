using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLittleBluRayThequeProject.Models;
using MyLittleBluRayThequeProject.Repositories;
using System.Diagnostics;
using MyLittleBluRayThequeProject.Models;
using MyLittleBluRayThequeProject.Business;

namespace MyLittleBluRayThequeProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly BluRayRepository brRepository;
        private readonly BluRayBusiness brBusiness;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            brRepository = new BluRayRepository();
            brBusiness = new BluRayBusiness();

        }

        /**public IActionResult Index(long? id)
        {
            IndexViewModel model = new IndexViewModel();
            var br = brRepository.GetListeBluRay();
            model.BluRays = br.ConvertAll(InfoBluRayViewModel.ToModel);
            if (id != null)
            {
                model.SelectedBluRay = brRepository.GetBluRay(id.Value);
            }
            return View(model);
        }**/
        public IActionResult Index(long? id)
        {
            IndexViewModel model = new IndexViewModel();
            var br = brRepository.GetListeBluRay();
            model.BluRays = br.ConvertAll(InfoBluRayViewModel.ToModel);
            if (id != null)
            {
                model.SelectedBluRay = brBusiness.GetBluRay(id.Value);
            }
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AjouterBluRay2()
        {
            AjouterBluRayViewModel model = new AjouterBluRayViewModel();
            return View(model);
        }



        public IActionResult AjouterBluRay()
        {
            AjouterBluRayViewModel list  = new AjouterBluRayViewModel();
            list.ListLangues = BindList();
            return View(list);
        }
        [HttpPost]
        public IActionResult AjouterBluRay(AjouterBluRayViewModel lng)
        {
            AjouterBluRayViewModel list = new AjouterBluRayViewModel();
            lng.ListLangues = BindList();
            if(lng.LangueId != null)
            {
                List<SelectListItem> selectedItems = lng.ListLangues.Where(p=>lng.LangueId.Contains(int.Parse(p.Value))).ToList();
                ViewBag.Message = "Selected Langues : ";
                foreach(var item in selectedItems)
                {
                    item.Selected = true;
                    ViewBag.Message += item.Text + ", ";
                }
            }

            return View(lng);
        }
        public List<SelectListItem> BindList()
        {
           List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "java", Value = "1" });
            list.Add(new SelectListItem { Text = "Css", Value = "2" });
            list.Add(new SelectListItem { Text = "Node", Value = "3" });
            list.Add(new SelectListItem { Text = "Vue", Value = "4" });
            list.Add(new SelectListItem { Text = "JS", Value = "5" });
            return list;
        }
    }
}