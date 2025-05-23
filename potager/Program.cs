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

// if (joueur.StockSemis != null)
// {
//     foreach (var semis in joueur.StockSemis)
//     {
//         Console.WriteLine($"- {semis.NomPlante} x{semis.Quantite}");
//     }
// }

// Console.WriteLine("🌍 Terrains :");

// if (joueur.Terrains != null)
// {
//     foreach (var terrain in joueur.Terrains)
//     {
//         string type = terrain.Type ?? "Type inconnu";
//         Console.WriteLine($"- {type} avec {terrain.Parcelles?.Count ?? 0} parcelles");
//     }
// }


simulation.SimulerJeu(100); // permet au joueur de faire max 100 semaines
