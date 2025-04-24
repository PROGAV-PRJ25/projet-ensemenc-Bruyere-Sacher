public class Joueur
{
    // Propriétés du jardinier
    public string Nom { get; set; }
    public double Argent { get; set; }
    public List<Semis> StockSemis { get; set; }
    public List<Terrain> Terrains { get; set; }
    public Joueur(string nom, int argent)
    {
        Nom = nom;
        Argent = argent;
        StockSemis = new List<Semis>();
        Terrains = new List<Terrain>();
    }
    public void AcheterTerrain(int prixTerrain)
    {
        
    }
    public void AcheterParcelle(Terrain terrain, int prixParcelle)
    {
        
    }
    public void AcheterSemis(Semis semis, int quantite)
    {
        
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