using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale
{
    public class Eleve
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public List<Note> Notes { get; set; }




        public Eleve(int id, string nom, string prenom, DateTime dateDeNaissance)
        {
            ID = id;
            Nom = nom;
            Prenom = prenom;
            DateDeNaissance = dateDeNaissance;
            Notes = new List<Note>();
        }
        public void AjouterNote(Note note)
        {
            Notes.Add(note);
        }
    }
    public class Note
    {
        public string Matiere { get; set; }
        public double ValeurNote { get; set; }
        public string Appreciation { get; set; }

        public Note(string cours, double valeurNote, string appreciation)
        {
            Matiere = cours;
            ValeurNote = valeurNote;
            Appreciation = appreciation;
        }
    }
}



