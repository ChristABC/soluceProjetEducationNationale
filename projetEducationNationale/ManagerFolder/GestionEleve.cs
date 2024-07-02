using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using projetEducationNationale.Modeles;

namespace projetEducationNationale.ManagerFolder
{
    public class GestionEleve
    {
        private MenuGestion.DonneesUtilisateur donnees;

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

                //Demande et valide la promotion de l'élève
                string nomPromotion = DemanderEtValiderNomPromotion();

                // Création de l'instance de Promotion avec le nom validé
                Promotion nouvellePromotion = new Promotion(nomPromotion);

                // Création du nouvel élève
                Eleve nouvelEleve = new Eleve(id, nom, prenom, dateDeNaissance, nomPromotion);
                donnees.listEleve.Add(nouvelEleve);
                donnees.listPromotion.Add(nouvellePromotion);

                Console.WriteLine($"L'élève, {prenom} {nom}, né le {dateDeNaissance:dd-MM-yyyy} a été ajouté à la promotion {nomPromotion}. ");
            }
            catch (FormatException)
            {
                Console.WriteLine("Entrée invalide. Veuillez réessayer.");
            }
        }


        private string DemanderEtValiderNom(string message)
        {
            string nom;
            do
            {
                Console.WriteLine(message);
                nom = Console.ReadLine();

                if (!EstNomValide(nom))
                {
                    Console.WriteLine("Le nom ou prénom doit contenir uniquement des lettres. Veuillez réessayer.");
                }
            }
            while (!EstNomValide(nom));

            return nom;
        }

        private DateTime DemanderEtValiderDate(string message)
        {
            DateTime date;
            while (true)
            {
                Console.WriteLine(message);
                string dateInput = Console.ReadLine();

                if (EstDateValide(dateInput, out date))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Le format de la date est invalide ou la personne a plus de 60 ans. Veuillez réessayer.");
                }
            }
            return date;
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

        public int ObtenirMaxIdEleve()
        {
            return donnees.listEleve.Max(eleve => eleve.ID);
        }
        public void AfficherDetailsEleve(Eleve eleve)
        {
            if (eleve != null)
            {
                Console.WriteLine("\n----------------------------------------------------------------------\n");
                Console.WriteLine($"\nNom\t\t\t\t: {eleve.Nom}\n \nPrénom\t\t\t\t: {eleve.Prenom}\n \n\t\t\t\tDate de Naissance: {eleve.DateDeNaissance.ToShortDateString()}\n");
                Console.WriteLine($"\nPromotion\t\t\t\t: {eleve.PromotionEleve}\n");
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
            int id = Convert.ToInt32(Console.ReadLine());
            Eleve eleve = ObtenirEleveParId(id);

            if (eleve != null)
            {
                Console.WriteLine("Entrez le numberId du cours: ");
                int x = Convert.ToInt32(Console.ReadLine());
                Cours cours = ObtenirCoursParId(x);

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
            return donnees.listCours.Find(cours => cours.numberId == id);
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

        public static string DemanderEtValiderNomPromotion() //Sans caractères spéciaux
        {
            string nom;
            Regex regex = new Regex("^[a-zA-Z ]+$"); // Expression régulière pour autoriser seulement des lettres de l'alphabet

            do
            {
                Console.WriteLine("Entrez le nom de la promotion sans caractères spéciaux (uniquement des lettres) :");
                nom = Console.ReadLine().Trim(); // Trim pour enlever les espaces au début et à la fin

                if (!regex.IsMatch(nom))
                {
                    Console.WriteLine("Le nom ne doit contenir que des lettres de l'alphabet. Veuillez réessayer.");
                }
            }
            while (!regex.IsMatch(nom));

            return nom;
        }
    }
}


