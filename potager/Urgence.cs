public class Urgence
{


    public string type { get; set; }
    public int Gravité { get; set; } //echelle de 1 a 5
    public bool ProblemeResolu { get; set; }




    public Urgence ()
    {
        ProblemeResolu=true; //le mettre en faux quand declenchement d'une urgence
    }




    public void DeclencherAleatoirement () // le moment et le type
    {


    }


    public void AfficherAlerte()
    {


    }


    // faire en sorte d'enlever la santé


    public void Resoudre (Joueur jardinier, Parcelle parcelleTouchee)
    {
        //enlever la santé avant de resoudre
    }


}
