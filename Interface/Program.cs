using Interface;
using System;
using System.Globalization;
using NAudio.Wave;

namespace Interface
{
    class Program
    {
        WaveOutEvent waveOut3 = new WaveOutEvent();
        AudioFileReader audioFile3 = new AudioFileReader("Round 1 Fight! (Mortal Kombat Meme) - Sound Effect for editing.mp3");
        WaveOutEvent waveOut2 = new WaveOutEvent();
        AudioFileReader audioFile2 = new AudioFileReader("The Immortals - Mortal Kombat - Techno Syndrome [Extended by Gilles Nuytens].mp3");
        /// <summary>
        /// Le Main s'occupe de faire l'affichage de l'entrée du jeu (titre) puis appelle AffMenu pour afficher le menu
        /// </summary>
        public static void Main()
        {
            //AUDIO
            var waveOut = new WaveOutEvent();
            var audioFile = new AudioFileReader("71.Zelda Links Awakening OST - Color Dungeon.mp3");
            
            waveOut.Init(audioFile);
            waveOut.Play();

            //AFFICHAGE
            // Obtenez les dimensions de la fenêtre de la console
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Titre
            Console.WriteLine("\n\n\n");
            string titre = "\t\t\t\t                  (         (        *       )        (     \r\n\t\t\t\t                  )\\ )      )\\ )   (  `   ( /(   *   ))\\ )  \r\n\t\t\t\t   (  (      (   (()/(  (  (()/(   )\\))(  )\\())` )  /(()/(  \r\n\t\t\t\t   )\\ )\\     )\\   /(_)) )\\  /(_)) ((_)()\\((_)\\  ( )(_))(_)) \r\n\t\t\t\t  ((_|(_) _ ((_) (_))_ ((_)(_))   (_()((_) ((_)(_(_()|_))   \r\n\t\t\t\t _ | | __| | | |  |   \\| __/ __|  |  \\/  |/ _ \\|_   _/ __|  \r\n\t\t\t\t| || | _|| |_| |  | |) | _|\\__ \\  | |\\/| | (_) | | | \\__ \\  \r\n\t\t\t\t \\__/|___|\\___/   |___/|___|___/  |_|  |_|\\___/  |_| |___/  \r\n                                                            \r\n\r\n";

            // Message sous le titre
            string message = "Taper entrée pour commencer";

            

            // Calculez la position de départ pour centrer le message
            int messageX = ((windowWidth) / 2)-(message.Length/2);
            int messageY = (windowHeight / 2 - 1) + 1; // Juste en dessous du titre

           
            Console.WriteLine(titre);

            // Positionnez le curseur et affichez le message
            Console.SetCursorPosition(messageX, messageY);
            Console.WriteLine(message);

            // Attendre que l'utilisateur appuie sur Entrée
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }

