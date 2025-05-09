using SkiaSharp;

namespace TransConnect
{
    internal class Graphe
    {

        public List<Noeud> Villes { get; }
        public List<Lien> Liaisons { get; }
        public Dictionary<Noeud, List<(Noeud, double)>> ListAdjacence { get; }
        public Dictionary<(Noeud, Noeud), double> MatriceAdjacence { get; }
        public Graphe()
        {
            Villes = new List<Noeud>();
            Liaisons = new List<Lien>();
            ListAdjacence = new Dictionary<Noeud, List<(Noeud, double)>>();
            MatriceAdjacence = new Dictionary<(Noeud, Noeud), double>();
        }
        public string[] NomVilles()
        {
            return Villes.Select(v => v.Nom).ToArray();
        }
        public void AjouterNoeud(string str)
        {
            Noeud n = Villes.Find(n => n.Nom == str);
            if (n == null)
            {
                n = new Noeud(str);
                Villes.Add(n);
                ListAdjacence[n] = new List<(Noeud, double)>();
            }
        }
        public void AjouterLien(string v1, string v2, double distance)
        {
            if (v1 != null && v2 != null)//&& distance > 0 )
            {
                AjouterNoeud(v1);
                AjouterNoeud(v2);

                Noeud ville1 = Villes.Find(n => n.Nom == v1);
                Noeud ville2 = Villes.Find(n => n.Nom == v2);

                Lien lien = new Lien(ville1, ville2, distance);
                Liaisons.Add(lien);

                ListAdjacence[ville1].Add((ville2, distance));
                ListAdjacence[ville2].Add((ville1, distance));

                MatriceAdjacence[(ville1, ville2)] = distance;
                MatriceAdjacence[(ville2, ville1)] = distance;
            }
        }
        public void AfficherListeAdjacence()
        {
            Console.WriteLine("Liste d’adjacence :\n");
            foreach (var v in ListAdjacence)
            {
                Console.Write($"{v.Key.Nom} : ");

                foreach ((Noeud, double) ville in v.Value)
                {
                    Console.Write($"({ville.Item1.Nom}, {ville.Item2} km) ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void AfficherMatriceAdjacence()
        {
            Console.WriteLine("Matrice d’adjacence :\n");
            Console.Write($"{"",-15}");
            foreach (Noeud v in Villes)
            {
                Console.Write($"{v.Nom,-15}"); // $"{string,n}" crée un espace de n=12 cases et si le string est plus petit que n, le reste est rempli par des espaces (" ")
            }
            Console.WriteLine("\n");

            foreach (Noeud v1 in Villes)
            {
                Console.Write($"{v1.Nom,-15}");
                foreach (Noeud v2 in Villes)
                {
                    if (MatriceAdjacence.ContainsKey((v1, v2)))
                    {
                        Console.Write($"{MatriceAdjacence[(v1, v2)],-15}");
                    }
                    else
                    {
                        Console.Write($"{"n/a",-15}");
                    }
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine();
        }
        public void ParcoursLargeur(string dep)
        {
            Noeud depart = Villes.Find(n => n.Nom == dep);

            if (depart == null)
            {
                Console.WriteLine("Erreur du Parcours en largeur, le Noeud de départ est introuvable.");
            }
            else
            {
                HashSet<Noeud> visites = new HashSet<Noeud>();
                Queue<Noeud> file = new Queue<Noeud>();

                visites.Add(depart);
                file.Enqueue(depart);

                Console.WriteLine("Parcours en largeur :\n");

                while (file.Count > 0)
                {
                    Noeud courant = file.Dequeue();
                    Console.WriteLine(courant.Nom);

                    foreach ((Noeud, double) voisin in ListAdjacence[courant]) // on regarde les voisin du noeud via la liste d'adjacence puis si on ne les a pas visier on les ajoute à la file et au noeud visiter ( parce que ils le seront après )
                    {
                        if (visites.Contains(voisin.Item1) == false)
                        {
                            visites.Add(voisin.Item1);
                            file.Enqueue(voisin.Item1);
                        }
                    }
                }
                Console.WriteLine();
            }
        }
        public void ParcoursProfondeur(string dep)
        {
            Noeud depart = Villes.Find(n => n.Nom == dep);

            if (depart == null)
            {
                Console.WriteLine("Erreur du Parcours en profondeur, le Noeud de départ est introuvable.");
            }
            else
            {
                HashSet<Noeud> visites = new HashSet<Noeud>();
                Console.WriteLine("Parcours en profondeur :\n");
                ParcoursProfondeurRecursive(depart, visites);
                Console.WriteLine();
            }
        }
        private void ParcoursProfondeurRecursive(Noeud courant, HashSet<Noeud> visites)
        {
            visites.Add(courant);
            Console.WriteLine(courant.Nom);

            foreach ((Noeud, double) voisin in ListAdjacence[courant])
            {
                if (visites.Contains(voisin.Item1) == false)
                {
                    ParcoursProfondeurRecursive(voisin.Item1, visites);
                }
            }
        }
        public bool EstConnexe()
        {
            if (Villes.Count == 0) return true;

            Noeud depart = Villes[0];
            HashSet<Noeud> visites = new HashSet<Noeud>();
            Queue<Noeud> file = new Queue<Noeud>();

            visites.Add(depart);
            file.Enqueue(depart);

            while (file.Count > 0)
            {
                Noeud courant = file.Dequeue();

                foreach ((Noeud, double) voisin in ListAdjacence[courant]) // on regarde les voisin du noeud via la liste d'adjacence puis si on ne les a pas visier on les ajoute à la file et au noeud visiter ( parce que ils le seront après )
                {
                    if (visites.Contains(voisin.Item1) == false)
                    {
                        visites.Add(voisin.Item1);
                        file.Enqueue(voisin.Item1);
                    }
                }
            }
            return visites.Count == Villes.Count;
        }
        public bool Cycle()
        {
            HashSet<Noeud> visites = new HashSet<Noeud>();

            foreach (Noeud ville in Villes)
            {
                if (visites.Contains(ville) == false)
                {
                    if (CycleRec(ville, visites, null))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CycleRec(Noeud courant, HashSet<Noeud> visites, Noeud parent)
        {
            visites.Add(courant);

            foreach ((Noeud, double) voisin in ListAdjacence[courant])
            {
                if (visites.Contains(voisin.Item1) == false)
                {
                    if (CycleRec(voisin.Item1, visites, courant))
                    {
                        return true;
                    }
                }
                else if (voisin.Item1 != parent)
                {
                    // Si voisin déjà visité et pas le parent c'est un cycle
                    return true;
                }
            }
            return false;
        }
        public void CSV(string file)
        {
            string[] lignes = System.IO.File.ReadAllLines(file);
            lignes = lignes.Skip(1).ToArray();

            foreach (string ligne in lignes)
            {
                string[] elements = ligne.Split(';');
                if (elements.Length == 3)
                {
                    string ville1 = elements[0];
                    string ville2 = elements[1];
                    double distance = double.Parse(elements[2]);
                    AjouterLien(ville1, ville2, distance);
                }
            }
        }
        public void DessinerGraphe(string cheminImage)
        {
            int largeur = 900;
            int hauteur = 600;
            int rayon = 250;
            int rayonTexte = rayon + 25;
            SKPoint centre = new SKPoint(largeur / 2 + 50, hauteur / 2); // décale un peu à droite pour laisser la légende

            // Surface de dessin
            var info = new SKImageInfo(largeur, hauteur);
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            var paintText = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 14,
                IsAntialias = true
            };

            var paintCircle = new SKPaint
            {
                Color = SKColors.LightSkyBlue,
                IsAntialias = true
            };

            var positions = new Dictionary<Noeud, SKPoint>();
            int totalVilles = Villes.Count;
            double angleEntreVilles = 2 * Math.PI / totalVilles;
            int index = 0;

            // Positionner les villes en cercle
            foreach (var ville in Villes)
            {
                float angle = (float)(index * angleEntreVilles);
                float x = centre.X + (float)(rayon * Math.Cos(angle));
                float y = centre.Y + (float)(rayon * Math.Sin(angle));
                positions[ville] = new SKPoint(x, y);
                index++;
            }

            // Trouver min et max distances
            double minDist = Liaisons.Min(l => l.distance);
            double maxDist = Liaisons.Max(l => l.distance);

            // Fonction pour interpoler la couleur en fonction de la distance
            SKColor InterpolerCouleur(double distance)
            {
                // Normalisation 0.0 à 1.0
                float t = (float)((distance - minDist) / (maxDist - minDist));

                // Dégradé de bleu (0,0,255) à rouge (255,0,0)
                byte r = (byte)(255 * t);
                byte g = 0;
                byte b = (byte)(255 * (1 - t));

                return new SKColor(r, g, b);
            }

            // Dessiner les liaisons colorées
            foreach (var lien in Liaisons)
            {
                var p1 = positions[lien.Ville1];
                var p2 = positions[lien.Ville2];

                var paintLine = new SKPaint
                {
                    Color = InterpolerCouleur(lien.distance),
                    StrokeWidth = 3,
                    IsAntialias = true
                };

                canvas.DrawLine(p1, p2, paintLine);
            }

            // Dessiner les villes (cercles)
            foreach (var ville in Villes)
            {
                var pos = positions[ville];
                canvas.DrawCircle(pos, 12, paintCircle);
            }

            // Dessiner les noms de villes à l'extérieur
            index = 0;
            foreach (var ville in Villes)
            {
                float angle = (float)(index * angleEntreVilles);
                float xTexte = centre.X + (float)(rayonTexte * Math.Cos(angle));
                float yTexte = centre.Y + (float)(rayonTexte * Math.Sin(angle));

                float decalageX = 10 * (float)Math.Cos(angle);
                float decalageY = 10 * (float)Math.Sin(angle);

                if (angle >= Math.PI / 2 && angle <= 3 * Math.PI / 2)
                    paintText.TextAlign = SKTextAlign.Right;
                else
                    paintText.TextAlign = SKTextAlign.Left;

                canvas.DrawText(ville.Nom, xTexte + decalageX, yTexte + decalageY + paintText.TextSize / 3, paintText);
                index++;
            }

            // Dessiner la légende (barre de dégradé)
            var rectLegende = new SKRect(50, 150, 70, 450);
            var shader = SKShader.CreateLinearGradient(
                new SKPoint(rectLegende.Left, rectLegende.Top),
                new SKPoint(rectLegende.Left, rectLegende.Bottom),
                new SKColor[] { SKColors.Blue, SKColors.Red },
                null,
                SKShaderTileMode.Clamp
            );

            var paintLegende = new SKPaint
            {
                Shader = shader,
                IsAntialias = true
            };

            canvas.DrawRect(rectLegende, paintLegende);

            // Ajouter les labels min et max distance
            paintText.TextAlign = SKTextAlign.Left;
            canvas.DrawText($"{minDist} km", rectLegende.Right + 10, rectLegende.Bottom, paintText);
            canvas.DrawText($"{maxDist} km", rectLegende.Right + 10, rectLegende.Top + paintText.TextSize, paintText);
            canvas.DrawText("Distance", rectLegende.Left - 5, rectLegende.Top - 10, paintText);

            // Export image
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(cheminImage);
            data.SaveTo(stream);
        }
        public (double, string[]) Dijkstra(string depart, string arrive)
        {
            Noeud source = Villes.Find(n => n.Nom == depart); // Trouve les noeud qui correspondent aux string
            Noeud desti = Villes.Find(n => n.Nom == arrive);
            if (source == null || desti == null) // si aucun noeud n'a été trouver pour la source ou la destination, renvoie -1 et null
            {
                return (-1, null);
            }


            Dictionary<Noeud, double> dist = Villes.ToDictionary(v => v, v => double.MaxValue); // initialisation d'une liste de rendu ( pour les distance ) et d'un hashset pour les noeud visités et non visités ( pas les noeuds vu )            
            Dictionary<Noeud, Noeud> predecesseurs = new Dictionary<Noeud, Noeud>();
            HashSet<Noeud> visites = new HashSet<Noeud>();
            PriorityQueue<Noeud, double>  queue = new PriorityQueue<Noeud, double>(); // Utilisation d'une priorityQueue pour traiter les noeud vu (voisin) non visiter et les trier par ordre de distance pour toujours prendre la plus petite distance

            dist[source] = 0; // distance du noeud de départ = 0
            queue.Enqueue(source, 0);

            while (queue.Count > 0) // boucle tant que des noeud n'ont pas été visités
            {
                queue.TryDequeue(out Noeud actu, out double distActu); // Instancie actu comme étant le noeud non visiter avec la plus petite distance et distActu comme la distance

                if (distActu == double.MaxValue) // Terminer la boucle si la distance minimale est MaxValue car le point n'est pas connecté au autres points
                {
                    break;
                }

                visites.Add(actu); // Ajoute actu au Noeud visiter

                foreach ((Noeud, double) voisin in ListAdjacence[actu]) // Boucle pour mettree à jour la distance optimale des voisins
                {
                    if (visites.Contains(voisin.Item1) == false) // actualise uniquement si le voisin n'as pas été visité
                    {
                        double DistBis = distActu + voisin.Item2; // calcul la distance bis avec ce voisin
                        if (DistBis < dist[voisin.Item1]) // si la distance bis est plus petite que la distance du noeud voisin ( Item1 ) enregistrer dans dist, elle est remplacée
                        {
                            dist[voisin.Item1] = DistBis;
                            predecesseurs[voisin.Item1] = actu; // le prédécesseur change pour celui avec la plus petite distance
                            queue.Enqueue(voisin.Item1, DistBis); // Si la distance change, Enqueue le voisin de actu pour mettre à jour la liste des noeud avec une "distance modifier"
                        }
                    }
                }
            }
            double distance = dist[desti];
            string[] chemin = null;
            string cheminStr = "";

            Noeud courant = desti;

            if (distance == double.MaxValue) // Regarde si la distance avec la destination est valide ( pas MaxValue ) car sinon le point source et desti ne sont pas connectés
            {
                return (-2, null);
            }

            while (true) // boucle qui reconstitue le chemin entre la source et la desti et qui la met sous forme de string pour ensuite là transformer en tableau de string
            {
                Noeud actu = courant;
                Noeud prede = predecesseurs[courant];

                cheminStr = ";" + actu.Nom + cheminStr;
                courant = prede;

                if (courant == source)
                {
                    cheminStr = courant.Nom + cheminStr;
                    break;
                }
            }
            chemin = cheminStr.Split(';');

            return (distance, chemin);
        }
        public (double, string[]) BellmanFord(string depart, string arrive)
        {
            Noeud source = Villes.Find(n => n.Nom == depart);
            Noeud desti = Villes.Find(n => n.Nom == arrive);

            if (source == null || desti == null) // si aucun noeud n'a été trouver pour la source ou la destination, renvoie -1 et null
            {
                return (-1, null);
            }

            Dictionary<Noeud, double> dist = Villes.ToDictionary(v => v, v => double.MaxValue); // initialisation des distance optimale et des prédécesseurs de chaque noeud
            Dictionary<Noeud, Noeud> predecesseurs = new Dictionary<Noeud, Noeud>();

            dist[source] = 0;

            int n = Villes.Count;


            for (int i = 0; i < n - 1; i++) // boucle de BellmanFord
            {
                foreach (Lien lien in Liaisons)
                {
                    if (dist[lien.Ville1] + lien.distance < dist[lien.Ville2])
                    {
                        dist[lien.Ville2] = dist[lien.Ville1] + lien.distance;
                        predecesseurs[lien.Ville2] = lien.Ville1;
                    }
                    else if (dist[lien.Ville2] + lien.distance < dist[lien.Ville1])
                    {
                        dist[lien.Ville1] = dist[lien.Ville2] + lien.distance;
                        predecesseurs[lien.Ville1] = lien.Ville2;
                    }
                }
            }

            foreach (Lien lien in Liaisons) // Vérification d'un poids négatif ( ville 1 ->  ville 2 ->  ville 1 < 0 ), normalement impossible ( condition ajout lien ) mais au cas où ( -3 indique qu'il y a un poids néga )
            {
                if (dist[lien.Ville1] + lien.distance < dist[lien.Ville2])
                {
                    return (-3, null);
                }
            }

            if (dist[desti] == double.MaxValue) // Regarde si la distance avec la destination est valide ( pas MaxValue ) et donc que les point sont relier dans le grpahe
            {
                return (-2, null);
            }

            double distance = dist[desti];
            string[] chemin = null;
            string cheminStr = "";

            Noeud courant = desti;

            while (true)
            {
                Noeud actu = courant;
                Noeud prede = predecesseurs[courant];

                cheminStr = ";" + actu.Nom + cheminStr;
                courant = prede;

                if (courant == source)
                {
                    cheminStr = courant.Nom + cheminStr;
                    break;
                }
            }
            chemin = cheminStr.Split(';');
            return (distance, chemin);
        }
        public (double, string[]) FloydWarshall(string depart, string arrive)
        {
            Noeud source = Villes.Find(n => n.Nom == depart);
            Noeud desti = Villes.Find(n => n.Nom == arrive);

            if (source == null || desti == null) // si aucun noeud n'a été trouver pour la source ou la destination, renvoie -1 et null
            {
                return (-1, null);
            }

            Dictionary<(Noeud, Noeud), double> dist = new Dictionary<(Noeud, Noeud), double>(); //Initialisation de dist et pred comme des dictionnaire qui agiront comme des matrices symétrique bien que l'on ai besoin que de matrice triangle
            Dictionary<(Noeud, Noeud), Noeud> pred = new Dictionary<(Noeud, Noeud), Noeud>();

            for (int i = 0; i < Villes.Count; i++) // boucle pour initialiser toute les distance et les prédécesseur avec se que l'on sait
            {
                for (int j = 0; j < Villes.Count; j++)
                {
                    Noeud v1 = Villes[i];
                    Noeud v2 = Villes[j];

                    if (v1 == v2) // 0 si c'est la même ville
                        dist[(v1, v2)] = 0;
                    else if (MatriceAdjacence.ContainsKey((v1, v2))) // la valeur de la Matrice d'Adjacence si elles sont voisines ( et donc les liaison des villes ), et dcp ça prédécésseur est ça voisine
                    {
                        dist[(v1, v2)] = MatriceAdjacence[(v1, v2)];
                        pred[(v1, v2)] = v1;
                    }
                    else // Max.Value sinon et null comme prédécesseur
                    {
                        dist[(v1, v2)] = double.MaxValue;
                        pred[(v1, v2)] = null;
                    }
                }
            }


            foreach (Noeud k in Villes) // triple boucle pour avoir 3 lien ( distance ), si le lien 1 est plus grand que le lien 2 + le lien 3, les lien 1 est remplacé par le lien 2 + 3
            {
                foreach (Noeud i in Villes)
                {
                    foreach (Noeud j in Villes)
                    {
                        if (dist[(i, k)] != double.MaxValue && dist[(k, j)] != double.MaxValue) // check si les lien 2 et 3 n'ont pas des distance infini car sinon ça sera à rien de faire la comparaison
                        {
                            double nouvelleDistance = dist[(i, k)] + dist[(k, j)];
                            if (nouvelleDistance < dist[(i, j)]) // compare le lien 1 au lien 2 + 3 et remplace si il faut
                            {
                                dist[(i, j)] = nouvelleDistance;
                                pred[(i, j)] = pred[(k, j)];
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Villes.Count; i++) // Vérification d'un poids négatif ( ville 1 ->  ville 2 ->  ville 1 < 0 ) , normalement impossible ( condition ajout lien ) mais au cas où ( -3 indique qu'il y a un poids néga )
            {
                Noeud v = Villes[i];
                if (dist[(v, v)] < 0)
                {
                    return (-3, null);
                }
            }

            double distance = dist[(source, desti)];
            if (distance == double.MaxValue) // Regarde si la distance avec la destination est valide ( pas MaxValue ) car sinon le point source et desti ne sont pas connectés
            {
                return (-2, null);
            }

            string[] chemin = null;
            string cheminStr = "";
            Noeud courant = desti;

            while (true) // retranscrie le dico pred en chemin string séparé par des ; pour ensuite le split en tableau
            {
                cheminStr = ";" + courant.Nom + cheminStr;
                courant = pred[(source, courant)];

                if (courant == source)
                {
                    cheminStr = courant.Nom + cheminStr;
                    break;
                }
                else if (courant == null) // sécurité au cas où la valeur courante est null même si impossible en théorie
                {
                    chemin = null;
                    break;
                }
            }
            chemin = cheminStr.Split(';');

            return (distance, chemin);
        }
    }
}
