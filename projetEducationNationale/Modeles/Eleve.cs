using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale.Modeles
{
    public class Eleve
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public string PromotionEleve;
        public double Moyenne;

        public List<Note> Notes { get; set; } = new List<Note>();


        public Eleve(int id, string nom, string prenom, DateTime dateDeNaissance, string promotion)
        {
            ID = id;
            Nom = nom;
            Prenom = prenom;
            DateDeNaissance = dateDeNaissance;
            PromotionEleve = promotion;
        }
        public void AjouterNote(Note note)
        {
            Notes.Add(note);
        }

        public double MoyenneNotesEleve()
        {
            if (Notes.Count == 0)
            {
                return 0;
            }
            else
            {
                double moyenneBrute = Notes.Average(note => note.ValeurNote);
                return ArrondirMoyenne(moyenneBrute);
            }
        }

        public double ArrondirMoyenne(double moyenne)
        {
            // Arrondir à 1 chiffre après la virgule
            double arrondi = Math.Round(moyenne * 2, MidpointRounding.AwayFromZero) / 2;
            return arrondi;
        }
    }
    public class Note
    {
        public string Matiere { get; set; }
        public double ValeurNote { get; set; }
        public string Appreciation { get; set; }

        public double Moyenne;

        public Note(string cours, double valeurNote, string appreciation)
        {
            Matiere = cours;
            ValeurNote = valeurNote;
            Appreciation = appreciation;
        }

    }

    public class Promotion
    {
        public string NamePromotion { get; set; }

        // Constructeur de la classe Promotion
        public Promotion(string name)
        {
            NamePromotion = name;
        }
    }

}


