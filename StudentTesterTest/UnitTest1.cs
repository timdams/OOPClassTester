using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOPClassBasicsTesterLibrary;
using StudentlikeConsoleOplossing;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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
            
            tester.CheckFullProperty("Toppings", typeof(string),"topje");
            tester.CheckAutoProperty("IsPizza", typeof(bool));
            
            string priceProp = "Price";
            if(tester.CheckFullProperty(priceProp, typeof(double)))
            {
                ////Controle prop testen
                double starttop = 66.68;
                tester.TestPropGetSet(priceProp, starttop, starttop); //gewone get/set test
                tester.TestPropGetSet(priceProp, -1.0, starttop); //negatief getal zou terug starttop moeten geven


                // backing field eens controleren
                tester.TestBackingFieldProp(priceProp, "price", 2.0, 2.0);
            }


            //if (hasProp)
            //{

            //    //TODO ZOVEEL HERHALING...HET IS BESCHAMEND
            //    double starttop = 66.68;
            //    r.GetType().GetProperty("Price").SetValue(r, starttop);
            //    var propwaarde = r.GetType().GetProperty("Price").GetValue(r);
            //    Assert.AreEqual(propwaarde, starttop, "Get van Price geeft niet de waarde terug die met de Set werd ingesteld");
            //    r.GetType().GetProperty("Price").SetValue(r, -2.5);
            //    Assert.AreEqual(r.GetType().GetProperty("Price").GetValue(r), starttop, "Een negatieve waarde als price geven geeft niet gewenste resultaat");
            //}



        }


        private void GraadTest(int graad, string text)
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                var r = new StudentlikeConsoleOplossing.StudentClassToMake();
                if (r.GetType().GetProperty("Percentage") != null)
                {
                    var p = r.GetType().GetProperty("Percentage");
                    p.SetValue(r, graad);
                    if (r.GetType().GetMethod("PrintGraad") != null)
                    {
                        var l = r.GetType().GetMethod("PrintGraad");
                        l.Invoke(r, null);
                        var result = sw.ToString().Trim();
                        Assert.AreEqual(text, result, $"{graad} zou {text} moeten geven maar ik kreeg {text}");
                    }
                    else
                    {
                        Assert.Fail("Geen methode PrintGraad gevonden");
                    }

                }
                else
                {
                    Assert.Fail("Geen autoproperty Percentage gevonden");
                }
            }
        }


        [TestMethod]
        public void PrintGraadTest()
        {
            GraadTest(10, "Niet geslaagd");
            GraadTest(40, "Niet geslaagd");
            GraadTest(50, "Voldoende");
            GraadTest(65, "Voldoende");
            GraadTest(68, "Voldoende");
            GraadTest(69, "Onderscheiding");
            GraadTest(75, "Onderscheiding");
            GraadTest(76, "Grote onderscheiding");
            GraadTest(85, "Grote onderscheiding");
            GraadTest(86, "Grootste onderscheiding");
        }


  
    }
}
