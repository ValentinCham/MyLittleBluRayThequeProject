using Microsoft.AspNetCore.Mvc;
using MyLittleBluRayThequeProject.DTOs;
using MyLittleBluRayThequeProject.Models;
using MyLittleBluRayThequeProject.Repositories;

namespace MyLittleBluRayThequeProject.Business
{
    public class BluRayBusiness
    {

        private readonly BluRayRepository bluRayRepository;
        private readonly PersonneRepository personneRepository;
        private readonly RealisateurRepository realisateurRepository;
        private readonly ScenaristeRepository scenaristeRepository;
        private readonly LangueRepository langueRepository;
        private readonly SousTitreRepository sousTitreRepository;
        private readonly ActeurRepository acteurRepository;

        public BluRayBusiness()
        {
            this.realisateurRepository = new RealisateurRepository();
            this.scenaristeRepository = new ScenaristeRepository();
            this.langueRepository = new LangueRepository();
            this.sousTitreRepository = new SousTitreRepository();
            this.bluRayRepository = new BluRayRepository();
            this.personneRepository = new PersonneRepository();
            this.acteurRepository = new ActeurRepository();
        }

        public List<BluRay> GetListeBluRay()
        {
            List<BluRay> br = new();
            br = bluRayRepository.GetListeBluRay();
            if (br == null)
            {
                throw new ArgumentException($"Blurays non trouvé");
            }
            return br;
        }

        public List<Langue> GetLangues()
        {
            List<Langue> langues = new();
            langues = bluRayRepository.GetLangues();
            if (langues == null)
            {
                throw new ArgumentException($"Langues non trouvé");
            }
            return langues;
        }


        public BluRay GetBluRay(long idBr)
        {
            BluRay bluRay = bluRayRepository.GetBluRay(idBr);


            if (bluRay == null)
            {
                throw new ArgumentException($"Bluray d'id :{idBr} non trouvé");
            }

            bluRay.Realisateur = personneRepository.GetRealisateurBr(idBr);

            bluRay.Acteurs = personneRepository.GetActeursBr(idBr);

            bluRay.Langues = langueRepository.GetLanguefromBluRay(idBr);

            bluRay.SsTitres = sousTitreRepository.GetSousTitrefromBluRay(idBr);

            return bluRay;
        }


        public void CreateBluRay(AddBluRayBodyViewModel model)
        {
            long idBr = bluRayRepository.CreateBluRay(model.Titre, model.Duree, model.Date, model.Version, true);
            //realisateur
            realisateurRepository.createRealisateur(idBr, long.Parse(model.idRealisateur));
            // scenariste
            scenaristeRepository.createScenariste(idBr, long.Parse(model.idScenariste));
            //acteurs

            // langues
            foreach (string id in model.Langues)
            {
                langueRepository.createLangue(idBr, long.Parse(id));
            }
            //sous titres
            foreach (string id in model.SousTitres)
            {
                sousTitreRepository.createSousTitre(idBr, long.Parse(id));
            }
            //acteurs
            foreach (string id in model.IdActeur)
            {
                acteurRepository.createActeurs(idBr, long.Parse(id));
            }

        }

        public void EmprunterBluRay(long id)
        {
            bluRayRepository.Empruter(id);
        }

        public BluRay bluRayDisponible(long id)
        {
            BluRay br = bluRayRepository.Dispo(id);
            return br;
        }
        public void DeleteEmprunt(long id)
        {
            bluRayRepository.DeleteBluRay(id);

        }
        public List<BluRay> GetListBluRayEmprunter()
        {
            return bluRayRepository.GetListBluRayEmprunter();
        }

        public void RendreBluRay(long Id)
        {
            bluRayRepository.RendreBluRay(Id);
        }

        public void supprimeFilm(long Id)
        {
            realisateurRepository.DeleteRealisateur(Id);
            // scenariste
            scenaristeRepository.DeleteScenariste(Id);

            // langues
            langueRepository.DeleteLangues(Id);
            
            //sous titres
            sousTitreRepository.DeleteSousTitre(Id);
            
            //acteurs
         
            acteurRepository.DeleteActeurs(Id);
            
            bluRayRepository.supprimeFilm(Id);
           
        }
    }
    
}
