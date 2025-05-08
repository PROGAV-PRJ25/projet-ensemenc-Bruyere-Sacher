Console.WriteLine("Hello, world!");

Joueur joueur = new Joueur("Sarah", 500);  // 500 pièces d'argent

// Ajouter quelques semis au stock
joueur.StockSemis.Add(new Semis("Tomate",5,true,5));
joueur.StockSemis.Add(new Semis("Piment",5,true,5));

// Créer un terrain avec 3 parcelles
Terrain terrain1 = new TerrainDesertique();

// Ajouter le terrain au joueur
joueur.Terrains.Add(terrain1);

// Affichage rapide
Console.WriteLine($"👩‍🌾 Joueur {joueur.Nom} créé avec {joueur.Argent} pièces !");
Console.WriteLine("🌱 Semis disponibles :");
foreach (var semis in joueur.StockSemis)
{
    Console.WriteLine($"- {semis.NomPlante} x{semis.Quantite}");
}
Console.WriteLine("🌍 Terrains disponibles :");
foreach (var terrain in joueur.Terrains)
{
    foreach (var parcelle in terrain.Parcelles)
    {
        Console.WriteLine($"- Parcelle n°{parcelle.NumeroParcelle} sur le {terrain} avec {terrain.Parcelles.Count} parcelles");
    }
}

joueur.Planter();