public class TerrainTropical : Terrain
{
    public TerrainTropical() : base("Tropical")
    {
        // Initialisation spécifique si nécessaire.
    }

    public override void MiseAJourCondition()
    {
        // L'humidité est généralement très élevée dans les climats tropicaux
        Humidite = (int)(Humidite * 1.2); // Augmenter l'humidité de 20% pour simuler le climat tropical humide

        // Limiter l'humidité à 100% maximum
        if (Humidite > 100) Humidite = 100;

        // Température: les températures sont souvent constantes et chaudes
        Temperature += 3; // On augmente légèrement la température pour simuler la chaleur tropicale

        // Ensoleillement: l'ensoleillement est généralement élevé mais il y a aussi souvent de la couverture nuageuse et des pluies
        if (Ensoleillement * 0.9 < 100)
        {
            Ensoleillement = (int)(Ensoleillement * 0.9); // Réduire l'ensoleillement de 10% pour tenir compte des périodes de pluie
        }
        else
        {
            Ensoleillement = 100; // On ne dépasse pas 100
        }
    }
}
