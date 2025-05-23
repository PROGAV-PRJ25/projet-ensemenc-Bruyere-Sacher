var menu = new MenuDebut();
Partie? partie = menu.AfficherMenu();

if (partie == null)
{
    Console.WriteLine("❌ Erreur : la partie n'a pas pu être initialisée.");
    return;
}

// Vérification de null sur les composants de la partie
if (partie.Joueur == null)
{
    Console.WriteLine("❌ Erreur : aucun joueur n'a été créé.");
    return;
}

if (partie.Simulation == null)
{
    Console.WriteLine("❌ Erreur : la simulation est introuvable.");
    return;
}
Guide.ReglesJeu();

Joueur joueur = partie.Joueur;
Simulation simulation = partie.Simulation;

Console.WriteLine($"\n👩‍🌾 Joueur {joueur.Nom} avec {joueur.Argent} pièces !");


simulation.SimulerJeu(100); // permet au joueur de faire max 100 semaines
