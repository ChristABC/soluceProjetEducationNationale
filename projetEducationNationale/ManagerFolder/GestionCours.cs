using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using projetEducationNationale.Modeles;
using Serilog;
using static projetEducationNationale.ManagerFolder.MenuGestion;

namespace projetEducationNationale.ManagerFolder
{
    public class GestionCours
    {
        private MenuGestion.DonneesUtilisateur donnees;

        public GestionCours(MenuGestion.DonneesUtilisateur donnees)
        {
            this.donnees = donnees;
        }

        public void MenuCours()
        {
            while (true)
            {
                Console.WriteLine("\n1. Lister les cours existants");
                Console.WriteLine("\n2. Ajouter un nouveau cours au programme");
                Console.WriteLine("\n3. Supprimer un cours par son identifiant");
                Console.WriteLine("\n4. Revenir au menu principal");

                Console.WriteLine("\nQuel est votre choix ? ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        AfficherCours();
                        break;
                    case "2":
                        AjouterCours();
                        break;
                    case "3":
                        AfficherCours();
                        SupprimerCours();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("\nChoix incorrect.");
                        break;
                }
            }
        }

        public void AjouterCours()
        {
            try
            {
                int coursID = ObtenirMaxIdCours() + 1;

                string nom = DemanderEtValiderNom("\nEntrez le nom du cours (lettres uniquement) :");

                Cours nouveauCours = new Cours(coursID, nom);
                donnees.listCours.Add(nouveauCours);

                Console.WriteLine($"\nLe cours {nom} avec l'ID {coursID} a été ajouté.");
                SauvegardeHelper.Save(donnees);
                Log.Information("\nLes données pour l'ajout d'un nouveau cours ont été sauvegardées avec succès.");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nEntrée invalide. Veuillez réessayer.");
            }
        }

        public void SupprimerCours()
        {
            Console.WriteLine("\nEntrez l'ID du cours :");

            if (int.TryParse(Console.ReadLine(), out int coursID))
            {
                Cours cours = ObtenirCoursParId(coursID);

                if (cours != null)
                {
                    if (DemanderConfirmationSuppression(cours))
                    {
                        donnees.listCours.Remove(cours);
                        Console.WriteLine($"\nLe cours avec l'coursID {coursID} a été supprimé.");
                        SauvegardeHelper.Save(donnees);
                        Log.Information("\nLes données de la suppression du cours ont été sauvegardées avec succès.");
                    }
                }
                else
                {
                    Console.WriteLine($"\nAucun cours trouvé avec le coursID suivant :  {coursID}.");
                }
            }
            else
            {
                Console.WriteLine("\nCoursID invalide. Veuillez entrer un nombre entier.");
            }
        }

        private bool DemanderConfirmationSuppression(Cours cours)
        {
            Console.WriteLine($"\nVoulez-vous supprimer le cours {cours.Nom} ? (oui/non)");
            string confirmer = Console.ReadLine().ToLower();

            return confirmer == "oui";
        }

        public void AfficherCours()
        {
            Console.WriteLine("\nListe de Cours : ");
            foreach (var cours in donnees.listCours)
            {
                Console.WriteLine($"\nID: {cours.coursID}, Nom: {cours.Nom}");
            }
        }

        public Cours ObtenirCoursParId(int id)
        {
            return donnees.listCours.Find(cours => cours.coursID == id);
        }

        public int ObtenirMaxIdCours()
        {
            // Logique pour obtenir le maximum des IDs des cours existants
            return donnees.listCours.Count > 0 ? donnees.listCours.Max(c => c.coursID) : 0;
        }
        public string DemanderEtValiderNom(string message)
        {
            string nom;
            do
            {
                Console.WriteLine(message);
                nom = Console.ReadLine();

                if (!EstNomValide(nom))
                {
                    Console.WriteLine("\nLe nom doit contenir uniquement des lettres. Veuillez réessayer.");
                }
            }
            while (!EstNomValide(nom));

            return nom;
        }

        public bool EstNomValide(string name)
        {
            // Une expression régulière pour vérifier que la chaîne ne contient que des lettres
            return Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }
    }
}