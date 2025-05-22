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
        // on initialise l'humidite, la temperature et l'ensoleillement 
        Humidite = 50;
        Temperature = 30;
        Ensoleillement = 60;
        //On ajoute 3 parcelle lors de la création d'un terrain
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

    // Méthode absctract destinée à être définie dans les sous-classes
    public abstract void MiseAJourCondition(Parcelle parcelle);
    
}
