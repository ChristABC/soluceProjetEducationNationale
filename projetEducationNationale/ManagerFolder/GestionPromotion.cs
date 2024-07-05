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
                Console.WriteLine("\n1. Liste des promotions\n");
                Console.WriteLine("\n2. Sélectionner une promotion\n");
                Console.WriteLine("\n3. Afficher la moyenne d'une promotion\n");
                Console.WriteLine("\n4. Afficher la moyenne de chaque cours par promotions\n");
                Console.WriteLine("\n5. Revenir au menu principal\n");

                Console.WriteLine("\nQuel est votre choix ? \n");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Console.WriteLine("\nListe des promotions :");
                        ObtenirPromotions();
                        break;
                    case "2":
                        Console.WriteLine("\nQuelle promotion voulez-vous afficher ? :");
                        string promotionToSelect = Console.ReadLine();
                        SelectionnerElevesParPromotion(promotionToSelect);
                        break;
                    case "3":
                        Console.WriteLine("\nVous souhaitez afficher la moyenne de quelle promotion? ");
                        string promotionToAverage = Console.ReadLine();
                        double moyenne = MoyennePromotion(promotionToAverage);
                        Console.WriteLine($"\nLa moyenne des élèves de la promotion {promotionToAverage} est {moyenne}");
                        break;
                    case "4":
                        AfficherMoyennesCoursParPromotion();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("\nChoix incorrect.");
                        break;
                }
            }
        }

        //Méthode pour sélectionner les élèves par promotion
        public List<Eleve> SelectionnerElevesParPromotion(string nomPromotion)
        {
            List<Eleve> eleves = donnees.listEleve.Where(e => e.PromotionEleve == nomPromotion).ToList();
            if (eleves.Count == 0)
            {
                Console.WriteLine("\nAucun élève trouvé pour cette promotion.");
            }
            else
            {
                Console.WriteLine($"\nÉlèves de la promotion {nomPromotion} :");
                foreach (Eleve eleve in eleves)
                {
                    Console.WriteLine($"\n- {eleve.Prenom} {eleve.Nom}");
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
                throw new InvalidOperationException("\nAucun élève trouvé pour cette promotion.");
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
                throw new InvalidOperationException("\nAucune note disponible pour les élèves de cette promotion.");
            }

            return Convert.ToDouble(sommeNotes / nombreNotes);
        }

        public void AfficherListPromotion()
        {
            if (listPromotion.Count == 0)
            {
                Console.WriteLine("\nAucune promotion n'est disponible.");
                return;
            }

            Console.WriteLine("\t\tVoici la liste des promotions : ");
            for (int i = 0; i < listPromotion.Count; i++)
            {
                Promotion promotion = listPromotion[i];
                Console.WriteLine($"\n{i + 1}. Nom : {promotion.NamePromotion}");
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

            Console.WriteLine("\nListe des promotions uniques :");
            foreach (string promotion in PromotionsList)
            {
                Console.WriteLine($"\n- {promotion}");
            }

            return PromotionsList;
        }
        // Méthode pour calculer et afficher les moyennes des cours par promotion
        public void AfficherMoyennesCoursParPromotion()
        {
            var promotions = ObtenirPromotions();

            foreach (var promotion in promotions)
            {
                Console.WriteLine($"\nPromotion: {promotion}");
                var elevesPromotion = SelectionnerElevesParPromotion(promotion);
                var coursDict = new Dictionary<string, List<double>>();

                foreach (var eleve in elevesPromotion)
                {
                    foreach (var note in eleve.Notes)
                    {
                        if (!coursDict.ContainsKey(note.Matiere))
                        {
                            coursDict[note.Matiere] = new List<double>();
                        }
                        coursDict[note.Matiere].Add(note.ValeurNote);
                    }
                }

                foreach (var entry in coursDict)
                {
                    var moyenne = entry.Value.Average();
                    Console.WriteLine($"\nMoyenne pour le cours {entry.Key}: {moyenne:F2}");
                }
            }
        }
    }
}