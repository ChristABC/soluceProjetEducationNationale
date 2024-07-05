using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale.Modeles
{
    public class Cours
    {
        //Accesseurs aux informations sur les attributs de la classe cours donc coursID cours et Nom
        public int coursID { get; set; }
        public string Nom { get; set; }

        public Cours(int id, string nom)
        {
            coursID = id;
            Nom = nom;
        }

    }

}

