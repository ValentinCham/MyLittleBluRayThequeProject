using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLittleBluRayThequeProject.DTOs;

namespace MyLittleBluRayThequeProject.Models
{
    public class AjouterBluRayViewModel
    {
        public BluRay? BluRay { get; set; }

        public List<SelectListItem> ListLangues { get; set; }  

        public int[] LangueId { get; set; }  

    }
}
