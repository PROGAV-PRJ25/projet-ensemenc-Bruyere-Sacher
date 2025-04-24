public class Meteo
{ 
    public double Precipitation {get; set;}
    public double Ensoleillement {get; set;}
    public int Temperature {get; set;}   
    public Meteo() 
    { 
        DefinirMeteoAleatoirement();
    }

    public override string ToString()
    {
        string message=$"Température: {Temperature}°C | ☀️ Ensoleillement: {Ensoleillement * 100}% | ☔ Précipitations: {Precipitation} mm ";;
        return message;
    }
    public void DefinirMeteoAleatoirement()  //ajuster les tirages au sort
    {
        Random rng = new Random();
        Temperature = rng.Next(10, 40);
        Precipitation = Math.Round(rng.NextDouble() * 100, 1); // en mm
        Ensoleillement = Math.Round(rng.NextDouble(), 2);
    }
    public void AppliquerEffet()  //sur les parcelles
    {

    }
   
}