using projetEducationNationale.ManagerFolder;
using projetEducationNationale.SaveManager;
using Serilog;


LoggerConfig.Configuration();

// Charge les données utilisateur depuis la sauvegarde
MenuGestion.DonneesUtilisateur donneesUtilisateur = SauvegardeHelper.Load();
//Log.Debug("chargement");
//Log.Information();
//Log.Warning();
//Log.Fatal


// Cré une instance de MenuGestion en utilisant les données utilisateur chargées
MenuGestion menuGestion = new MenuGestion(donneesUtilisateur);

// Appel le menu principal pour commencer l'interaction avec l'utilisateur
menuGestion.MenuPrincipal();

// Sauvegarde les données utilisateur après les modifications
SauvegardeHelper.Save(donneesUtilisateur);


Log.Information("Info test !!");


