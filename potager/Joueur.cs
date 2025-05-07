public class Joueur
{
    public string Nom { get; set; }
    public double Argent { get; set; }
    public List<Semis> StockSemis { get; set; }
    public List<Terrain> Terrains { get; set; }
    public List<Recoltes> MesRecoltes { get; set; }
    public List<Outils> StockOutils { get; set; }


    public Joueur(string nom, int argent)
    {
        Nom = nom;
        Argent = argent;
        StockSemis = new List<Semis>();
        Terrains = new List<Terrain>();
        StockOutils=new List<Outils>();
        MesRecoltes=new List<Recoltes>();
    }


    public void Planter()
    {
        Console.WriteLine("Quel type de plante veux tu planter? Voici les semis que tu possède:");
        foreach (var semis in this.StockSemis)
        {
            Console.WriteLine($"- {semis.NomPlante} x{semis.Quantite}");
        }
        string choixPlante = Convert.ToString(Console.ReadLine()!);
    }
    
    public void Arroser()
    {
        Console.WriteLine("Tapez 1 pour arroser un terrain entier ou tapez 2 pour arroser seulement une parcelle?");
        int decisionArroser2 = Convert.ToInt32(Console.ReadLine()!);
        while (decisionArroser2 != 1 && decisionArroser2 != 2)
        {
            Console.WriteLine("Erreur! Tapez 1 pour arroser un terrain entier ou tapez 2 pour arroser seulement une parcelle?");
            decisionArroser2 = Convert.ToInt32(Console.ReadLine()!);
        }
        Console.WriteLine("Dans quel terrain souhaite tu arroser?");
        string terrainArrosage = Convert.ToString(Console.ReadLine()!);
        Terrain? terrainChoisi = null;
        foreach (var terrain in Terrains)
        {
            if (terrain.Type == terrainArrosage)
            {
                terrainChoisi = terrain;
            }
        }
        while ((terrainChoisi == null)||(!Terrains.Contains(terrainChoisi)))
        {
            Console.WriteLine("Ce terrain n'existe pas! Dans quel terrain souhaite tu arroser?");
            terrainArrosage = Convert.ToString(Console.ReadLine()!);
            foreach (var terrain in Terrains)
            {
                if (terrain.Type == terrainArrosage)
                {
                    terrainChoisi = terrain;
                }
            }
        }
        if (decisionArroser2==1)
        {
            ArroserTerrain(terrainChoisi);
        }
        else
        {
            Console.WriteLine("Quel est le numéro de la parcelle que tu souhaite arroser?");
            int parcelleArrosage = Convert.ToInt32(Console.ReadLine()!);
            Parcelle? parcelleChoisi = null;
            foreach (var parcelle in terrainChoisi.Parcelles)
            {
                if (parcelle.NumeroParcelle == parcelleArrosage)
                {
                    parcelleChoisi = parcelle;
                }
            }
            while ((parcelleChoisi == null)||(!terrainChoisi.Parcelles.Contains(parcelleChoisi)))
            {
                Console.WriteLine("Cette parcelle n'existe pas! Quel est le numéro de la parcelle que tu souhaite arroser?");
                parcelleArrosage = Convert.ToInt32(Console.ReadLine()!);
                foreach (var parcelle in terrainChoisi.Parcelles)
                {
                    if (parcelle.NumeroParcelle == parcelleArrosage)
                    {
                        parcelleChoisi = parcelle;
                    }
                }  
            }
            ArroserParcelle(parcelleChoisi);
        }
    }
    public void ArroserParcelle(Parcelle parcelle)
    {
        if(parcelle.HumiditeParcelle+10<100)
            {
                parcelle.HumiditeParcelle+=10;
            }
            else
            {
                parcelle.HumiditeParcelle=100;
            }
        Console.WriteLine($"La parcelle n°{parcelle.NumeroParcelle} du terrain {parcelle.TerrainAssocie.Type} a été arrosé.");
    }
    public void ArroserTerrain(Terrain terrain)
    {
        foreach (var parcelle in terrain.Parcelles)
        {
            if(parcelle.HumiditeParcelle+10<100)
            {
                parcelle.HumiditeParcelle+=10;
            }
            else
            {
                parcelle.HumiditeParcelle=100;
            }
        }
        Console.WriteLine($"Le terrain {terrain.Type} a été arrosé.");
    }
    public void Recolter(Parcelle parcelle)
    {
       if (parcelle.Plante != null && !parcelle.Plante.EstMorte && parcelle.Plante.Croissance >= 100)
        {
            Console.WriteLine($"{parcelle.Plante.Nom} récoltée depuis la parcelle {parcelle.NumeroParcelle}.");
            int quantiteRecolte = parcelle.Plante.AvoirQuantiteRecolte();
            bool trouveDansRecolte = false;

            foreach (var recolte in MesRecoltes)
            {
                if (recolte.TypePlante == parcelle.Plante.Nom)
                {
                    recolte.Quantite += quantiteRecolte;   //à ajuster quand y'a plusieurs produits pour une récolte
                    trouveDansRecolte = true;
                }
            }
            if (!trouveDansRecolte)
            {
                MesRecoltes.Add(new Recoltes(parcelle.Plante.Nom, 1));  //à ajuster quand y'a plusieurs produits pour une récolte
            }
            parcelle.Plante = null;  // Réinitialise la parcelle
        }
        else
        {
            Console.WriteLine("La parcelle ne contient pas de plante mûre à récolter.");
        }
    }
    public void Vendre()
    {
       
    }
}
