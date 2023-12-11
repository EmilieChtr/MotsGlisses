using System;

using System.Collections.Generic;

using System.IO;
using System.Threading;



namespace Interface

{

    public class Plateau

    {

        char[,] matricePlateau;

        string[][] lettresRegles = new string[26][];

        string fichierCSVPlateau;

        string fichierLettresRegles;



        double[] probabilitesLettres = {

            0.085, 0.01, 0.035, 0.036, 0.171, 0.009, 0.008, 0.073, 0.075, 0.003, 0.006, 0.044, 0.049, 0.058,0.055, 0.029, 0.010, 0.073, 0.075, 0.057, 0.014,0.003, 0.004, 0.001, 0.013, 0.001

        };



        int[] nbLettresPlateau = new int[26];




        /// <summary>
        /// Constructeur d'un plateau pour le cas sans fichier
        /// </summary>
        /// <param name="taille"></param>
        /// <param name="fPlateau"></param>
        /// <param name="fLettresRegles"></param>
        public Plateau(int taille = 8, string fPlateau = "Plateau.csv", string fLettresRegles = "Lettre.txt")

        {

            this.fichierCSVPlateau = fPlateau;

            this.fichierLettresRegles = fLettresRegles;

            InitiaIntTab0(nbLettresPlateau);



            CreerLettresRegles(fichierLettresRegles);


            this.matricePlateau = new char[taille, taille];

            RemplirPlateauLettres();

        }


        /// <summary>
        /// Constructeur du plateau pour le cas avec un fichier
        /// </summary>
        /// <param name="fPlateau"></param>
        public Plateau(string fPlateau)

        {

            this.fichierCSVPlateau = fPlateau;

            this.fichierLettresRegles = "Lettre.txt";

            InitiaIntTab0(nbLettresPlateau);



            //Declaration lettresRegles

            CreerLettresRegles(fichierLettresRegles);

            //-----------------------------

            this.matricePlateau = ToRead();

        }

        /// <summary>
        /// Met le contenu du fichier Lettre.txt dans string[][] lettresRegles
        /// </summary>
        /// <param name="fLettresRegles"></param>
        public void CreerLettresRegles(string fLettresRegles)

        {

            string[] lignes = File.ReadAllLines(fLettresRegles);

            for (int i = 0; i < lignes.Length; i++)

            {

                if (lignes[i].Split(',').Length == 3)

                {

                    lettresRegles[i] = lignes[i].Split(',');

                }

            }

        }


        /// <summary>
        /// Rempli le plateau de lettre en appelant ChoisirLettre pour choisir des lettre avec une probabilité d'apparition cohérente à la probabilité de retrouvé ces lettres dans la langue francaise
        /// </summary>
        public void RemplirPlateauLettres()
        {
            Random rand = new Random();
            for (int i = 0; i < matricePlateau.GetLength(0); i++)
            {
                for (int j = 0; j < matricePlateau.GetLength(1); j++)
                {
                    char lettre = ChoisirLettre(probabilitesLettres, rand);
                    matricePlateau[i, j] = lettre;
                }
            }

        }


        /// <summary>
        /// Utilise le tableau de probabilité des lettres déclaré dans les attributs de la classe pour choisir une lettre
        /// </summary>
        /// <param name="probabilites"></param>
        /// <param name="rand"></param>
        /// <returns></returns>
        static char ChoisirLettre(double[] probabilites, Random rand)
        {
            double valeurAleatoire = rand.NextDouble();
            double probabiliteCumulative = 0.0;
            for (int i = 0; i < probabilites.Length; i++)
            {
                probabiliteCumulative += probabilites[i];
                if (valeurAleatoire < probabiliteCumulative)
                {
                    return (char)('A' + i);
                }
            }
            return 'E';
        }


        /// <summary>
        /// Methode qui initialise chacun des membres d'un tableau de int à 0 de facon à ne pas avoir un tableau rempli de valeur null
        /// </summary>
        /// <param name="tab"></param>
        public static void InitiaIntTab0(int[] tab)

        {

            for (int i = 0; i < tab.Length; i++)

            {

                tab[i] = 0;

            }

        }

        /// <summary>
        /// Methode qui retourne le plateau en string (toString())
        /// </summary>
        /// <returns></returns>
        public string PlateauToString()

        {

            string s = "\t\t\t\t\t\t|";

            for (int i = 0; i < this.matricePlateau.GetLength(0); i++)

            {
                for (int j = 0; j < matricePlateau.GetLength(1); j++)

                {

                    s += matricePlateau[i, j] + "|";

                }

                if (i < matricePlateau.GetLength(0) - 1)
                {
                    s += "\n\t\t\t\t\t\t|";
                }

            }
            s += "\n";
            return s;

        }

        /// <summary>
        /// Methode qui ecrit le fichier du plateau et sauvegarde la plateau à l'interieur
        /// </summary>
        public void ToFile()

        {

            using (StreamWriter sr = new StreamWriter(fichierCSVPlateau))

            {



                for (int i = 0; i < matricePlateau.GetLength(0); i++)

                {

                    for (int j = 0; j < matricePlateau.GetLength(1); j++)

                    {

                        sr.Write(matricePlateau[i, j]);



                        if (j < matricePlateau.GetLength(1) - 1)

                        {

                            sr.Write(",");

                        }

                    }

                    sr.WriteLine();

                }

            }

        }

