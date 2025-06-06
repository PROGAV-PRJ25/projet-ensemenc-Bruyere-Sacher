using System.Formats.Asn1;

public abstract class Plante
{
    public string Nom { get; set; }
    public string SaisonSemis { get; set; }  //possibilité de mettre en liste
    public string TerrainPrefere { get; set; }
    // public int EspacementNecessaire {get; set;}   // place necessaire pour planter un semi
    public int TemperaturePrefere { get; set; }
    public int BesoinEau { get; set; }   // en pourcentage
    public int BesoinLumiere { get; set; }   // en pourcentage
    public int TempsDeMaturation { get; set; }  //temps en semaine pour qu'on puisse recolter la plante
    public int Sante { get; set; }
    public int Age { get; set; }  //en semaine
    public bool EstMorte { get; set; } // plante morte ou non
    public int Prix { get; set; } //prix pour les vendre
    public Parcelle? IdParcelle { get; set; } // parcelle sur laquelle la plante est

    public Plante(string nom, string saison, string terrainPref, int temp, int eau, int soleil, int tempsMure, int prix)
    {
        Nom = nom;
        SaisonSemis = saison;
        TerrainPrefere = terrainPref;
        TemperaturePrefere = temp;
        BesoinEau = eau;
        BesoinLumiere = soleil;
        TempsDeMaturation = tempsMure;
        Sante = 100;
        Age = 0;
        EstMorte = false;
        Prix = prix;
        //cas des maladies
    }

    public override string ToString()
    {
        string description = "";
        if (EstMorte == false)
        {
            description = $"";
        }
        else
        {
            description = $"{Nom} est morte";
        }
        return description;
    }
    public virtual void AnalyserSante(int tempActuelle, int humiditeActuelle, int luminositeActuelle)
    {
        // Vérification des conditions préférées
        //Temperature
        int conditionsRemplies = 0;
        if (tempActuelle >= TemperaturePrefere - 2 && tempActuelle <= TemperaturePrefere + 2)// si l'ecart de temperature est très faible, la plante regagne de la vie
        {
            Sante += 5;
            conditionsRemplies++;
        }
        else if (tempActuelle >= TemperaturePrefere - 5 && tempActuelle <= TemperaturePrefere + 5)
        {
            conditionsRemplies++;
        }
        else
        {
            Sante -= 5;
        }

        //Humidité
        int ecartEau = Math.Abs(BesoinEau - humiditeActuelle);
        if (ecartEau <5)// si l'ecart d'humidité est très faible, la plante regagne de la vie
        {
            Sante += 5;
            conditionsRemplies++;
        }
        else if (ecartEau < 10)
        {
            conditionsRemplies++;
        }
        else
        {
            Sante -= 5;
        }

        //Luminosité
        int ecartLumiere = Math.Abs(BesoinLumiere - luminositeActuelle);
        if (ecartLumiere <5) // si l'ecart de l'ensoleillement est très faible, la plante regagne de la vie
        {
            Sante += 5;
            conditionsRemplies++;
        }
        else if (ecartLumiere < 10)
        {
            conditionsRemplies++;
        }
        else
        {
            Sante -= 5;
        }

        if (conditionsRemplies == 0)
        {
            Sante = 0;
        }

        Age += conditionsRemplies; // accélerer la croissance si les conditions sont remplies
        Age++;

        // ajuster sante si inferieur à 0 ou supérieur a 100
        if (Sante > 100)
        {
            Sante = 100;
        }
        if (Sante < 0)
        {
            Sante = 0;
        }
        //Si 0 de santé -> plante morte 
        if (Sante == 0)
        {
            EstMorte = true;
        }
    }
    public abstract int AvoirQuantiteRecolte();
    public virtual void VerifierMort()
    {
        if (Sante == 0)
        {
            EstMorte = true;
        }
    }
}