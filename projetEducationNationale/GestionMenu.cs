using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale
{
    public class GestionMenu
    {
        public GestionMenu() { }

        public void MenuEleves()
        {
            Console.WriteLine("1. Lister les élèves");
            Console.WriteLine("2. Créer un nouvel élève");
            Console.WriteLine("3. Consulter un élève existant");
            Console.WriteLine("4. Ajouter une note et une appréciation pour un cours sur un élève existant");
            Console.WriteLine("5. Revenir au menu principal");

            string choix2 = Console.ReadLine();

            switch (choix2)
            {
                case "1":
                    // Aller à la liste des élèves

                    Console.WriteLine("Liste des élèves");



                    break;

                case "2":
                    // Créer un nouvel élève
                    Console.WriteLine("Créer un nouvel élève");

                    break;
                case "3":
                    // Consulter un élève existant
                    Console.WriteLine("Consulter un élève existant");
                    break;
                case "4":
                    // Ajouter une note et une appréciation
                    Console.WriteLine("Ajouter une note et une appréciation pour un cours sur un élève existant");
                    break;
                case "5":
                    // Revenir au menu principal
                    break;
                default:
                    Console.WriteLine("Valeur incorrecte.");
                    break;

            }
        }

        public void MenuCours()
        {
            Console.WriteLine("1. Lister les cours existants");
            Console.WriteLine("2. Ajouter un nouveau cours au programme");
            Console.WriteLine("3. Supprimer un cours par son identifiant");
            Console.WriteLine("4. Revenir au menu principal");
            string choix3 = Console.ReadLine();

            switch (choix3)
            {
                case "1":
                    // Lister les cours existants
                    Console.WriteLine("Lister les cours existants");
                    break;
                case "2":
                    // Ajouter un nouveau cours au programme
                    Console.WriteLine("Ajouter un nouveau cours au programme");

                    break;
                case "3":
                    // Supprimer un cours par son identifiant
                    Console.WriteLine("Supprimer un cours par son identifiant");
                    break;
                case "4":
                    // Revenir au menu principal
                    break;
                default:
                    Console.WriteLine("Choix incorrect.");
                    break;
            }
        }



    }
}
