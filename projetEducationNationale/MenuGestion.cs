using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projetEducationNationale
{
    public class MenuGestion
    {
        public GestionEleve gestionEleve = new GestionEleve();
        public GestionCours gestionCours = new GestionCours();
        public List<Cours> listCours = new List<Cours>();
        public List<Eleve> listEleve = new List<Eleve>();
        


        public void MenuEleves()
        {
            while (true)
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
                        AfficherListeEleves();
                        break;
                    case "2":
                        Console.WriteLine("Créer un nouvel élève");
                        AjouterEleve();
                        break;
                    case "3":
                        Console.WriteLine("Consulter un élève existant");
                        ConsulterEleveParId();
                        break;
                    case "4":
                        Console.WriteLine("Ajouter une note et une appréciation pour un cours sur un élève existant");
                        AjouterNoteEtAppreciation();
                        break;
                    case "5":
                        return;                        
                    default:
                        Console.WriteLine("Valeur incorrecte.");
                        break;
                }
            }
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
                        return;
                    default:
                        Console.WriteLine("Choix incorrect.");
                        break;
                }
            }
        }



        // METHODE POUR ELEVE

        public void AjouterEleve()
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
            listEleve.Add(nouvelEleve);

            Console.WriteLine($"L'élève {nom}{prenom} avec l'identifiant {id} a été ajouté.");

        }

        public void AfficherListeEleves()
        {
            foreach (var eleve in listEleve)
            {

                Console.WriteLine($"Nom: {eleve.Nom}, Prénom: {eleve.Prenom}");
            }
        }

        public Eleve ObtenirEleveParId(int id)
        {
            return listEleve.Find(listEleve => listEleve.ID == id);
        }

        public void AfficherDetailsEleve(Eleve eleve)
        {
            if (eleve != null)
            {
                Console.WriteLine("----------------------------------------------------------------------\n");
                Console.WriteLine($"ID: {eleve.ID} \nNom: {eleve.Nom} \nPrénom: {eleve.Prenom} \nDate de Naissance: {eleve.DateDeNaissance.ToShortDateString()}");
                Console.WriteLine("Résultats scolaires :");
                foreach (var note in eleve.Notes)
                {
                    Console.WriteLine($"Cours: {note.Matiere} \nNote: {note.ValeurNote} \nAppréciation: {note.Appreciation}");
                }
                double moyenne = eleve.MoyenneNotesEleve();
                Console.WriteLine($"\nMoyenne: {moyenne}/20");
            }
            else
            {
                Console.WriteLine("Élève non trouvé.");
            }
        }
        public void ConsulterEleveParId()
        {
            Console.WriteLine("Entrez l'ID de l'élève à consulter:");
            int id = int.Parse(Console.ReadLine());

            Eleve eleve = ObtenirEleveParId(id);
            AfficherDetailsEleve(eleve);
        }

        public void AjouterNoteEtAppreciation()
        {
            Console.WriteLine("Entrez l'ID de l'élève:");
            int id = int.Parse(Console.ReadLine());
            Eleve eleve = ObtenirEleveParId(id);

            if (eleve != null)
            {
                Console.WriteLine("Entrez l'Id du cours: ");
                int x = int.Parse(Console.ReadLine());
                Cours cours = ObtenirCoursParId(x);
                
                Console.WriteLine("Entrez la note: ");
                double note = double.Parse(Console.ReadLine());

                Console.WriteLine("Entrez l'appréciation:");
                string appreciation = Console.ReadLine();

                Note nouvelleNote = new Note(cours.Nom, note, appreciation);
                eleve.AjouterNote(nouvelleNote);

                Console.WriteLine($"La note {note} et l'appréciation sont ajoutées au cours {cours.Nom } à l'élève : {eleve.Nom}.");
            }
            else
            {
                Console.WriteLine("Élève non trouvé.");
            }
        }

        //METHODE POUR LE COURS

        public void AjouterCoursALaList(Cours cours) => listCours.Add(cours);


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
                Console.WriteLine($"Voulez-vous supprimer le cours ? {cours.Nom}(oui/non) ");
                string confirmer = Console.ReadLine();

                if (confirmer.ToLower() != "oui")
                {
                  listCours.Remove(cours);
                Console.WriteLine($"Le cours avec l'ID {id} a été supprimé.");  
                }
                else
                {
                    return;
                }
                
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



