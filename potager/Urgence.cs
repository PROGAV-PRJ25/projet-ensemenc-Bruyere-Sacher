public class Urgence
{
    public string TypeUrgenceDeclenchee { get; set; } = string.Empty; // Toujours initialisé
    public int Gravite { get; set; } // Échelle de 1 à 5
    public bool ProblemeResolu { get; set; }
    public Random random = new Random();
    public int ChanceDeSurvenir { get; set; } = 100; // 40% de chance d'avoir une urgence
    public int SemainesNonResolue { get; set; } = 0;

    public Urgence()
    {
        ProblemeResolu = true;
    }


    // Déclenche une urgence aléatoire en fonction de la probabilité
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


    //Affiche un message d'alerte d'une urgence dans la console
    public void AfficherAlerte()
    {
        Console.WriteLine($"🚨 URGENCE ! : Il y a un problème de type {TypeUrgenceDeclenchee.ToUpper()} avec une gravité de {Gravite}.");
    }

    // Tire aléatoirement un type d’urgence dans une liste des urgences prédéfinie
    public string TypeUrgence()
    {
        string[] typesUrgence = { "Pacari (cochon sauvage)", "Oiseaux", "Grêle" };
        int typeUrgence = random.Next(typesUrgence.Length);
        return typesUrgence[typeUrgence];
    }


    // Gère la résolution de l’urgence sur une parcelle donnée
    public void Resoudre(Joueur joueur, Parcelle parcelleTouchee, Magasin magasin)
    {
        if (!ProblemeResolu)
        {
            Console.WriteLine($"{TypeUrgenceDeclenchee} est survenue sur la parcelle {parcelleTouchee.NumeroParcelle}.");

            // Si une plante est présente, elle subit des dégâts
            if (parcelleTouchee.Plante != null)
            {
                int perteDeSante = Gravite * 5;
                parcelleTouchee.Plante.Sante -= perteDeSante;
                Console.WriteLine($"La santé de la plante à diminuée, elle est à: {parcelleTouchee.Plante.Sante}%");

                // Si la plante n’a plus de santé, elle meurt   
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
            // Propose de protéger le terrain contre l’urgence
            ProtegerTerrain(joueur, parcelleTouchee, magasin);

            //  Protection dure 2 semaines si appliquée
            if (parcelleTouchee.EstProtegee)
            {
                parcelleTouchee.DureeProtectionRestante = 2;
            }


            ProblemeResolu = true;
        }
        else
        {
            // Affiche que le problème a déjà été résolu
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

    // Permet au joueur d’utiliser un outil de protection sur une parcelle
    public void UtiliserOutil(Joueur joueur, Parcelle parcelleTouchee)
    {
        if (joueur.StockOutils.Count == 0)
        {
            Console.WriteLine("❌ Vous n'avez aucun outil de protection dans votre stock.");
            return;
        }

        // Affiche les outils disponibles
        Console.WriteLine("Quel outil souhaitez-vous utiliser pour protéger le terrain ?");
        for (int i = 0; i < joueur.StockOutils.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {joueur.StockOutils[i].NomOutil} (x{joueur.StockOutils[i].Quantite})");
        }

        // Demande à l’utilisateur de choisir un outil
        Console.WriteLine("Entrez le numéro de l'outil ou tapez '0' pour annuler.");
        string choix = Console.ReadLine()?.ToLower() ?? "";
        int choixOutil = -1;

        if (choix != "0" && int.TryParse(choix, out choixOutil) && choixOutil > 0 && choixOutil <= joueur.StockOutils.Count)
        {
            var outilChoisi = joueur.StockOutils[choixOutil - 1];

            string nom = outilChoisi.NomOutil.ToLower();
            // Si l’outil est un outil de protection reconnu
            if (nom.Contains("bache") || nom.Contains("bâche") || nom.Contains("serre") || nom.Contains("cloture") || nom.Contains("clôture") || nom.Contains("épouvantail") || nom.Contains("epouventail"))
            {
                ProblemeResolu = true;
                parcelleTouchee.EstProtegee = true;
                Console.WriteLine($"✅ La parcelle a été protégée avec succès grâce à l'{outilChoisi.NomOutil}'.");

                outilChoisi.Quantite -= 1;
                if (outilChoisi.Quantite <= 0)
                {
                    joueur.StockOutils.Remove(outilChoisi);
                }
            }
            else
            {
                // Si mauvais outil utilisé, la plante meurt si elle existe
                if (parcelleTouchee.Plante != null)
                {
                    parcelleTouchee.Plante.Sante = 0;
                    parcelleTouchee.Plante.EstMorte = true;
                    Console.WriteLine("❌ Cet outil ne peut pas être utilisé pour protéger le terrain, votre plante est morte.");
                }
                else
                {
                    Console.WriteLine("Aucun outil acheté, mais la parcelle est vide.");
                }
            }
                
            
        }
        else if (choix == "0")
        {
            // En cas d'annulation, la plante meurt si elle est présente
            if (parcelleTouchee.Plante != null)
            {
                parcelleTouchee.Plante.Sante = 0;
                parcelleTouchee.Plante.EstMorte = true;
                Console.WriteLine("❌ Vous avez annulé l'utilisation d'un outil, votre plante est morte.");
            }
            else
            {
                Console.WriteLine("Aucun outil acheté, mais la parcelle est vide.");
            }
          
        }
        else
        {
            Console.WriteLine("❌ Numéro d'outil invalide.");
        }
    }

// Demande au joueur s’il souhaite protéger sa parcelle et gère l’achat d’outil si nécessaire
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
                    // Vérifie si un outil de protection a été acheté
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
                        // Si aucun outil acheté, la plante meurt
                        if (parcelleTouchee.Plante != null)
                        {
                            parcelleTouchee.Plante.Sante = 0;
                            parcelleTouchee.Plante.EstMorte = true;
                            Console.WriteLine("❌ Vous n'avez pas acheté d'outil de protection, votre plante est morte.");
                        }
                        else
                        {
                            Console.WriteLine("❌ Vous n'avez pas acheté d'outil de protection, mais la parcelle est vide.");
                        }
                    }
                }
                else
                {
                    // Si aucun outil acheté, la plante meurt
                    if (parcelleTouchee.Plante != null)
                    {
                        parcelleTouchee.Plante.Sante = 0;
                        parcelleTouchee.Plante.EstMorte = true;
                        Console.WriteLine("Aucun outil acheté, votre plante est morte.");
                    }
                    else
                    {
                        Console.WriteLine("Aucun outil acheté, mais la parcelle est vide.");
                    }
                }
            }
        }
        else
        {
            // Si le joueur refuse de protéger la parcelle, la plante meurt
            if (parcelleTouchee.Plante != null)
            {
                parcelleTouchee.Plante.EstMorte = true;
                parcelleTouchee.Plante.Sante = 0;
                Console.WriteLine("Vous n'avez pas voulu protéger votre terrain, votre plante est morte.");
                Console.WriteLine($"Parcelle {parcelleTouchee.NumeroParcelle} - Santé : {parcelleTouchee.Plante.Sante}% - Morte ? {parcelleTouchee.Plante.EstMorte}");

            }
            else
            {
                Console.WriteLine("Vous n'avez pas voulu protéger le terrain, mais la parcelle est vide.");
            }
        }
    }
        
    

}
