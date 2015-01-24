using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;

namespace TestCases
{
    /// <summary>
    /// These test cases are in no sense comprehensive!  They are intended primarily to show you
    /// how to create your own, which we strong recommend that you do!  To run them, pull down
    /// the Test menu and do Run > All Tests.
    /// </summary>
    [TestClass]
    public class UnitTests
    {
        // Example
        public static double Lookup(String s)
        {
            return 0.0;
        }

        [TestMethod]
        public void test()
        {
            Formula f = new Formula("2+3");
            Assert.AreEqual(f.Evaluate(Lookup), 5.0, 1e-6);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void uneqParen()
        {
            Formula f = new Formula("(()))");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct1()
        {
            Formula f = new Formula("x");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct2()
        {
            Formula f = new Formula("2++3");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct3()
        {
            Formula f = new Formula("2 3");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct4()
        {
            Formula f = new Formula("2f3");
        }

        [TestMethod]
        public void Construct5()
        {
            Formula f = new Formula("xy12");
        }

        [TestMethod]
        public void Construct6()
        {
            Formula f = new Formula("x2 + y3");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct7()
        {
            Formula f = new Formula("2x + 3y");
        }

        [TestMethod]
        public void Construct8()
        {
            Formula f = new Formula("2.5e9 + x5 / 17");
        }

        [TestMethod]
        public void Construct9()
        {
            Formula f = new Formula("5 * X5 / 134.77 - D7");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct10()
        {
            Formula f = new Formula("5 * 5x / 134.77 - 74");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct11()
        {
            Formula f = new Formula("5 * xg5 x7 / 134.77 - 74");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct13()
        {
            Formula f = new Formula("(x6 + 3) - ");
        }

        [TestMethod]
        public void Construct14()
        {
            Formula f = new Formula("(x6 + 3) - 4 ");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct15()
        {
            Formula f = new Formula("(x6 + 3)5 ");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct16()
        {
            Formula f = new Formula("5 - -54");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct17()
        {
            Formula f = new Formula("( + )");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct18()
        {
            Formula f = new Formula("( ) + ( )");
        }


        //Evaluation


        [TestMethod]
        public void Evaluate1()
        {
            Formula f = new Formula("2+3");
            Assert.AreEqual(f.Evaluate(s => 0), 5.0, 1e-6);
        }

        [TestMethod]
        public void Evaluate2()
        {
            Formula f = new Formula("x5");
            Assert.AreEqual(f.Evaluate(s => 22.5), 22.5, 1e-6);
        }


        // s => 22.5

        // double lookup(string s)
        //      return 22.5;


        [TestMethod]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Evaluate3()
        {
            Formula f = new Formula("x5 + y6");
            f.Evaluate(s => { throw new ArgumentException(); });
        }

        [TestMethod]
        public void Evaluate4()
        {
            Formula f = new Formula("13 - 6");
            Assert.AreEqual(f.Evaluate(s => 0), 7.0, 1e-6);
        }

        [TestMethod]
        public void Evaluate5()
        {
            Formula f = new Formula("45 + 10 * 4");
            Assert.AreEqual(f.Evaluate(s => 0), 85.0, 1e-6);
        }

        [TestMethod]
        public void Evaluate6()
        {
            Formula f = new Formula("10 / 5 + 3");
            Assert.AreEqual(f.Evaluate(s => 0), 5.0, 1e-6);
        }

        [TestMethod]
        public void Evaluate7()
        {
            Formula f = new Formula("10 / (2 + 3)");
            Assert.AreEqual(f.Evaluate(s => 0), 2.0, 1e-6);
        }
        
        [TestMethod]
        public void Evaluate8()
        {
            Formula f = new Formula("17");
            Assert.AreEqual(f.Evaluate(s => 0), 17.0, 1e-6);
        }
    }
}
