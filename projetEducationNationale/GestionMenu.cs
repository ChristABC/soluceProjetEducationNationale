using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale
{
    public class GestionMenu
    {
        public GestionEleve gestion;
        public GestionMenu(GestionEleve gestion) {
            this.gestion = gestion;
        }

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
            void AjouterEleve()
            {
                Console.WriteLine("Entrez l'ID de l'élève:");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Entrez le nom de l'élève:");
                string nom = Console.ReadLine();

                Console.WriteLine("Entrez le prénom de l'élève:");
                string prenom = Console.ReadLine();

                Console.WriteLine("Entrez la date de naissance de l'élève (yyyy-mm-dd):");
                DateTime dateDeNaissance = DateTime.Parse(Console.ReadLine());

                Eleve nouvelEleve = new Eleve(id, nom, prenom, dateDeNaissance);
                gestion.AjouterEleve(nouvelEleve);
            }

            void ConsulterEleve()
            {
                Console.WriteLine("Entrez l'ID de l'élève à consulter:");
                int id = int.Parse(Console.ReadLine());

                Eleve eleve = gestion.ObtenirEleveParId(id);
                gestion.AfficherDetailsEleve(eleve);
            }

            void AjouterNoteEtAppreciation()
            {
                Console.WriteLine("Entrez l'ID de l'élève:");
                int id = int.Parse(Console.ReadLine());

                Eleve eleve = gestion.ObtenirEleveParId(id);
                if (eleve != null)
                {
                    Console.WriteLine("Entrez le nom du cours:");
                    string cours = Console.ReadLine();

                    Console.WriteLine("Entrez la note:");
                    int note = int.Parse(Console.ReadLine());

                    Console.WriteLine("Entrez l'appréciation:");
                    string appreciation = Console.ReadLine();

                    Note nouvelleNote = new Note(cours, note, appreciation);
                    eleve.AjouterNote(nouvelleNote);

                    Console.WriteLine("Note et appréciation ajoutées avec succès.");
                }
                else
                {
                    Console.WriteLine("Élève non trouvé.");
                }
            }
        }
    }

        void MenuCours()
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

