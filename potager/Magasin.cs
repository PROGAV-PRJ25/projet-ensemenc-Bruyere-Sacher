public class Magasin
{
    public List<Semis> SemisDisponible { get; set; }
    public List<Outils> OutilsDisponible { get; set; }
    public int PrixTerrain { get; set; }
    public int PrixParcelle { get; set; }
    private Joueur joueur;

    public Magasin(Joueur joueur, int prixTerrain = 100, int prixParcelle = 25)
    {
        PrixTerrain = prixTerrain;
        PrixParcelle = prixParcelle;
        this.joueur = joueur;

        SemisDisponible = new List<Semis>
        {
            new Semis("Tomate", 3, true),
            new Semis("Piment", 4, true),
            new Semis("Nopale", 6, false),
            new Semis("Agave", 7, false),
            new Semis("Pasteque", 5, false),
            new Semis("Fleur de Tithonia", 6, false),
            new Semis("Haricot", 2, true),
            new Semis("Avocat", 8, true),
            new Semis("Papaye", 7, true),
            new Semis("Igname", 4, true)
        };

        OutilsDisponible = new List<Outils>
        {
            new Outils("Bâche​", 10),
            new Outils("Serre", 25),
            new Outils("Epouventail​", 8),
            new Outils("Clôture", 15)
        };
    }

    public void Menu()
    {
        int choix;
        do
        {
            Console.WriteLine("\n🛒 Bienvenue au Magasin !");
            Console.WriteLine("1. Acheter un semis");
            Console.WriteLine("2. Acheter un outil");
            Console.WriteLine("3. Acheter un terrain");
            Console.WriteLine("4. Acheter une parcelle");
            Console.WriteLine("5. Quitter le magasin");
            Console.Write("Ton choix : ");

            if (!int.TryParse(Console.ReadLine(), out choix)) choix = 0;

            switch (choix)
            {
                case 1:
                    AcheterSemis();
                    break;
                case 2:
                    AcheterOutils();
                    break;
                case 3:
                    AcheterTerrain(PrixTerrain);
                    break;
                case 4:
                    ChoisirEtAcheterParcelle();
                    break;
                case 5:
                    Console.WriteLine("👋 À bientôt !");
                    break;
                default:
                    Console.WriteLine("❌ Choix invalide.");
                    break;
            }
        } while (choix != 5);
        Console.WriteLine();
    }

    public void AcheterTerrain(int prixTerrain)
    {
        Console.WriteLine("Quel type de terrain veux-tu acheter ?");
        Console.WriteLine("1. Désertique");
        Console.WriteLine("2. Tropical");
        Console.WriteLine("3. Volcanique");
        Console.Write("Entre le numéro du terrain : ");

        if (!int.TryParse(Console.ReadLine(), out int choix) || choix < 1 || choix > 3)
        {
            Console.WriteLine("❌ Numéro invalide.");
            return;
        }

        Terrain terrain = choix switch
        {
            1 => new TerrainDesertique(),
            2 => new TerrainTropical(),
            3 => new TerrainVolcanique(),
            _ => null!
        };

        if (joueur.Argent >= prixTerrain)
        {
            joueur.Argent -= prixTerrain;
            joueur.Terrains.Add(terrain);
            Console.WriteLine($"✅ Terrain {terrain.Type} acheté !");
        }
        else
        {
            Console.WriteLine("❌ Fonds insuffisants !");
        }
    }

    public void ChoisirEtAcheterParcelle()
    {
        if (joueur.Terrains.Count == 0)
        {
            Console.WriteLine("❗ Tu n'as pas encore de terrain. Achète-en un d'abord.");
            return;
        }

        Console.WriteLine("Sur quel terrain veux-tu ajouter une parcelle ?");
        for (int i = 0; i < joueur.Terrains.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Terrain {joueur.Terrains[i].Type}");
        }

        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= joueur.Terrains.Count)
        {
            Terrain terrainChoisi = joueur.Terrains[index - 1];
            AcheterParcelle(terrainChoisi, PrixParcelle);
        }
        else
        {
            Console.WriteLine("❌ Choix invalide.");
        }
    }

    public void AcheterParcelle(Terrain terrain, int prixParcelle)
    {
        if (joueur.Argent >= prixParcelle)
        {
            joueur.Argent -= prixParcelle;
            terrain.AjouterParcelle();
            Console.WriteLine("✅ Parcelle ajoutée !");
        }
        else
        {
            Console.WriteLine("❌ Pas assez d'argent !");
        }
    }

    public void AcheterSemis()
    {
        Console.WriteLine("Quels semis veux-tu acheter ?");
        for (int i = 0; i < SemisDisponible.Count; i++)
        {
            var semis = SemisDisponible[i];
            Console.WriteLine($"{i + 1}. {semis.NomPlante} ({semis.PrixAchat} pièces)");
        }

        Console.Write("Entre le numéro du semis : ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > SemisDisponible.Count)
        {
            Console.WriteLine("❌ Numéro invalide.");
            return;
        }

        var semisChoisi = SemisDisponible[index - 1];

        if (joueur.Argent < semisChoisi.PrixAchat)
        {
            Console.WriteLine("❌ Tu n'as pas assez d'argent.");
            return;
        }

        joueur.Argent -= semisChoisi.PrixAchat;

        bool trouve = false;
        foreach (var semis in joueur.StockSemis)
        {
            if (semis.NomPlante == semisChoisi.NomPlante)
            {
                semis.Quantite += 1;
                trouve = true;
                break;
            }
        }

        if (!trouve)
        {
            joueur.StockSemis.Add(new Semis(semisChoisi.NomPlante, semisChoisi.PrixAchat, semisChoisi.EstProductionMultiple, 1));
        }

        Console.WriteLine($"✅ Tu as acheté : {semisChoisi.NomPlante}");
        Console.WriteLine("📦 Ton stock de semis contient :");
        foreach (var semis in joueur.StockSemis)
        {
            Console.WriteLine($"- {semis.NomPlante} : {semis.Quantite} semis");
        }
    }


   public void AcheterOutils()
{
    Console.WriteLine("Quels outils veux-tu acheter ?");
    for (int i = 0; i < OutilsDisponible.Count; i++)
    {
        var outil = OutilsDisponible[i];
        Console.WriteLine($"{i + 1}. {outil.NomOutil} ({outil.PrixAchat} pièces)");
    }

    Console.Write("Entre le numéro de l'outil : ");
    if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > OutilsDisponible.Count)
    {
        Console.WriteLine("❌ Numéro invalide.");
        return;
    }

    var outilChoisi = OutilsDisponible[index - 1];

    if (joueur.Argent < outilChoisi.PrixAchat)
    {
        Console.WriteLine("❌ Tu n'as pas assez d'argent.");
        return;
    }

    joueur.Argent -= outilChoisi.PrixAchat;

    bool existeDeja = false;
    foreach (var outil in joueur.StockOutils)
    {
        if (outil.NomOutil == outilChoisi.NomOutil)
        {
            outil.Quantite += 1;
            existeDeja = true;
            break;
        }
    }

    if (!existeDeja)
    {
        joueur.StockOutils.Add(new Outils(outilChoisi.NomOutil, outilChoisi.PrixAchat, 1));
    }

    Console.WriteLine($"✅ Tu as acheté : {outilChoisi.NomOutil}");
    Console.WriteLine("📦 Ton stock d'outils contient :");
    foreach (var outil in joueur.StockOutils)
    {
        Console.WriteLine($"- {outil.NomOutil} x{outil.Quantite}");
    }
}


}
