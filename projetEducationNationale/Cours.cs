using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale
{
    public class Cours
    {
        //Accesseurs aux infos sur le cours donc id cours et nom
        public int numberId { get; set; }
        public string Nom { get; set; }
        public List<string> Courses { get; set; } = new List<string>();

        public Cours(int id, string nom)
        {
            numberId = id;
            Nom = nom;
        }

    }

}

