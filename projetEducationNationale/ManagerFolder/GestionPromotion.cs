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
                Console.WriteLine("2. Sélectionner une promotion");
                Console.WriteLine("3. Afficher la moyenne d'une promotion");
                Console.WriteLine("4. Afficher les promotions uniques");
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
                        string promotionToSelect = Console.ReadLine();
                        SelectionnerElevesParPromotion(promotionToSelect);
                        break;
                    case "3":
                        Console.WriteLine("Vous souhaitez afficher la moyenne de quelle promotion? ");
                        string promotionToAverage = Console.ReadLine();
                        double moyenne = MoyennePromotion(promotionToAverage);
                        Console.WriteLine($"La moyenne des élèves de la promotion {promotionToAverage} est {moyenne}");
                        break;
                    case "4":
                        ObtenirPromotions();
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
            List<Eleve> eleves = donnees.listEleve.Where(e => e.PromotionEleve == nomPromotion).ToList();
            if (eleves.Count == 0)
            {
                Console.WriteLine("Aucun élève trouvé pour cette promotion.");
            }
            else
            {
                Console.WriteLine($"Élèves de la promotion {nomPromotion} :");
                foreach (Eleve eleve in eleves)
                {
                    Console.WriteLine($"- {eleve.Prenom} {eleve.Nom}");
                }
            }
            return eleves;
        }

        // Méthode pour calculer la moyenne des notes de tous les élèves d'une promotion
        public double MoyennePromotion(string nomPromotion)
        {
            List<Eleve> elevesPromotion = SelectionnerElevesParPromotion(nomPromotion);

            if (elevesPromotion.Count == 0)
            {
                throw new InvalidOperationException("Aucun élève trouvé pour cette promotion.");
            }

            double sommeNotes = 0;
            double nombreNotes = 0;

            foreach (Eleve eleve in elevesPromotion)
            {
                if (eleve.Notes != null && eleve.Notes.Count > 0)
                {
                    foreach (Note note in eleve.Notes)
                    {
                        sommeNotes += note.ValeurNote;
                        nombreNotes++;
                    }
                }
            }

            if (nombreNotes == 0)
            {
                throw new InvalidOperationException("Aucune note disponible pour les élèves de cette promotion.");
            }

            return Convert.ToDouble(sommeNotes / nombreNotes);
        }

        public void AfficherListPromotion()
        {
            if (listPromotion.Count == 0)
            {
                Console.WriteLine("Aucune promotion n'est disponible.");
                return;
            }

            Console.WriteLine("\t\tVoici la liste des promotions : ");
            for (int i = 0; i < listPromotion.Count; i++)
            {
                Promotion promotion = listPromotion[i];
                Console.WriteLine($"{i + 1}. Nom : {promotion.NamePromotion}");
            }
        }
        public void AjouterPromotion(Promotion promotion)
        {
            listPromotion.Add(promotion);
        }
        // Méthode pour obtenir la liste des promotions uniques
        public List<string> ObtenirPromotions()
        {
            List<string> PromotionsList = donnees.listEleve
                .Select(e => e.PromotionEleve)
                .Distinct()
                .ToList();

            Console.WriteLine("Liste des promotions uniques :");
            foreach (string promotion in PromotionsList)
            {
                Console.WriteLine($"- {promotion}");
            }

            return PromotionsList;
        }
    }
}