public class PlanteProductionSimple : Plante
{
    public PlanteProductionSimple(string nom, string saison, string terrainPref, int temp, int eau, int soleil, int tempsMure) : base(nom, saison, terrainPref, temp, eau, soleil, tempsMure)
    {

    }
    public override string ToString()
    {
        string description="";
        if (EstMorte==false)
        {
            description=$"{Nom} | Santé: {Sante}% | Croissance: {Croissance}%";
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