            // Nettoyez la console après avoir appuyé sur Entrée
            Console.Clear();
            AffMenu(waveOut);

        }
        /// <summary>
        /// La méthode AffMenu affiche le menu et laisse le choix a l'utilisateur entre le fichier, aléatoire et sortir
        /// Elle demande le fichier tant qu'elle ne le trouve pas dans le l'ordinateur
        /// </summary>
        public static void AffMenu(WaveOutEvent waveOut)
        {
            Dictionnaire dico = new Dictionnaire("Mots_Français.txt", "Français");
            Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t1. Jouer à partir d’un fichier");
            Console.WriteLine("\n\n\t\t\t\t\t2. Jouer à partir d’un plateau généré aléatoirement");
            Console.WriteLine("\n\n\t\t\t\t\t3. Sortir");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    {
                        Console.Clear();
                        //A PARTIR D UN FICHIER

                        string nomFichier = null;
                        Plateau plateau = null;
                        bool EstReussi = false;
                        do
                        {
                            Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\tEntrer le nom de votre fichier (il doit être dans le bin/NET.6)");
                            nomFichier = Console.ReadLine() + ".csv";
                            try
                            {
                                plateau = new Plateau(nomFichier);
                                EstReussi = true;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        } while (EstReussi == false);
                        Console.Clear();
                        Console.Write("\n\n\n\n\n\n\n\n\t\t\t\t\tVeuillez entrer le pseudo du joueur 1 : ");
                        string j1 = Console.ReadLine();
                        Console.Write("\n\n\t\t\t\t\tVeuillez entrer le pseudo du joueur 2 : ");
                        string j2 = Console.ReadLine();
                        Jeu jeu = new Jeu(dico, plateau, j1, j2);
                        Console.Clear();
                        AffRegles();
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        Console.Clear();
                        DateTime debut = DateTime.Now;
                        //AUDIO 2
                        waveOut.Stop();
                        var waveOut2 = new WaveOutEvent();
                        var audioFile2 = new AudioFileReader("The Immortals - Mortal Kombat - Techno Syndrome [Extended by Gilles Nuytens].mp3");
                        var waveOut3 = new WaveOutEvent();
                        var audioFile3 = new AudioFileReader("Round 1 Fight! (Mortal Kombat Meme) - Sound Effect for editing.mp3");
                        waveOut2.Stop();
                        waveOut3.Init(audioFile3);
                        waveOut3.Play();
                        Thread.Sleep(3000);
                        
                        waveOut2.Init(audioFile2);
                        waveOut2.Play();
                        jeu.Joueur1(TimeSpan.Zero, debut);


                        break;
                    }
                case ConsoleKey.D2:
                    {
                        Console.Clear();
                        Plateau plateau = new Plateau();
                        Console.Write("\n\n\n\n\n\n\n\n\t\t\t\t\tVeuillez entrer le pseudo du joueur 1 : ");
                        string j1 = Console.ReadLine();
                        Console.Write("\n\n\t\t\t\t\tVeuillez entrer le pseudo du joueur 2 : ");
                        string j2 = Console.ReadLine();
                        Jeu jeu = new Jeu(dico, plateau, j1, j2);
                        Console.Clear();
                        AffRegles();
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        Console.Clear();
                        DateTime debut = DateTime.Now;
                        //AUDIO 2

                        waveOut.Stop();
                        var waveOut3 = new WaveOutEvent();
                        var audioFile3 = new AudioFileReader("Round 1 Fight! (Mortal Kombat Meme) - Sound Effect for editing.mp3");
                        var waveOut2 = new WaveOutEvent();
                        waveOut2.Stop();
                        waveOut3.Init(audioFile3);
                        waveOut3.Play();
                        Thread.Sleep(3000);

                        var audioFile2 = new AudioFileReader("The Immortals - Mortal Kombat - Techno Syndrome [Extended by Gilles Nuytens].mp3");

                        waveOut2.Init(audioFile2);
                        waveOut2.Play();
                        jeu.Joueur1(TimeSpan.Zero, debut);
                        //ALEA
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t\tON ESPERE VOUS REVOIR BIENTOT!");
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("\n\n\t\t\t\t\tAppuyez sur 1,2 ou 3 pour séléctionner");
                        Thread.Sleep(3000);
                        Console.Clear();
                        AffMenu(waveOut);
                        break;
                    }
            }
        }
        /// <summary>
        /// La méthode Fin s'execute quand une partie est finie, elle demande à l'utilisateur si il veut refaire une partie ou non
        /// si oui on retourne au menu
        /// si non on sort de la console
        /// </summary>
        public static void Fin()
        {
            Console.Clear();
            Console.WriteLine("\n\n (                \r\n\t\t\t\t\t\t\t )\\ )             \r\n\t\t\t\t\t\t\t(()/(  (          \r\n\t\t\t\t\t\t\t /(_)) )\\   (     \r\n\t\t\t\t\t\t\t(_))_|((_)  )\\ )  \r\n\t\t\t\t\t\t\t| |_   (_) _(_/(  \r\n\t\t\t\t\t\t\t| __|  | || ' \\)) \r\n\t\t\t\t\t\t\t|_|    |_||_||_|  \r\n\t\t\t\t\t\t\t                  \r\n\r\n");
            


            // Positionnez le curseur et affichez le titre
            
            string reco = "Voulez vous recommencer une nouvelle partie? Y/N";
            
            int recoX = (Console.WindowWidth - reco.Length) / 2;
            int recoY = (Console.WindowHeight / 2) + 1;

            // Positionnez le curseur et afficher le sous titre
            Console.SetCursorPosition(recoX, recoY);
            Console.WriteLine(reco);
            

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Y:
                    {
                        Thread.Sleep(3000);
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t\tOK C'EST REPARTI!!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        Main();
                        break;
                    }
                case ConsoleKey.N:
                    {
                        Thread.Sleep(3000);
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t\tON ESPERE VOUS REVOIR BIENTOT!");
                        Thread.Sleep(3000);
                        
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Thread.Sleep(3000);
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t\tON ESPERE VOUS REVOIR BIENTOT!");
                        Thread.Sleep(5000);
                        Environment.Exit(0);
                        break;
                    }

            }
        }
        public static void AffRegles()
        {
            string r = "Règles :";
            int messageX = ((Console.WindowWidth) / 2) + (r.Length / 2);
            
            Console.WriteLine("\n\n\n\n"+r.PadLeft(messageX));
            Console.WriteLine("\n\n\n\t\tA votre tour, vous allez devoir trouver un mot qui commence sur la dernière ligne du plateau." +
                "\n\n\t\tLe mot peut continuer seulement vers le haut(vertical horizontal ou diagonal)." +
                "\n\n\t\tSi le mot n'est pas dans le n'est pas dans le dictionnaire ou sur le plateau la main passe à l'autre joueur." +
                "\n\n\t\tChaque joueur a 10 secondes pour trouver un mot. Passé ce temps appuyez sur une touche pour continuer." +
                "\n\n\t\tLes lettres ont la même valeur qu'au Scrabble donc pensez y si vous voulez gagner !" +
                "\n\n\n\n\n\t\t\t\t\t\t>>Appuyez sur entrée pour continuer<< ");
        }
    }
}




