using System.Runtime.CompilerServices;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface
{
    public class Dictionnaire
    {
        string langue;
        string[][] dicoTab = new string[26][];
        string nomFichier;
        /// <summary>
        /// Constructeur du dictionnaire prend en entrée le nom du fichier qu'il va lire et convertir en tableau de tableaux
        /// Tout en triant grace à un tri fusion les elements de chaque ligne quand il l'ajoute au tableau
        /// Les try catch permettent d'éviter les exception du type : Fichier non trouvé ou indice hors des limites
        /// </summary>
        /// <param name="nomDuFichier"></param>
        /// <param name="l"></param>
        public Dictionnaire(string nomDuFichier, string l)
        {
            this.nomFichier = nomDuFichier;
            this.langue = l;

            StreamReader sr = new StreamReader(nomDuFichier);
            try
            {
                string s;
                int i = 0;
                s = sr.ReadLine();
                while (i < 26)
                {
                    if (s != null)
                    {
                        this.dicoTab[i] = s.Split(' ');
                        TriFusionRecursif(this.dicoTab[i], 0, this.dicoTab[i].Length - 1);
                    }
                    else
                    {
                        this.dicoTab[i] = null;
                    }
                    s = sr.ReadLine();
                    i++;
                }
                sr.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Fichier introuvable: {nomDuFichier}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Erreur: IndexOutOfRange");
            }
            catch (FormatException)
            {
                Console.WriteLine("Erreur convertir en double");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur non pévue: {ex.Message}");
            }


        }

        public string NomFichier
        {
            get
            {
                return this.nomFichier;
            }
        }


        /// <summary>
        /// Methode pour decrire une instance de la classe Dictionaire en donnant pour chaque lettre le nom de mots qu'il contient
        /// </summary>
        /// <returns></returns>
        public string DicoToString()
        {
            int dicoTabILongueur = 0;
            string s = "Langue: " + langue;
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            for (int i = 0; i < 26; i++)
            {
                if (dicoTab[i] != null)
                {
                    dicoTabILongueur = dicoTab[i].Length - 1;
                }
                s = s + "\n" + alphabet[i] + ": " + dicoTabILongueur + " Mots";
                dicoTabILongueur = 0;
            }
            s += "\n";
            for (int i = 0; i < dicoTab[25].Length; i++)
            {
                s += " " + dicoTab[25][i];
            }
            return s;
        }

        /// <summary>
        /// Methode d'instance qui fait la recherche Recursive dans le dico pour plus de practicité
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>
        public bool RechDichoRecursifInstance(string mot)
        {
            bool b = false;
            for (int i = 0; i < 26 && b == false; i++)
            {
                b = RechDichoRecursif(0, dicoTab[i].Length - 1, dicoTab[i], mot);
            }
            return b;
        }
        
        /// <summary>
        /// Methode de classe qui effectue la recherche recursive dans le dico comme vu en TD et cours de Compléxité
        /// </summary>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        /// <param name="dico"></param>
        /// <param name="mot"></param>
        /// <returns></returns>
        public static bool RechDichoRecursif(int debut, int fin, string[] dico, string mot)
        {
            if (fin < debut || dico == null || dico.Length == 0 || String.Compare(dico[fin], mot) == -1 || String.Compare(mot, dico[debut]) == -1)
            {
                return false;
            }
            if (debut <= fin)
            {
                int c = (debut + fin) / 2;
                if (dico[c] == mot)
                {
                    return true;
                }
                if (String.Compare(dico[c], mot) == -1)
                {
                    debut = c + 1;
                    return RechDichoRecursif(debut, fin, dico, mot);
                }
                if (debut == fin)
                {
                    if (dico[c] == mot)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    fin = c - 1;
                    return RechDichoRecursif(debut, fin, dico, mot);
                }
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Methode qui effectue le tri fusion comme vu en TD
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        public static void TriFusionRecursif(string[] tab, int debut, int fin)
        {
            if (debut < fin)
            {
                int m = debut + (fin - debut) / 2;

                TriFusionRecursif(tab, debut, m);
                TriFusionRecursif(tab, m + 1, fin);
                Fusion(tab, debut, m, fin);
            }
        }
        /// <summary>
        /// Methode qui effectue la fusion entre deux tableaux
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="debut"></param>
        /// <param name="milieu"></param>
        /// <param name="fin"></param>
        private static void Fusion(string[] tab, int debut, int milieu, int fin)
        {
            int i, j, k;
            int n1 = milieu - debut + 1;
            int n2 = fin - milieu;
            string[] L = new string[n1];
            string[] R = new string[n2];

            for (i = 0; i < n1; i++)
                L[i] = tab[debut + i];

            for (j = 0; j < n2; j++)
                R[j] = tab[milieu + 1 + j];

            i = 0;
            j = 0;
            k = debut;

            while (i < n1 && j < n2)
            {
                if (String.Compare(L[i], R[j]) < 1)
                {
                    tab[k] = L[i];
                    i++;
                }
                else
                {
                    tab[k] = R[j];
                    j++;
                }

                k++;
            }

            while (i < n1)
            {
                tab[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                tab[k] = R[j];
                j++;
                k++;
            }
        }
        



    }
}
