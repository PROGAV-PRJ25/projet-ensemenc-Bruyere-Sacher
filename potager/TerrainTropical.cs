public class TerrainTropical : Terrain
{
    public TerrainTropical() : base("Tropical")
    {
        // Initialisation spécifique si nécessaire.
    }

    public override void MiseAJourCondition(Parcelle parcelle)
    {
        // L'humidité est généralement très élevée dans les climats tropicaux
        parcelle.HumiditeParcelle = (int)(Humidite * 1.2); // Augmenter l'humidité de 20% pour simuler le climat tropical humide

        // Limiter l'humidité à 100% maximum
        if (parcelle.HumiditeParcelle > 100)
        {
            parcelle.HumiditeParcelle = 100;
        }
        
        // Ensoleillement: l'ensoleillement est généralement élevé mais il y a aussi souvent de la couverture nuageuse et des pluies
        if (parcelle.EnsoleillementParcelle * 0.9 < 100)
        {
            parcelle.EnsoleillementParcelle = (int)(parcelle.EnsoleillementParcelle * 0.9); // Réduire l'ensoleillement de 10% pour tenir compte des périodes de pluie
        }
        else
        {
            parcelle.EnsoleillementParcelle = 100; // On ne dépasse pas 100
        }
    }
}
