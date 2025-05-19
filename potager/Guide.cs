public class Guide
{

    public static void CaracteristiquesPlantes()
    {
        Console.WriteLine("\n📗 Liste des plantes et leurs terrains préférés :\n");
        Console.WriteLine("🍅 Tomate           → Terrain volcanique");
        Console.WriteLine("🌶️ Piment           → Terrain volcanique");
        Console.WriteLine("🥑 Avocat           → Terrain volcanique");
        Console.WriteLine("🌵 Nopale (Cactus)  → Terrain désertique");
        Console.WriteLine("🌵 Agave            → Terrain désertique");
        Console.WriteLine("🌺 Fleurs Tithonia  → Terrain désertique");
        Console.WriteLine("🌱 Haricot          → Terrain désertique");
        Console.WriteLine("🍉 Pastèque         → Terrain tropical");
        Console.WriteLine("🥭 Papaye           → Terrain tropical");
        Console.WriteLine("🍠 Igname           → Terrain tropical");
    }

    public static void ReglesJeu()
    {
        Console.WriteLine("📘 RÈGLES DU JEU – Simulateur de Potager");
        Console.WriteLine("\n🌱 Dans ce jeu, tu dois t'occuper de ton potager, pour cela tu peux planter, arroser, acheter et vendre tes récoles.");
        Console.WriteLine("\n Tu ne peux planter des semis que sur leur terrains favoris!");
        Console.WriteLine("\n Mais attention ! Si moins de 50% des conditions préférées (ensoleillement, précipitation...) des plantes ne sont pas respectées, la plante meurt");
        Console.WriteLine("\n Il y a 40% de chance qu'une urgence se déclanche. Attention aux intrus et intempéries qui peuvent tué tes plantes si tu ne t'en occupes pas, il faudra alors avoir les outils necessaires pour maintenir tes plantes en vie.");
        Console.WriteLine("\n Ces outils de protections ne peuvent servir que 2 semaines avant de se casser");
    }
}
