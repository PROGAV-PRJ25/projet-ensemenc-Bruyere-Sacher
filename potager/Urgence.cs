public class Urgence
{
    public string? TypeUrgenceDeclenchee { get; set; }
    public int Gravite { get; set; } // échelle de 1 à 5
    public bool ProblemeResolu { get; set; }
    private Random random = new Random(); // Instancier Random de manière privée
    public int ChanceDeSurvenir { get; set; } = 10; // 20% de chance pour qu'une urgence survienne
    public int SemainesNonResolue { get; private set; } = 0; // pour gérer la double temporalité

    public Urgence()
    {
        ProblemeResolu = true; // Le problème est résolu par défaut
    }

    // Déclenche une urgence aléatoirement en fonction de la chance
    public void DeclencherAleatoirement()
    {
        int chance = random.Next(1, 101); // Valeur entre 1 et 100 inclus

        if (chance <= ChanceDeSurvenir) // Si le nombre généré est inférieur ou égal à la chance de survenir
        {
            TypeUrgenceDeclenchee = TypeUrgence();
            Gravite = random.Next(1, 6); // Choisir un niveau de gravité entre 1 et 5
            ProblemeResolu = false; // Le problème n'est pas encore résolu
            AfficherAlerte(); // Avertir le joueur
        }
    }

    // Affiche une alerte pour l'urgence déclenchée
    public void AfficherAlerte()
    {
        Console.WriteLine($"🚨 URGENCE ! : Il y a un problème de type {TypeUrgenceDeclenchee.ToUpper()} avec une gravité de {Gravite}.");
    }

    // Méthode pour choisir un type d'urgence aléatoire
    private string TypeUrgence()
    {
        string[] typesUrgence = { "Pacari (cochon sauvage)", "Oiseaux", "Grêle" };
        int typeUrgence = random.Next(typesUrgence.Length); // Choisir un type d'urgence parmi les trois
        return typesUrgence[typeUrgence];
    }

    // Si l'urgence n'est pas résolue, on l'empire avec le temps
    public void EmpirerSiNonResolue()
    {
        if (!ProblemeResolu)
        {
            SemainesNonResolue++;
            Gravite = Math.Min(5, Gravite + 1); // La gravité augmente, mais ne dépasse pas 5
        }
    }

    // Réinitialise l'urgence après qu'elle ait été résolue
    public void ResetUrgence()
    {
        ProblemeResolu = true;
        SemainesNonResolue = 0;
        TypeUrgenceDeclenchee = null;
    }

    //Ne se lance pas et ne marche pas 
    // Résoudre l'urgence en utilisant un jardinier et en vérifiant la protection du terrain
    public void Resoudre(Joueur joueur, Parcelle parcelleTouchee, Magasin magasin)
    {
        // Si l'urgence est toujours en cours, on traite la situation
        if (!ProblemeResolu)
        {
            Console.WriteLine($" {TypeUrgenceDeclenchee} est survenue sur la parcelle {parcelleTouchee.NumeroParcelle}.");

            // Empêcher la plante de se détériorer selon la gravité de l'urgence
            int perteDeSante = Gravite * 5;
            parcelleTouchee.Plante.Sante -= perteDeSante;

            if (parcelleTouchee.Plante.Sante <= 0)
            {
                parcelleTouchee.Plante.EstMorte = true;
                Console.WriteLine($"❌ La plante de la parcelle {parcelleTouchee.NumeroParcelle} est morte.");
            }

            // Demander si le joueur veut protéger le terrain
            ProtegerTerrain(joueur, parcelleTouchee, magasin);
        }
        else
        {
            Console.WriteLine($"✅ L'urgence a déjà été résolue sur la parcelle {parcelleTouchee.NumeroParcelle}. Santé actuelle : {parcelleTouchee.Plante.Sante}%");
        }
    }

    // Méthode pour permettre au joueur de protéger le terrain
    private void ProtegerTerrain(Joueur joueur, Parcelle parcelleTouchee, Magasin magasin)
    {
        string reponse = string.Empty;
        do
        {
            Console.WriteLine($"Voulez-vous protéger le terrain de la parcelle {parcelleTouchee.NumeroParcelle} ? (oui/non)");
            reponse = Console.ReadLine()?.ToLower();

            if (reponse != "oui" && reponse != "non")
            {
                Console.WriteLine("Réponse invalide. Veuillez répondre par 'oui' ou 'non'.");
            }

        } while (reponse != "oui" && reponse != "non");

        if (reponse == "oui")
        {
            // Vérifier si le joueur a des outils dans son stock
            if (joueur.StockOutils.Count > 0)
            {
                Console.WriteLine("Voici les outils de protection dans votre stock :");
                for (int i = 0; i < joueur.StockOutils.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {joueur.StockOutils[i].NomOutil}");
                }

                string choix = string.Empty;
                int choixOutil = -1;

                do
                {
                    Console.WriteLine("Entrez le numéro de l'outil que vous voulez utiliser, ou tapez '0' pour annuler.");
                    choix = Console.ReadLine()?.ToLower();


                    if (choix != "0" && int.TryParse(choix, out choixOutil) && choixOutil > 0 && choixOutil <= joueur.StockOutils.Count)
                    {
                        Outils outilChoisi = joueur.StockOutils[choixOutil - 1];

                        // Vérifier si l'outil est un outil de protection
                        if (outilChoisi.NomOutil == "Bâche​" || outilChoisi.NomOutil == "Bache"|| outilChoisi.NomOutil == "Clôture " || outilChoisi.NomOutil == "Cloture"|| outilChoisi.NomOutil == "Epouventail​")
                        {
                            joueur.Terrains[0].ProtegerTerrain();
                            ProblemeResolu = true;
                            Console.WriteLine($"Le terrain a été protégé avec succès grâce à l'{outilChoisi.NomOutil}.");
                        }
                        else
                        {
                            Console.WriteLine("Cet outil ne peut pas être utilisé pour protéger le terrain.");
                        }
                    }
                    else if (choix != "0")
                    {
                        Console.WriteLine("Numéro d'outil invalide.");
                    }

                } while (choixOutil == -1 && choix != "0");

                if (choixOutil == 0)
                {
                    Console.WriteLine("Vous avez annulé l'utilisation de l'outil.");
                }
            }
            else
            {
                Console.WriteLine("Vous n'avez pas d'outil de protection dans votre stock.");
                Console.WriteLine("Voulez-vous acheter un outil de protection ? (oui/non)");

                string achatReponse = Console.ReadLine()?.ToLower();

                if (achatReponse == "oui")
                {
                    magasin.AcheterOutils();

                    if (joueur.StockOutils.Any(o => o.NomOutil == "Bache" || o.NomOutil == "Bâche" || o.NomOutil == "Clôture" || o.NomOutil == "Cloture"|| o.NomOutil == "Epouventail​"))
                    {
                        joueur.Terrains[0].ProtegerTerrain();
                        ProblemeResolu = true;
                        Console.WriteLine("Le terrain a été protégé avec succès grâce à l'outil de protection acheté.");
                    }
                    else
                    {
                        Console.WriteLine("Vous n'avez pas acheté d'outil de protection.");
                    }
                }
                else
                {
                    Console.WriteLine("Vous avez choisi de ne pas acheter d'outil. Le terrain ne sera pas protégé.");
                }
            }
        }
        else
        {
            Console.WriteLine("Vous avez choisi de ne pas protéger le terrain.");
        }
    }
}
