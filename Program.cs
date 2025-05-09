using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Security.Policy;

namespace TransConnect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Test();
            //Interface();
        }
        static void Interface()
        {
            Console.ForegroundColor = ConsoleColor.Cyan; // Met la police en couleur Cyan
            Console.CursorVisible = false; // efface le curseur de l'écran
            Console.WriteLine("\n\n    _____                   _____                             _   ");
            Console.WriteLine("   |_   _|                 /  __ \\                           | |  ");
            Console.WriteLine("     | |_ __ __ _ _ __  ___| /  \\/ ___  _ __  _ __   ___  ___| |_ ");
            Console.WriteLine("     | | '__/ _` | '_ \\/ __| |    / _ \\| '_ \\| '_ \\ / _ \\/ __| __|");
            Console.WriteLine("     | | | | (_| | | | \\__ \\ \\__/\\ (_) | | | | | | |  __/ (__| |_ ");
            Console.WriteLine("     \\_/_|  \\__,_|_| |_|___/\\____/\\___/|_| |_|_| |_|\\___|\\___|\\__|");
            Console.ReadKey();
            bool main = true; // instancie une variable pour signifier que le jeu tourne
            int flèche = 0; // instancie une variable flèche qui va aller de 0 à 2 pour pouvoir parcourir les menus
            bool enter = false; // instancie enter pour dire lorsque la touche entrer est appuier
            Console.Clear();
            Console.WriteLine("\nOption :\n"); // le menu est sur 4 ligne donc 3 qui peuvent être parcouru (Directeur, Client, et Quitter ) et commence sur Directeur donc écrit Directeur avec une flèche "<--"
            Console.WriteLine("Directeur            <--");
            Console.WriteLine("Client                  ");
            Console.WriteLine("Quitter                 ");
            while (main)
            {
                ConsoleKeyInfo touche = Console.ReadKey(); // lis la touche qui est appuier
                switch (touche.Key)
                {
                    case ConsoleKey.UpArrow: // si la touche est flèche du haut, fait -1 à la variable flèche
                        flèche -= 1;
                        break;
                    case ConsoleKey.DownArrow: // si la touche est flèche du bas, fait +1 à la variable flèche
                        flèche += 1;
                        break;
                    case ConsoleKey.Enter: // si la touche est Entrer, met true à la variabel enter pour dire que l'utilisateur veux accéder à la ligne où la flèche "<--" est.
                        enter = true;
                        break;
                    default:
                        break;
                }
                Console.CursorVisible = false;
                Console.Clear(); // clear la console pour actualiser l'affichage
                Console.WriteLine("\nOption : \n");
                if (flèche > 2) // fait en sorte que la variable flèche de puisse pas dépasser 3
                {
                    flèche = 2;
                }
                else if (flèche < 0) // fait en sorte que la variable flèche de puisse pas être en dessous de 0
                {
                    flèche = 0;
                }
                if (flèche == 0)
                {
                    Console.WriteLine("Directeur            <--");
                    Console.WriteLine("Client                  ");
                    Console.WriteLine("Quitter                 ");
                }
                else if (flèche == 1)
                {
                    Console.WriteLine("Directeur               ");
                    Console.WriteLine("Client               <--");
                    Console.WriteLine("Quitter                 ");
                }
                else if (flèche == 2)
                {
                    Console.WriteLine("Directeur               ");
                    Console.WriteLine("Client                  ");
                    Console.WriteLine("Quitter              <--");
                }
                if (enter == true && flèche == 0) // si la var flèche est égale à 0, et que la variable enter est true, lance le menu admin puis après, actualise l'affichage pour "retouner" au menu de base
                {
                    Console.Clear();
                    enter = false;
                    Directeur();

                    Console.Clear();
                    Console.CursorVisible = false;
                    Console.WriteLine("\nOption : \n");
                    Console.WriteLine("Directeur            <--");
                    Console.WriteLine("Client                  ");
                    Console.WriteLine("Quitter                 ");
                }
                else if (enter == true && flèche == 1) // si la var flèche est égale à 1, et que la variable enter est true, lance le menu client puis après que menu soit quitté, actualise l'affichage pour "retouner" au menu de base
                {
                    Console.Clear();
                    enter = false;


                    Console.Clear();
                    Console.CursorVisible = false;
                    Console.WriteLine("\nOption : \n");
                    Console.WriteLine("Directeur               ");
                    Console.WriteLine("Client               <--");
                    Console.WriteLine("Quitter                 ");
                }
                else if (enter == true && flèche == 2) // si la var flèche est égale à 2, et que la variable enter est true, l'utilisateur souhaite quitter l'application
                {
                    Console.Clear();
                    Console.WriteLine("\n A bientôt !"); // affiche un message d'au revoir
                    main = false; // instancie jeu comme false pour que la boucle se termine et donc que l'application s'arrête
                }
            }
        }
        static void Directeur()
        {

        }
        static void Test()
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Transconnect;UID=root;PASSWORD=Rootroot1;";

            //InsertSalarie(connectionString);
            //UpdatePersonne(connectionString);
            //UpdateSalarie(connectionString);
            //Delete(connectionString);

            //Console.WriteLine(AfficheTable(connectionString, "Personne"));
            Console.WriteLine(AfficheTable(connectionString, "Personne", "Salarie"));

            Graphe g = new Graphe();
            //g.CSV("distances_villes_france.csv");

            RemplirGrapheTest(g);

            Commande(1002, g);

            g.AfficherListeAdjacence();
            g.AfficherMatriceAdjacence();

            g.ParcoursLargeur("Paris");
            g.ParcoursProfondeur("Paris");

            Console.WriteLine("Le graphe est connexe : " + g.EstConnexe());
            Console.WriteLine("Le graphe a un cycle : " + g.Cycle() + "\n");

            g.DessinerGraphe("TestGraphe.png");


            string ville1 = "Paris";
            string ville2 = "Bruxelles"; // Exemple de ville : Bilbao / PasConnecter



            // Test Dijkstra
            var (distance, chemin) = g.Dijkstra(ville1, ville2);
            Console.Write("\nDijkstra : ");
            if (distance == -1)
            {
                Console.WriteLine("Villes recherchées introuvable.");
            }
            else if (distance == -2)
            {
                Console.WriteLine("Les villes ne sont pas connectées entre elle.");
            }
            else
            {
                Console.WriteLine($"La distance entre {chemin[0]} et {chemin[chemin.Length - 1]} : " + distance + "km");
                for (int i = 0; i < chemin.Length - 1; i++)
                {
                    Console.Write(chemin[i] + "->");
                }
                Console.WriteLine(chemin[chemin.Length - 1]);
            }



            // Test BellmanFord
            (distance, chemin) = g.BellmanFord(ville1, ville2);
            Console.Write("\nBellmanFord : ");
            if (distance == -1)
            {
                Console.WriteLine("Villes recherchées introuvable.");
            }
            else if (distance == -2)
            {
                Console.WriteLine("Les villes ne sont pas connectées entre elle.");
            }
            else if (distance == -3)
            {
                Console.WriteLine("Le Graphe contient une distance négative.");
            }
            else
            {
                Console.WriteLine($"La distance entre {chemin[0]} et {chemin[chemin.Length - 1]} : " + distance + "km");
                for (int i = 0; i < chemin.Length - 1; i++)
                {
                    Console.Write(chemin[i] + "->");
                }
                Console.WriteLine(chemin[chemin.Length - 1]);
            }



            // Test FloydWarshall
            (distance, chemin) = g.FloydWarshall(ville1, ville2);
            Console.Write("\nFloydWarshall : ");
            if (distance == -1)
            {
                Console.WriteLine("Villes recherchées introuvable.");
            }
            else if (distance == -2)
            {
                Console.WriteLine("Les villes ne sont pas connectées entre elle.");
            }
            else if (distance == -3)
            {
                Console.WriteLine("Le Graphe contient une distance négative.");
            }
            else
            {
                Console.WriteLine($"La distance entre {chemin[0]} et {chemin[chemin.Length - 1]} : " + distance + "km");
                for (int i = 0; i < chemin.Length - 1; i++)
                {
                    Console.Write(chemin[i] + "->");
                }
                Console.WriteLine(chemin[chemin.Length - 1]);
            }
            Console.WriteLine();

            //BenchMarkGraphe(g); // Fais un BenchMark des 3 algo pour avoir les temps moyen d'execution et determiner lequel est le meilleur ( 100000 répétition )

            Console.WriteLine("Prix pour une Voiture, 300km, 10ans d'ancienneté : " + Prix(10, 300, 0)); // (20 + 300*1) * 0.025
            Console.WriteLine("Prix pour une Camionnette, 300km, 10ans d'ancienneté : " + Prix(10, 300, 1)); // (20 + 300*1.5) * 0.025
            Console.WriteLine("Prix pour un PoidsLourd, 300km, 10ans d'ancienneté : " + Prix(10, 300, 2)); // (20 + 300*1.75) * 0.025
        }
        static void RemplirGrapheTest(Graphe g)
        {
            g.AjouterLien("Paris", "Lyon", 450);
            g.AjouterLien("Paris", "Lille", 220);
            g.AjouterLien("Paris", "Nantes", 380);
            g.AjouterLien("Paris", "Strasbourg", 490);
            g.AjouterLien("Lyon", "Marseille", 315);
            g.AjouterLien("Lyon", "Nice", 470);
            g.AjouterLien("Lyon", "Grenoble", 110);
            g.AjouterLien("Grenoble", "Marseille", 275);
            g.AjouterLien("Nantes", "Bordeaux", 340);
            g.AjouterLien("Bordeaux", "Toulouse", 245);
            g.AjouterLien("Toulouse", "Montpellier", 240);
            g.AjouterLien("Montpellier", "Marseille", 170);
            g.AjouterLien("Nice", "Marseille", 200);
            g.AjouterLien("Strasbourg", "Metz", 165);
            g.AjouterLien("Metz", "Lille", 340);
            g.AjouterLien("Lille", "Bruxelles", 110);
            g.AjouterLien("Paris", "Bruxelles", 300);
            g.AjouterLien("Bordeaux", "San Sebastian", 230);
            g.AjouterLien("San Sebastian", "Bilbao", 100);
            g.AjouterLien("Bilbao", "Toulouse", 340);

            //g.AjouterNoeud("PasConnecter");
            //g.AjouterLien("San Sebastian", "Toulouse", -10);
        }
        static void BenchMarkGraphe(Graphe g)
        {
            int rep = 100000;
            Stopwatch sw = new Stopwatch();
            double totalDijkstra = 0, totalBellman = 0, totalFloyd = 0;

            for (int i = 0; i < rep; i++)
            {
                sw.Restart();
                g.Dijkstra("Paris", "Marseille");
                sw.Stop();
                totalDijkstra += sw.Elapsed.TotalMilliseconds;

                sw.Restart();
                g.BellmanFord("Paris", "Marseille");
                sw.Stop();
                totalBellman += sw.Elapsed.TotalMilliseconds;

                sw.Restart();
                g.FloydWarshall("Paris", "Marseille");
                sw.Stop();
                totalFloyd += sw.Elapsed.TotalMilliseconds;
            }
            Console.WriteLine($"Temps moyen Dijkstra       : {Math.Round(totalDijkstra / rep, 6)}ms");
            Console.WriteLine($"Temps moyen Bellman-Ford   : {Math.Round(totalBellman / rep, 6)}ms");
            Console.WriteLine($"Temps moyen Floyd-Warshall : {Math.Round(totalFloyd / rep, 6)}ms");
        }
        static string NomColumn(string connectionString, bool group, string table, string table2 = null)
        {
            string res = "";
            string cmd = "SHOW COLUMNS FROM " + table + " ;";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = cmd;

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())                               // parcours ligne par ligne
            {
                res += reader.GetValue(0).ToString() + ",";
            }
            connection.Close();

            connection.Open();
            if (group)
            {
                cmd = "SHOW COLUMNS FROM " + table2 + " ;";
                command.CommandText = cmd;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    res += reader.GetValue(0).ToString() + ",";
                }
            }

            res = res.Remove(res.Length - 1);
            connection.Close();
            return res;
        }
        static string TypeColumn(string connectionString, bool group, string table, string table2 = null)
        {
            string res = "";
            string cmd = "SHOW COLUMNS FROM " + table + " ;";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = cmd;

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())                               // parcours ligne par ligne
            {
                res += reader.GetValue(1).ToString() + ",";
            }
            connection.Close();

            connection.Open();
            if (group)
            {
                cmd = "SHOW COLUMNS FROM " + table2 + " ;";
                command.CommandText = cmd;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    res += reader.GetValue(1).ToString() + ",";
                }
            }

            res = res.Remove(res.Length - 1);
            connection.Close();
            return res;
        }
        static string AfficheTable(string connectionString, string table, string table2 = null)
        {
            string res = "\n| ";
            string cmd = "SELECT * FROM " + table + ";";
            bool group = false;
            if (table2 != null) group = true;
            if (group)
            {
                cmd = "SELECT * FROM " + table + " NATURAL JOIN " + table2 + ";";
            }
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = cmd;

            MySqlDataReader reader = command.ExecuteReader();

            string[] columName = NomColumn(connectionString, group, table, table2).Split(",").Distinct().ToArray();

            int[] tab = new int[reader.FieldCount];
            for (int i = 0; i < columName.Length; i++)
            {
                tab[i] = columName[i].Length;

            }
            while (reader.Read())
            {
                int max = 0;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetValue(i).ToString() != null)
                    {
                        max = reader.GetValue(i).ToString().Length;
                    }
                    if (max > tab[i])
                    {
                        tab[i] = max;
                    }
                }
            }
            connection.Close();
            connection.Open();
            reader = command.ExecuteReader();
            for (int i = 0; i < columName.Length; i++)
            {
                string value = columName[i];
                for (int j = 0; value.Length < tab[i]; j++)
                {
                    value += " ";
                }
                res += value + " | ";
            }
            res += "\n| ";
            for (int i = 0; i < columName.Length; i++)
            {
                string value = "";
                for (int j = 0; value.Length < tab[i]; j++)
                {
                    value += " ";
                }
                res += value + " | ";
            }
            res += "\n";
            while (reader.Read())
            {
                string currentRowAsString = "";
                for (int i = 0; i < reader.FieldCount; i++)    // parcours cellule par cellule
                {
                    string valueAsString = reader.GetValue(i).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string
                    if (valueAsString == "") { valueAsString = "Null"; }
                    for (int j = 0; valueAsString.Length < tab[i]; j++)
                    {
                        valueAsString += " ";
                    }
                    currentRowAsString += valueAsString + " | ";
                }

                res += "| " + currentRowAsString + "\n";
            }
            connection.Close();
            return res;
        }
        static string SelectCommande(string commande, string connectionString)
        {
            string res = "";
            if (commande != "" && commande != null && connectionString != null && connectionString != "")
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = commande;

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())                           // parcours ligne par ligne
                {
                    string currentRowAsString = "";
                    if (res != "")
                    {
                        currentRowAsString = "|";
                    }
                    for (int i = 0; i < reader.FieldCount - 1; i++)    // parcours cellule par cellule
                    {
                        string valueAsString = reader.GetValue(i).ToString();  // recuperation de la valeur de chaque cellule sous forme d'un string
                        currentRowAsString += valueAsString + ";";
                    }
                    res += currentRowAsString + reader.GetValue(reader.FieldCount - 1).ToString();
                }
                connection.Close();
            }
            return res;
        }
        static void InsertSalarie(string connectionString)
        {
            string nom = "Personne";
            string nom2 = "Salarie";
            bool fin = true;
            string data = "";
            bool date = true;
            int pos = 8;
            string exemple = "SELECT * FROM " + nom + " NATURAL JOIN " + nom2 + " NATURAL JOIN Organigramme WHERE Personne.Nom = 'Fiesta';";

            string[] colonneType = TypeColumn(connectionString, true, nom, nom2).Split(',');
            string[] colonneNametab = NomColumn(connectionString, true, nom, nom2).Split(",").Distinct().ToArray();

            string colonneName = "";
            for (int i = 0; i < colonneNametab.Length; i++)
            {
                colonneName += colonneNametab[i] + ", ";
            }
            try
            {
                Console.CursorVisible = true;
                while (true)
                {
                    Console.Clear();

                    Console.WriteLine("\nAjouter : " + nom.First().ToString().ToUpper() + String.Join("", nom.ToLower().Skip(1)) + "\n");
                    Console.WriteLine("Nom des colonnes : " + colonneName + "NSS Supérieur " + "\n");
                    Console.WriteLine("Insérer : ");
                    Console.WriteLine("\n\n(mettre des ; pour séparer les éléments)");
                    if (date) { Console.WriteLine("(format pour la date : année-mois-jour)"); pos++; }
                    Console.WriteLine("\nExemple : " + SelectCommande(exemple, connectionString));
                    Console.WriteLine("\n(exit pour sortir)");
                    Console.SetCursorPosition(10, Console.CursorTop - pos);
                    string var = Console.ReadLine();
                    if (var == "exit")
                    {
                        fin = false;
                        break;
                    }
                    try
                    {
                        data = var;
                        break;
                    }
                    catch
                    {
                        Console.Clear();
                    }
                }
                if (fin)
                {
                    string[] dataSplit = data.Split(';');

                    // Insert Personne

                    data = "";
                    colonneName = NomColumn(connectionString, false, nom);
                    for (int i = 0; i < colonneType.Length - 5; i++)
                    {
                        if (colonneType[i + 1] != "int")
                        {
                            data += "'" + dataSplit[i] + "',";
                        }
                        else
                        {
                            data += dataSplit[i] + ",";
                        }
                    }
                    data = data.Remove(data.Length - 1);

                    string commande = "INSERT INTO " + nom + "(" + colonneName + ") VALUES (" + data + ");";

                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();

                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = commande;
                    command.ExecuteNonQuery();
                    connection.Close();

                    // Insert Salarie

                    data = "'" + dataSplit[0] + "',";
                    colonneName = NomColumn(connectionString, false, nom2);
                    for (int i = 7; i < colonneType.Length - 2; i++)
                    {
                        if (colonneType[i + 1] != "int")
                        {
                            data += "'" + dataSplit[i] + "',";
                        }
                        else
                        {
                            data += dataSplit[i] + ",";
                        }
                    }
                    data = data.Remove(data.Length - 1);

                    commande = "INSERT INTO " + nom2 + "(" + colonneName + ") VALUES (" + data + ");";

                    connection = new MySqlConnection(connectionString);
                    connection.Open();

                    command = connection.CreateCommand();
                    command.CommandText = commande;
                    command.ExecuteNonQuery();
                    connection.Close();

                    // Insert Organigrame

                    data = "'" + dataSplit[0] + "','" + dataSplit[dataSplit.Length - 1] + "'";
                    commande = "INSERT INTO Organigramme (NSS, NSS_Superieur) VALUES (" + data + ");";
                    connection = new MySqlConnection(connectionString);
                    connection.Open();

                    command = connection.CreateCommand();
                    command.CommandText = commande;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch
            {
                Console.CursorVisible = false;
                Console.Clear();
                Console.Write("Une erreur est survenue, les données saisies sont incorrectes.");
                Console.ReadKey();
            }
        }
        static void UpdatePersonne(string connectionString)
        {
            bool fin = true;
            string nss = "";
            string data1 = "";
            string colonne1 = "";
            Console.CursorVisible = true;
            string[] ColumnName = new string[4] { "nom", "adressePostale", "adresseMail", "telephone" };
            int pos = 10;

            while (fin)
            {
                Console.Clear();
                Console.WriteLine("\nUpdate : Personne");
                Console.WriteLine("\nPour quel personne voulez-vous modifier (NSS) : ");
                Console.WriteLine("\n\nNom des colonnes : Nom, AdressePostale, AdresseMail, Telephone");
                Console.WriteLine("\nQuelle colonne voulez-vous modifier : ");
                Console.WriteLine("\nQue voulez-vous y mettre : ");
                Console.WriteLine("\n(exit pour sortir)");
                Console.SetCursorPosition(48, Console.CursorTop - pos);
                Console.CursorVisible = true;
                string var = Console.ReadLine();
                Console.CursorVisible = false;
                if (var == "exit")
                {
                    fin = false;
                    break;
                }
                try
                {
                    bool ok = true;
                    if (var == "") ok = false;
                    for (int i = 0; i < var.Length && ok; i++)
                    {
                        if (Char.IsDigit(var[i]) == false)
                        {
                            ok = false;
                        }
                    }
                    if (ok)
                    {
                        nss = var;
                        break;
                    }
                }
                catch { Console.Clear(); }
            }
            while (fin)
            {
                Console.Clear();
                Console.WriteLine("\nUpdate : Personne");
                Console.WriteLine("\nPour quel personne voulez-vous modifier (NSS) : " + nss);
                Console.WriteLine("\n\nNom des colonnes : Nom, AdressePostale, AdresseMail, Telephone");
                Console.WriteLine("\nQuelle colonne voulez-vous modifier : ");
                Console.WriteLine("\nQue voulez-vous y mettre : ");
                Console.WriteLine("\n(exit pour sortir)");
                Console.SetCursorPosition(38, Console.CursorTop - pos + 5);
                Console.CursorVisible = true;
                string var = Console.ReadLine().ToLower();
                Console.CursorVisible = false;
                if (var == "exit")
                {
                    fin = false;
                    break;
                }
                try
                {
                    bool ok = false;
                    foreach (string i in ColumnName)
                    {
                        if (var == i) ok = true;
                    }
                    if (ok)
                    {
                        colonne1 = var;
                        break;
                    }
                }
                catch
                { Console.Clear(); }
            }
            while (fin)
            {
                Console.Clear();
                Console.WriteLine("\nUpdate : Personne");
                Console.WriteLine("\nPour quel personne voulez-vous modifier (NSS) : " + nss);
                Console.WriteLine("\n\nNom des colonnes : Nom, AdressePostale, AdresseMail, Telephone");
                Console.WriteLine("\nQuelle colonne voulez-vous modifier : " + colonne1);
                Console.WriteLine("\nQue voulez-vous y mettre : ");
                Console.WriteLine("\n(exit pour sortir)");
                Console.SetCursorPosition(27, Console.CursorTop - pos + 7);
                Console.CursorVisible = true;
                string var = Console.ReadLine();
                Console.CursorVisible = false;
                if (var == "exit")
                {
                    fin = false;
                    break;
                }
                try
                {
                    data1 = var;
                    break;
                }
                catch { Console.Clear(); }
            }
            if (fin)
            {
                string commande = "UPDATE Personne SET " + colonne1 + " = '" + data1 + "' WHERE NSS = " + nss + ";";

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = commande;
                command.ExecuteNonQuery();
                connection.Close();

            }

        }
        static void UpdateSalarie(string connectionString)
        {
            bool fin = true;
            string nss = "";
            string data1 = "";
            string colonne1 = "";
            Console.CursorVisible = true;
            string[] ColumnName = new string[2] { "poste", "salaire" };
            int pos = 10;

            while (fin)
            {
                Console.Clear();
                Console.WriteLine("\nUpdate : Salarié");
                Console.WriteLine("\nPour quel salarié voulez-vous modifier (NSS) : ");
                Console.WriteLine("\n\nNom des colonnes : Poste, Salaire");
                Console.WriteLine("\nQuelle colonne voulez-vous modifier : ");
                Console.WriteLine("\nQue voulez-vous y mettre : ");
                Console.WriteLine("\n(exit pour sortir)");
                Console.SetCursorPosition(47, Console.CursorTop - pos);
                Console.CursorVisible = true;
                string var = Console.ReadLine();
                Console.CursorVisible = false;
                if (var == "exit")
                {
                    fin = false;
                    break;
                }
                try
                {
                    bool ok = true;
                    if (var == "") ok = false;
                    for (int i = 0; i < var.Length && ok; i++)
                    {
                        if (Char.IsDigit(var[i]) == false)
                        {
                            ok = false;
                        }
                    }
                    if (ok)
                    {
                        nss = var;
                        break;
                    }
                }
                catch { }
            }
            while (fin)
            {
                Console.Clear();
                Console.WriteLine("\nUpdate : Personne");
                Console.WriteLine("\nPour quel personne voulez-vous modifier (NSS) : " + nss);
                Console.WriteLine("\n\nNom des colonnes : Poste, Salaire");
                Console.WriteLine("\nQuelle colonne voulez-vous modifier : ");
                Console.WriteLine("\nQue voulez-vous y mettre : ");
                Console.WriteLine("\n(exit pour sortir)");
                Console.SetCursorPosition(38, Console.CursorTop - pos + 5);
                Console.CursorVisible = true;
                string var = Console.ReadLine().ToLower();
                Console.CursorVisible = false;
                if (var == "exit")
                {
                    fin = false;
                    break;
                }
                try
                {
                    bool ok = false;
                    foreach (string i in ColumnName)
                    {
                        if (var == i) ok = true;
                    }
                    if (ok)
                    {
                        colonne1 = var;
                        break;
                    }
                }
                catch
                { }
            }
            while (fin)
            {
                Console.Clear();
                Console.WriteLine("\nUpdate : Personne");
                Console.WriteLine("\nPour quel personne voulez-vous modifier (NSS) : " + nss);
                Console.WriteLine("\n\nNom des colonnes : Poste, Salaire");
                Console.WriteLine("\nQuelle colonne voulez-vous modifier : " + colonne1);
                Console.WriteLine("\nQue voulez-vous y mettre : ");
                Console.WriteLine("\n(exit pour sortir)");
                Console.SetCursorPosition(27, Console.CursorTop - pos + 7);
                Console.CursorVisible = true;
                string var = Console.ReadLine();
                Console.CursorVisible = false;
                if (var == "exit")
                {
                    fin = false;
                    break;
                }
                try
                {

                    bool ok = true;
                    if (var == "") ok = false;
                    for (int i = 0; i < var.Length && ok; i++)
                    {
                        if (Char.IsDigit(var[i]) == false)
                        {
                            ok = false;
                        }
                    }
                    if (colonne1 == "salaire" && ok)
                    {
                        data1 = var;
                        break;
                    }
                    else if (colonne1 == "poste")
                    {
                        data1 = var;
                        break;
                    }
                }
                catch { }
            }
            if (fin)
            {
                string commande = "UPDATE Salarie SET " + colonne1 + " = '" + data1 + "' WHERE NSS = " + nss + ";";

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                Console.WriteLine(commande);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = commande;
                command.ExecuteNonQuery();
                connection.Close();

            }

        }
        static void Delete(string connectionString)
        {
            bool fin = true;
            string nss = "";
            Console.CursorVisible = true;
            int pos = 3;

            while (fin)
            {
                Console.Clear();
                Console.WriteLine("\nSupprimer : Client ou Salarié");
                Console.WriteLine("\nQuel est la NSS de la personne à supprimer : ");
                Console.WriteLine("\n(exit pour sortir)");
                Console.SetCursorPosition(45, Console.CursorTop - pos);
                Console.CursorVisible = true;
                string var = Console.ReadLine();
                Console.CursorVisible = false;
                if (var == "exit")
                {
                    fin = false;
                    break;
                }
                try
                {
                    bool ok = true;
                    if (var == "") ok = false;
                    for (int i = 0; i < var.Length && ok; i++)
                    {
                        if (Char.IsDigit(var[i]) == false)
                        {
                            ok = false;
                        }
                    }
                    if (ok)
                    {
                        nss = var;
                        break;
                    }
                }
                catch { }
            }
            string personne = string.Join(" ", SelectCommande("SELECT Prenom,Nom FROM PERSONNE WHERE NSS = " + nss + ";", connectionString).Split(";"));
            while (fin)
            {
                Console.Clear();
                Console.WriteLine("\nSuprimer : Client ou Salarié");
                Console.WriteLine("\nQuel est la NSS de la personne à supprimer : " + nss);
                Console.WriteLine("\nÊtes vous sur de vouloir supprimer " + personne + " : ");
                Console.WriteLine("\n(oui ou non)");
                Console.WriteLine("\n(exit pour sortir)");
                Console.SetCursorPosition(38 + personne.Length, Console.CursorTop - pos - 2);
                Console.CursorVisible = true;
                string var = Console.ReadLine().ToLower();
                Console.CursorVisible = false;
                if (var == "exit")
                {
                    fin = false;
                    break;
                }
                try
                {
                    if (var == "oui")
                    {
                        break;
                    }
                    else if (var == "non")
                    {
                        fin = false;
                        break;
                    }
                }
                catch
                { }
            }
            if (fin)
            {
                string commande = "DELETE FROM Personne WHERE NSS = " + nss;

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                Console.WriteLine(commande);

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = commande;
                command.ExecuteNonQuery();
                connection.Close();

            }

        }
        static void Commande(int nss, Graphe g)
        {
            string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Transconnect;UID=root;PASSWORD=Rootroot1;";
            string personne = string.Join(" ", SelectCommande("SELECT Prenom,Nom FROM PERSONNE WHERE NSS = " + nss + ";", connectionString).Split(";"));
            bool fin = true;
            string[] ville = g.NomVilles();
            string villeStr = string.Join(", ", ville);
            string Ville1 = "";
            string Ville2 = "";
            string date = "";
            string chauffeurNSS = "";
            string chauffeur = "";
            string véhicule = "";
            string utilité = "";
            string km = "";
            double prix = 0;
            double prixEntier = 0;
            double prixDecimal = 0;

            while (fin)
            {
                Console.Clear();
                Console.WriteLine($"\nCommande de {personne} : \n");
                Console.WriteLine("Service : Voiture, Camionnette, Camions");
                Console.WriteLine("Quel service voulez-vous prendre : ");
                Console.WriteLine("\n( exit pour sortir )");
                Console.SetCursorPosition(35, Console.CursorTop - 3);
                Console.CursorVisible = true;
                string var = Console.ReadLine().ToLower();
                Console.CursorVisible = false;
                if (var == "exit")
                {
                    fin = false;
                    break;
                }
                else if (var == "voiture" || var == "camionnette" || var == "camions")
                {
                    if (var == "voiture")
                    {
                        var = char.ToUpper(var[0]) + var.Substring(1).ToLower();
                        int nbpassager = 0;
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine("Service : " + var);
                            Console.WriteLine("Pour combien de personnes : ");
                            Console.WriteLine("( 6 maximum )");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(28, Console.CursorTop - 4);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                try
                                {
                                    bool ok = true;
                                    if (var2 == "") ok = false;
                                    if (Char.IsDigit(var2[0]) == false || var2.Length > 1 || Int32.Parse(var2) > 6)
                                    {
                                        ok = false;
                                    }
                                    if (ok)
                                    {
                                        nbpassager = Int32.Parse(var2);
                                        break;
                                    }
                                }
                                catch { continue; }
                            }
                        }
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var} pour {nbpassager} personnes.");
                            Console.WriteLine("Ville disponible : " + villeStr);
                            Console.WriteLine("\nVous partez de quel Ville :");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(28, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (var2 != "") var2 = char.ToUpper(var2[0]) + var2.Substring(1);
                                if (ville.Contains(var2))
                                {
                                    Ville1 = var2;
                                    break;
                                }
                            }
                        }
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var} pour {nbpassager} personnes.");
                            Console.WriteLine("Ville disponible : " + villeStr);
                            Console.WriteLine("\nVous allez à quel Ville :");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(26, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (var2 != "") var2 = char.ToUpper(var2[0]) + var2.Substring(1);
                                if (ville.Contains(var2) && Ville1 != var2)
                                {
                                    Ville2 = var2;
                                    break;
                                }
                            }
                        }
                        bool dispo = false;
                        string datedispo = "";
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var} pour {nbpassager} personnes de {Ville1} à {Ville2}.");
                            Console.ForegroundColor = ConsoleColor.Red;
                            if (dispo) Console.WriteLine($"Il n'y a pas de chauffeur ou de voiture disponible pour le {datedispo}.");
                            else Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("A quel date : ");
                            Console.WriteLine("( année-mois-jour )");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(14, Console.CursorTop - 4);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (DateTime.TryParseExact(var2, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _)) // vérifie que la date est bien sous le format année-mois-jour, paramère mis pour dire qu'il n'y a pas de dépendandce et que l'on ne veut pas de sortie DateTime, juste le bool
                                {
                                    string ChauffeurDispo = SelectCommande($"SELECT NSS FROM Salarie WHERE Poste Like \"Chauffeu%\" AND NSS NOT IN (SELECT NSS_Chauffeur FROM Commande WHERE DateCommande = '{var2}');", connectionString);
                                    string VoitureDispo = SelectCommande($"SELECT Immatriculation FROM Voiture WHERE NbPlace >= {nbpassager + 1} AND Immatriculation NOT IN ( SELECT Immatriculation FROM Commande WHERE DateCommande = '{var2}') ORDER BY NbPlace;", connectionString);
                                    if (ChauffeurDispo != "" && VoitureDispo != "")
                                    {
                                        date = var2;
                                        chauffeurNSS = ChauffeurDispo.Split('|').First();
                                        véhicule = VoitureDispo.Split("|").First();
                                        break;
                                    }
                                    else
                                    {
                                        dispo = true;
                                        datedispo = var2;
                                    }
                                }
                            }
                        }
                        if (fin)
                        {
                            utilité = $"Transport de {nbpassager} passagers de {Ville1} à {Ville2}.";
                            var (distance, chemin) = g.Dijkstra(Ville1, Ville2); //récupérartion de la distance et du chemin le plus court entre les 2 ville choisi
                            km = distance.ToString();

                            string Entre = SelectCommande($"SELECT DateEntree FROM Salarie WHERE NSS = '{chauffeurNSS}';", connectionString);
                            DateTime dateEntre = DateTime.Parse(Entre);
                            int ancienneté = DateTime.Today.Year - dateEntre.Year;
                            chauffeur = string.Join(" ", SelectCommande("SELECT Prenom,Nom FROM PERSONNE WHERE NSS = " + chauffeurNSS + ";", connectionString).Split(";"));


                            prix = Prix(ancienneté, Double.Parse(km), 0); // initialisation du prix, prixEntier et prixDecimal servent à noter le prix en SQL car les string mettent des virgule alors que le SQL veux un point entre la partie entière et décimal
                            prixEntier = Math.Truncate(prix);
                            prixDecimal = Math.Round((prix - prixEntier) * 100, 2);
                        }
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var} pour {nbpassager} personnes de {Ville1} à {Ville2} le {date}.");
                            Console.WriteLine($"Le trajet est de {km}km et coûte {prix} euros.");
                            Console.WriteLine($"Votre chauffeur sera {chauffeur}.");
                            Console.WriteLine("\nAvez-vous payer : ");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(18, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (var2 == "oui")
                                {
                                    break;
                                }
                                else if (var2 == "non")
                                {
                                    fin = false;
                                    break;
                                }
                            }
                        }
                        if (fin)
                        {
                            string commande = $"INSERT INTO Commande (NSS_Client, NSS_Chauffeur, Immatriculation, Utilisation, DateCommande, VilleDepart, VilleArrivee, Prix, KmTotal) VALUES ('{nss}','{chauffeurNSS}','{véhicule}','{utilité}','{date}','{Ville1}','{Ville2}','{prixEntier}.{prixDecimal}','{km}')";

                            MySqlConnection connection = new MySqlConnection(connectionString);
                            connection.Open();

                            Console.WriteLine(commande);

                            MySqlCommand command = connection.CreateCommand();
                            command.CommandText = commande;
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    else if (var == "camionnette")
                    {
                        var = char.ToUpper(var[0]) + var.Substring(1).ToLower();

                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine("Service : " + var);
                            Console.WriteLine("Pour quel utilisation : ");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(24, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                utilité = "Camionnette : " + char.ToUpper(var2[0]) + var2.Substring(1).ToLower();
                                break;
                            }
                        }
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var}");
                            Console.WriteLine("Ville disponible : " + villeStr);
                            Console.WriteLine("\nVous partez de quel Ville :");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(28, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (var2 != "") var2 = char.ToUpper(var2[0]) + var2.Substring(1);
                                if (ville.Contains(var2))
                                {
                                    Ville1 = var2;
                                    break;
                                }
                            }
                        }
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var}");
                            Console.WriteLine("Ville disponible : " + villeStr);
                            Console.WriteLine("\nVous allez à quel Ville :");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(26, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (var2 != "") var2 = char.ToUpper(var2[0]) + var2.Substring(1);
                                if (ville.Contains(var2) && Ville1 != var2)
                                {
                                    Ville2 = var2;
                                    break;
                                }
                            }
                        }
                        bool dispo = false;
                        string datedispo = "";
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var} de {Ville1} à {Ville2}.");
                            Console.ForegroundColor = ConsoleColor.Red;
                            if (dispo) Console.WriteLine($"Il n'y a pas de chauffeur ou de voiture disponible pour le {datedispo}.");
                            else Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("A quel date : ");
                            Console.WriteLine("( année-mois-jour )");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(14, Console.CursorTop - 4);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (DateTime.TryParseExact(var2, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _)) // vérifie que la date est bien sous le format année-mois-jour, paramère mis pour dire qu'il n'y a pas de dépendandce et que l'on ne veut pas de sortie DateTime, juste le bool
                                {
                                    string ChauffeurDispo = SelectCommande($"SELECT NSS FROM Salarie WHERE Poste Like \"Chauffeu%\" AND NSS NOT IN (SELECT NSS_Chauffeur FROM Commande WHERE DateCommande = '{var2}');", connectionString);
                                    string VoitureDispo = SelectCommande($"SELECT Immatriculation FROM Camionnette WHERE Immatriculation NOT IN ( SELECT Immatriculation FROM Commande WHERE DateCommande = '{var2}');", connectionString);
                                    if (ChauffeurDispo != "" && VoitureDispo != "")
                                    {
                                        date = var2;
                                        chauffeurNSS = ChauffeurDispo.Split('|').First();
                                        véhicule = VoitureDispo.Split("|").First();
                                        break;
                                    }
                                    else
                                    {
                                        dispo = true;
                                        datedispo = var2;
                                    }
                                }
                            }
                        }
                        if (fin)
                        {
                            var (distance, chemin) = g.Dijkstra(Ville1, Ville2); //récupérartion de la distance et du chemin le plus court entre les 2 ville choisi
                            km = distance.ToString();

                            string Entre = SelectCommande($"SELECT DateEntree FROM Salarie WHERE NSS = '{chauffeurNSS}';", connectionString);
                            DateTime dateEntre = DateTime.Parse(Entre);
                            int ancienneté = DateTime.Today.Year - dateEntre.Year;
                            chauffeur = string.Join(" ", SelectCommande("SELECT Prenom,Nom FROM PERSONNE WHERE NSS = " + chauffeurNSS + ";", connectionString).Split(";"));


                            prix = Prix(ancienneté, Double.Parse(km), 1); // initialisation du prix, prixEntier et prixDecimal servent à noter le prix en SQL car les string mettent des virgule alors que le SQL veux un point entre la partie entière et décimal
                            prixEntier = Math.Truncate(prix);
                            prixDecimal = Math.Round((prix - prixEntier) * 100, 2);
                        }
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var} de {Ville1} à {Ville2} le {date}.");
                            Console.WriteLine($"Le trajet est de {km}km et coûte {prix} euros.");
                            Console.WriteLine($"Votre chauffeur sera {chauffeur}.");
                            Console.WriteLine("\nAvez-vous payer : ");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(18, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (var2 == "oui")
                                {
                                    break;
                                }
                                else if (var2 == "non")
                                {
                                    fin = false;
                                    break;
                                }
                            }
                        }
                        if (fin)
                        {
                            string commande = $"INSERT INTO Commande (NSS_Client, NSS_Chauffeur, Immatriculation, Utilisation, DateCommande, VilleDepart, VilleArrivee, Prix, KmTotal) VALUES ('{nss}','{chauffeurNSS}','{véhicule}','{utilité}','{date}','{Ville1}','{Ville2}','{prixEntier}.{prixDecimal}','{km}')";

                            MySqlConnection connection = new MySqlConnection(connectionString);
                            connection.Open();

                            Console.WriteLine(commande);

                            MySqlCommand command = connection.CreateCommand();
                            command.CommandText = commande;
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    else
                    {
                        var = char.ToUpper(var[0]) + var.Substring(1).ToLower();
                        string Type = "";
                        double NbLitre = 0;
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine("Service : " + var);
                            Console.WriteLine("Type disponible : Frigorifique, Citerne, Benne.");
                            Console.WriteLine("De quel type avez-vous besoin : ");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(32, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (var2 == "frigorifique" || var2 == "citerne" || var2 == "benne")
                                {
                                    Type = char.ToUpper(var2[0]) + var2.Substring(1).ToLower();
                                    break;
                                }
                            }
                        }
                        double LitreMax = Double.Parse(SelectCommande($"SELECT MAX(VolumeTransportable) FROM PoidsLourd WHERE Type = '{Type}';", connectionString)); ;
                        bool litre = false;
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var} {Type}");
                            Console.WriteLine("De combien de Litres avez-vous besoin : ");
                            if (litre) Console.WriteLine(LitreMax + " Litres maximum pour les camions " + Type);
                            else Console.WriteLine();
                            Console.WriteLine("( exit pour sortir )");
                            Console.SetCursorPosition(40, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                try
                                {
                                    bool ok = true;
                                    if (var2 == "") ok = false;
                                    if (Char.IsDigit(var2[0]) == false || double.Parse(var2) > LitreMax)
                                    {
                                        litre = true;
                                        ok = false;
                                    }
                                    if (ok)
                                    {
                                        NbLitre = double.Parse(var2);
                                        break;
                                    }
                                }
                                catch { continue; }
                            }
                        }
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var} {Type} pour {NbLitre} litres.");
                            Console.WriteLine("Ville disponible : " + villeStr);
                            Console.WriteLine("\nVous partez de quel Ville :");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(28, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (var2 != "") var2 = char.ToUpper(var2[0]) + var2.Substring(1);
                                if (ville.Contains(var2))
                                {
                                    Ville1 = var2;
                                    break;
                                }
                            }
                        }
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var} {Type} pour {NbLitre} litres.");
                            Console.WriteLine("Ville disponible : " + villeStr);
                            Console.WriteLine("\nVous allez à quel Ville :");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(26, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (var2 != "") var2 = char.ToUpper(var2[0]) + var2.Substring(1);
                                if (ville.Contains(var2) && Ville1 != var2)
                                {
                                    Ville2 = var2;
                                    break;
                                }
                            }
                        }
                        bool dispo = false;
                        string datedispo = "";
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var} {Type} pour {NbLitre} litres de {Ville1} à {Ville2}.");
                            Console.ForegroundColor = ConsoleColor.Red;
                            if (dispo) Console.WriteLine($"Il n'y a pas de chauffeur ou de voiture disponible pour le {datedispo}.");
                            else Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("A quel date : ");
                            Console.WriteLine("( année-mois-jour )");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(14, Console.CursorTop - 4);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (DateTime.TryParseExact(var2, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _)) // vérifie que la date est bien sous le format année-mois-jour, paramère mis pour dire qu'il n'y a pas de dépendandce et que l'on ne veut pas de sortie DateTime, juste le bool
                                {
                                    string ChauffeurDispo = SelectCommande($"SELECT NSS FROM Salarie WHERE Poste Like \"Chauffeu%\" AND NSS NOT IN (SELECT NSS_Chauffeur FROM Commande WHERE DateCommande = '{var2}');", connectionString);
                                    string VoitureDispo = SelectCommande($"SELECT Immatriculation FROM PoidsLourd WHERE VolumeTransportable >= {NbLitre} AND Type = '{Type}' AND Immatriculation NOT IN ( SELECT Immatriculation FROM Commande WHERE DateCommande = '{var2}') ORDER BY VolumeTransportable;", connectionString);
                                    if (ChauffeurDispo != "" && VoitureDispo != "")
                                    {
                                        date = var2;
                                        chauffeurNSS = ChauffeurDispo.Split('|').First();
                                        véhicule = VoitureDispo.Split("|").First();
                                        break;
                                    }
                                    else
                                    {
                                        dispo = true;
                                        datedispo = var2;
                                    }
                                }
                            }
                        }
                        if (fin)
                        {
                            utilité = $"Transport de {NbLitre} Litre de {Ville1} à {Ville2} dans un camions {Type}.";
                            var (distance, chemin) = g.Dijkstra(Ville1, Ville2); //récupérartion de la distance et du chemin le plus court entre les 2 ville choisi
                            km = distance.ToString();

                            string Entre = SelectCommande($"SELECT DateEntree FROM Salarie WHERE NSS = '{chauffeurNSS}';", connectionString);
                            DateTime dateEntre = DateTime.Parse(Entre);
                            int ancienneté = DateTime.Today.Year - dateEntre.Year;
                            chauffeur = string.Join(" ", SelectCommande("SELECT Prenom,Nom FROM PERSONNE WHERE NSS = " + chauffeurNSS + ";", connectionString).Split(";"));


                            prix = Prix(ancienneté, Double.Parse(km), 2); // initialisation du prix, prixEntier et prixDecimal servent à noter le prix en SQL car les string mettent des virgule alors que le SQL veux un point entre la partie entière et décimal
                            prixEntier = Math.Truncate(prix);
                            prixDecimal = Math.Round((prix - prixEntier) * 100, 2);
                        }
                        while (fin)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nCommande de {personne} : \n");
                            Console.WriteLine($"Service : {var} {Type} pour {NbLitre} litres de {Ville1} à {Ville2}.");
                            Console.WriteLine($"Le trajet est de {km}km et coûte {prix} euros.");
                            Console.WriteLine($"Votre chauffeur sera {chauffeur}.");
                            Console.WriteLine("\nAvez-vous payer : ");
                            Console.WriteLine("\n( exit pour sortir )");
                            Console.SetCursorPosition(18, Console.CursorTop - 3);
                            Console.CursorVisible = true;
                            string var2 = Console.ReadLine().ToLower();
                            Console.CursorVisible = false;
                            if (var2 == "exit")
                            {
                                fin = false;
                                break;
                            }
                            else
                            {
                                if (var2 == "oui")
                                {
                                    break;
                                }
                                else if (var2 == "non")
                                {
                                    fin = false;
                                    break;
                                }
                            }
                        }
                        if (fin)
                        {
                            string commande = $"INSERT INTO Commande (NSS_Client, NSS_Chauffeur, Immatriculation, Utilisation, DateCommande, VilleDepart, VilleArrivee, Prix, KmTotal) VALUES ('{nss}','{chauffeurNSS}','{véhicule}','{utilité}','{date}','{Ville1}','{Ville2}','{prixEntier}.{prixDecimal}','{km}')";

                            MySqlConnection connection = new MySqlConnection(connectionString);
                            connection.Open();

                            Console.WriteLine(commande);

                            MySqlCommand command = connection.CreateCommand();
                            command.CommandText = commande;
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    break;
                }
            }
        }
        static double Prix(int ancienneté, double km, int type)
        {
            double res = 20; // prix de base pour la prise en charge du client
            if (type == 0) // prix par km Voiture
            {
                res += km;
            }
            else if (type == 1) // prix par km Camionnette
            {
                res += km * 1.5;
            }
            else if (type == 2) // prix par km PoidsLourd
            {
                res += km * 1.75;
            }
            res = res * (1 + ((double)ancienneté / 400));
            return Math.Round(res, 2);
        }
    }
}