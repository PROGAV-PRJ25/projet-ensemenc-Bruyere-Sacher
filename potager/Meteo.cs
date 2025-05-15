public class Meteo
{ 
    public int Precipitation {get; set;}  //en mm
    public int Ensoleillement {get; set;} //en pourcentage
    public int Temperature {get; set;}   
    public Meteo() 
    { 
        DefinirMeteoAleatoirement();
    }

    public override string ToString()
    {
        string message=$"Météo pour la semaine à venir: -Température: {Temperature}°C | -Ensoleillement: {Ensoleillement * 100}% | -Précipitations: {Precipitation} mm ";;
        return message;
    }
    public void DefinirMeteoAleatoirement()  //ajuster les tirages au sort
    {
        Random rng = new Random();
        Temperature = rng.Next(20, 36);  //tirage aléatoire d'une temperature entre 20 et 35°

        int pluieChance = rng.Next(0, 100);
        if (pluieChance < 70)  //70% de chance qu'il y ait très peu de pluie
        {
            Precipitation = rng.Next(0, 11);
        }
        else //30% de chance qu'il y ait beaucoup de pluie
        {
            Precipitation = rng.Next(20, 101);
        }
        int soleilChance=rng.Next(0, 100);
        if (soleilChance < 70)  //70% de chance qu'il beaucoup de soleil
        {
            Precipitation = rng.Next(80, 101);
        }
        else //30% de chance qu'il y ait un peu moins de soleil
        {
            Precipitation = rng.Next(20, 101);
        }
        Ensoleillement = rng.Next(60, 80); //ensoleillement entre 60 et 100%
    }
    public void AppliquerEffet(List<Terrain> terrains)  //sur les parcelles
    {
        foreach (var terrain in terrains)
        {
            foreach (var parcelle in terrain.Parcelles)
            {
                // Effet de la pluie
                if (Precipitation>50)
                {
                    parcelle.HumiditeParcelle+=20;
                }
                else if (Precipitation>20)
                {
                    parcelle.HumiditeParcelle+=10;
                }
                else if (Precipitation>5)
                {
                    parcelle.HumiditeParcelle+=5;
                }

                // Effet du soleil
                if (Ensoleillement>90)
                {
                    parcelle.HumiditeParcelle-=15;
                }   
                else if (Ensoleillement>75)
                {
                    parcelle.HumiditeParcelle-=10;
                }
                else if (Ensoleillement>60)
                {
                    parcelle.HumiditeParcelle-=5;
                }

                parcelle.EnsoleillementParcelle=Ensoleillement;

                // On limite l’humidité et l'ensoleillement entre 0 et 100
                parcelle.HumiditeParcelle=Math.Max(0, Math.Min(100, parcelle.HumiditeParcelle));
            }
        }

        Console.WriteLine("L'effet de la météo a été appliqué sur l'humidité des parcelles.");
    }
   
}