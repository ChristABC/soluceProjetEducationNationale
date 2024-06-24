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
        public int Id { get; set; }
        public string Nom { get; set; }

        public Cours(int id, string nom)
        {
            Id = id; 
            Nom = nom;
        }
        
    }
    
}

