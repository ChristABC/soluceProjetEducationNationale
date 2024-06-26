using projetEducationNationale;
using System;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
public class MainProgram
{
    public static void Main(string[] args)
    {
        bool continuerProgramme = true;
        GestionEleve gestion = new GestionEleve();
        MenuGestion menu = new MenuGestion();

        while (continuerProgramme)
        {
            Console.WriteLine("Souhaitez-vous ouvrir le menu élève (1) ou cours (2): ");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    menu.MenuEleves();
                    break;
                case "2":
                    menu.MenuCours();
                    break;
                default:

                    Console.WriteLine("Choix inccorect. Veuillez choisir entre (1) et (2)");
                    break;
            }
            Console.WriteLine("Voulez-vous continuer ?(oui/non) ");
            string continuer = Console.ReadLine();

            if (continuer.ToLower() != "oui")
            {
                continuerProgramme &= false;

            }

        }

    }
}

