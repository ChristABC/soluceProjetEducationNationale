using projetEducationNationale.ManagerFolder;
using projetEducationNationale.SaveManager;
using projetEducationNationale.Modeles;
using Serilog;
using System;
using projetEducationNationale;

// Configuration du logger
LoggerConfig.Configuration();

try
{
    // Étape 1 : Charger les données utilisateur depuis la sauvegarde
    MenuGestion.DonneesUtilisateur donneesUtilisateur = SauvegardeHelper.Load();

    // Étape 2 : Créer une instance de MenuGestion avec les données utilisateur chargées
    MenuGestion menuGestion = new MenuGestion(donneesUtilisateur);

    // Étape 3 : Appeler le menu principal pour commencer l'interaction avec l'utilisateur
    menuGestion.MenuPrincipal();

    // Étape 4 : Sauvegarder les données utilisateur après les modifications
    SauvegardeHelper.Save(donneesUtilisateur);

    // Log d'information pour confirmer la sauvegarde
    Log.Information("Les données utilisateur ont été sauvegardées avec succès.");
}
catch (Exception ex)
{
    // Log de l'erreur fatale en cas d'exception
    Log.Fatal(ex, "Une erreur fatale est survenue lors de l'exécution du programme.");
}
finally
{
    // Assurer que tous les logs sont correctement écrits et les ressources libérées
    Log.CloseAndFlush();
}




