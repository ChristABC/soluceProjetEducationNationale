using Newtonsoft.Json;
using projetEducationNationale.ManagerFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace projetEducationNationale.SaveManager
{

    public class ConstantesApp
    {
        public static string Chemin = Directory.GetCurrentDirectory();
    }

    public static class SauvegardeHelper
    {
        private static readonly string FichierChemin = Path.Combine(ConstantesApp.Chemin, "donnees.json");

        public static void Save(MenuGestion.DonneesUtilisateur donnees)
        {
            // Sérialiser l'instance en une chaîne JSON
            string jsonString = JsonConvert.SerializeObject(donnees, Newtonsoft.Json.Formatting.Indented);

            // Écrire la chaîne JSON dans un fichier
            File.WriteAllText(FichierChemin, jsonString); // Utiliser le chemin du fichier de la classe des constantes
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