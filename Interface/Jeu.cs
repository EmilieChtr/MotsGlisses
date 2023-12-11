using System.Runtime.CompilerServices;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;


using System.Threading;
using Interface;

namespace Interface
{
    //FINITOOO
    public class Jeu

    {

        Joueur[] joueurs;

        Plateau plateau;

        Dictionnaire dico;

        TimeSpan tempsTour;

        TimeSpan tempsTot;

        bool EstTourJoueur1 = true;



        public Jeu(Dictionnaire dico, Plateau plateau, string joueur1, string joueur2, int tempsTour = 10, int tempsTot = 180)
        {
            this.dico = dico;
            this.plateau = plateau;
            this.joueurs = new Joueur[] { new Joueur(joueur1), new Joueur(joueur2) };
            this.tempsTour = TimeSpan.FromSeconds(tempsTour);
            this.tempsTot = TimeSpan.FromSeconds(tempsTot);
        }





        /// <summary>

        /// Avec Joueur2: cette methode est une partie d'une sorte d'algorithme croisé pour passer d'un joueur à lautre dans la partie

        /// </summary>

        /// <param name="t"></param>

        /// <param name="debut"></param>

        public void Joueur1(TimeSpan t, DateTime debut)
        {
            DateTime debutTour = DateTime.Now;


            if (t >= tempsTot)
            {
                Resume();

            }
            else
            {

                Console.WriteLine("\n\n\t\t\t\t\t\tTour de : " + joueurs[0].Nom + "\n\n");//


                bool tourEstFini = false;

                while (tourEstFini == false)//ou timer depassé
                {

                    Console.Write(plateau.PlateauToString());

                    Console.WriteLine("\n\n\t\t\t\t\t" + joueurs[0].toString());

                    string mot = "";

                    Console.WriteLine("\n\n\t\t\t\t\t\tEntrer un mot:");

                    mot = Console.ReadLine().ToUpper();

                    if (mot == "X")
                    {

                        Resume();

                    }
                    else if (DateTime.Now - debutTour > tempsTour)
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\t\tTu as pris trop de temps !!!!!!!!!!!!!");

                        Thread.Sleep(3000);

                        Console.Clear();
                    }

                    else if (dico.RechDichoRecursifInstance(mot) == false)
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\t\tLe mot " + mot + " n'est pas dans le dictionnaire!");
                        Thread.Sleep(3000);

                        Console.Clear();



                    }
                    else if (joueurs[0].Mots.Contains(mot) == true || joueurs[1].Mots.Contains(mot) == true)
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t\tLe mot " + mot + " a déjà été utilisé !");
                        Thread.Sleep(3000);
                        Console.Clear();
                    }

                    else
                    {
                        joueurs[0].Add_Mot(mot);
                        joueurs[0].Add_Score(mot);
                        Console.Clear();
                        plateau.MajPlateau(plateau.RechercheMot(mot));
                    }
                    tourEstFini = true;

                }

                Joueur2(t + (DateTime.Now - debutTour), debut);



            }

        }

        /// <summary>

        /// Avec Joueur1: cette methode est une partie d'une sorte d'algorithme croisé pour passer d'un joueur à lautre dans la partie

        /// </summary>

        /// <param name="t"></param>

        /// <param name="debut"></param>

        public void Joueur2(TimeSpan t, DateTime debut)
        {
            DateTime debutTour = DateTime.Now;


            if (t >= tempsTot)
            {

                Resume();

            }
            else
            {

                Console.WriteLine("\n\n\t\t\t\t\t\tTour de : " + joueurs[1].Nom + "\n\n");

                bool tourEstFini = false;

                while (tourEstFini == false)
                {

                    Console.Write(plateau.PlateauToString()); //write + score

                    Console.WriteLine("\n\n\t\t\t\t\t" + joueurs[1].toString());

                    string mot = "";

                    Console.WriteLine("\n\n\t\t\t\t\t\tEntrer un mot:");

                    mot = Console.ReadLine().ToUpper();

                    if (mot == "X")
                    {

                        Resume();

                    }
                    else if (DateTime.Now - debutTour > tempsTour)
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\t\tTu as pris trop de temps!!!!!!!!!!!!!");

                        Thread.Sleep(3000);

                        Console.Clear();
                    }

                    else if (dico.RechDichoRecursifInstance(mot) == false)
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\t\tLe mot " + mot + " n'est pas dans le dictionnaire!");
                        Thread.Sleep(3000);
                        Console.Clear();
                    }
                    else if (joueurs[0].Mots.Contains(mot) == true || joueurs[1].Mots.Contains(mot) == true)
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t\tLe mot " + mot + " a déjà été utilisé !");
                        Thread.Sleep(3000);
                        Console.Clear();
                    }

                    else
                    {
                        joueurs[1].Add_Mot(mot);
                        joueurs[1].Add_Score(mot);
                        Console.Clear();
                        plateau.MajPlateau(plateau.RechercheMot(mot));
                    }
                    tourEstFini = true;

                }

                Joueur1(t + (DateTime.Now - debutTour), debut);



            }

        }



        public void Resume()
        {

            Console.Clear();

            if (joueurs[0].Score > joueurs[1].Score)
            {

                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\t" + joueurs[0].Nom + " a gagné la partie avec " + joueurs[0].Score + " points contre " + joueurs[1].Nom + " avec " + joueurs[1].Score + " points");

            }

            else if (joueurs[0].Score < joueurs[1].Score)
            {

                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\t" + joueurs[1].Nom + " a gagné la partie avec " + joueurs[1].Score + " points contre " + joueurs[0].Nom + " avec " + joueurs[0].Score + " points");

            }

            if (joueurs[0].Score == joueurs[1].Score)
            {
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\t" + joueurs[0].Nom + " et " + joueurs[1].Nom + " sont a égalité avec " + joueurs[1].Score + " points! Bravo à tous les deux!");
            }
            Console.WriteLine("\n\n\n\n\n\t\t\t\t" + joueurs[0].toStringListe() + "\n\n\n\n\n\t\t\t\t" + joueurs[1].toStringListe());
            Thread.Sleep(8000);
            Program.Fin();

        }

    }

}
