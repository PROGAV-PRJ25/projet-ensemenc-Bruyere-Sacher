public class PlanteProductionSimple : Plante
{
    public PlanteProductionSimple(string nom, string saison, string terrainPref, int temp, int eau, int soleil, int tempsMure) : base(nom, saison, terrainPref, temp, eau, soleil, tempsMure)
    {

    }
    public override void AnalyserSante(int tempActuelle, int humiditeActuelle, int luminositeActuelle)
    {
        base.AnalyserSante(tempActuelle, humiditeActuelle, luminositeActuelle); //Appel de la logique de base de la classe Plante
    }
    
    public override int AvoirQuantiteRecolte()
    {
        return 1;
    }
}