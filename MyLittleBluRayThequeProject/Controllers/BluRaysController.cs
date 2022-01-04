using Microsoft.AspNetCore.Mvc;
using MyLittleBluRayThequeProject.Business;
using MyLittleBluRayThequeProject.DTOs;
using MyLittleBluRayThequeProject.Models;
using MyLittleBluRayThequeProject.Repositories;

namespace MyLittleBluRayThequeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BluRaysController
    {
        private readonly ILogger<BluRaysController> _logger;

        private readonly BluRayRepository brRepository;
        private readonly BluRayBusiness brBusiness;

        public BluRaysController(ILogger<BluRaysController> logger)
        {
            _logger = logger;
            brRepository = new BluRayRepository();
            brBusiness = new BluRayBusiness();
        }

        [HttpGet()]
        public ObjectResult GetListeBluRay()
        {
            List<BluRay> br = brBusiness.GetListeBluRay();

            List<InfoBluRayViewModel> bluRays = br.ConvertAll(InfoBluRayViewModel.ToModel);
            return new OkObjectResult(bluRays);
        }

        [HttpGet("{IdBluray}")]
        public ObjectResult GetBluRay([FromRoute] IdBluRayRoute route)
        {
            BluRay br = brBusiness.GetBluRay(route.IdBluray);
            return new OkObjectResult(br);
        }

        [HttpPost("{IdBluray}/Emprunt")]
        public ObjectResult EmprunterBluRay([FromRoute] IdBluRayRoute route)
        {
            return new CreatedResult($"{route.IdBluray}", null);
        }
        [HttpPost()]
        public ObjectResult AddBluRay(string titre, long duree, DateTime date, string version, Boolean disponible)
        {
            BluRay bluRay = new BluRay();
            bluRay.Titre = titre;
            bluRay.Duree = duree;
            bluRay.DateSortie = date;   
            bluRay.Version = version;
            bluRay.Disponible  = disponible;
            brBusiness.AddBluRay(titre, duree, date, version, disponible);


            return new OkObjectResult(bluRay);
        }
    }
}
