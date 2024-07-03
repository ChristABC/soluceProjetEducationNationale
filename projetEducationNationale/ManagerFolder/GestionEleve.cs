using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using projetEducationNationale.Modeles;
using Serilog;
using projetEducationNationale;
using projetEducationNationale.ManagerFolder;
using projetEducationNationale.SaveManager;



namespace projetEducationNationale.ManagerFolder
{
    public class GestionEleve
    {
        public MenuGestion.DonneesUtilisateur donnees;
        private GestionPromotion gestionPromotion;

        public GestionEleve(GestionPromotion gestionPromotion)
        {
            this.gestionPromotion = gestionPromotion;
        }



        public GestionEleve(MenuGestion.DonneesUtilisateur donnees)
        {
            this.donnees = donnees;
        }

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
                        Console.WriteLine("Liste des élèves :");
                        AfficherListeEleves();
                        break;
                    case "2":
                        Console.WriteLine("Création d'un nouvel élève :");
                        AjouterEleve();
                        break;
                    case "3":
                        Console.WriteLine("Consulter un élève existant :");
                        ConsulterEleveParId();
                        break;
                    case "4":
                        Console.WriteLine("Ajouter une note et une appréciation pour un cours sur un élève existant :");
                        AjouterNoteEtAppreciation();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Choix incorrect.");
                        break;
                }
            }
        }

        public void AjouterEleve()
        {
            try
            {
                // Donne un ID unique à l'élève
                int id = ObtenirMaxIdEleve() + 1;

                // Demande et valide le nom de l'élève
                string nom = DemanderEtValiderNom("Entrez le nom de l'élève (lettres uniquement) :");

                // Demande et valide le prénom de l'élève
                string prenom = DemanderEtValiderNom("Entrez le prénom de l'élève (lettres uniquement) :");

                // Demande et valide la date de naissance de l'élève
                DateTime dateDeNaissance = DemanderEtValiderDate("Entrez la date de naissance de l'élève (dd-MM-yyyy) :");

                // Demande et valide la promotion de l'élève
                string nomPromotion = DemanderEtValiderNomPromotion();

                // Vérifier si la promotion existe déjà
                Promotion promotionExistante = gestionPromotion.listPromotion.FirstOrDefault(p => p.NamePromotion == nomPromotion);
                if (promotionExistante == null)
                {
                    // Si la promotion n'existe pas, la créer et l'ajouter à la liste des promotions
                    Promotion nouvellePromotion = new Promotion(nomPromotion);
                    gestionPromotion.listPromotion.Add(nouvellePromotion);
                }

                // Création du nouvel élève
                Eleve nouvelEleve = new Eleve(id, nom, prenom, dateDeNaissance, nomPromotion);
                donnees.listEleve.Add(nouvelEleve);

                Console.WriteLine($"L'élève, {prenom} {nom}, né le {dateDeNaissance:dd-MM-yyyy} a été ajouté à la promotion {nomPromotion}.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrée invalide. Veuillez réessayer.");
            }
        }



        private int ObtenirMaxIdEleve()
        {
            // Implémentation pour obtenir le maximum ID des élèves existants
            return donnees.listEleve.Any() ? donnees.listEleve.Max(e => e.ID) : 0;
        }

        private string DemanderEtValiderNom(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || !input.All(char.IsLetter))
            {
                throw new FormatException("Le nom et le prenom doivent contenir uniquement des lettres.");
            }
            return input;
        }

        private DateTime DemanderEtValiderDate(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            if (!DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                throw new FormatException("Format de date invalide. Utilisez le format dd-MM-yyyy.");
            }
            return date;
        }

        public string DemanderEtValiderNomPromotion()
        {
            Console.WriteLine("Entrez le nom de la promotion :");
            string input = Console.ReadLine();

            // Utilisation d'une expression régulière pour vérifier si l'entrée contient uniquement des lettres ou des chiffres
            if (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, @"^[a-zA-Z0-9 ]+$"))
            {
                throw new FormatException("Le nom de la promotion ne doit contenir que des lettres (a-zA-Z) ou des chiffres (0-9).");
            }

            return input;
        }


        public void AfficherListeEleves()
        {
            foreach (var eleve in donnees.listEleve)
            {
                Console.WriteLine($"Nom: {eleve.Nom}, Prénom: {eleve.Prenom}");
            }
        }



        public Eleve ObtenirEleveParId(int id)
        {
            return donnees.listEleve.Find(eleve => eleve.ID == id);
        }


        public void AfficherDetailsEleve(Eleve eleve)
        {
            if (eleve != null)
            {
                Console.WriteLine("\n----------------------------------------------------------------------\n");
                Console.WriteLine($"\nNom\t\t\t\t: {eleve.Nom}\n \nPrénom\t\t\t\t: {eleve.Prenom}\n \nDate de Naissance \t\t: {eleve.DateDeNaissance.ToShortDateString()}\n");
                Console.WriteLine($"\nPromotion\t\t\t: {eleve.PromotionEleve}\n");
                Console.WriteLine("\nRésultats scolaires :\n");
                foreach (var note in eleve.Notes)
                {
                    Console.WriteLine($"\tCours: {note.Matiere}\n \t\tNote: {note.ValeurNote}\n \t\tAppréciation: {note.Appreciation}\n");
                }
                double moyenne = eleve.MoyenneNotesEleve();
                Console.WriteLine($"\tMoyenne: {moyenne}/20\n");
                Console.WriteLine("\n----------------------------------------------------------------------\n");
            }
            else
            {
                Console.WriteLine("\nÉlève non trouvé.\n");
            }
        }

        public void ConsulterEleveParId()
        {
            Console.WriteLine("Entrez l'ID de l'élève à consulter:");
            int id = Convert.ToInt32(Console.ReadLine());

            Eleve eleve = ObtenirEleveParId(id);
            AfficherDetailsEleve(eleve);
        }

        public void AjouterNoteEtAppreciation()
        {
            Console.WriteLine("Entrez l'ID de l'élève:");
            int ID = Convert.ToInt32(Console.ReadLine());
            Eleve eleve = ObtenirEleveParId(ID);

            if (eleve != null)
            {
                Console.WriteLine("Entrez le coursID du cours: ");
                int coursID = Convert.ToInt32(Console.ReadLine());
                Cours cours = ObtenirCoursParId(coursID);

                if (cours != null)
                {
                    Console.WriteLine("Entrez la note: ");
                    double note;
                    if (double.TryParse(Console.ReadLine(), out note) && note >= 0 && note <= 20)
                    {
                        Console.WriteLine("Entrez l'appréciation:");
                        string appreciation = Console.ReadLine();

                        Note nouvelleNote = new Note(cours.Nom, note, appreciation);
                        eleve.AjouterNote(nouvelleNote);

                        Console.WriteLine($"La note {note} et l'appréciation sont ajoutées au cours {cours.Nom} à l'élève : {eleve.Nom}.");

                    }
                    else
                    {
                        Console.WriteLine("Veuillez entrer une note valide entre 0 et 20.");
                    }
                }
                else
                {
                    Console.WriteLine("Cours non trouvé.");
                }
            }
            else
            {
                Console.WriteLine("Élève non trouvé.");
            }
        }
        public Cours ObtenirCoursParId(int id)
        {
            return donnees.listCours.Find(cours => cours.coursID == id);
        }
        public bool EstNomValide(string input)
        {
            // Une expression régulière pour vérifier que la chaîne ne contient que des lettres
            return Regex.IsMatch(input, @"^[a-zA-Z]+$");
        }


        private bool EstDateValide(string dateString, out DateTime date)
        {
            // Vérifie que la date est au format dd-MM-yyyy
            if (DateTime.TryParseExact(dateString, "dd-MM-yyyy", null, DateTimeStyles.None, out date))
            {
                // Obtient la date d'aujourd'hui
                DateTime today = DateTime.Today;

                // Vérifie que la date n'est pas dans le futur
                if (date > today)
                {
                    Console.WriteLine("La date de naissance est future donc veuillez essayer à nouveau. ");
                    return false;
                }

                // Calcule l'âge de la personne
                int age = today.Year - date.Year;

                // Si la date de naissance est après la date actuelle moins l'âge en années, cela signifie que l'anniversaire n'est pas encore arrivé cette année, donc on soustrait une année à l'âge
                if (date > today.AddYears(-age))
                {
                    age--;
                }

                // Retourne true si l'âge est inférieur ou égal à 60 ans, sinon retourne false
                return age <= 60;
            }

            return false;
        }


    }
}



