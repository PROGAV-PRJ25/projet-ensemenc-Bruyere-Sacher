public class Simulation
{
    public Joueur Jardinier { get; set; }  // Référence au jardin (anciennement "ferme" et jardinier)
    public Meteo Meteo { get; set; }
    public int Semaine { get; set; }

    public Simulation(Joueur jardinier, Meteo meteo)
    {
        Jardinier = jardinier;
        Meteo = meteo;
        Semaine = 0;
    }
    
}