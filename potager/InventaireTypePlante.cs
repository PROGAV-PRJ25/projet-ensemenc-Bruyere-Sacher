public class InventaireTypePlante
{   
    public InventaireTypePlante() 
    { 
    
    }

    public Plante DefinirPlante(string nom)  //ajuster les tirages au sort
    {
        if (nom=="Tomate"||nom=="tomate")
        {
            return new PlanteProductionMultiple("Tomate", "Printemps", "Volcanique", 23, 90, 90, 6, 2);
        }
        else if (nom=="Piment"||nom=="piment")
        {
            return new PlanteProductionMultiple("Piment", "Printemps", "Volcanique", 25, 50, 90, 7, 2);
        }
        else if (nom=="Nopale"||nom=="nopale")
        {
            return new PlanteProductionSimple("Nopale", "Eté", "Désertique", 30, 20, 90, 13, 2);
        }
        else if (nom=="Agave"||nom=="agave")
        {
            return new PlanteProductionSimple("Agave", "Eté", "Désertique", 35, 10, 90, 100, 2);
        }
        else if (nom=="Pastèque"||nom=="pastèque")
        {
            return new PlanteProductionSimple("Pastèque", "Eté", "Tropical", 30, 90, 90, 8, 2);
        }
        else if (nom=="Fleurs de Tithonia"||nom=="fleurs de tithonia")
        {
            return new PlanteProductionSimple("Fleurs de Tithonia", "Printemps", "Désertique", 35, 20, 90, 6, 2);
        }
        else if (nom=="Haricot"||nom=="haricot")
        {
            return new PlanteProductionMultiple("Haricot", "Printemps", "Désertique", 23, 10, 90, 6, 2);
        }
        else if (nom=="Avocat"||nom=="avocat")
        {
            return new PlanteProductionMultiple("Avocat", "Printemps", "Volcanique", 25, 70, 90, 60, 2);
        }
        else if (nom=="Papaye"||nom=="papaye")
        {
            return new PlanteProductionMultiple("Papaye", "Eté", "Tropical", 27, 90, 90, 18, 2);
        }
        else // pour Igname
        {
            return new PlanteProductionMultiple("Igname", "Printemps", "Tropical", 28, 70, 90, 15, 2);
        }
        
    }
}