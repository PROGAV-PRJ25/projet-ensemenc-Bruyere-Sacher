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

    public void ProtegerTerrain()
    {
        Console.WriteLine("Quel type de protection souhaitez-vous appliquer ?");
        Console.WriteLine("1. Protection contre les Parasites (Pacaris)");
        Console.WriteLine("2. Protection contre les Oiseaux");
        Console.WriteLine("3. Protection contre la Grêle");

        string choixProtection = Console.ReadLine()!;

        bool protectionValide = choixProtection == "1" || choixProtection == "2" || choixProtection == "3";

        if (!protectionValide)
        {
            Console.WriteLine("Choix invalide.");
            return;
        }

        EstProtege = true;

        foreach (var parcelle in Parcelles)
        {
            parcelle.EstProtegee = true;
            Console.WriteLine($"Parcelle {parcelle.NumeroParcelle} est maintenant protégée.");
        }

        //Console.WriteLine("✅ Toutes les parcelles de ce terrain sont maintenant protégées.");
    }

    public virtual void MiseAJourCondition()
    {
        
    }
}
