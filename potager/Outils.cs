public class Outils
{
    public string NomOutil { get; set; }
    public int PrixAchat { get; set; }
    public int Quantite { get; set; }


    public Outils(string nomOutils, int prix, int quantite=0)
    {
        NomOutil = nomOutils;
        PrixAchat = prix;
        Quantite = quantite;
    }
}
