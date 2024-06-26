using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale
{
    public class GestionCours
    {
        public List<Cours> listCours = new List<Cours>();

        public void AjouterCoursALaList(Cours cours) => listCours.Add(cours);

        public List<Cours> ObtenirListCours()
        {
            return listCours;
        }

        public Cours ObtenirCoursParId(int ID)
        {
            return listCours.Find(cours => cours.numberId == ID);
        }

        public void AjouterCours()
        {
            Console.WriteLine("Entrez l'ID du cours:");
            int numberId = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrez le nom du cours:");
            string nom = Console.ReadLine();

            Cours nouveauCours = new Cours(numberId, nom);
        }
        public void SupprimerCours()
        {
            Console.WriteLine("Entrez l'ID du cours:");
            int id = int.Parse(Console.ReadLine());
            Cours cours = ObtenirCoursParId(id);

            if (cours != null)
            {
                listCours.Remove(cours);
                Console.WriteLine($"Le cours avec l'ID {id} a été supprimé.");
            }
            else
            {
                Console.WriteLine($"Aucun cours trouvé avec l'ID {id}.");
            }
        }

        public void AfficherCours()
        {
            foreach (var cours in listCours)
            {
                Console.WriteLine($"ID: {cours.numberId}, Nom: {cours.Nom}");
            }
        }
    }
}

