public abstract class Plante 
{ 
    public string Nom {get; set;}
    public string SaisonSemis {get; set;}  //possibilité de mettre en liste
    public string TerrainPrefere {get; set;}
    // public int EspacementNecessaire {get; set;}   // place necessaire pour planter un semi
    public int TemperaturePrefere {get; set;}
    public int BesoinEau {get; set;}   // en pourcentage
    public int BesoinLumiere {get; set;}   // en pourcentage
    // public int Prix {get; set;} on verra ou on le met
    public int TempsDeMaturation {get; set;}  //temps en semaine pour qu'on puisse recolter la plante
    public int Sante {get; set;}
    public int Age {get; set;}  //en semaine
    public int Croissance {get; set;}  // en pourcentage
    public bool EstMorte {get; set;} // plante morte ou non
    public Parcelle? IdParcelle { get; set; } // parcelle sur laquelle la plante est

    public Plante(string nom, string saison, string terrainPref, int temp, int eau, int soleil, int tempsMure) 
    { 
        Nom=nom;
        SaisonSemis=saison;
        TerrainPrefere=terrainPref;
        TemperaturePrefere=temp;
        BesoinEau=eau;
        BesoinLumiere=soleil;
        TempsDeMaturation=tempsMure;
        Sante=100;
        Age=0;
        Croissance=(int)(Age/TempsDeMaturation*100); //quand 100% on peut recoleter
        EstMorte=false;
        //cas des maladies
    } 

    public override string ToString()
    {
        string description="";
        if (EstMorte==false)
        {
            description=$"{Nom} | Santé: {Sante}% | Croissance: {Croissance}%";
        }
        else
        {
            description=$"{Nom} est morte";
        }
        return description;
    }

    public virtual void AnalyserSante(int tempActuelle, int humiditeActuelle, int luminositeActuelle)
    {
        if (tempActuelle < TemperaturePrefere - 3 || tempActuelle > TemperaturePrefere+3)
            Sante -= 10;

        int ecartEau = Math.Abs(BesoinEau - humiditeActuelle);
        if (ecartEau >= 10)
            Sante -= 10;

        int ecartSoleil = Math.Abs(BesoinLumiere - luminositeActuelle);
        if (ecartSoleil >= 10)
            Sante -= 10;

        Sante = Math.Max(Sante, 0);

        // Vérification des conditions préférées
        int conditionsRemplies = 0;
        if (tempActuelle >= TemperaturePrefere-3 && tempActuelle <= TemperaturePrefere+3)
        {
            conditionsRemplies++;
        }

        if (ecartEau < 10) 
        {
            conditionsRemplies++;
            Age+=1;
        }
        if (ecartSoleil < 10) 
        {
            conditionsRemplies++;
        }

        double tauxReussi = conditionsRemplies *100/ 3;
        if (tauxReussi < 50)
        {
            Sante=0;
        }

        if (ecartSoleil == 0)
        {
            Age+=1;
        }
        Age+=1;

        if (Sante==0)
        {
            EstMorte=true;
        }
        //ajuster les regles
    }
}