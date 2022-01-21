using Microsoft.AspNetCore.Mvc;
using MyLittleBluRayThequeProject.Business;
using MyLittleBluRayThequeProject.DTOs;
using MyLittleBluRayThequeProject.Models;
using MyLittleBluRayThequeProject.Repositories;

namespace MyLittleBluRayThequeProject.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BluRayController
	{
		private readonly BluRayBusiness brBusiness = new BluRayBusiness();

		[HttpGet("{idBluRay}")]
		public BluRay GetBluRay(long idBr)
		{
			BluRay bluRay = brBusiness.GetBluRay(idBr);

			return bluRay;
		}

		[HttpGet()]
		public List<BluRay> GetListeBluRay()
		{
			List<BluRay> bluRays = new();
			bluRays = brBusiness.GetListeBluRay();
			if (bluRays == null)
			{
				throw new ArgumentException($"Blurays non trouvé");
			}
			return bluRays;
		}

		[HttpPost("{IdBluray}/Emprunt")]
		public void EmprunterBluRay([FromRoute] IdBluRayRoute route)
		{
			brBusiness.EmprunterBluRay(route.IdBluray);
		}

		[HttpGet("{IdBluray}/Disponible")]
		public BluRay bluRayDisponible([FromRoute] IdBluRayRoute route)
		{
			BluRay br = brBusiness.bluRayDisponible(route.IdBluray);
			return br;
		}


		[HttpDelete("{IdBluray}/Emprunt")]
		public void DeleteEmprunt([FromRoute] IdBluRayRoute route)
		{
			brBusiness.DeleteEmprunt(route.IdBluray);

		}

		[HttpDelete("{IdBluray}")]
		public void DeleteBluRay([FromRoute] IdBluRayRoute route)
		{
			brBusiness.supprimeFilm(route.IdBluray);

		}
	}
}
