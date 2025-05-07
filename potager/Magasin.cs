public class Magasin
{
    public List<Semis> SemisDisponible { get; set; }
    public List<Outils> OutilsDisponible { get; set; }
    public int PrixTerrain { get; set; }
    public int PrixParcelle { get; set; }
    private Joueur joueur;


    public Magasin ( int prixTerrain = 100, int prixParcelle=25, Joueur joueur) //adapter les prix
    {
        PrixTerrain=prixTerrain;
        PrixParcelle=prixParcelle;
        this.joueur = joueur;

        SemisDisponible=new List<Semis>
        {
            new Semis("Tomate 🍅",3, true), //nom et prix et production multiple 
            new Semis("Piment 🌶️",4,true),
            new Semis("Nopale 🌵",6,false),
            new Semis("Agave 🌵",7,false),
            new Semis("Pasteque 🍉",5,false),
            new Semis("Fleur de Tithonia 🌺",6,false),
            new Semis("Haricot 🫘",2,true),
            new Semis("Avocat 🥑",8,true),
            new Semis("Papaye 🥭",7,true),
            new Semis("Igname 🍠",4,true)

        };


        OutilsDisponible=new List<Outils>
        {
            new Outils("Bâche ☂️​",10), //nom et prix protège de la grêle que mode urgence 
            new Outils("Serre ⛺",25), // augmente la température 
            new Outils ("Epouventail ​⛄​",8),// protège des oiseaux que mode urgence
            new Outils ("Clôture 🚧 ",15 )//protège des pacaris que mode urgence 
        };
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
                Console.WriteLine("Type de terrain invalide.");
                return;
        }

        if (Joueur.Argent >= prixTerrain)
        {
            joueur.Argent -= prixTerrain;
            joueur.Terrains.Add(terrain);
            Console.WriteLine($"Terrain {terrain.Type} acheté !");
        }
        else
        {
            Console.WriteLine("Fonds insuffisants !");
        }
    }

    public void AcheterParcelle(Terrain terrain, int prixParcelle)
    {   
        if (joueur.Argent >= prixParcelle)
        {
            joueur.Argent -= prixParcelle;
            terrain.AjouterParcelle();
            Console.WriteLine("Parcelle ajoutée !");
        }
        else
        {
            Console.WriteLine("Pas assez d'argent !");
        }
    }
        
    public void AcheterSemis(Semis semis, int quantite)
    {
        Console.WriteLine("Quel semis veux-tu acheter ?");
        string nomSemis = Console.ReadLine()!;

        Semis? semisChoisi = null;
        foreach (var semis in SemisDisponible)
        {
            if (semis.NomPlante == nomSemis)
            {
                semisChoisi = semis;
            }
        }

        if (semisChoisi == null)
        {
            Console.WriteLine("Ce semis n'est pas disponible.");
            return;
        }

        if (joueur.Argent < semisChoisi.PrixAchat)
        {
            Console.WriteLine("Tu n'as pas assez d'argent pour acheter ce semis.");
            return;
        }

        joueur.Argent -= semisChoisi.PrixAchat;
        joueur.StockSemis.Add(semisChoisi); //Modifier pour ajouter quantité si recolte dans le stock

        Console.WriteLine($"Tu as acheté : {semisChoisi.NomPlante} pour {semisChoisi.PrixAchat} pièces.");
    }

    public void AcheterOutils(Semis semis, int quantite)
    {
        Console.WriteLine("Quel outil veux-tu acheter ?");
        string nomOutil = Console.ReadLine()!;

        Outils? outilChoisi = null;
        foreach (var outil in OutilsDisponible)
        {
            if (outil.NomOutil == nomOutil)
            {
                outilChoisi = outil;
            }
        }

        if (outilChoisi == null)
        {
            Console.WriteLine("Cet outil n'est pas disponible.");
            return;
        }

        if (joueur.Argent < outilChoisi.PrixAchat)
        {
            Console.WriteLine("Tu n'as pas assez d'argent pour acheter cet outil.");
            return;
        }

        joueur.Argent -= outilChoisi.PrixAchat;
        joueur.InventaireOutils.Add(outilChoisi);

        Console.WriteLine($"Tu as acheté : {outilChoisi.NomOutil} pour {outilChoisi.PrixAchat} pièces.");
    }
}