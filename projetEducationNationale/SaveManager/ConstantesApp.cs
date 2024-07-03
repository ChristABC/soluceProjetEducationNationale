using Newtonsoft.Json;
using projetEducationNationale.ManagerFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace projetEducationNationale
{
    public static class ConstantesApp
    {
        public static readonly string Chemin;

        static ConstantesApp()
        {
            // Obtenir le répertoire de travail actuel
            string currentDirectory = Directory.GetCurrentDirectory();

            // Séparer le chemin au niveau du répertoire 'bin'
            string[] test = currentDirectory.Split(new string[] { "bin" }, StringSplitOptions.None);

            // Garder la partie avant 'bin'
            Chemin = test[0];
        }
    }

    public static class SauvegardeHelper
    {
        private static readonly string FichierChemin = Path.Combine(ConstantesApp.Chemin, "donnees.json");

        public static void Save(MenuGestion.DonneesUtilisateur donnees)
        {
            // Sérialiser l'instance en une chaîne JSON
            string jsonString = JsonConvert.SerializeObject(donnees, Newtonsoft.Json.Formatting.Indented);

            // Écrire la chaîne JSON dans un fichier
            File.WriteAllText(FichierChemin, jsonString);
        }

        public static MenuGestion.DonneesUtilisateur Load()
        {
            if (!File.Exists(FichierChemin)) // Vérifier si le fichier existe au chemin spécifié
            {
                return new MenuGestion.DonneesUtilisateur();
            }

            // Lire la chaîne JSON à partir du fichier
            string jsonString = File.ReadAllText(FichierChemin);

            // Désérialiser la chaîne JSON en une instance de DonneesUtilisateur
            return JsonConvert.DeserializeObject<MenuGestion.DonneesUtilisateur>(jsonString);
        }
    }
}