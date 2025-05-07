public class TerrainDesertique : Terrain
{
    public TerrainDesertique() : base("Désertique")
    {
    }

    public override void MiseAJourCondition()
    {
        // L'humidité baisse dans un environnement désertique (réduction de moitié)
        Humidite = (int)(Humidite * 0.5); // Réduction de moitié de l'humidité, les déserts étant secs.
        
        // Limiter l'humidité à 100% maximum après réduction
        if (Humidite > 100) 
        {
            Humidite = 100;
        }

        // Température: les températures des déserts peuvent être très chaudes pendant la journée et froides la nuit.
        Temperature += 5; // On augmente la température de 5°C pour simuler la chaleur du désert

        // Ensoleillement: généralement très élevé dans les déserts
        Ensoleillement = (int)(Ensoleillement * 1.2); // Augmenter l'ensoleillement de 20%
        
        // Limiter l'ensoleillement à 100% maximum
        if (Ensoleillement > 100)
        {
            Ensoleillement = 100;
        }
    }
}
