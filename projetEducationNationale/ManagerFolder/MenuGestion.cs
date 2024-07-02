﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projetEducationNationale.Modeles;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projetEducationNationale.ManagerFolder
{

    public class MenuGestion
    {
        private DonneesUtilisateur donneesUtilisateur;
        private GestionEleve gestionEleve;
        private GestionCours gestionCours;

        public MenuGestion(DonneesUtilisateur donnees)
        {
            donneesUtilisateur = donnees;
            gestionEleve = new GestionEleve(donnees);
            gestionCours = new GestionCours(donnees);
        }

        public class DonneesUtilisateur
        {
            public List<Cours> listCours { get; set; } = new List<Cours>();
            public List<Eleve> listEleve { get; set; } = new List<Eleve>();
            public List<Promotion> listPromotion { get; set; } = new List<Promotion>();
        }

        public void MenuPrincipal()
        {
            while (true)
            {
                Console.WriteLine("1. Gestion des élèves");
                Console.WriteLine("2. Gestion des cours");
                Console.WriteLine("3. Quitter");

                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        gestionEleve.MenuEleves();
                        break;
                    case "2":
                        gestionCours.MenuCours();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Choix incorrect.");
                        break;
                }
            }
        }
    }
}