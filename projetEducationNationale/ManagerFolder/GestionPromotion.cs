using projetEducationNationale.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static projetEducationNationale.Modeles.Eleve;

namespace projetEducationNationale.ManagerFolder
{
    public class GestionPromotion
    {
        private MenuGestion.DonneesUtilisateur donnees;

        public List<Promotion> listPromotion { get; set; } = new List<Promotion>();

        public GestionPromotion(MenuGestion.DonneesUtilisateur donnees)
        {
            this.donnees = donnees;
        }
        

        public void MenuPromotion()
        {
            while (true)
            {
                Console.WriteLine("1. Liste des promotions");

                Console.WriteLine("5. Revenir au menu principal");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Console.WriteLine("Liste des promotions :");
                        AfficherListPromotion();
                        break;
                    case "2":
                        Console.WriteLine("Quelle promotion voulez-vous afficher ? :");
                        SelectionnerElevesParPromotion(Console.ReadLine());
                        break;
                    case "3":
                        Console.WriteLine("Vous souhaitez afficher la moyenne de quelle promotion? ");
                        MoyennePromotion(Console.ReadLine());
                        break;
                    case "4":
                        Console.WriteLine("Ajouter une note et une appréciation pour un cours sur un élève existant :");

                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Choix incorrect.");
                        break;
                }
            }
        }

        // Méthode pour sélectionner les élèves par promotion
        public List<Eleve> SelectionnerElevesParPromotion(string nomPromotion)
        {
            return donnees.listEleve.Where(e => e.PromotionEleve == nomPromotion).ToList();
        }

        // Méthode pour calculer la moyenne des notes de tous les élèves d'une promotion
        public float MoyennePromotion(string nomPromotion)
        {
            List<Eleve> elevesPromotion = SelectionnerElevesParPromotion(nomPromotion);

            if (elevesPromotion.Count == 0)
            {
                throw new InvalidOperationException("Aucun élève trouvé pour cette promotion.");
            }

            float sommeNotes = 0;
            int nombreNotes = 0;

            foreach (Eleve eleve in elevesPromotion)
            {
                System.Collections.IList list = eleve.Notes;
                for (int i = 0; i < list.Count; i++)
                {
                    float note = (float)list[i];
                    sommeNotes += note;
                    nombreNotes++;
                }
            }

            if (nombreNotes == 0)
            {
                throw new InvalidOperationException("Aucune note disponible pour les élèves de cette promotion.");
            }

            return sommeNotes / nombreNotes;
        }
        public void AfficherListPromotion()
        {
            Console.WriteLine("\t\tVoici la liste des promotions : ");
            foreach (var promotion in listPromotion)
            {
                Console.WriteLine($"Nom : {promotion.NamePromotion}");
            }
        }
        public void AjouterPromotion(Promotion Name)
        {
            listPromotion.Add(Name);
        }
    }
}