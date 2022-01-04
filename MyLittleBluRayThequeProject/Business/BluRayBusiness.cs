﻿using MyLittleBluRayThequeProject.DTOs;
using MyLittleBluRayThequeProject.Repositories;

namespace MyLittleBluRayThequeProject.Business
{
    public class BluRayBusiness
    {

        private readonly BluRayRepository bluRayRepository;
        private readonly PersonneRepository personneRepository;

        public BluRayBusiness()
        { 

            this.bluRayRepository = new BluRayRepository();
            this.personneRepository = new PersonneRepository();
        }

        public List<BluRay> GetListeBluRay()
        {
            List<BluRay> bluRays =  new();
            bluRays = bluRayRepository.GetListeBluRay();
            if (bluRays == null)
            {
                throw new ArgumentException($"Blurays non trouvé");
            }
            return bluRays;
        }
        public BluRay GetBluRay(long idBr)
        {
            BluRay bluRay = bluRayRepository.GetBluRay(idBr);


            if(bluRay == null)
            {
                throw new ArgumentException($"Bluray d'id :{idBr} non trouvé");
            }

            bluRay.Realisateur = personneRepository.GetRealisateurBr(idBr);

            bluRay.Acteurs = personneRepository.GetActeursBr(idBr);

            return bluRay;
        }
        public void AddBluRay(string titre, long duree, DateTime date, string version, Boolean disponible)
        {
            bluRayRepository.AjouterBluRay(titre, duree, date, version, disponible);
        }


    }
}
