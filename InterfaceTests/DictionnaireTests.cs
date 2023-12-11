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
    public class DictionnaireTests
    {
        [TestMethod()]
        public void RechDichoRecursifInstance_TestPasDsDico()
        {
            Dictionnaire dico = new Dictionnaire("Mots_Français.txt", "Français");
            string motPasDansDico = "LSNDNSZJ";
            Assert.AreEqual(false, dico.RechDichoRecursifInstance(motPasDansDico));
        }

        [TestMethod()]
        public void RechDichoRecursifInstance_TestEstDsDico()
        {
            Dictionnaire dico = new Dictionnaire("Mots_Français.txt", "Français");
            string motEstDansDico = "MOT";
            Assert.AreEqual(true, dico.RechDichoRecursifInstance(motEstDansDico));
        }
    }
}