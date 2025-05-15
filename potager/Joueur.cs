public class Joueur
{
    public string Nom { get; set; }
    public double Argent { get; set; }
    public List<Semis> StockSemis { get; set; }
    public List<Terrain> Terrains { get; set; }
    public List<Recoltes> MesRecoltes { get; set; }
    public List<Outils> StockOutils { get; set; }
    public InventaireTypePlante ListePlante { get; set; }
    public Magasin Magasin { get; set; } //rajouté par J


    public Joueur(string nom, int argent)
    {
        Nom = nom;
        Argent = argent;
        StockSemis = new List<Semis>();
        Terrains = new List<Terrain>();
        StockOutils=new List<Outils>();
        MesRecoltes=new List<Recoltes>();
        ListePlante=new InventaireTypePlante();
        Magasin = new Magasin(this);  // ajouté par J Le joueur est passé comme argument à la classe Magasin
    }

    public void Planter()
    {
        Console.WriteLine("Quel type de plante veux-tu planter?");
        AfficherSemis();
        Semis? semisChoisi = null;
        Plante? planteAPlanter=null;
        bool plantePossible = false;
        List<Parcelle> parcellesDisponibles = new List<Parcelle>();

        do
        {
            parcellesDisponibles.Clear(); // 🧹 vide la liste
            plantePossible = false;
            AfficherSemis();
            string choixPlante = Convert.ToString(Console.ReadLine()!);
            // Recherche du semis correspondant
            foreach (var semi in StockSemis)
            {
                if (semi.NomPlante==choixPlante)
                {
                    semisChoisi=semi;
                }
            }
            // Si le semis n'est pas trouvé ou la quantité est insuffisante, demande à l'utilisateur de choisir à nouveau
            if (semisChoisi == null)
            {
                Console.WriteLine("Ce semis n'existe pas! Choisis un autre semis!");
            }
            else if (semisChoisi.Quantite == 0)
            {
                Console.WriteLine("Tu ne possèdes pas de semis de cette plante dans ton stock! Choisis un autre semis!");
            }
            else
            {
                planteAPlanter = ListePlante.DefinirPlante(semisChoisi.NomPlante);
                foreach (var terrain in Terrains)
                {
                    if (terrain.Type == planteAPlanter.TerrainPrefere)
                    {
                        foreach (var parcelle in terrain.Parcelles)
                        {
                            if (parcelle.Plante == null)
                            {
                                parcellesDisponibles.Add(parcelle);
                                plantePossible = true;
                            }
                        }
                    }
                }
            }
        }while(semisChoisi == null || semisChoisi.Quantite == 0 || plantePossible==false);
        
        Parcelle? parcelleCible=null;
        Console.WriteLine("Dans quel numéro de parcelle veux-tu planter ?");
        Console.WriteLine("Voici les parcelles libres sur lesquelles tu peux plnater ton semi?");
        do
        {
            foreach (var parcelle in parcellesDisponibles)
            {
                Console.WriteLine($"- Parcelle n°{parcelle.NumeroParcelle} du terrain {parcelle.TerrainAssocie.Type}");
            }
            int choixParcelle = Convert.ToInt32(Console.ReadLine()!);
            
            // Recherche de la parcelle
            foreach (var parcelle in parcellesDisponibles)
            {
                if (parcelle.NumeroParcelle==choixParcelle)
                {
                    parcelleCible=parcelle;
                }
            }
            if (parcelleCible == null)
            {
                Console.WriteLine("Cette parcelle n'existe pas ou n'est pas disponible! Choisis une autre parcelle!");
            }
            
        }while(parcelleCible == null);

        // Planter
        parcelleCible.Plante = planteAPlanter;
        semisChoisi.Quantite--;
        Console.WriteLine($"{planteAPlanter?.Nom} a été plantée dans la parcelle n°{parcelleCible.NumeroParcelle} du terrain {parcelleCible.TerrainAssocie.Type}.");
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
        
        Terrain? terrainChoisi = null;
        do
        {
            string terrainArrosage = Convert.ToString(Console.ReadLine()!);
            foreach (var terrain in Terrains)
            {
                if (terrain.Type == terrainArrosage)
                {
                    terrainChoisi = terrain;
                }
            }
            if ((terrainChoisi == null) || (!Terrains.Contains(terrainChoisi)))
            {
                Console.WriteLine("Ce terrain n'existe pas! Dans quel terrain souhaite tu arroser?");
            }

        } while ((terrainChoisi == null) || (!Terrains.Contains(terrainChoisi)));
        
        if (decisionArroser2==1)
        {
            ArroserTerrain(terrainChoisi);
        }
        else
        {
            Console.WriteLine("Quel est le numéro de la parcelle que tu souhaite arroser?");
            Parcelle? parcelleChoisi = null;
            do
            {
                int parcelleArrosage = Convert.ToInt32(Console.ReadLine()!);
                foreach (var parcelle in terrainChoisi.Parcelles)
                {
                    if (parcelle.NumeroParcelle == parcelleArrosage)
                    {
                        parcelleChoisi = parcelle;
                    }
                }
                if ((parcelleChoisi == null) || (!terrainChoisi.Parcelles.Contains(parcelleChoisi)))
                {
                    Console.WriteLine("Cette parcelle n'existe pas! Quel est le numéro de la parcelle que tu souhaite arroser?");
                }
            } while ((parcelleChoisi == null) || (!terrainChoisi.Parcelles.Contains(parcelleChoisi)));
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
    public void Recolter()
    {
        Terrain? terrainChoisi = null;
        do
        {
            string terrainRecolte = Convert.ToString(Console.ReadLine()!);
            foreach (var terrain in Terrains)
            {
                if (terrain.Type == terrainRecolte)
                {
                    terrainChoisi = terrain;
                }
            }
            if ((terrainChoisi == null) || (!Terrains.Contains(terrainChoisi)))
            {
                Console.WriteLine("Ce terrain n'existe pas! Dans quel terrain souhaite tu arroser?");
            }
        } while ((terrainChoisi == null) || (!Terrains.Contains(terrainChoisi)));

        Console.WriteLine("Quel est le numéro de la parcelle que tu souhaite arroser?");
        Parcelle? parcelleChoisi = null;
        do
        {
            int parcelleRecolte = Convert.ToInt32(Console.ReadLine()!);
            foreach (var parcelle in terrainChoisi.Parcelles)
            {
                if (parcelle.NumeroParcelle == parcelleRecolte)
                {
                    parcelleChoisi = parcelle;
                }
            }
            if ((parcelleChoisi == null) || (!terrainChoisi.Parcelles.Contains(parcelleChoisi)))
            {
                Console.WriteLine("Cette parcelle n'existe pas! Quel est le numéro de la parcelle que tu souhaite arroser?");
            }
        } while ((parcelleChoisi == null) || (!terrainChoisi.Parcelles.Contains(parcelleChoisi)));

        if (parcelleChoisi.Plante != null && !parcelleChoisi.Plante.EstMorte && parcelleChoisi.Plante.Croissance >= 100)
        {
            Console.WriteLine($"{parcelleChoisi.Plante.Nom} récoltée depuis la parcelle {parcelleChoisi.NumeroParcelle}.");
            int quantiteRecolte = parcelleChoisi.Plante.AvoirQuantiteRecolte();
            bool trouveDansRecolte = false;

            foreach (var recolte in MesRecoltes)
            {
                if (recolte.TypePlante == parcelleChoisi.Plante.Nom)
                {
                    recolte.Quantite += quantiteRecolte;
                    trouveDansRecolte = true;
                }
            }
            if (trouveDansRecolte == false)
            {
                MesRecoltes.Add(new Recoltes(parcelleChoisi.Plante.Nom, quantiteRecolte, parcelleChoisi.Plante.Prix));
            }
            parcelleChoisi.Plante = null;  // Réinitialise la parcelle
        }
        else
        {
            Console.WriteLine("La parcelle ne contient pas de plante mûre à récolter.");
        }
    }
    public void Vendre()
    {
        Console.WriteLine("Voici ce que tu peux vendre :");
        for (int i = 0; i < MesRecoltes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {MesRecoltes[i].TypePlante} - Quantité : {MesRecoltes[i].Quantite}");
        }

        Console.WriteLine("Quel produit veux-tu vendre ? (entre le numéro correspondant)");
        int choixVente = Convert.ToInt32(Console.ReadLine()!) - 1;

        while(choixVente < 0 || choixVente >= MesRecoltes.Count)
        {
            Console.WriteLine("Choix invalide. Quel produit veux-tu vendre ? (entre le numéro correspondant).");
            choixVente = Convert.ToInt32(Console.ReadLine()!) - 1;
        }

        Recoltes recolteAVendre = MesRecoltes[choixVente];
        Console.WriteLine($"Tu as {recolteAVendre.TypePlante} et son prix est de {recolteAVendre.Prix}. Combien veux-tu en vendre ?"); 
        int quantiteAVendre = Convert.ToInt32(Console.ReadLine()!);

        while(quantiteAVendre <= 0 || quantiteAVendre > recolteAVendre.Quantite)
        {
            Console.WriteLine($"Choix invalide. Tu as {recolteAVendre.TypePlante} et son prix est de {recolteAVendre.Prix}. Combien veux-tu en vendre ?");
            quantiteAVendre = Convert.ToInt32(Console.ReadLine()!);
        }

        int gain = quantiteAVendre * recolteAVendre.Prix;
        recolteAVendre.Quantite -= quantiteAVendre;
        Argent += gain;
        Console.WriteLine($"Tu as vendu {quantiteAVendre} {recolteAVendre.TypePlante} pour {gain} pièces.");
    }
    public void AfficherSemis()
    {
        Console.WriteLine("Voici les semis que tu possède:");
        foreach (var semis in this.StockSemis)
        {
            Console.WriteLine($"- {semis.NomPlante} x{semis.Quantite}");
        }
    }
    public void AfficherTerrain()
    {
        Console.WriteLine("Voici les parcelles disponibles:");
        foreach (var terrain in Terrains)
        {
            foreach (var parcelle in terrain.Parcelles)
            {
                Console.WriteLine($"- Parcelle n°{parcelle.NumeroParcelle} du terrain {terrain.Type}");
            }
        }
    }
}