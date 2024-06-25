using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale
{
    ppublic class GestionEleve
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
