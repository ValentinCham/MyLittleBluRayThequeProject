using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLittleBluRayThequeProject.DTOs;

namespace MyLittleBluRayThequeProject.Models
{
    public class EmprunterBluRayViewModel
    {
        public List<InfoBluRayViewModel> BluRays { get; set; }

        public BluRay SelectedBluRay { get; set; }

        public Boolean Emprunt { get; set; }
    }
}
