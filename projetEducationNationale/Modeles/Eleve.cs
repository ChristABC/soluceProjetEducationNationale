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
        public string Moyenne;

        public List<Note> Notes { get; set; }


        public Eleve(int id, string nom, string prenom, DateTime dateDeNaissance, string promotion)
        {
            ID = id;
            Nom = nom;
            Prenom = prenom;
            DateDeNaissance = dateDeNaissance;
            PromotionEleve = promotion;
            Notes = new List<Note>();
        }
        public void AjouterNote(Note note)
        {
            Notes.Add(note);
        }
        public double MoyenneNotesEleve()
        {
            if (Notes.Count == 0)
            {
                return double.NaN;  // Indiquer "non noté" avec NaN
            }
            else
            {
                double moyenneBrute = Notes.Average(note => note.ValeurNote);
                return ArrondirMoyenne(moyenneBrute);
            }
        }

        public double ArrondirMoyenne(double moyenne)
        {
            return Math.Round(moyenne, 2); // Arrondir à 2 décimales
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

}




