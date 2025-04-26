public class Semis
{
    public string NomPlante { get; set; }
    public int PrixAchat { get; set; }
    public int Quantite { get; set; }


    public Semis(string nomPlante, int prix, int quantite=0)
    {
        NomPlante = nomPlante;
        PrixAchat = prix;
        Quantite = quantite;
    }
}
