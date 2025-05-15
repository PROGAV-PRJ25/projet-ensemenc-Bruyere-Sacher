// public class Simulation
// {
//     public Joueur Jardinier { get; set; }
//     public Meteo Meteo { get; set; }
//     public int Semaine { get; set; }

//     public Simulation(Joueur jardinier, Meteo meteo)
//     {
//         Jardinier = jardinier;
//         Meteo = meteo;
//         Semaine = 1;
//     }

//     public void SimulerSemaine()
//     {
//         Console.WriteLine($"\n Semaine {Semaine} - Jardin de {Jardinier} - Argent: {Jardinier.Argent}€");
//         Meteo.DefinirMeteoAleatoirement();   //Definition de la semaine aléatoirement
//         Console.WriteLine(Meteo.ToString());

//         // Mise à jour des conditions de chaque terrain
//         foreach (var terrain in Jardinier.Terrains)
//         { //proposition de chat
//             terrain.MiseAJourCondition();
//         }


//         // foreach (var terrain in Jardinier.Terrains)
//         // {
//         //     Console.WriteLine($"- Terrain {terrain.Type}:");
//         //     // terrain.MiseAJourCondition();    //à voir où le mettre pour que ca prenne en compte la météo de la semaine d'avant
//         //     foreach (var parcelle in terrain.Parcelles)
//         //     {
//         //         Console.WriteLine($"           - Parcelle n°{parcelle.NumeroParcelle}:");
//         //         if (parcelle.Plante != null)
//         //         {
//         //             parcelle.Plante.AnalyserSante(Meteo.Temperature, parcelle.HumiditeParcelle,parcelle.EnsoleillementParcelle);
//         //             Console.WriteLine(parcelle.Plante.ToString());
//         //         }
//         //         else
//         //         {
//         //             Console.WriteLine($"La parcelle n°{parcelle.NumeroParcelle} du terrain {terrain.Type} est vide.");
//         //         }
//         //         // Vérifie une urgence aléatoire
//         //         Urgence urgence = new Urgence();
//         //         urgence.DeclencherAleatoirement();
//         //         if (!urgence.ProblemeResolu && parcelle.Plante != null && !parcelle.Plante.EstMorte)
//         //         {
//         //             urgence.Resoudre(Jardinier, parcelle);
//         //         }

//         //     }
//         // }
//         Semaine++;
//     }

//     public void RealiserAction()
//     {
//         bool arretSemaine=false;
//         // do
//         // {
//         //     Console.WriteLine($"Voici les actions possibles:");
//         //     Console.WriteLine($"1.Arroser un terrain ou une parcelle \n 2.Acheter quelque chose au magasin \n 3.Vendre les récoltes \n 4.Recolter une parcelle \n 5.Finir la semaine");
//         //     Console.WriteLine($"Tapez le numéro associé à l'action que vous souhaitez effectuer.");
//         //     int choixAction = Convert.ToInt32(Console.ReadLine()!);
//         //     if (choixAction==1)
//         //     {
//         //         Jardinier.Arroser();
//         //     }
//         //     else if (choixAction==2)
//         //     {
//         //         //à completer
//         //     }
//         //     else if (choixAction==3)
//         //     {
//         //         Jardinier.Vendre();
//         //     }
//         //     else if (choixAction==4)
//         //     {
//         //         // Jardinier.Recolter();
//         //     }
//         //     else if (choixAction==5)
//         //     {
//         //         arretSemaine=true;
//         //     }
//         // } while(arretSemaine==true);
            


//     }

//     public void SimulerJeu(int nombreSemaine)
//     {
//         for (int i=0; i<nombreSemaine; i++)
//         {
//             SimulerSemaine();
//             RealiserAction();
//         }
//     }

// }


public class Simulation
{
    public Joueur Jardinier { get; set; }
    public Meteo Meteo { get; set; }
    public int Semaine { get; set; }

    public Simulation(Joueur jardinier, Meteo meteo)
    {
        Jardinier = jardinier;
        Meteo = meteo;
        Semaine = 1;
    }

    public void SimulerSemaine()
    {
        Console.WriteLine($"\n Semaine {Semaine} - Jardin de {Jardinier} - Argent: {Jardinier.Argent} pièces");
        
        Meteo.DefinirMeteoAleatoirement(); // météo aléatoire pour la semaine
        Console.WriteLine(Meteo.ToString());

        foreach (var terrain in Jardinier.Terrains)
        {
            terrain.MiseAJourCondition(); // mise à jour de l'humidité/ensoleillement
            foreach (var parcelle in terrain.Parcelles)
            {
                if (parcelle.Plante != null)
                {
                    parcelle.Plante.AnalyserSante(Meteo.Temperature, parcelle.HumiditeParcelle, parcelle.EnsoleillementParcelle);
                    Console.WriteLine($"🌿 Parcelle {parcelle.NumeroParcelle} : {parcelle.Plante}");
                }
                else
                {
                    Console.WriteLine($"🌱 Parcelle {parcelle.NumeroParcelle} sur terrain {terrain.Type} est vide.");
                }

                // -------------------------------Pour le mode Urgence----------------------------------
                // Si l'urgence n'est pas encore associée, on en crée une nouvelle
                if (parcelle.UrgenceAssociee == null)
                {
                    parcelle.UrgenceAssociee = new Urgence();
                    parcelle.UrgenceAssociee.DeclencherAleatoirement();
                }
                // Gérer l'urgence associée à la parcelle si elle existe
                if (parcelle.UrgenceAssociee != null && !parcelle.UrgenceAssociee.ProblemeResolu && parcelle.Plante != null && !parcelle.Plante.EstMorte)
                {
                    parcelle.UrgenceAssociee.Resoudre(Jardinier, parcelle, Jardinier.Magasin);
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
            Console.WriteLine("6. Passer à la semaine suivante");

            Console.Write("Votre choix : ");
            string? choixStr = Console.ReadLine();
            int choixAction;
            if (!int.TryParse(choixStr, out choixAction))
            {
                Console.WriteLine("⛔ Choix invalide. Veuillez entrer un nombre entre 1 et 5.");
                continue;
            }

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
                    Jardinier.Recolter();
                    break;
                case 6:
                    finSemaine = true;
                    break;
                default:
                    Console.WriteLine("⛔ Choix invalide.");
                    break;
            }

        } while (!finSemaine);
    }


    public void SimulerJeu(int nombreSemaines)
    {
        Random rng = new Random();
        int tirage = rng.Next(1, 4); // Tire un nombre entre 1 et 3
        Terrain? terrain = null;
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
        for (int i = 0; i < nombreSemaines; i++)
        {
            SimulerSemaine();
            RealiserAction();
        }

        Console.WriteLine("🎉 Simulation terminée !");
    }
}
