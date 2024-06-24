using projetEducationNationale;
using System;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
public class MainProgram
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Souhaitez-vous ouvrir le menu eleve (1) ou cours (2): ");
        string choix = Console.ReadLine();
        if (choix == "1")
        {
            MenuEleves();
        }
        else if (choix == "2")
        {
            MenuCours();
        }
        else
        {
            Console.WriteLine("Choix inccorect. Veuillez choisir entre (1) et (2)");
        }

    }

    public static void MenuEleves()
    {
        Console.WriteLine("1. Lister les élèves\r\n2. Créer un nouvel élève\r\n3. Consulter un élève existant\r\n4. Ajouter une note et une appréciation pour un cours sur un élève existant\r\n5. Revenir au menu principal");
        string choix2 = Console.ReadLine();

        if (choix2 == "1")
        {
            //Aller à la liste des eleves
        }
        else if (choix2 == "2")
        {
            //Nouvel eleve
        }
        else if (choix2 == "3")
        {
            //consulter eleve existant
        }
        else if (choix2 == "4")
        {
            //Ajouter une note et appreciation
        }
        else if (choix2 == "5")
        {
            //Revenir au menu principal
        }
        else
        {
            Console.WriteLine("Valeur incorrecte.");


        }

    }

    public static void MenuCours()
    {
        Console.WriteLine("1. Lister les cours existants\r\n2. Ajouter un nouveau cours au programme\r\n3. Supprimer un cours par son identifiant\r\n4. Revenir au menu principal");

        string choix3 = Console.ReadLine();

        if (choix3 == "1")
        {
            //Lister les cours
        }
        else if (choix3 == "2")
        {
            //Nouveau cours
        }
        else if (choix3 == "3")
        {
            //supprimer un cours par son identifiant
        }
        else if (choix3 == "4")
        {
            //revenir au menu principal
        }
        else
        {
            Console.WriteLine("Choix incorrect.");
        }

    }
}
