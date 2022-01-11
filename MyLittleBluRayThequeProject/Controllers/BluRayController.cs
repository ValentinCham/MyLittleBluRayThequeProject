using Microsoft.AspNetCore.Mvc;
using MyLittleBluRayThequeProject.Business;
using MyLittleBluRayThequeProject.DTOs;
using MyLittleBluRayThequeProject.Repositories;

namespace MyLittleBluRayThequeProject.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BluRayController
	{
		private readonly BluRayBusiness brBusiness;

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
	}
}
