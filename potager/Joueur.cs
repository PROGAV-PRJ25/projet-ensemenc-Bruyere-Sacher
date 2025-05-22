public class Joueur
{
    public string Nom { get; set; }
    public double Argent { get; set; }
    public List<Semis> StockSemis { get; set; }
    public List<Terrain> Terrains { get; set; }
    public List<Recoltes> MesRecoltes { get; set; }
    public List<Outils> StockOutils { get; set; }
    public InventaireTypePlante ListePlante { get; set; }  //tout les types de plante disponible
    public Magasin Magasin { get; set; }

    public Joueur(string nom, int argent)
    {
        Nom = nom;
        Argent = argent;
        StockSemis = new List<Semis>();
        Terrains = new List<Terrain>();
        StockOutils = new List<Outils>();
        MesRecoltes = new List<Recoltes>();
        ListePlante = new InventaireTypePlante();
        Magasin = new Magasin(this);
    }

    public void Planter()
    {
        if (StockSemis.Count == 0)
        {
            Console.WriteLine("Vous n'avez pas de semis. Allez en achter au magasin!");
            return;
        }

        //initialisation de variables
        Semis? semisChoisi = null;
        Plante? planteAPlanter = null;
        bool plantePossible = false;
        List<Parcelle> parcellesDisponibles = new List<Parcelle>();
        Parcelle? parcelleCible = null;

        do
        {
            parcellesDisponibles.Clear();
            plantePossible = false;
            AfficherSemis();  //affiche le stock de semis
            Console.WriteLine("Quel type de plante veux-tu planter? (entre le chiffre correspondant) (ou tapes 0 si tu ne veux plus réaliser cette action)");
            string? numeroSemi = Console.ReadLine();
            if (!int.TryParse(numeroSemi, out int index) || index < 0 || index > StockSemis.Count) //condition si la saisie n'est pas un entier qui correspond à un semis
            {
                Console.WriteLine(" Numéro invalide!");
                continue;
            }
            if (index == 0) //condition si on veur annuler l'action
            {
                Console.WriteLine("Action annulée");
                return;
            }
            semisChoisi = StockSemis[index - 1]; //associe le bon semi si le numéro était valide

            if (semisChoisi.Quantite == 0) //condition si 0 quantité du semis
            {
                Console.WriteLine("Tu ne possèdes ce semis dans ton stock!");
            }
            else
            {
                planteAPlanter = ListePlante.DefinirPlante(semisChoisi.NomPlante); //créer la plante
                //vérifier qu'il y a un terrain pour planter la plante choisi
                foreach (var terrain in Terrains)
                {
                    if (terrain.Type == planteAPlanter.TerrainPrefere)
                    {
                        foreach (var parcelle in terrain.Parcelles)
                        {
                            if (parcelle.Plante == null)
                            {
                                parcellesDisponibles.Add(parcelle); // ajoute la parcelle a une liste si on peut planter la plante choisi
                                plantePossible = true; //informe qu'on a une parcelle libre pour planter la plante
                            }
                        }
                    }
                }
                if (plantePossible == false)
                {
                    Console.WriteLine($"Tu n'a pas de parcelle libre sur un terrain {planteAPlanter.TerrainPrefere}. Tu ne peux pas planter ce semis sur un autre terrain! Choisis un autre semis ou va au magasin acheter une parcelle de type {planteAPlanter.TerrainPrefere} ");
                }
            }
        } while (semisChoisi == null || semisChoisi.Quantite == 0 || plantePossible == false);

        do
        {
            Console.WriteLine("Voici les parcelles libres sur lesquelles tu peux planter ton semis?");
            foreach (var parcelle in parcellesDisponibles)
            {
                Console.WriteLine($"- Parcelle n°{parcelle.NumeroParcelle} du terrain {parcelle.TerrainAssocie.Type}");
            }
            Console.WriteLine("Dans quel numéro de parcelle veux-tu planter ?");
            string? numeroParcelle = Console.ReadLine();
            if (!int.TryParse(numeroParcelle, out int choixParcelle)) //condition si la saisie n'est pas un entier 
            {
                Console.WriteLine("Numéro invalide.");
                continue;
            }

            // Recherche de la parcelle
            foreach (var parcelle in parcellesDisponibles) //cherche la parcelle selectionnée
            {
                if (parcelle.NumeroParcelle == choixParcelle)
                {
                    parcelleCible = parcelle;
                }
            }
            if (parcelleCible == null)
            {
                Console.WriteLine("Cette parcelle n'existe pas ou n'est pas disponible!");
            }

        } while (parcelleCible == null);

        parcelleCible.Plante = planteAPlanter; //on ajoute la plante sur la parcelle choisi
        semisChoisi.Quantite--; //on retire un semi
        if (semisChoisi.Quantite == 0) // si on a utilisé le dernier semis d'une plante
        {
            StockSemis.Remove(semisChoisi);   //on retire le semi de nos stock
        }

        Console.WriteLine($"{planteAPlanter?.Nom} a été plantée dans la parcelle n°{parcelleCible.NumeroParcelle} du terrain {parcelleCible.TerrainAssocie.Type}.");
    }
    public void Arroser()
    {
        //initialisation des variables
        int decisionArroser = 0;
        Terrain? terrainChoisi = null;

        do
        {
            Console.WriteLine("Que veux tu faire? (entre le numéro correspondant) (ou tapes 0 si tu ne veux plus réaliser cette action)");
            Console.WriteLine("1. Arroser un terrain entier");
            Console.WriteLine("2. Arroser seulement une parcelle");
            string? choixArroser = Console.ReadLine();
            if (!int.TryParse(choixArroser, out decisionArroser) || (decisionArroser != 0 && decisionArroser != 1 && decisionArroser != 2)) //condition si la saisie n'est pas un entier correspondant a une proposition
            {
                Console.WriteLine("Choix invalide");
            }
            if (decisionArroser == 0) //condition si on veur annuler l'action
            {
                Console.WriteLine("Action annulée");
                return;
            }
        } while (decisionArroser != 1 && decisionArroser != 2);

        do
        {
            Console.WriteLine("Voici les terrains disponibles:");
            AfficherTerrain();
            Console.WriteLine("Dans quel terrain souhaite tu arroser?");
            string? terrainArrosage = Console.ReadLine();

            if (!int.TryParse(terrainArrosage, out int numeroTerrain) || numeroTerrain < 1 || numeroTerrain > Terrains.Count) //condition si la saisie n'est pas un entier correspondant à une proposition
            {
                Console.WriteLine("Numéro invalide");
                continue;
            }
            terrainChoisi = Terrains[numeroTerrain - 1];
        } while (terrainChoisi == null);

        if (decisionArroser == 1) //si on arrose tout le terrain
        {
            ArroserTerrain(terrainChoisi);
        }
        else
        {
            Parcelle? parcelleChoisi = null;
            do
            {
                AfficherParcelle(terrainChoisi);
                Console.WriteLine("Quel est le numéro de la parcelle que tu souhaite arroser?");
                string? numeroParcelle = Console.ReadLine();
                if (!int.TryParse(numeroParcelle, out int choixParcelle)) //verifie que c'est un entier 
                {
                    Console.WriteLine("Numéro invalide.");
                    continue;
                }
                foreach (var parcelle in terrainChoisi.Parcelles) //cherche la parcelle selectionnée
                {
                    if (parcelle.NumeroParcelle == choixParcelle)
                    {
                        parcelleChoisi = parcelle;
                    }
                }
                if ((parcelleChoisi == null))
                {
                    Console.WriteLine("Cette parcelle n'existe pas!");
                }
            } while ((parcelleChoisi == null));

            ArroserParcelle(parcelleChoisi);
        }
    }
    public void ArroserParcelle(Parcelle parcelle)
    {
        if (parcelle.HumiditeParcelle + 10 < 100)
        {
            parcelle.HumiditeParcelle += 10;
        }
        else
        {
            parcelle.HumiditeParcelle = 100;
        }
        Console.WriteLine($"La parcelle n°{parcelle.NumeroParcelle} du terrain {parcelle.TerrainAssocie.Type} a été arrosé.");
    }
    public void ArroserTerrain(Terrain terrain)
    {
        foreach (var parcelle in terrain.Parcelles)
        {
            if (parcelle.HumiditeParcelle + 10 < 100)
            {
                parcelle.HumiditeParcelle += 10;
            }
            else
            {
                parcelle.HumiditeParcelle = 100;
            }
        }
        Console.WriteLine($"Le terrain {terrain.Type} a été arrosé.");
    }
    public void Recolter()
    {
        Terrain? terrainChoisi = null;
        do
        {
            Console.WriteLine("Voici les terrains disponibles:");
            AfficherTerrain();
            Console.WriteLine("Dans quel terrain souhaite tu recolter? (ou tapes 0 si tu ne veux plus réaliser cette action)");
            string? terrainRecolte = Console.ReadLine();

            if (!int.TryParse(terrainRecolte, out int numeroTerrain) || numeroTerrain < 0 || numeroTerrain > Terrains.Count) //condition si la saisie n'est pas un entier correspondant à une proposition
            {
                Console.WriteLine("Numéro invalide");
                continue;
            }
            if (numeroTerrain == 0) //condition si on veur annuler l'action
            {
                Console.WriteLine("Action annulée");
                return;
            }
            terrainChoisi = Terrains[numeroTerrain - 1];

        } while (terrainChoisi == null);

        Parcelle? parcelleChoisi = null;
        do
        {
            AfficherParcelle(terrainChoisi);
            Console.WriteLine("Quel est le numéro de la parcelle que tu veux recolter (ou tapes 0 si tu ne veux plus réaliser cette action)?");
            string? numeroParcelle = Console.ReadLine();
            if (!int.TryParse(numeroParcelle, out int choixParcelle)) //verifie que c'est un entier 
            {
                Console.WriteLine("Numéro invalide.");
                continue;
            }
            if (choixParcelle == 0) //condition si on veur annuler l'action
            {
                Console.WriteLine("Action annulée");
                return;
            }
            foreach (var parcelle in terrainChoisi.Parcelles) //cherche la parcelle selectionnée
            {
                if (parcelle.NumeroParcelle == choixParcelle)
                {
                    parcelleChoisi = parcelle;
                }
            }
            if ((parcelleChoisi == null))
            {
                Console.WriteLine("Cette parcelle n'existe pas!");
            }
        } while ((parcelleChoisi == null));

        if (parcelleChoisi.Plante != null && !parcelleChoisi.Plante.EstMorte && parcelleChoisi.Plante.Age>= parcelleChoisi.Plante.TempsDeMaturation)
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
        Console.WriteLine("Voici tes récoltes:");
        for (int i = 0; i < MesRecoltes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {MesRecoltes[i].TypePlante} - Quantité : {MesRecoltes[i].Quantite}");
        }

        int choixNum;
        do
        {
            Console.WriteLine("Quel produit veux-tu vendre ? (entre le numéro correspondant) (ou tapes 0 si tu ne veux plus réaliser cette action)");
            string? choixVente = Console.ReadLine();

            if (!int.TryParse(choixVente, out choixNum)) //condition si la saisie n'est pas un entier
            {
                Console.WriteLine("Choix invalide");
                continue;
            }
            if (choixNum == 0) //condition si on veur annuler l'action
            {
                Console.WriteLine("Action annulée");
                return;
            }

        } while (choixNum < 0 || choixNum >= MesRecoltes.Count);
        Recoltes recolteAVendre = MesRecoltes[choixNum - 1]; //associe la recolte choisi

        int quantiteAVendre;
        do
        {
            Console.WriteLine($"Tu as {recolteAVendre.Quantite} {recolteAVendre.TypePlante} et son prix est de {recolteAVendre.Prix}.");
            Console.WriteLine($"Combien veux-tu en vendre?");

            string? choixQuantite = Console.ReadLine();

            if (!int.TryParse(choixQuantite, out quantiteAVendre))
            {
                Console.WriteLine("Choix invalide");
                continue;
            }
        } while (quantiteAVendre <= 0 || quantiteAVendre > recolteAVendre.Quantite);

        int gain = quantiteAVendre * recolteAVendre.Prix;
        recolteAVendre.Quantite -= quantiteAVendre; // on rettire la quantité vendu de nos recoltes
        Argent += gain;
        Console.WriteLine($"Tu as vendu {quantiteAVendre} {recolteAVendre.TypePlante} pour {gain} pièces.");

        if (recolteAVendre.Quantite == 0) // si on vendu toute la quantité d'un produit
        {
            MesRecoltes.Remove(recolteAVendre);   //on retire la produit des récoltes
        }
    }
    public void Nettoyer()
    {
        Terrain? terrainChoisi = null;
        do
        {
            Console.WriteLine("Voici tes terrains:");
            AfficherTerrain();
            Console.WriteLine("Dans quel terrain souhaite tu retirer une plante morte? (entre le numéro correspondant) (ou tapes 0 si tu ne veux plus réaliser cette action) ");
            string? terrainNettoye = Console.ReadLine();

            if (!int.TryParse(terrainNettoye, out int numeroTerrain) || numeroTerrain < 0 || numeroTerrain > Terrains.Count) //condition si la saisie n'est pas un entier correspondant à une proposition
            {
                Console.WriteLine("Numéro invalide");
                continue;
            }
            if (numeroTerrain == 0) //condition si on veur annuler l'action
            {
                Console.WriteLine("Action annulée");
                return;
            }
            terrainChoisi = Terrains[numeroTerrain - 1];
        } while (terrainChoisi == null);

        Parcelle? parcelleChoisi = null;
        do
        {
            AfficherParcelle(terrainChoisi);
            Console.WriteLine("Quel est le numéro de la parcelle de la plante morte que tu veux retirer? (ou tapes 0 si tu ne veux plus réaliser cette action)");
            string? numeroParcelle = Console.ReadLine();
            if (!int.TryParse(numeroParcelle, out int parcelleNettoye)) //verifie que c'est un entier 
            {
                Console.WriteLine("Numéro invalide.");
                continue;
            }
            if (parcelleNettoye == 0) //condition si on veur annuler l'action
            {
                Console.WriteLine("Action annulée");
                return;
            }
            foreach (var parcelle in terrainChoisi.Parcelles) //cherche la parcelle selectionnée
            {
                if (parcelle.NumeroParcelle == parcelleNettoye)
                {
                    parcelleChoisi = parcelle;
                }
            }
            if ((parcelleChoisi == null))
            {
                Console.WriteLine("Cette parcelle n'existe pas!");
            }
        } while ((parcelleChoisi == null));

        if (parcelleChoisi.Plante != null && parcelleChoisi.Plante.EstMorte) //si il y a une plante morte sur la parcelle choisi
        {
            Console.WriteLine($"{parcelleChoisi.Plante.Nom} qui était mort est retiré depuis la parcelle {parcelleChoisi.NumeroParcelle}.");
            parcelleChoisi.Plante = null;  //retire la plante morte
        }
        else
        {
            Console.WriteLine("La parcelle ne contient pas de plante morte à retirer.");
        }
    }
    public void AfficherEtatTerrains()
    {
        Console.WriteLine("État de tous les terrains :");

        foreach (var terrain in Terrains)
        {
            Console.WriteLine($"► Terrain : {terrain.Type}");
            Console.WriteLine("----------------------------------------------------------");

            foreach (var parcelle in terrain.Parcelles)
            {
                Console.Write($"  - Parcelle n°{parcelle.NumeroParcelle} | ");
                Console.Write($"Humidité : {parcelle.HumiditeParcelle}% | ");
                Console.Write($"Ensoleillement : {parcelle.EnsoleillementParcelle}% | ");
                Console.WriteLine($"Température : {parcelle.TerrainAssocie.Temperature}°C");

                if (parcelle.Plante == null)
                {
                    Console.WriteLine("       → Aucune plante (Parcelle vide)\n");
                }
                else
                {
                    Console.WriteLine($"       → {parcelle.Plante.Nom} | Santé : {parcelle.Plante.Sante}% ");
                }
            }
        }
    }
    public void AfficherSemis()
    {
        if (StockSemis.Count == 0)
        {
            Console.WriteLine("Tu ne possèdes pas de semi, vas en acheter au Magasin!");
        }
        else
        {
            Console.WriteLine("Voici les semis que tu possède:");
            for (int i = 0; i < StockSemis.Count; i++)
            {
                Semis semis = StockSemis[i];
                Console.WriteLine($"{i + 1}.{semis.NomPlante} x{semis.Quantite}");
            }
        }
    }
    public void AfficherRecolte()
    {
        foreach (var recolte in MesRecoltes)
        {
            Console.WriteLine($"- {recolte.TypePlante} x{recolte.Quantite}");
        }
    }
    public void AfficherParcelle(Terrain terrain)
    {
        Console.WriteLine($"Voici les parcelles du terrain {terrain.Type}");
        foreach (var parcelle in terrain.Parcelles)
        {
            Console.WriteLine($"- Parcelle n°{parcelle.NumeroParcelle} du terrain {terrain.Type}");
        }

    }
    public void AfficherTerrain()
    {
        for (int i = 0; i < Terrains.Count; i++)
        {
            Terrain terrain = Terrains[i];
            Console.WriteLine($"{i + 1}.Terrain {terrain.Type}");
        }
    }
    public void UtiliserOutil()
    {
        if (StockOutils.Count == 0)
        {
            Console.WriteLine("❗ Tu n'as aucun outil dans ton stock.");
            return;
        }

        Console.WriteLine("🧰 Voici les outils disponibles dans ton stock :");
        for (int i = 0; i < StockOutils.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {StockOutils[i].NomOutil} x{StockOutils[i].Quantite}");
        }

        int choixOutil = -1;
        string saisieOutil = string.Empty;

        do
        {
            Console.WriteLine("\nEntre le numéro de l'outil à utiliser, ou '0' pour annuler :");
            saisieOutil = Console.ReadLine()?.Trim() ?? "";

            if (saisieOutil == "0")
            {
                Console.WriteLine("❌ Action annulée.");
                return;
            }

            if (!int.TryParse(saisieOutil, out choixOutil) || choixOutil < 1 || choixOutil > StockOutils.Count)
            {
                Console.WriteLine("❌ Numéro invalide. Réessaie.");
                choixOutil = -1;
            }

        } while (choixOutil == -1);

        Outils outilChoisi = StockOutils[choixOutil - 1];

        if (outilChoisi.Quantite <= 0)
        {
            Console.WriteLine($"❌ Tu n'as plus de {outilChoisi.NomOutil}.");
            return;
        }

        // Sélection de la parcelle
        Console.WriteLine("\n🌱 Choisis une parcelle sur laquelle utiliser l'outil :");
        Dictionary<int, Parcelle> parcellesDisponibles = new Dictionary<int, Parcelle>();
        int compteur = 1;

        foreach (var terrain in Terrains)
        {
            foreach (var parcelle in terrain.Parcelles)
            {
                Console.WriteLine($"{compteur}. Terrain {terrain.Type} - Parcelle {parcelle.NumeroParcelle} {(parcelle.EstProtegee ? "[Protégée]" : "")}");
                parcellesDisponibles[compteur] = parcelle;
                compteur++;
            }
        }

        if (parcellesDisponibles.Count == 0)
        {
            Console.WriteLine("❗ Tu n'as aucune parcelle disponible.");
            return;
        }

        int choixParcelle = -1;
        string saisieParcelle = string.Empty;

        do
        {
            Console.WriteLine("Entre le numéro de la parcelle, ou '0' pour annuler :");
            saisieParcelle = Console.ReadLine()?.Trim() ?? "";

            if (saisieParcelle == "0")
            {
                Console.WriteLine("❌ Action annulée.");
                return;
            }

            if (!int.TryParse(saisieParcelle, out choixParcelle) || !parcellesDisponibles.ContainsKey(choixParcelle))
            {
                Console.WriteLine("❌ Numéro invalide. Réessaie.");
                choixParcelle = -1;
            }

        } while (choixParcelle == -1);

        Parcelle parcelleCible = parcellesDisponibles[choixParcelle];

        if (parcelleCible.EstProtegee)
        {
            Console.WriteLine($"🔒 La parcelle {parcelleCible.NumeroParcelle} est déjà protégée.");
            return;
        }

        // Utilisation libre de l’outil
        parcelleCible.EstProtegee = true;
        parcelleCible.DureeProtectionRestante = 2; // Protégée pour 2 semaines par exemple
        outilChoisi.Quantite--;

        if (outilChoisi.Quantite <= 0)
        {
            StockOutils.Remove(outilChoisi);
        }

        Console.WriteLine($"✅ Tu as utilisé l'{outilChoisi.NomOutil} sur la parcelle {parcelleCible.NumeroParcelle}. Elle est maintenant protégée.");
    }

    public override string ToString()
    {
        return Nom;
    }

}