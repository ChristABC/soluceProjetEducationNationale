﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetEducationNationale.Modeles
{
    public class Cours
    {
        //Accesseurs aux infos sur le cours donc id cours et nom
        public int coursID { get; set; }
        public string Nom { get; set; }

        public Cours(int id, string nom)
        {
            coursID = id;
            Nom = nom;
        }

    }

}

