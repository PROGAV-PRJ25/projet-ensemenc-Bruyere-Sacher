public class Magasin
{
    public List<Semis> SemisDisponible { get; set; }
    public List<Outils> OutilsDisponible { get; set; }
    public int PrixTerrain { get; set; }
    public int PrixParcelle { get; set; }
    private Joueur Joueur;

    public Magasin(Joueur joueur, int prixTerrain = 100, int prixParcelle = 25)
    {
        PrixTerrain = prixTerrain;
        PrixParcelle = prixParcelle;
        Joueur = joueur;

        // Liste  des semis vendus dans le magasin
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

        // Liste  des objets vendus dans le magasin
        OutilsDisponible = new List<Outils>
        {
            new Outils("Bâche​", 10),
            new Outils("Serre", 25),
            new Outils("Epouventail​", 8),
            new Outils("Clôture", 15)
        };
    }

    //// Affiche le menu principal du magasin et traite les choix du joueur
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

            // Exécution de l'action choisie
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
    
    // Permet au joueur d'acheter un terrain en fonction d’un type de terrain choisi
    public void AcheterTerrain(int prixTerrain)
    {
        Console.WriteLine("\nQuel type de terrain veux-tu acheter ?");
        Console.WriteLine("0. Annuler");
        Console.WriteLine("1. Désertique");
        Console.WriteLine("2. Tropical");
        Console.WriteLine("3. Volcanique");
        Console.Write("Entre le numéro du terrain : ");

        if (!int.TryParse(Console.ReadLine(), out int choix))
        {
            Console.WriteLine("❌ Entrée invalide.");
            return;
        }

        if (choix == 0)
        {
            Console.WriteLine("✅ Achat annulé.");
            return;
        }

        Terrain terrain;
        // Création du type de terrain selon le choix
        switch (choix)
        {
            case 1:
                terrain = new TerrainDesertique();
                break;
            case 2:
                terrain = new TerrainTropical();
                break;
            case 3:
                terrain = new TerrainVolcanique();
                break;
            default:
                Console.WriteLine("❌ Numéro invalide.");
                return;
        }

        // Vérifie si le joueur a assez d'argent
        if (Joueur.Argent >= prixTerrain)
        {
            Joueur.Argent -= prixTerrain;
            Joueur.Terrains.Add(terrain);
            Console.WriteLine($"✅ Terrain {terrain.Type} acheté !");
        }
        else
        {
            Console.WriteLine("❌ Fonds insuffisants !");
        }
    }

    // Permet au joueur de sélectionner un terrain pour y ajouter une nouvelle parcelle
    public void ChoisirEtAcheterParcelle()
    {
        if (Joueur.Terrains.Count == 0)
        {
            Console.WriteLine("❗ Tu n'as pas encore de terrain. Achète-en un d'abord.");
            return;
        }

        Console.WriteLine("\nSur quel terrain veux-tu ajouter une parcelle ?");
        Console.WriteLine("0. Annuler");

        for (int i = 0; i < Joueur.Terrains.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Terrain {Joueur.Terrains[i].Type}");
        }

        Console.Write("Entre le numéro du terrain : ");
        if (!int.TryParse(Console.ReadLine(), out int index))
        {
            Console.WriteLine("❌ Entrée invalide.");
            return;
        }

        if (index == 0)
        {
            Console.WriteLine("✅ Action annulée.");
            return;
        }

        if (index < 1 || index > Joueur.Terrains.Count)
        {
            Console.WriteLine("❌ Choix invalide.");
            return;
        }

        Terrain terrainChoisi = Joueur.Terrains[index - 1];
        AcheterParcelle(terrainChoisi, PrixParcelle);
    }


    // Ajoute une parcelle à un terrain donné si le joueur a assez d'argent
    public void AcheterParcelle(Terrain terrain, int prixParcelle)
    {
        if (Joueur.Argent >= prixParcelle)
        {
            Joueur.Argent -= prixParcelle;
            terrain.AjouterParcelle();
            Console.WriteLine("✅ Parcelle ajoutée !");
        }
        else
        {
            Console.WriteLine("❌ Pas assez d'argent !");
        }
    }

    // Permet au joueur d’acheter des semis
    public void AcheterSemis()
    {
        Console.WriteLine("\nQuels semis veux-tu acheter ?");
        Console.WriteLine("0. Annuler");

        for (int i = 0; i < SemisDisponible.Count; i++)
        {
            var semis = SemisDisponible[i];
            Console.WriteLine($"{i + 1}. {semis.NomPlante} ({semis.PrixAchat} pièces)");
        }

        Console.Write("Entre le numéro du semis : ");
        if (!int.TryParse(Console.ReadLine(), out int index))
        {
            Console.WriteLine("❌ Entrée invalide.");
            return;
        }

        if (index == 0)
        {
            Console.WriteLine("✅ Achat annulé.");
            return;
        }

        if (index < 1 || index > SemisDisponible.Count)
        {
            Console.WriteLine("❌ Numéro invalide.");
            return;
        }

        var semisChoisi = SemisDisponible[index - 1];

        if (Joueur.Argent < semisChoisi.PrixAchat)
        {
            Console.WriteLine("❌ Tu n'as pas assez d'argent.");
            return;
        }

        Joueur.Argent -= semisChoisi.PrixAchat;

        // Ajoute le semis au stock du joueur ou augmente la quantité si déjà présent
        bool trouve = false;
        foreach (var semis in Joueur.StockSemis)
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
            Joueur.StockSemis.Add(new Semis(
                semisChoisi.NomPlante,
                semisChoisi.PrixAchat,
                semisChoisi.EstProductionMultiple,
                1
            ));
        }

        Console.WriteLine($"✅ Tu as acheté : {semisChoisi.NomPlante}");
        Console.WriteLine("📦 Ton stock de semis contient :");
        foreach (var semis in Joueur.StockSemis)
        {
            Console.WriteLine($"- {semis.NomPlante} : {semis.Quantite} semis");
        }
    }


     // Permet au joueur d’acheter un outil
    public void AcheterOutils()
    {
        Console.WriteLine("\nQuels outils veux-tu acheter ?");
        Console.WriteLine("0. Annuler");

        for (int i = 0; i < OutilsDisponible.Count; i++)
        {
            var outil = OutilsDisponible[i];
            Console.WriteLine($"{i + 1}. {outil.NomOutil} ({outil.PrixAchat} pièces)");
        }

        Console.Write("Entre le numéro de l'outil : ");
        if (!int.TryParse(Console.ReadLine(), out int index))
        {
            Console.WriteLine("❌ Entrée invalide.");
            return;
        }

        if (index == 0)
        {
            Console.WriteLine("✅ Achat annulé.");
            return;
        }

        if (index < 1 || index > OutilsDisponible.Count)
        {
            Console.WriteLine("❌ Numéro invalide.");
            return;
        }

        var outilChoisi = OutilsDisponible[index - 1];

        if (Joueur.Argent < outilChoisi.PrixAchat)
        {
            Console.WriteLine("❌ Tu n'as pas assez d'argent.");
            return;
        }

        Joueur.Argent -= outilChoisi.PrixAchat;

        // Ajoute l’outil ou augmente la quantité si déjà présent
        bool existeDeja = false;
        foreach (var outil in Joueur.StockOutils)
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
            Joueur.StockOutils.Add(new Outils(outilChoisi.NomOutil, outilChoisi.PrixAchat, 1));
        }

        Console.WriteLine($"✅ Tu as acheté : {outilChoisi.NomOutil}");
        Console.WriteLine("📦 Ton stock d'outils contient :");
        foreach (var outil in Joueur.StockOutils)
        {
            Console.WriteLine($"- {outil.NomOutil} x{outil.Quantite}");
        }
    }
}