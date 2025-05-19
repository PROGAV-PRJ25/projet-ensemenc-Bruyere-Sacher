public class Parcelle
{
    public int NumeroParcelle { get; set; }
    public Plante? Plante { get; set; }  // null si vide
    public bool Vide { get; set; }
    public Terrain TerrainAssocie { get; set; } 
    public int HumiditeParcelle {get; set;}
    public int EnsoleillementParcelle {get; set;}
    public bool EstProtegee {get; set;}
    public Urgence? UrgenceAssociee { get; set; }
    public int DureeProtectionRestante { get; set; } = 0; //durée de la protection
    public Parcelle(int numero, Terrain terrain)
    {
        NumeroParcelle = numero;
        Vide = true;
        TerrainAssocie = terrain;
        HumiditeParcelle = terrain.Humidite;
        EnsoleillementParcelle = terrain.Ensoleillement;
        EstProtegee = false;
    }
    public override string ToString()
    {
        string message=$"%";;
        return message;
    }

    public void AjouterPlante(Plante plante)
    {
        if (Vide==true)
        {
            Plante = plante;
            plante.IdParcelle = this;
            Console.WriteLine($"la {plante} a bien été ajouté à la parcelle numéro {NumeroParcelle}");
        }
        else
            Console.WriteLine($"Parcelle {NumeroParcelle} est déjà occupée !");
    }

}