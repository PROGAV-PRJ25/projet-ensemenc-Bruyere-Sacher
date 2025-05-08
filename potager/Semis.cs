public class Semis
{
    public string NomPlante { get; set; }
    public int PrixAchat { get; set; }
    public int Quantite { get; set; }
    public bool EstProductionMultiple { get; set; }


    public Semis(string nomPlante, int prix,bool estProductionMultiple, int quantite=0)
    {
        NomPlante = nomPlante;
        PrixAchat = prix;
        Quantite = quantite;
        EstProductionMultiple=estProductionMultiple;
    }
}
