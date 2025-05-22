public abstract class Terrain
{
    public string Type { get; set; }
    public int Humidite { get; set; }
    public int Ensoleillement { get; set; }
    public int Temperature { get; set; }
    public List<Parcelle> Parcelles { get; set; }
    public bool EstProtege { get; set; } = false;

    public Terrain(string type)
    {
        Type = type;
        Humidite = 30;
        Parcelles = new List<Parcelle>();
        AjouterParcelle();
        AjouterParcelle();
        AjouterParcelle(); // 3 parcelles de base
    }

    
    public void AjouterParcelle()
    {
        int numero = Parcelles.Count + 1;
        Parcelles.Add(new Parcelle(numero, this));
    }

     // Méthode virtuelle destinée à être redéfinie dans les sous-classes
    public virtual void MiseAJourCondition(Parcelle parcelle)
    {

    }
}
