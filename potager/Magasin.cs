public class Magasin
{
    public List<Semis> SemisDisponible { get; set; }
    public List<Outils> OutilsDisponible { get; set; }
    public int PrixTerrain { get; set; }
    public int PrixParcelle { get; set; }


    public Magasin ( int prixTerrain = 100, int prixParcelle=25) //adapter les prix
    {
        PrixTerrain=prixTerrain;
        PrixParcelle=prixParcelle;


        SemisDisponible=new List<Semis>
        {
            new Semis("Tomate",5) //nom et prix
        };


        OutilsDisponible=new List<Outils>
        {
            new Outils("Bâche",5) //nom et prix
        };
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


    public void AcheterOutils(Semis semis, int quantite)
    {
       
    }
}
