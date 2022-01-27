using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOPClassBasicsTesterLibrary;
using StudentlikeConsoleOplossing;
using System;

namespace StudentTesterTest
{
    [TestClass]
    public class UnitTest1
    {

        //OPGELET: VERGEET NIET HIER JUISTE KLASSE MEE TE GEVEN AAN TESTER
        TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new StudentClassToMake());


        [TestMethod, Description("Controleert of nodige properties aanwezig zijn en werken")]
        public void PropTest()
        {

            tester.CheckFullProperty("Toppings", typeof(string), "topje");
            tester.CheckAutoProperty("IsPizza", typeof(bool));

            string priceProp = "Price";
            if (tester.CheckFullProperty(priceProp, typeof(double)))
            {
                ////Controle prop testen
                double starttop = 66.68;
                tester.TestPropGetSet(priceProp, starttop, starttop); //gewone get/set test
                tester.TestPropGetSet(priceProp, -1.0, starttop); //negatief getal zou terug starttop moeten geven


                // backing field eens controleren
                tester.TestBackingFieldProp(priceProp, "price", 2.0, 2.0);
            }
        }

        [TestMethod, Description("Controleert of nodige methoden aanwezig zijn en werken")]
        public void MethodTest()
        {

            if (tester.CheckMethod("Bereken", typeof(int), new Type[] { typeof(int), typeof(string) }))
            {
                var arguments = new object[] { 3 , "test" };
                string consoleOutput= tester.TestMethod("Bereken", arguments, 3);
                Assert.AreEqual("second is gelijk aan test", consoleOutput);
                
            }
        }
    }
}
