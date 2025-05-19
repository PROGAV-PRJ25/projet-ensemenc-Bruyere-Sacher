public class Urgence
{
    public string TypeUrgenceDeclenchee { get; set; } = string.Empty; // Toujours initialisé
    public int Gravite { get; set; } // Échelle de 1 à 5
    public bool ProblemeResolu { get; set; }
    private Random random = new Random();
    public int ChanceDeSurvenir { get; set; } = 100; // 90% de chance d'avoir une urgence
    public int SemainesNonResolue { get; private set; } = 0;

    public Urgence()
    {
        ProblemeResolu = true;
    }

    // public void DeclencherAleatoirement()
    // {
    //     int chance = random.Next(1, 101);

    //     if (chance <= ChanceDeSurvenir)
    //     {
    //         TypeUrgenceDeclenchee = TypeUrgence();
    //         Gravite = random.Next(1, 6);
    //         ProblemeResolu = false;
    //         AfficherAlerte();
    //     }
    // }

    public bool DeclencherAleatoirement()
    {
        int chance = random.Next(1, 101);

        if (chance <= ChanceDeSurvenir)
        {
            TypeUrgenceDeclenchee = TypeUrgence();
            Gravite = random.Next(1, 6);
            ProblemeResolu = false;
            AfficherAlerte();
            return true;
        }

        return false;
    }

    public void AfficherAlerte()
    {
        Console.WriteLine($"🚨 URGENCE ! : Il y a un problème de type {TypeUrgenceDeclenchee.ToUpper()} avec une gravité de {Gravite}.");
    }

    private string TypeUrgence()
    {
        string[] typesUrgence = { "Pacari (cochon sauvage)", "Oiseaux", "Grêle" };
        int typeUrgence = random.Next(typesUrgence.Length);
        return typesUrgence[typeUrgence];
    }

    // public void EmpirerSiNonResolue()
    // {
    //     if (!ProblemeResolu)
    //     {
    //         SemainesNonResolue++;
    //         Gravite = Math.Min(5, Gravite + 1);
    //     }
    // }

    // public void ResetUrgence()
    // {
    //     ProblemeResolu = true;
    //     SemainesNonResolue = 0;
    //     TypeUrgenceDeclenchee = string.Empty;
    // }

    public void Resoudre(Joueur joueur, Parcelle parcelleTouchee, Magasin magasin)
    {
        if (!ProblemeResolu)
        {
            Console.WriteLine($"{TypeUrgenceDeclenchee} est survenue sur la parcelle {parcelleTouchee.NumeroParcelle}.");

            if (parcelleTouchee.Plante != null)
            {
                int perteDeSante = Gravite * 5;
                parcelleTouchee.Plante.Sante -= perteDeSante;

                if (parcelleTouchee.Plante.Sante <= 0)
                {
                    parcelleTouchee.Plante.EstMorte = true;
                    Console.WriteLine($"❌ La plante de la parcelle {parcelleTouchee.NumeroParcelle} est morte.");
                }
            }
            else
            {
                Console.WriteLine($"⚠️ La parcelle {parcelleTouchee.NumeroParcelle} ne contient pas de plante.");
            }

            ProtegerTerrain(joueur, parcelleTouchee, magasin);
        }
        else
        {
            if (parcelleTouchee.Plante != null)
            {
                Console.WriteLine($"✅ L'urgence a déjà été résolue sur la parcelle {parcelleTouchee.NumeroParcelle}. Santé actuelle : {parcelleTouchee.Plante.Sante}%");
            }
            else
            {
                Console.WriteLine($"✅ L'urgence a été résolue sur la parcelle {parcelleTouchee.NumeroParcelle}, mais elle est vide.");
            }
        }
    }

    private void UtiliserOutil(Joueur joueur, Parcelle parcelleTouchee)
    {
        if (joueur.StockOutils.Count == 0)
        {
            Console.WriteLine("❌ Vous n'avez aucun outil de protection dans votre stock.");
            return;
        }

        Console.WriteLine("Quel outil souhaitez-vous utiliser pour protéger le terrain ?");
        for (int i = 0; i < joueur.StockOutils.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {joueur.StockOutils[i].NomOutil} (x{joueur.StockOutils[i].Quantite})");
        }

        Console.WriteLine("Entrez le numéro de l'outil ou tapez '0' pour annuler.");
        string choix = Console.ReadLine()?.ToLower() ?? "";
        int choixOutil = -1;

        if (choix != "0" && int.TryParse(choix, out choixOutil) && choixOutil > 0 && choixOutil <= joueur.StockOutils.Count)
        {
            var outilChoisi = joueur.StockOutils[choixOutil - 1];

            string nom = outilChoisi.NomOutil.ToLower();
            if (nom.Contains("bache") || nom.Contains("bâche")|| nom.Contains("serre")  || nom.Contains("cloture") || nom.Contains("clôture") || nom.Contains("épouvantail") || nom.Contains("epouventail"))
            {
                ProblemeResolu = true;
                Console.WriteLine($"✅ La parcelle a été protégée avec succès grâce à l'{outilChoisi.NomOutil}'.");

                outilChoisi.Quantite -= 1;
                if (outilChoisi.Quantite <= 0)
                {
                    joueur.StockOutils.Remove(outilChoisi);
                }
            }
            else
            {
                Console.WriteLine("❌ Cet outil ne peut pas être utilisé pour protéger le terrain.");
            }
        }
        else if (choix == "0")
        {
            Console.WriteLine("❌ Vous avez annulé l'utilisation d'un outil.");
        }
        else
        {
            Console.WriteLine("❌ Numéro d'outil invalide.");
        }
    }


  private void ProtegerTerrain(Joueur joueur, Parcelle parcelleTouchee, Magasin magasin)
    {
        string reponse = string.Empty;
        do
        {
            Console.WriteLine($"Voulez-vous protéger le terrain de la parcelle {parcelleTouchee.NumeroParcelle} ? (oui/non)");
            reponse = Console.ReadLine()?.ToLower() ?? "";

            if (reponse != "oui" && reponse != "non")
            {
                Console.WriteLine("Réponse invalide. Veuillez répondre par 'oui' ou 'non'.");
            }

        } while (reponse != "oui" && reponse != "non");

        if (reponse == "oui")
        {
            if (joueur.StockOutils.Count > 0)
            {
                UtiliserOutil(joueur, parcelleTouchee);
            }
            else
            {
                Console.WriteLine("❌ Vous n'avez pas d'outil de protection dans votre stock.");
                Console.WriteLine("Voulez-vous acheter un outil de protection ? (oui/non)");

                string achatReponse = Console.ReadLine()?.ToLower() ?? "";

                if (achatReponse == "oui")
                {
                    magasin.AcheterOutils();

                    if (joueur.StockOutils.Any(o =>
                        o.NomOutil.ToLower().Contains("bache") ||
                        o.NomOutil.ToLower().Contains("bâche") ||
                        o.NomOutil.ToLower().Contains("serre") ||
                        o.NomOutil.ToLower().Contains("cloture") ||
                        o.NomOutil.ToLower().Contains("clôture") ||
                        o.NomOutil.ToLower().Contains("épouvantail") ||
                        o.NomOutil.ToLower().Contains("epouventail")))
                    {
                        UtiliserOutil(joueur, parcelleTouchee);
                    }
                    else
                    {
                        Console.WriteLine("❌ Vous n'avez pas acheté d'outil de protection.");
                    }
                }
                else
                {
                    Console.WriteLine("Aucun outil acheté.");
                }
            }
        }
    }

}
