public class TerrainVolcanique : Terrain
{
    public TerrainVolcanique() : base("Volcanique")
    {
    }

    public override void MiseAJourCondition()
    {
        // L'humidité peut être modérée à faible en raison de la chaleur des volcans.
        Humidite = (int)(Humidite * 0.7); // Réduction de l'humidité pour simuler la chaleur volcanique

        // Limiter l'humidité à 100% maximum
        if (Humidite > 100) 
        {
            Humidite = 100;
        }

        // Température: la chaleur provenant des volcans peut augmenter la température.
        Temperature += 6; // On augmente la température de manière significative pour simuler la chaleur volcanique

        // Ensoleillement: l'ensoleillement peut être obscurci par les cendres volcaniques, surtout en période d'activité.
        if (Ensoleillement * 0.8 < 100)
        {
            Ensoleillement = (int)(Ensoleillement * 0.8); // On réduit l'ensoleillement de 20% pour simuler les cendres volcaniques
        }
        else
        {
            Ensoleillement = 100; // On ne dépasse pas 100
        }
    }
}
