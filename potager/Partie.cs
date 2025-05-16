public class Partie
{
    public Joueur Joueur { get; set; }
    public Simulation Simulation { get; set; }

    public Partie(Joueur joueur, Simulation simulation)
    {
        Joueur = joueur;
        Simulation = simulation;
    }
}
