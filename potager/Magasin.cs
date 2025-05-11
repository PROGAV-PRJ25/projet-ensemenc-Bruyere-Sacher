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
            new Semis("Tomate 🍅", 3, true),
            new Semis("Piment 🌶️", 4, true),
            new Semis("Nopale 🌵", 6, false),
            new Semis("Agave 🌵", 7, false),
            new Semis("Pasteque 🍉", 5, false),
            new Semis("Fleur de Tithonia 🌺", 6, false),
            new Semis("Haricot 🫘", 2, true),
            new Semis("Avocat 🥑", 8, true),
            new Semis("Papaye 🥭", 7, true),
            new Semis("Igname 🍠", 4, true)
        };

        OutilsDisponible = new List<Outils>
        {
            new Outils("Bâche ☂️​", 10),
            new Outils("Serre ⛺", 25),
            new Outils("Epouventail ​⛄​", 8),
            new Outils("Clôture 🚧 ", 15)
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
    }

    public void AcheterTerrain(int prixTerrain)
    {
        Console.WriteLine("Quel type de terrain voulez-vous acheter ? (Désertique / Tropical / Volcanique)");
        string choix = Console.ReadLine()!;
        Terrain? terrain = null;

        switch (choix.ToLower())
        {
            case "désertique":
                terrain = new TerrainDesertique();
                break;
            case "tropical":
                terrain = new TerrainTropical();
                break;
            case "volcanique":
                terrain = new TerrainVolcanique();
                break;
            default:
                Console.WriteLine("❌ Type de terrain invalide.");
                return;
        }

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
        foreach (var semis in SemisDisponible)
        {
            Console.WriteLine($"- {semis.NomPlante} ({semis.PrixAchat} pièces)");
        }

        string nomSemis = Console.ReadLine()!;
        Semis semisChoisi = null;

        foreach (var s in SemisDisponible)
        {
            if (s.NomPlante == nomSemis)
            {
                semisChoisi = s;
                break;
            }
        }

        if (semisChoisi == null)
        {
            Console.WriteLine("❌ Ce semis n'est pas disponible.");
            return;
        }

        if (joueur.Argent < semisChoisi.PrixAchat)
        {
            Console.WriteLine("❌ Tu n'as pas assez d'argent.");
            return;
        }

        joueur.Argent -= semisChoisi.PrixAchat;
        joueur.StockSemis.Add(semisChoisi);
        Console.WriteLine($"✅ Tu as acheté : {semisChoisi.NomPlante}");
    }

    public void AcheterOutils()
    {
        Console.WriteLine("Quels outils veux-tu acheter ?");
        foreach (var outil in OutilsDisponible)
        {
            Console.WriteLine($"- {outil.NomOutil} ({outil.PrixAchat} pièces)");
        }

        string nomOutil = Console.ReadLine()!;
        Outils outilChoisi = null;

        foreach (var o in OutilsDisponible)
        {
            if (o.NomOutil == nomOutil)
            {
                outilChoisi = o;
                break;
            }
        }

        if (outilChoisi == null)
        {
            Console.WriteLine("❌ Cet outil n'est pas disponible.");
            return;
        }

        if (joueur.Argent < outilChoisi.PrixAchat)
        {
            Console.WriteLine("❌ Tu n'as pas assez d'argent.");
            return;
        }

        joueur.Argent -= outilChoisi.PrixAchat;
        joueur.StockOutils.Add(outilChoisi);
        Console.WriteLine($"✅ Tu as acheté : {outilChoisi.NomOutil}");
    }
}
