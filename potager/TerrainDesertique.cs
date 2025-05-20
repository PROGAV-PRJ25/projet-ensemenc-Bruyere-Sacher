public class TerrainDesertique : Terrain
{
    public TerrainDesertique() : base("Désertique")
    {
    }

    public override void MiseAJourCondition(Parcelle parcelle)
    {
        // L'humidité baisse dans un environnement désertique (réduction de moitié)
        parcelle.HumiditeParcelle = (int)(parcelle.HumiditeParcelle * 0.5); // Réduction de moitié de l'humidité, les déserts étant secs.
        
        // Limiter l'humidité à 100% maximum après réduction
        if (parcelle.HumiditeParcelle > 100) 
        {
            parcelle.HumiditeParcelle = 100;
        }

        // Ensoleillement: généralement très élevé dans les déserts
        parcelle.EnsoleillementParcelle = (int)(parcelle.EnsoleillementParcelle * 1.2); // Augmenter l'ensoleillement de 20%
        
        // Limiter l'ensoleillement à 100% maximum
        if (parcelle.EnsoleillementParcelle > 100)
        {
            parcelle.EnsoleillementParcelle = 100;
        }
    }
}
