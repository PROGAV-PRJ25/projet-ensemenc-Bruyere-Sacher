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
            description=$"{Nom} | Santé: {Sante}% | Croissance: {Croissance}% | Nombres de produits: {NombreProduit}%";
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
        if (tempActuelle >= TemperaturePrefere - 3 && tempActuelle <= TemperaturePrefere + 3)
        {
            conditionsRemplies++;
        }
        else
        {
            Sante -= 10;
        }

        //Humidité
        int ecartEau = Math.Abs(BesoinEau - humiditeActuelle);
        if (ecartEau < 10)
        {
            conditionsRemplies++;
        }
        else
        {
            Sante -= 10;
        }

        //Luminosité
        int ecartLumiere = Math.Abs(BesoinLumiere - luminositeActuelle);
        if (ecartLumiere < 10)
        {
            conditionsRemplies++;
        }
        else
        {
            Sante -= 10;
        }

        if (conditionsRemplies==0)
        {
            Sante = 0;
        }
        
        NombreProduit+=conditionsRemplies; //augmenter le  nombre de produits si les conditions sont remplies
        Age+=conditionsRemplies; // accélerer la croissance si les conditions sont remplies

        Sante = Math.Max(Sante, 0); // Si santé inférieur à 0 -> ramener la santé a 0%
        //Si 0 de santé -> plante morte 
        if (Sante==0)
        {
            EstMorte=true;
        }
    }
    public override int AvoirQuantiteRecolte()
    {
        return NombreProduit;
    }
}