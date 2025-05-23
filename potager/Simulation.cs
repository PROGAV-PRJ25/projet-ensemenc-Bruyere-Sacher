public class Simulation
{
    public Joueur Jardinier { get; set; }
    public Meteo Meteo { get; set; }
    public int Semaine { get; set; }
    public Guide Guide { get; set; }

    public Simulation(Joueur jardinier, Meteo meteo)
    {
        Jardinier = jardinier;
        Meteo = meteo;
        Semaine = 1;
        Guide = new Guide();
    }

    public void SimulerSemaine()
    {
        Console.WriteLine($"\n Semaine {Semaine} - Jardin de {Jardinier} - Argent: {Jardinier.Argent} pièces\n");

        foreach (var terrain in Jardinier.Terrains) // reduit le temps de la protection 
        {
            foreach (var parcelle in terrain.Parcelles)
            {
                if (parcelle.EstProtegee)
                {
                    parcelle.DureeProtectionRestante--;
                    Console.WriteLine($"Le temps de vie de la protection de la parcelle {parcelle.NumeroParcelle} a diminuée.");
                    if (parcelle.DureeProtectionRestante <= 0)
                    {
                        parcelle.EstProtegee = false;
                        Console.WriteLine($"La protection de la parcelle {parcelle.NumeroParcelle} a expiré.");
                    }
                }
            }
        }
        
        Meteo.DefinirMeteoAleatoirement(); // météo aléatoire pour la semaine d'apres
        Console.WriteLine(Meteo.ToString());

        foreach (var terrain in Jardinier.Terrains)
        {
            
            foreach (var parcelle in terrain.Parcelles)
            {
                terrain.MiseAJourCondition(parcelle); // mise à jour de l'humidité/ensoleillement sur chaque parcelle selon le type de terrain
                
                if (parcelle.Plante != null)
                {
                    parcelle.Plante.VerifierMort(); //verifie si la plante est morte
                    if (parcelle.Plante.EstMorte)
                    {
                        Console.WriteLine($"Parcelle {parcelle.NumeroParcelle} : {parcelle.Plante} est morte");
                    }
                    else
                    {
                        Console.WriteLine($"Parcelle {parcelle.NumeroParcelle} : {parcelle.Plante}");
                    }
                    
                }
                else
                {
                    Console.WriteLine($"Parcelle {parcelle.NumeroParcelle} sur terrain {terrain.Type} est vide.");
                }

                // -------------------------------Pour le mode Urgence----------------------------------
                // Vérifie d'abord que la parcelle contient une plante vivante
                if (parcelle.Plante != null && !parcelle.Plante.EstMorte && !parcelle.EstProtegee)
                {
                    if (parcelle.UrgenceAssociee == null && Semaine != 1)
                    {
                        Urgence nouvelleUrgence = new Urgence();
                        bool urgenceCreee = nouvelleUrgence.DeclencherAleatoirement();

                        if (urgenceCreee)
                        {
                            parcelle.UrgenceAssociee = nouvelleUrgence;
                        }
                    }

                    if (Semaine != 1 &&
                        parcelle.UrgenceAssociee != null &&
                        !parcelle.UrgenceAssociee.ProblemeResolu)
                    {
                        parcelle.UrgenceAssociee.Resoudre(Jardinier, parcelle, Jardinier.Magasin);
                    }
                }


            }
        }

        Semaine++;
    }
    public void RealiserAction()
    {
        bool finSemaine = false;
        do
        {
            Console.WriteLine("\n📋 Que voulez-vous faire ?");
            Console.WriteLine("1. Arroser un terrain ou une parcelle");
            Console.WriteLine("2. Planter un semi");
            Console.WriteLine("3. Acheter au magasin");
            Console.WriteLine("4. Vendre les récoltes");
            Console.WriteLine("5. Récolter une parcelle");
            Console.WriteLine("6  Utiliser un outils");
            Console.WriteLine("7. Retirer une plante morte");
            Console.WriteLine("8. Afficher l'état de nos terrains");
            Console.WriteLine("9. Afficher les caractéristiques des plantes");
            Console.WriteLine("10. Afficher les règles du jeu");
            Console.WriteLine("11. Passer à la semaine suivante");
            Console.WriteLine("12. Arreter totalement la simulation maintenant");

            Console.Write("Votre choix : ");
            string? choixStr = Console.ReadLine();
            int choixAction;
            if (!int.TryParse(choixStr, out choixAction))
            {
                Console.WriteLine("⛔ Choix invalide. Veuillez entrer un nombre entre 1 et 10.");
            }
            else
            {
                switch (choixAction)
                {
                    case 1:
                        Jardinier.Arroser();
                        break;
                    case 2:
                        Jardinier.Planter();
                        break;
                    case 3:
                        Jardinier.Magasin.Menu();
                        break;
                    case 4:
                        Jardinier.Vendre();
                        break;
                    case 5:
                        if (Jardinier.MesRecoltes.Count == 0)
                        {
                            Console.WriteLine("Vous n'avez aucune plante à récolter");
                        }
                        else
                        {
                            Jardinier.Recolter();
                        }
                        break;
                    case 6:
                        Jardinier.UtiliserOutil();
                        break;
                    case 7:
                        Jardinier.Nettoyer();
                        break;
                    case 8:
                        Jardinier.AfficherEtatTerrains();
                        break;
                    case 9:
                        Guide.CaracteristiquesPlantes();
                        break;
                    case 10:
                        Guide.ReglesJeu();
                        break;
                    case 11:
                        finSemaine = true;
                        break;
                    case 12:
                        Console.WriteLine("❌ Fin de la simulation.");
                        Console.WriteLine();
                        Console.WriteLine($"Bravo, vous finissez votre simulation de potager avec {Jardinier.Argent} pièces.");
                        if (Jardinier.MesRecoltes.Count != 0)
                        {
                            Console.WriteLine($"Vous avez dans vos récoltes:");
                            Jardinier.AfficherRecolte();
                        }

                        Environment.Exit(0); //arrête l'exécution du programme
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }

        } while (!finSemaine);
    }
    public void SimulerJeu(int nombreSemaines)
    {
        Random rng = new Random();
        int tirage = rng.Next(1, 4); //tire un nombre entre 1 et 3
        Terrain terrain = null!;
        switch (tirage)
        {
            case 1:
                terrain=new TerrainDesertique();
                break;
            case 2:
                terrain=new TerrainTropical();
                break;
            case 3:
                terrain=new TerrainVolcanique();
                break;
        }
        Jardinier.Terrains.Add(terrain);
        Meteo.DefinirMeteoAleatoirement(); //météo initale
        Meteo.AppliquerEffet(Jardinier.Terrains); // appliquer météo pour les terrains initiaux

        for (int i = 0; i < nombreSemaines; i++)
        {
            SimulerSemaine();
            foreach (var parcelle in terrain.Parcelles)
            {
                terrain.MiseAJourCondition(parcelle); // mise à jour de l'humidité/ensoleillement sur chaque parcelle selon le type de terrain
                if (parcelle.Plante != null)
                {
                    parcelle.Plante.AnalyserSante(Meteo.Temperature, parcelle.HumiditeParcelle, parcelle.EnsoleillementParcelle);
                }
            }
            Meteo.AppliquerEffet(Jardinier.Terrains);
            RealiserAction();
            
        }

        Console.WriteLine("🎉 Simulation terminée !");
    }
}
