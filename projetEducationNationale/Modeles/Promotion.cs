using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projetEducationNationale.SaveManager;

namespace projetEducationNationale.Modeles
{
    public class Promotion
    {
        public string NamePromotion { get; set; }

        // Constructeur de la classe Promotion
        public Promotion(string name)
        {
            NamePromotion = name;
        }

    }
}
