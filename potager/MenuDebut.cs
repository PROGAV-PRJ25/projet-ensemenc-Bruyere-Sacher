public class MenuDebut
{
  public Partie? AfficherMenu()
  {
    //Console.Clear();
    AffichageBanniereDebut();
    string? choix = "";
    Console.WriteLine("1. 🌱 Commencer une nouvelle partie");
    Console.WriteLine("2. ❌ Quitter le jeu");

    while (choix != "1" && choix != "2")
    {
      Console.Write("Ton choix : ");
      choix = Console.ReadLine();

      if (choix == "1")
      {
        return DemarrerPartie();
      }
      else if (choix == "2")
      {
        Console.WriteLine("👋 À bientôt !");
        Environment.Exit(0);
      }
      else
      {
        Console.WriteLine("❌ Choix invalide. Tape '1' pour commencer ou '2' pour quitter.");
      }
    }
    return null; //ne sera jamais atteint, mais nécessaire pour compiler
    
  }
  public Partie DemarrerPartie()  //Partie = le type de l’objet que la méthode renvoie
  {
    string nom = "";

    while (string.IsNullOrWhiteSpace(nom)) //évite que l’utilisateur entre une chaîne vide ou juste des espaces.
    {
        Console.Write("👤 Entre le nom de ton joueur : ");
        nom = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(nom)) //évite que l’utilisateur entre une chaîne vide ou juste des espaces.
        {
            Console.WriteLine("❌ Le nom ne peut pas être vide. Réessaie.");
        }
    }
    Joueur joueur = new Joueur(nom, 500);
    Meteo meteo = new Meteo(); 

    Simulation simulation = new Simulation(joueur, meteo);
    
    return new Partie(joueur, simulation);
  }


  public void AffichageBanniereDebut()
  {
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.DarkYellow;


    Console.WriteLine("================================================================================================================================================");

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("      ⠀⠀⠀⠀⠀         ");
    Console.WriteLine("        ⠀⢀⣀⠀⠀          ");
    Console.WriteLine("    ⠀   ⠰⠿⠿⢿⣆ ⠀          ");
    Console.WriteLine("    ⠀        ⠿ ⠀          ");
    Console.WriteLine("     ⠀⠀  ⠀⢀⣠⡿⠀          ");
    Console.WriteLine("        ⢠⣾⠟           ");
    Console.WriteLine("        ⠉⠛⠀         ");
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("       ⢀⣀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀         ");
    Console.WriteLine("     ⣰⣿⣿⣿⣿⣿⣷⡀⠀⠀⠀⠀⠀       ");
    Console.WriteLine("   ⢠⣿⣿⣿⣿⣿⣿⣿⣿⣦⠀⠀⠀⠀        ");
    Console.WriteLine("   ⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀        ");
    Console.WriteLine("   ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀       🌱 Bienvenue dans le projet *ENSEMENC* !⠀");
    Console.WriteLine("   ⢿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⠀⠀⠀   Projet réalisé par Juliette SACHER & Sarah BRUYERE");
    Console.WriteLine("   ⠘⣿⣿⣿⣿⣿⣿⣿⡿⠃⠀⠀⠀⠀      Module : Programmation avancée – S6 ENSC 2024");
    Console.WriteLine("    ⣿⣿⣿⣿⣿⣿⣿⡇⠀⠀⠀⠀⠀ ");
    Console.WriteLine("    ⢹⣿⣿⣿⣿⣿⣿⠁⠀⠀⠀⠀⠀");
    Console.WriteLine("    ⢸⣿⣿⣿⣿⣿⡏⠀⠀⠀⠀⠀⠀");
    Console.WriteLine("    ⠈⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀");
    Console.WriteLine("     ⢿⣿⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀");
    Console.WriteLine("     ⠘⣿⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀");
    Console.WriteLine("      ⠙⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀");



    Console.WriteLine("  ███████╗███╗   ██╗███████╗███████╗███╗   ███╗███████╗███╗   ██╗ ██████╗ ");
    Console.WriteLine("  ██╔════╝████╗  ██║██╔════╝██╔════╝████╗ ████║██╔════╝████╗  ██║██╔════╝  ");
    Console.WriteLine("  █████╗  ██╔██╗ ██║███████╗█████╗  ██╔████╔██║█████╗  ██╔██╗ ██║██║      ");
    Console.WriteLine("  ██╔══╝  ██║╚██╗██║     ██║██╔══╝  ██║╚██╔╝██║██╔══╝  ██║╚██╗██║██║      ");
    Console.WriteLine("  ███████╗██║ ╚████║███████║███████╗██║ ╚═╝ ██║███████╗██║ ╚████║╚██████╔╝");
    Console.WriteLine("  ╚══════╝╚═╝  ╚═══╝╚══════╝╚══════╝╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝ ╚═════╝ ");
    Console.WriteLine();

    Console.WriteLine("                       🌱 Bienvenue dans le projet ENSemenC 🌱");
    Console.WriteLine("         Projet 2024 - Module programmation avancée (S6 - ENSC)");
    Console.WriteLine("                 Jeu réalisé par Juliette SACHER et Sarah BRUYERE");
    Console.WriteLine();

    Console.WriteLine("================================================================================================================================================");


    Console.ForegroundColor = ConsoleColor.White; //on remet la couleur blanche par default
  }
  
}

