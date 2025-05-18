public class InventaireTypePlante
{   
    public InventaireTypePlante() 
    { 
    
    }

    public Plante DefinirPlante(string nom)  //ajuster les tirages au sort
    {
        if (nom=="Tomate")
        {
            return new PlanteProductionMultiple("Tomate", "Printemps", "Volcanique", 23, 90, 90, 6, 2);
        }
        else if (nom=="Piment")
        {
            return new PlanteProductionMultiple("Piment", "Printemps", "Volcanique", 25, 50, 90, 7, 2);
        }
        else if (nom=="Nopale")
        {
            return new PlanteProductionSimple("Nopale", "Eté", "Désertique", 30, 20, 90, 13, 2);
        }
        else if (nom=="Agave")
        {
            return new PlanteProductionSimple("Agave", "Eté", "Désertique", 35, 10, 90, 100, 2);
        }
        else if (nom=="Pasteque")
        {
            return new PlanteProductionSimple("Pastèque", "Eté", "Tropical", 30, 90, 90, 8, 2);
        }
        else if (nom=="Fleur de Tithonia")
        {
            return new PlanteProductionSimple("Fleurs de Tithonia", "Printemps", "Désertique", 35, 20, 90, 6, 2);
        }
        else if (nom=="Haricot")
        {
            return new PlanteProductionMultiple("Haricot", "Printemps", "Désertique", 23, 10, 90, 6, 2);
        }
        else if (nom=="Avocat")
        {
            return new PlanteProductionMultiple("Avocat", "Printemps", "Volcanique", 25, 70, 90, 60, 2);
        }
        else if (nom=="Papaye")
        {
            return new PlanteProductionMultiple("Papaye", "Eté", "Tropical", 27, 90, 90, 18, 2);
        }
        else // pour Igname
        {
            return new PlanteProductionMultiple("Igname", "Printemps", "Tropical", 28, 70, 90, 15, 2);
        }
        
    }
}