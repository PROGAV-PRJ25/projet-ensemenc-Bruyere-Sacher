public class Joueur
{
    // Propriétés du jardinier
    public string Nom { get; set; }
    public double Argent { get; set; }
    public List<Semis> StockSemis { get; set; }
    public List<Terrain> Terrains { get; set; }
    public List<Recoltes> MesRecoltes { get; set; }
    public List<Outils> StockOutils { get; set; }


    public Joueur(string nom, int argent)
    {
        Nom = nom;
        Argent = argent;
        StockSemis = new List<Semis>();
        Terrains = new List<Terrain>();
        StockOutils=new List<Outils>();
        MesRecoltes=new List<Recoltes>();
    }


    public void Planter(Plante plante, Parcelle parcelle)
    {
       
    }
    public void Arroser(Parcelle parcelle)
    {
       
    }
    public void Recolter(Parcelle parcelle)
    {
       
    }
    public void Vendre()
    {
       
    }
}
