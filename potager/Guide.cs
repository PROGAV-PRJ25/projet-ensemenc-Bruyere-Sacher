public class Guide
{

    public static void CaracteristiquesPlantes()
    {
        Console.WriteLine("\n📗 Liste des plantes et leurs terrains préférés :\n");
        Console.WriteLine("🍅 Tomate           → Terrain volcanique | 23°C | Humidité: 90% | Ensoleillement: 90% | Age mure: 6 ");
        Console.WriteLine("🌶️ Piment           → Terrain volcanique | 25°C | Humidité: 50% | Ensoleillement: 90% | Age mure: 7");
        Console.WriteLine("🥑 Avocat           → Terrain volcanique | 25°C | Humidité: 70% | Ensoleillement: 90% | Age mure: 60");
        Console.WriteLine("🌵 Nopale (Cactus)  → Terrain désertique | 30°C | Humidité: 20% | Ensoleillement: 90% | Age mure: 13");
        Console.WriteLine("🌵 Agave            → Terrain désertique | 35°C | Humidité: 10% | Ensoleillement: 90% | Age mure: 100");
        Console.WriteLine("🌺 Fleurs Tithonia  → Terrain désertique | 35°C | Humidité: 20% | Ensoleillement: 90% | Age mure: 6");
        Console.WriteLine("🌱 Haricot          → Terrain désertique | 23°C | Humidité: 10% | Ensoleillement: 90% | Age mure: 6");
        Console.WriteLine("🍉 Pastèque         → Terrain tropical | 30°C | Humidité: 90% | Ensoleillement: 90% | Age mure: 8");
        Console.WriteLine("🥭 Papaye           → Terrain tropical | 27°C | Humidité: 90% | Ensoleillement: 90% | Age mure: 18");
        Console.WriteLine("🍠 Igname           → Terrain tropical | 28°C | Humidité: 70% | Ensoleillement: 90% | Age mure: 15");
    }

    public static void ReglesJeu()
    {
        Console.WriteLine("\n📘 RÈGLES DU JEU – Simulateur de Potager");
        Console.WriteLine("\n🌱 Dans ce jeu, tu dois t'occuper de ton potager, pour cela tu peux planter, arroser, acheter et vendre tes récoles.");
        Console.WriteLine("\n Tu ne peux planter des semis que sur leur terrains favoris!");
        Console.WriteLine("\n Mais attention ! Si toutes les conditions préférées (ensoleillement, précipitation...) des plantes ne sont pas respectées, la plante meurt");
        Console.WriteLine("\n Il y a 20% de chance qu'une urgence se déclanche. Attention aux intrus et intempéries qui tuent tes plantes si tu ne t'en occupes pas, il faudra alors avoir les outils necessaires pour maintenir tes plantes en vie.");
        Console.WriteLine("\n Ces outils de protections ne peuvent servir que 2 semaines avant de se casser");
    }

}
