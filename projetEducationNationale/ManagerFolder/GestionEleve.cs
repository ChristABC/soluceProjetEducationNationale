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
using static projetEducationNationale.ManagerFolder.MenuGestion;



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
                Console.WriteLine("\n1. Lister les élèves");
                Console.WriteLine("\n2. Créer un nouvel élève");
                Console.WriteLine("\n3. Consulter un élève existant");
                Console.WriteLine("\n4. Ajouter une note et une appréciation pour un cours sur un élève existant");
                Console.WriteLine("\n5. Revenir au menu principal");

                Console.WriteLine("\nQuel est votre choix? ");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Console.WriteLine("\nListe des élèves :");
                        AfficherListeEleves();
                        break;
                    case "2":
                        Console.WriteLine("\nCréation d'un nouvel élève :");
                        AjouterEleve();
                        break;
                    case "3":
                        Console.WriteLine("\nConsulter un élève existant :");
                        AfficherListeEleves();

                        ConsulterEleveParId();
                        break;
                    case "4":
                        Console.WriteLine("\nAjouter une note et une appréciation pour un cours sur un élève existant :");
                        AjouterNoteEtAppreciation();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("\nChoix incorrect.");
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
                string nom = DemanderEtValiderNom("\nEntrez le nom de l'élève (lettres uniquement) :");

                // Demande et valide le prénom de l'élève
                string prenom = DemanderEtValiderNom("\nEntrez le prénom de l'élève (lettres uniquement) :");

                // Demande et valide la date de naissance de l'élève
                DateTime dateDeNaissance = DemanderEtValiderDate("\nEntrez la date de naissance de l'élève (dd-MM-yyyy) :");

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

                Console.WriteLine($"\nL'élève, {prenom} {nom}, né le {dateDeNaissance:dd-MM-yyyy} a été ajouté à la promotion {nomPromotion}.\n");
                SauvegardeHelper.Save(donnees);
                Log.Information("Les données utilisateur ont été sauvegardées avec succès.");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nEntrée invalide. Veuillez réessayer.\n");
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
                throw new FormatException("\nLe nom et le prenom doivent contenir uniquement des lettres.");
            }
            return input;
        }

        private DateTime DemanderEtValiderDate(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            if (!DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                throw new FormatException("\nFormat de date invalide. Utilisez le format dd-MM-yyyy.\n");
            }
            return date;
        }

        public string DemanderEtValiderNomPromotion()
        {
            Console.WriteLine("\nEntrez le nom de la promotion :\n");
            string input = Console.ReadLine();

            // Utilisation d'une expression régulière pour vérifier si l'entrée contient uniquement des lettres ou des chiffres
            if (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, @"^[a-zA-Z0-9 ]+$"))
            {
                throw new FormatException("\nLe nom de la promotion ne doit contenir que des lettres ou des chiffres. \n");
            }

            return input;
        }


        public void AfficherListeEleves()
        {
            foreach (var eleve in donnees.listEleve)
            {
                Console.WriteLine($"\nID : {eleve.ID}, Nom: {eleve.Nom}, Prénom: {eleve.Prenom}");
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
            Console.WriteLine("\nEntrez l'ID de l'élève à consulter :");
            int id = Convert.ToInt32(Console.ReadLine());

            Eleve eleve = ObtenirEleveParId(id);
            AfficherDetailsEleve(eleve);
        }

        public void AjouterNoteEtAppreciation()
        {
            Console.WriteLine("\nEntrez l'ID de l'élève :");
            int ID = Convert.ToInt32(Console.ReadLine());
            Eleve eleve = ObtenirEleveParId(ID);

            if (eleve != null)
            {
                Console.WriteLine("\nEntrez le coursID du cours : ");
                int coursID = Convert.ToInt32(Console.ReadLine());
                Cours cours = ObtenirCoursParId(coursID);

                if (cours != null)
                {
                    Console.WriteLine("\nEntrez la note : ");
                    double note;

                    if (double.TryParse(Console.ReadLine(), out note) && note >= 0 && note <= 20)
                    {
                        Console.WriteLine("\nEntrez l'appréciation :");
                        string appreciation = Console.ReadLine();

                        Note nouvelleNote = new Note(cours.Nom, note, appreciation);
                        eleve.AjouterNote(nouvelleNote);

                        Console.WriteLine($"\nLa note {note} et l'appréciation sont ajoutées au cours {cours.Nom} à l'élève : {eleve.Nom}.");
                    }
                    else
                    {
                        Console.WriteLine("\nVeuillez entrer une note valide entre 0 et 20.");
                    }
                }
                else
                {
                    Console.WriteLine("\nCours non trouvé.");
                }
            }
            else
            {
                Console.WriteLine("\nÉlève non trouvé.");
            }
        }


        public Cours ObtenirCoursParId(int id)
        {
            return donnees.listCours.Find(cours => cours.coursID == id);
        }

        //Methode pour verifier que la chaîne ne contient que des lettres
        public bool EstNomValide(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z]+$");
        }

        //Methode pour valider la date de Naissance
        private bool EstDateValide(string dateString, out DateTime date)
        {
            // Vérifie que la date est au format dd-MM-yyyy
            if (DateTime.TryParseExact(dateString, "dd-MM-yyyy", null, DateTimeStyles.None, out date))
            {

                DateTime today = DateTime.Today;

                // Vérifie que la date n'est pas dans le futur
                if (date > today)
                {
                    Console.WriteLine("\nLa date de naissance est future donc veuillez essayer à nouveau. ");
                    return false;
                }

                // Calcule l'âge de la personne
                int age = today.Year - date.Year;

                // Si la date de naissance est après la date actuelle moins l'âge en années, cela signifie que l'anniversaire n'est pas encore arrivé cette année, donc on soustrait une année à l'âge
                if (date > today.AddYears(-age))
                {
                    age--;
                }

                // Retourne true si l'âge est inférieur ou égal à 30 ans, sinon retourne false
                return age <= 30;
            }

            return false;
        }


    }
}



