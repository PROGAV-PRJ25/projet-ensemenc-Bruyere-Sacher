Console.WriteLine("🌿 Bienvenue dans le simulateur de jardinage !");

// Créer le joueur et la météo
Joueur joueur = new Joueur("Sarah", 500);
Meteo meteo = new Meteo();
Simulation simulation = new Simulation(joueur, meteo);

// Ajouter des semis au stock du joueur
joueur.StockSemis.Add(new Semis("Tomate", 5, true, 5));
joueur.StockSemis.Add(new Semis("Piment", 5, true, 5));

// Créer et ajouter un terrain avec 3 parcelles
Terrain terrain1 = new TerrainDesertique();
joueur.Terrains.Add(terrain1);

// Créer une instance de magasin et l'associer au joueur
joueur.Magasin = new Magasin(joueur);  // On passe le joueur dans le constructeur de Magasin

// Affichage des infos initiales
Console.WriteLine($"\n👩‍🌾 Joueur {joueur.Nom} avec {joueur.Argent} pièces !");
Console.WriteLine("🌱 Semis en stock :");
foreach (var semis in joueur.StockSemis)
{
    Console.WriteLine($"- {semis.NomPlante} x{semis.Quantite}");
}

Console.WriteLine("🌍 Terrains :");
foreach (var terrain in joueur.Terrains)
{
    Console.WriteLine($"- {terrain.Type} avec {terrain.Parcelles.Count} parcelles");
}

// Lancer la plantation initiale
//joueur.Planter();

// Lancer la simulation pour 5 semaines
simulation.SimulerJeu(10);



