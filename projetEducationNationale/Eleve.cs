using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale
{
    public class Eleve
    {
        public int ID { get; set; }//Identifiant privée eleve
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

    public Eleve CreationEleve()
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

        return nouvelEleve;

    }

}

public class Note
{
    public string Cours { get; set; }
    public int ValeurNote { get; set; }
    public string Appreciation { get; set; }

    public Note(string cours, int valeurNote, string appreciation)
    {
        Cours = cours;
        ValeurNote = valeurNote;
        Appreciation = appreciation;
    }
}


public class EleveControl
{

    public List<Eleve> eleves = new List<Eleve>();

    public void AjouterEleve(Eleve eleve)
    {
        eleves.Add(eleve);
    }

    public List<Eleve> ObtenirListeEleves()
    {
        return eleves;
    }

    public Eleve ObtenirEleveParId(int id)
    {
        return eleves.Find(eleve => eleve.ID == id);
    }


    public void AfficherDetailsEleve(Eleve eleve)
    {
        if (eleve != null)
        {
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine($"ID: {eleve.ID}, Nom: {eleve.Nom}, Prénom: {eleve.Prenom}, Date de Naissance: {eleve.DateDeNaissance.ToShortDateString()}");
            Console.WriteLine("Notes :");
            foreach (var note in eleve.Notes)
            {
                Console.WriteLine($"Cours: {note.Matiere}, Note: {note.ValeurNote}, Appréciation: {note.Appreciation}");
            }
        }
        else
        {
            Console.WriteLine("Élève non trouvé.");
        }
    }
    public void AfficherListeEleves()
    {
        foreach (Eleve eleve in eleves)
        {

            Console.WriteLine($"Nom: {eleve.Nom}, Prénom: {eleve.Prenom}");
        }
    }
}
}



