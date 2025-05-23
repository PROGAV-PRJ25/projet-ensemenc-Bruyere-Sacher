public class PlanteProductionMultiple : Plante
{
    public int NombreProduit {get; set;}
    public PlanteProductionMultiple(string nom, string saison, string terrainPref, int temp, int eau, int soleil, int tempsMure,int prix,int nombreProduit=1) : base(nom, saison, terrainPref, temp, eau, soleil, tempsMure, prix)
    {
    
    }
    public override string ToString()
    {
        string description="";
        if (EstMorte==false)
        {
            description=$"{Nom} | Santé: {Sante}% | Age: {Age}| Nombres de produits: {NombreProduit}";
        }
        else
        {
            description=$"{Nom} est morte";
        }
        return description;
    }

    public override void AnalyserSante(int tempActuelle, int humiditeActuelle, int luminositeActuelle)
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

        NombreProduit+=conditionsRemplies; //augmenter le  nombre de produits si les conditions sont remplies
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
    
    public override int AvoirQuantiteRecolte()
    {
        return NombreProduit;
    }
}