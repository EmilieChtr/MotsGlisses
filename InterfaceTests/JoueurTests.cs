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
    public class JoueurTests
    {

        [TestMethod()]
        public void Add_ScoreTest()
        {
            Joueur j1 = new Joueur("Emi");
            j1.Add_Score("JOUEUR");
            Assert.AreEqual(13, j1.Score);
        }

        [TestMethod()]
        public void Add_MotTest()
        {
            Joueur j1 = new Joueur("Emi");
            j1.Add_Mot("JOUEUR");
            Assert.AreEqual(j1.Mots[j1.Mots.Count-1], "JOUEUR");
        }
    }
}