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
        [HttpPost()]
        public void AjouterBluRay(string titre, int duree, DateTime date, string version, Boolean emprunt, Boolean disponible)
        {
            brRepository.AjouterBluRay(titre, duree, date, version, emprunt, disponible);
        }
        [HttpPost("{IdBluray}/Emprunt")]
        public ObjectResult EmprunterBluRay([FromRoute]IdBluRayRoute route)
        {
            return new CreatedResult($"{route.IdBluray}", null);
        }
    }
}
