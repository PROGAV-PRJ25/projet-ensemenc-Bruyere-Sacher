public abstract class Terrain
{
    public string Type {get; set;}
    public int Humidite {get; set;}
    public int Ensoleillement {get; set;}
    public int Temperature {get; set;}
    public List<Parcelle> Parcelles { get; set; }


    public Terrain(string type)
    {
        Type = type;
        Parcelles = new List<Parcelle>();
        AjouterParcelle();
        AjouterParcelle();
    }
    public void AjouterParcelle()
    {
        int numero = Parcelles.Count + 1;
        Parcelles.Add(new Parcelle(numero,this));
    }


    public virtual void MiseAJourCondition()
    {
            //laisser vide puis adapter a chqua sous classe avec override
    }


    // Est compatible
}
