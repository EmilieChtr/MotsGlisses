using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;



namespace Interface

{

    //2 tests reussis

    public class Joueur

    {

        string nom;
        string[][] lettresRegles = new string[26][];
        List<string> mots;

        int score;

        /// <summary>

        /// Le constructeur de Joueur qui prend en parametre un string pour le nom du joueur

        /// </summary>

        /// <param name="n"></param>

        public Joueur(string n)

        {

            this.nom = n;

            this.mots = new List<string>();

            this.score = 0;
            CreerLettresRegles("Lettre.txt");


        }

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



        public string Nom

        {                                           //PROPRIETES

            get { return this.nom; }

        }

        public List<string> Mots

        {

            get { return this.mots; }

        }

        public int Score

        {

            get { return this.score; }



        }

        /// <summary>

        /// Methode Add_Mot ajoute un mot a la liste de mots du joueur quand il trouve un mot dans le plateau

        /// </summary>

        /// <param name="mot"></param>

        public void Add_Mot(string mot)            //METHODES

        {

            mots.Add(mot);

        }

        /// <summary>

        /// La methode Add_Score prend en entrée le mot trouvé par le joueur et calcule combien de points ca fait (valeurs du Scrabble)

        /// Une fois le mot analysé, sa valeur est ajoutée au score total du joueur

        /// </summary>

        /// <param name="mot"></param>

        public void Add_Score(string mot)

        {

            int val = 0;

            for (int i = 0; i < mot.Length; i++)

            {
                for (int j = 0; j < 26; j++)
                {
                    if (lettresRegles[j][0][0] == mot[i])
                    {
                        val += Convert.ToInt32(lettresRegles[j][2]);
                    }
                }

            }

            this.score += val;

        }

        /// <summary>

        /// La methode Contient renvoie vrai si le mot passé en paramètre a déjà été trouvé par le joueur et false sinon

        /// </summary>

        /// <param name="mot"></param>

        /// <returns></returns>

        public bool Contient(string mot)

        {

            return Mots.Contains(mot);

        }

        public string toString()

        {

            return ("Joueur : " + this.nom + " a actuellement " + this.score + " points");

        }

        public string toStringListe()

        {

            string liste = null;

            foreach (string mot in mots)

            {

                liste = liste + "   " + mot;

            }

            return ("Mots trouvés par " + this.nom + " : " + liste);

        }

    }

}