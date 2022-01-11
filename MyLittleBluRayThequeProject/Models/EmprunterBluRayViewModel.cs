using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLittleBluRayThequeProject.Business;
using MyLittleBluRayThequeProject.DTOs;
using MyLittleBluRayThequeProject.Repositories;

namespace MyLittleBluRayThequeProject.Models
{
    public class EmprunterBluRayViewModel
    {
        BluRayRepository bluRayRepository = new BluRayRepository();
        PersonneRepository personneRepository = new PersonneRepository();
        BluRayBusiness brBusiness = new BluRayBusiness();

        public List<BluRay> BluRays { get; set; }

        public BluRay SelectedBluRay { get; set; }

        public Boolean Emprunt { get; set; }
       

        public EmprunterBluRayViewModel()
        {
           BluRays = bluRayRepository.GetListBluRayEmprunter();

        }

    }
}
