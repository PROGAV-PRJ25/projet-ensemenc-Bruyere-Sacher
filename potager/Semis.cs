public class Semis //semis que le joueur possede
{
    public string NomPlante { get; set; }
    public int Prix { get; set; }
    public int Quantite { get; set; }

    public Semis(string nomPlante, int prix, int quantite)
    {
        NomPlante = nomPlante;
        Prix = prix;
        Quantite = quantite;
    }
}