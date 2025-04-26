public class Outils
{
    public string NomOutils { get; set; }
    public int PrixAchat { get; set; }
    public int Quantite { get; set; }


    public Outils(string nomOutils, int prix, int quantite=0)
    {
        NomOutils = nomOutils;
        PrixAchat = prix;
        Quantite = quantite;
    }
}
