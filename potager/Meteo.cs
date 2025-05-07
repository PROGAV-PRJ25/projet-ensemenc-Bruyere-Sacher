public class Meteo
{ 
    public double Precipitation {get; set;}  //en mm
    public double Ensoleillement {get; set;} //en pourcentage
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
        Ensoleillement = rng.Next(60, 101); //ensoleillement entre 60 et 100%
    }
    public void AppliquerEffet()  //sur les parcelles
    {

    }
   
}