        /// <summary>
        /// Methode qui lit le fichier csv et convertit son contenu en Plateau
        /// </summary>
        /// <returns></returns>
        public char[,] ToRead()

        {
            string[] lines = File.ReadAllLines(fichierCSVPlateau);

            char[,] matrice = new char[lines.Length, lines[0].Split(';').Length];



            for (int i = 0; i < lines.Length; i++)

            {

                for (int j = 0; j < lines[0].Split(';').Length; j++)

                {

                    matrice[i, j] = lines[i].Split(';')[j].ToUpper()[0];

                }

            }



            return matrice;

        }

        /// <summary>
        /// Methode qui Recherche un mot dans la matrice et qui retourne un Dictionary avec les coordonnéés des lettres si le mot est trouvé
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
        public Dictionary<(int, int), char> RechercheMot(string mot)



        {

            Dictionary<(int, int), char> res = new Dictionary<(int, int), char>();

            bool b = false;

            if (mot.Length <= 1)

            {

                Console.WriteLine("Veuillez écrire un mot d'au moins de deux lettres.");

                return null;

            }

            int nblignes = matricePlateau.GetLength(0);

            int nbcol = matricePlateau.GetLength(1);

            for (int col = 0; col < nbcol && b == false; col++) //on parcours la base du plateau de gauche à droite jusqu'à trouver le mot entier

            {

                if (MotEstPresentRecursif(mot, nblignes - 1, col, 0, res) == true)

                {

                    b = true;

                }

            }

            if (b == false)

            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\tLe mot n'existe pas dans cette matrice ou ne respecte pas les règles. \n\tt\tVeuillez rentrer un mot qui commence sur la base et qui continue en haut, à droite ou à gauche.");
                Thread.Sleep(3000);
                Console.Clear();
                return null;

            }

            return res;

        }

        /// <summary>
        /// Methode qui renvoit si true si un mot est present dans la matrice et false sinon
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="ligne"></param>
        /// <param name="col"></param>
        /// <param name="index"></param>
        /// <param name="res"></param>
        /// <returns></returns>
        bool MotEstPresentRecursif(string mot, int ligne, int col, int index, Dictionary<(int, int), char> res)

        {

            if (index == mot.Length) //condition d'arret

            {

                return true;

            }

            try

            {

                if (matricePlateau[ligne, col] == mot[index] && res.ContainsKey((ligne, col)) == false) //on s'assure que la case est pas deja dans

                {

                    res.Add((ligne, col), matricePlateau[ligne, col]);

                    bool b = MotEstPresentRecursif(mot, ligne, col - 1, index + 1, res) || MotEstPresentRecursif(mot, ligne - 1, col - 1, index + 1, res) || MotEstPresentRecursif(mot, ligne - 1, col, index + 1, res) || MotEstPresentRecursif(mot, ligne - 1, col + 1, index + 1, res) || MotEstPresentRecursif(mot, ligne, col + 1, index + 1, res);

                    //On verifie la case à gauche, en haut puis à droite +diag

                    if (b == true)

                    {

                        return true;

                    }

                    res.Remove((ligne, col));

                    return false;

                }

                return false;

            }

            catch (IndexOutOfRangeException e)

            {

                return false;

            }

        }

        /// <summary>
        /// Methode qui met à jour le Plateau
        /// </summary>
        /// <param name="res"></param>
        public void MajPlateau(Dictionary<(int, int), char> res)

        {

            if (res != null)

            {

                foreach (var key in res.Keys)

                {

                    int ligne = key.Item1;

                    int col = key.Item2;

                    matricePlateau[ligne, col] = ' ';

                }
                for (int col = 0; col < matricePlateau.GetLength(1); col++)
                {
                    int ligneVide = -1;
                    for (int ligne1 = matricePlateau.GetLength(0) - 1; ligne1 >= 0; ligne1--)
                    {
                        if (matricePlateau[ligne1, col] == ' ' && ligneVide == -1)
                        {
                            ligneVide = ligne1;
                        }
                        else if (matricePlateau[ligne1, col] != ' ' && ligneVide != -1)
                        {
                            matricePlateau[ligneVide, col] = matricePlateau[ligne1, col];
                            matricePlateau[ligne1, col] = ' ';
                            ligneVide--;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Methode qui decale une colone d'une matrice plusieurs fois en fonction de la ligne à laquel la lettre effacée est et si la case en dessous est effacé aussi ou pas
        /// </summary>
        /// <param name="plateau"></param>
        /// <param name="col"></param>
        /// <param name="ligne"></param>
        public static void DecaleColonnePlateau(char[,] plateau, int col, int ligne)
        {
            int lignes = plateau.GetLength(0);
            if (plateau[lignes - 1, col] == ' ')
            {
                DecaleColonnePlateau1(plateau, col);
            }
            else
            {
                for (int i = 0; i < lignes - ligne; i++)
                {
                    DecaleColonnePlateau1(plateau, col);
                }
            }
        }

        /// <summary>
        /// Methode qui decale une colonne d'une matrice de 1
        /// </summary>
        /// <param name="plateau"></param>
        /// <param name="col"></param>
        public static void DecaleColonnePlateau1(char[,] plateau, int col)

        {
            int lignes = plateau.GetLength(0);
            char temp = plateau[lignes - 1, col];
            for (int j = lignes - 1; j > 0; j--)
            {
                plateau[j, col] = plateau[j - 1, col];
            }
            plateau[0, col] = temp;
        }
    }
}