using Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Tests
{
    [TestClass()]
    public class PlateauTests
    {
        [TestMethod()]
        public void DecaleColonnePlateau1Test_Valide()
        {
            char[,] matricePlateau = { { 'A', 'B', 'C' }, { 'D', 'A', 'F' }, { 'G', 'H', 'A' } };
            char[,] matricePlateauDecaleeVrai = { { 'A', 'B', 'A' }, { 'D', 'A', 'C' }, { 'G', 'H', 'F' } };
            Plateau.DecaleColonnePlateau1(matricePlateau, 2);
            for (int i = 0; i < matricePlateau.GetLength(0); i++)
            {
                for (int j = 0; j < matricePlateau.GetLength(1); j++)
                {
                    Assert.AreEqual(matricePlateauDecaleeVrai[i, j], matricePlateau[i, j]);
                }
            }

        }

        [TestMethod()]
        public void RechercheMotTest_Mot1Lettre()
        {
            Plateau plateau = new Plateau();
            Dictionary<(int, int), char> res = new Dictionary<(int, int), char>();
            string mot = "s";
            
            Assert.AreEqual(null,plateau.RechercheMot(mot));
        }

        [TestMethod()]
        public void RechercheMotTest_MotVrai()
        {
            Plateau plateau = new Plateau("PlateauTest.csv");
            string mot = "MAIS";
            Dictionary<(int, int), char> attendu = new Dictionary<(int, int), char>
            {
                [(7, 0)] = 'M',
                [(7, 1)] = 'A',
                [(6, 1)] = 'I',
                [(5, 1)] = 'S'
            };

            var resultat = plateau.RechercheMot(mot);

            Assert.AreNotEqual(null, resultat);
            Assert.AreEqual(attendu.Count, resultat.Count);

            foreach (var pair in attendu)
            {
                Assert.IsTrue(resultat.ContainsKey(pair.Key), $"Le dictionnaire résultat ne contient pas la clé {pair.Key}");
                Assert.AreEqual(pair.Value, resultat[pair.Key], $"La valeur pour la clé {pair.Key} ne correspond pas");
            }
        }


    }
}