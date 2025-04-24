pulbic class Recoltes
{
    public string TypePlante { get; set; }
    public int Quantite { get; set; }


    public Recoltes(string typePlante, int quantite)
    {
        TypePlante=typePlante;
        Quantite=quantite;
    }
   
}
