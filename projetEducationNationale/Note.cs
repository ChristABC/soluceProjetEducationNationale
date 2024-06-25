using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale
{
    public class Note
    {
        public string Matiere { get; set; }
        public int ValeurNote { get; set; }
        public string Appreciation { get; set; }
        public List<Note> Notes { get; set; }

        public Note(string cours, int valeurNote, string appreciation)
        {
            Matiere = cours;
            ValeurNote = valeurNote;
            Appreciation = appreciation;
        }
        public Note(string cours, int valeurNote)
        {
            Matiere = cours;
            ValeurNote = valeurNote;
        }

    }
}
