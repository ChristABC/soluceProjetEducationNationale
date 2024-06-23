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

        //Trouver un moyen pour notes; poser questions sur les dictionnaires
        //Ce que fait Pauline

        // Creation constructeur ELEVE

        public Eleve(int id, string nom, string prenom, DateTime dateDeNaissance)
        {
            ID = id;
            Nom = nom;
            Prenom = prenom;
            DateDeNaissance = dateDeNaissance;
        }

    }
}
