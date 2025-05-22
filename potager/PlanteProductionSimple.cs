public class PlanteProductionSimple : Plante
{
    public PlanteProductionSimple(string nom, string saison, string terrainPref, int temp, int eau, int soleil, int tempsMure, int prix) : base(nom, saison, terrainPref, temp, eau, soleil, tempsMure, prix)
    {

    }
    public override string ToString()
    {
        string description="";
        if (EstMorte==false)
        {
            description=$"{Nom} | Santé: {Sante}% | Age: {Age}";
        }
        else
        {
            description=$"{Nom} est morte";
        }
        return description;
    }
    public override int AvoirQuantiteRecolte()
    {
        return 1;
    }
}