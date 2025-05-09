namespace TransConnect
{
    internal class Noeud
    {
        public string Nom { get; set; }

        public Noeud(string nom)
        {
            this.Nom = nom;
        }
        public override string ToString()
        {
            return Nom;
        }
    }
}
