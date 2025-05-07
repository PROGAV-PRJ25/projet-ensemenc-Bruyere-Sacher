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
        Console.WriteLine($"\n Semaine {Semaine} - Jardin de {Jardinier} - Argent: {Jardinier.Argent}€");
        Meteo.DefinirMeteoAleatoirement();   //Definition de la semaine aléatoirement
        Console.WriteLine(Meteo.ToString());

        foreach (var terrain in Jardinier.Terrains)
        {
            Console.WriteLine($"- Terrain {terrain.Type}:");
            // terrain.MiseAJourCondition();    //à voir où le mettre pour que ca prenne en compte la météo de la semaine d'avant
            foreach (var parcelle in terrain.Parcelles)
            {
                Console.WriteLine($"           - Parcelle n°{parcelle.NumeroParcelle}:");
                if (parcelle.Plante != null)
                {
                    parcelle.Plante.AnalyserSante(Meteo.Temperature, parcelle.HumiditeParcelle,parcelle.EnsoleillementParcelle);
                    Console.WriteLine(parcelle.Plante.ToString());
                }
                else
                {
                    Console.WriteLine($"La parcelle n°{parcelle.NumeroParcelle} du terrain {terrain.Type} est vide.");
                }
            }
        }
        Semaine++;
    }

    public void RealiserAction()
    {
        bool arretSemaine=false;
        do
        {
            Console.WriteLine($"Voici les actions possibles:");
            Console.WriteLine($"1.Arroser un terrain ou une parcelle \n 2.Acheter quelque chose au magasin \n 3.Vendre les récoltes \n 4.Recolter une parcelle \n 5.Finir la semaine");
            Console.WriteLine($"Tapez le numéro associé à l'action que vous souhaitez effectuer.");
            int choixAction = Convert.ToInt32(Console.ReadLine()!);
            if (choixAction==1)
            {
                Jardinier.Arroser();
            }
            else if (choixAction==2)
            {
                //à completer
            }
            else if (choixAction==3)
            {
                Jardinier.Vendre();
            }
            else if (choixAction==4)
            {
                // Jardinier.Recolter();
            }
            else if (choixAction==5)
            {
                arretSemaine=true;
            }
        } while(arretSemaine==true);
            
    }

    public void SimulerJeu(int nombreSemaine)
    {
        for (int i=0; i<nombreSemaine; i++)
        {
            SimulerSemaine();
            RealiserAction();
        }
    }

}