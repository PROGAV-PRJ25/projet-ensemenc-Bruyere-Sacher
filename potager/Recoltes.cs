public class Recoltes
{
    public string TypePlante { get; set; }
    public int Quantite { get; set; }
    public int Prix { get; set; }

    public Recoltes(string typePlante, int quantite, int prix)
    {
        TypePlante=typePlante;
        Quantite=quantite;
        Prix=prix;
    }
   
}
