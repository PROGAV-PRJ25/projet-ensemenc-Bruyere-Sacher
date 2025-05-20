public class TerrainVolcanique : Terrain
{
    public TerrainVolcanique() : base("Volcanique")
    {
    }

    public override void MiseAJourCondition(Parcelle parcelle)
    {
        // L'humidité peut être modérée à faible en raison de la chaleur des volcans.
        parcelle.HumiditeParcelle = (int)(parcelle.HumiditeParcelle * 0.7); // Réduction de l'humidité pour simuler la chaleur volcanique

        // Limiter l'humidité à 100% maximum
        if (parcelle.HumiditeParcelle > 100) 
        {
            parcelle.HumiditeParcelle = 100;
        }

     
        // Ensoleillement: l'ensoleillement peut être obscurci par les cendres volcaniques, surtout en période d'activité.
        if (parcelle.EnsoleillementParcelle * 0.8 < 100)
        {
            parcelle.EnsoleillementParcelle = (int)(parcelle.EnsoleillementParcelle * 0.8); // On réduit l'ensoleillement de 20% pour simuler les cendres volcaniques
        }
        else
        {
            parcelle.EnsoleillementParcelle = 100; // On ne dépasse pas 100
        }
    }
}
