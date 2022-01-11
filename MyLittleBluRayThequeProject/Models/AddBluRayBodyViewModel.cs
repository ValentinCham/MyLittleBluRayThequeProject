using Microsoft.AspNetCore.Mvc.Rendering;
using MyLittleBluRayThequeProject.DTOs;
using MyLittleBluRayThequeProject.Repositories;

namespace MyLittleBluRayThequeProject.Models
{
    public class AddBluRayBodyViewModel
    {
        BluRayRepository bluRayRepository = new BluRayRepository();
        PersonneRepository personneRepository = new PersonneRepository();


        public AddBluRayBodyViewModel(){
            List<Langue> ListLangue= bluRayRepository.GetLangues();
            SelectedLangues = (from lang in ListLangue
                               select new SelectListItem
                               {
                                   Text = lang.Valeur,
                                   Value = lang.Id.ToString()
                               }).ToList();
            createBluRay = false;
            List<Personne> ListPersonne = personneRepository.GetPersonne();
            SelectedPersonne = (from pers in ListPersonne
                select new SelectListItem
                {
                    Text = pers.Nom +" "+ pers.Prenom,
                    Value = pers.Id.ToString()
                }).ToList();
            
      
        }
        public Boolean createBluRay { get; set; }
        public string Titre { get; set; }

        public long Duree { get; set; }
        
        public DateTime Date { get; set; }

        public string Version { get; set; }

        public string idRealisateur { get; set; }

        public string idScenariste { get; set; }

        public List<string> Langues { get; set; }

        public List<string> SousTitres { get; set; }

        public List<SelectListItem> SelectedLangues { get; set; }

        public List<SelectListItem> SelectedPersonne { get; set; }


        public List<Langue> SsTitreLangues { get; set; }
    }
}
