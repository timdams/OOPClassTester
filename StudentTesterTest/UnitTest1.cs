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
                tester.TestMethod("Bereken", arguments, 3);

                //using (var sw = new StringWriter())
                //{
                //    Console.SetOut(sw);
                //    var r = new StudentlikeConsoleOplossing.StudentClassToMake();
                //    if (r.GetType().GetProperty("Percentage") != null)
                //    {
                //        var p = r.GetType().GetProperty("Percentage");
                //        p.SetValue(r, graad);
                //        if (r.GetType().GetMethod("PrintGraad") != null)
                //        {
                //            var l = r.GetType().GetMethod("PrintGraad");
                //            l.Invoke(r, null);
                //            var result = sw.ToString().Trim();
                //            Assert.AreEqual(text, result, $"{graad} zou {text} moeten geven maar ik kreeg {text}");
                //        }
                //        else
                //        {
                //            Assert.Fail("Geen methode PrintGraad gevonden");
                //        }

                //    }
                //    else
                //    {
                //        Assert.Fail("Geen autoproperty Percentage gevonden");
                //    }
                //}
            }





        }
    }
}
