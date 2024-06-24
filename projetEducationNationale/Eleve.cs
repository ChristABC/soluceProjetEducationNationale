using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale
{
    public class Eleve
    {
        private int ID { get; set; }//Identifiant privée eleve
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateDeNaissance { get; set; }

       
        public Eleve(int id, string nom, string prenom, DateTime dateDeNaissance)
        {
            ID = id;
            Nom = nom;
            Prenom = prenom;
            DateDeNaissance = dateDeNaissance;
        }
        

        public void consulterEleves ()
        {
            Console.WriteLine("----------------------------------------------------------------------\n");
            Console.WriteLine("Nom: " + Nom);
            Console.WriteLine("Prenom: "+ Prenom);
            Console.WriteLine("Date de Naissance : " + DateDeNaissance);

        }

    }
}
