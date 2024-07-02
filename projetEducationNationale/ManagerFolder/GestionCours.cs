using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using projetEducationNationale.Modeles;

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
                Console.WriteLine("1. Lister les cours existants");
                Console.WriteLine("2. Ajouter un nouveau cours au programme");
                Console.WriteLine("3. Supprimer un cours par son identifiant");
                Console.WriteLine("4. Revenir au menu principal");

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
                        SupprimerCours();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Choix incorrect.");
                        break;
                }
            }
        }

        public void AjouterCours()
        {
            try
            {
                int id = ObtenirMaxIdCours() + 1;
                
                string nom = DemanderEtValiderNom("Entrez le nom du cours (lettres uniquement) :");

                Cours nouveauCours = new Cours(id, nom);
                donnees.listCours.Add(nouveauCours);

                Console.WriteLine($"Le cours {nom} avec l'ID {id} a été ajouté.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrée invalide. Veuillez réessayer.");
            }
        }

        public void SupprimerCours()
        {
            Console.WriteLine("Entrez l'ID du cours:");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Cours cours = ObtenirCoursParId(id);

                if (cours != null)
                {
                    if (DemanderConfirmationSuppression(cours))
                    {
                        donnees.listCours.Remove(cours);
                        Console.WriteLine($"Le cours avec l'ID {id} a été supprimé.");
                    }
                }
                else
                {
                    Console.WriteLine($"Aucun cours trouvé avec l'ID {id}.");
                }
            }
            else
            {
                Console.WriteLine("ID invalide. Veuillez entrer un nombre entier.");
            }
        }

        private bool DemanderConfirmationSuppression(Cours cours)
        {
            Console.WriteLine($"Voulez-vous supprimer le cours {cours.Nom} ? (oui/non)");
            string confirmer = Console.ReadLine().ToLower();

            return confirmer == "oui";
        }

        public void AfficherCours()
        {
            Console.WriteLine("Liste de Cours : ");
            foreach (var cours in donnees.listCours)
            {
                Console.WriteLine($"ID: {cours.numberId}, Nom: {cours.Nom}");
            }
        }

        public Cours ObtenirCoursParId(int id)
        {
            return donnees.listCours.Find(cours => cours.numberId == id);
        }

        public int ObtenirMaxIdCours()
        {
            // Logique pour obtenir le maximum des IDs des cours existants
            return donnees.listCours.Count > 0 ? donnees.listCours.Max(c => c.numberId) : 0;
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
                    Console.WriteLine("Le nom doit contenir uniquement des lettres. Veuillez réessayer.");
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