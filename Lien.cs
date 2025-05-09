namespace TransConnect
{
    internal class Lien
    {
        public Noeud Ville1 { get; set; }
        public Noeud Ville2 { get; set; }
        public double distance { get; set; }

        public Lien(Noeud ville1, Noeud ville2, double distance)
        {
            this.Ville1 = ville1;
            this.Ville2 = ville2;
            this.distance = distance;
        }

        public override string ToString()
        {
            return Ville1.Nom + "à" + Ville2.Nom + ":" + distance + "km";
        }
    }
}
