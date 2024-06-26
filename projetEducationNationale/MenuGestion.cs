using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace projetEducationNationale
{
    public class MenuGestion
    {
        public GestionEleve gestionEleve;
        public GestionCours gestionCours;
        public List<Cours> listCours = new List<Cours>();

        /*public void gestionEleveMenu( GestionEleve gestion )
        {
            this.gestionEleve = gestion;
        }
        public void gestionCoursMenu(GestionCours gestion)
        {
            this.gestionCours = gestion;
        }*/

        public void MenuEleves()
        {
            Console.WriteLine("1. Lister les élèves");
            Console.WriteLine("2. Créer un nouvel élève");
            Console.WriteLine("3. Consulter un élève existant");
            Console.WriteLine("4. Ajouter une note et une appréciation pour un cours sur un élève existant");
            Console.WriteLine("5. Revenir au menu principal");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    Console.WriteLine("Liste des élèves");
                    gestionEleve.AfficherListeEleves();
                    break;
                case "2":
                    Console.WriteLine("Créer un nouvel élève");
                    AjouterEleve();
                    break;
                case "3":
                    Console.WriteLine("Consulter un élève existant");
                    ConsulterEleve();
                    break;
                case "4":
                    Console.WriteLine("Ajouter une note et une appréciation pour un cours sur un élève existant");
                    AjouterNoteEtAppreciation();
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

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    Console.WriteLine("Lister les cours existants");
                    AfficherCours();
                    break;
                case "2":
                    Console.WriteLine("Ajouter un nouveau cours au programme");
                    AjouterCours();
                    break;
                case "3":
                    Console.WriteLine("Supprimer un cours par son identifiant");
                    SupprimerCours();
                    break;
                case "4":
                    // Revenir au menu principal
                    break;
                default:
                    Console.WriteLine("Choix incorrect.");
                    break;
            }
        }

        private void AjouterEleve()
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
        }


        private void ConsulterEleve()
        {
            Console.WriteLine("Entrez l'ID de l'élève à consulter:");
            int id = int.Parse(Console.ReadLine());

            Eleve eleve = gestionEleve.ObtenirEleveParId(id);
            gestionEleve.AfficherDetailsEleve(eleve);
        }

        private void AjouterNoteEtAppreciation()
        {
            Console.WriteLine("Entrez l'ID de l'élève:");
            int id = int.Parse(Console.ReadLine());

            Eleve eleve = gestionEleve.ObtenirEleveParId(id);
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
            listCours.Add(nouveauCours);

            Console.WriteLine($"Le cours {nom} avec l'ID {numberId} a été ajouté.");
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
            Console.WriteLine("Liste de Cours : ");
            foreach (var cours in listCours)
            {
                Console.WriteLine($"ID: {cours.numberId}, Nom: {cours.Nom}");
            }
        }
    }
}



