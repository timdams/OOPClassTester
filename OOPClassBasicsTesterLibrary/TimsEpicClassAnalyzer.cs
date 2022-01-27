using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace OOPClassBasicsTesterLibrary
{
    public class TimsEpicClassAnalyzer
    {
        private object classToTest;
        public TimsEpicClassAnalyzer(object classToTest)
        {
            if (classToTest != null)
                this.classToTest = classToTest;
            else
                throw new NullReferenceException("Vergeet niet om de te testen klasse te instantieren en aan constructor mee te geven");
        }
        
        public bool CheckFullProperty(string propName, Type returnType, string backFieldName = "")
        {
            return CheckProperty(propName, returnType, false, backFieldName);
        }

        public bool CheckAutoProperty(string propName, Type returnType)
        {
            return CheckProperty(propName, returnType, true);
        }

        public bool CheckMethod(string methodName, Type returntype, Type[] args = null)
        {
            bool methodExist = classToTest.GetType().GetMethod(methodName) != null;
            Assert.AreEqual(true, methodExist, $"Geen methode {methodName} gevonden");
            if (!methodExist)
                return false;

            var propType = classToTest.GetType().GetMethod(methodName).ReturnType;
            Assert.AreEqual(returntype, propType, $"Methode {methodName} niet het verwachte returntype ({returntype.Name})");
            
            var m = classToTest.GetType().GetMethod(methodName);
            var argInMethod = m.GetParameters();
            if (args == null && argInMethod?.Length != 0)
            {
                Assert.Fail($"De methode {methodName} mag geen parameters volgens de opgave hebben. Maar in de klasse staat dat ze er {argInMethod.Length} vereist.");
            }
            else if (args!=null)
            {
                if(argInMethod==null)
                {
                    Assert.Fail($"De methode {methodName} verwacht geen parameters. Maar volgens de opgave vereist de methode er {args.Length}.");
                    return false;
                }
                else
                {
                    if(argInMethod.Length != args.Length)
                    {
                        Assert.Fail($"Aantal parameters volgens opgave  ({args.Length}) komt niet overeen met aantal in klasse effectief  ({argInMethod.Length}).");
                    }
                    //argumenten verwerken en testen
                    for (int i = 0; i < argInMethod.Length; i++)
                    {
                        Assert.AreEqual(argInMethod[i].ParameterType, args[i], $"Bij de methode {methodName} verwachtte ik bij parameter {i + 1} het type {args[i]} maar kreeg {argInMethod[i].ParameterType}");
                    }
                }
            }
            else
            {
                
            }

            return true;

        }


        public string TestMethod(string methodName, object[] args, object expectedValue =null, string extraInfo="")
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                var methodToTest = classToTest.GetType().GetMethod(methodName);
                var result = methodToTest.Invoke(classToTest, args);
                string info = $"{methodName} aanroepen ";
                if (args != null)
                    info += "met volgende parameters({ printObjects(args)})";
                info += $" gaf {result} i.p.v. {expectedValue}. {extraInfo}";
                Assert.AreEqual(expectedValue, result, info);

                return sw.ToString().Trim();
            }
   
        }

        private string printObjects(object[] args)
        {
            
            if (args!=null)
            {
                string res = "";
                foreach (var arg in args)
                {
                    res += arg + ",";
                }
                return res.Substring(0, res.Length - 2);
            }
            return "";
           
        }

        public void TestBackingFieldProp(string propName, string backfieldName,object valueToSet, object ValueExpected)
        {
            SetProp(propName, valueToSet);
            var backfieldWaarde = classToTest.GetType().GetField(backfieldName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(classToTest);
            Assert.AreEqual(backfieldWaarde, ValueExpected, $"De instantievariabele {backfieldName} krijgt niet de waarde die aan de {propName} property werd meegegeven");    
        }

        public void TestPropGetSet(string propName, object valueToSet, object valueExpected)
        {

            SetProp(propName, valueToSet);
            var result = GetProp(propName);
            Assert.AreEqual(valueExpected, result, $"{propName} property getter/setter (doe je de nodige controles in de setter?) ");

        }
        public object GetProp(string propName)
        {
            return classToTest.GetType().GetProperty(propName).GetValue(classToTest);
           
        }
        public void SetProp(string propName, object valueToSet)
        {
            classToTest.GetType().GetProperty(propName).SetValue(classToTest, valueToSet);
        }
        private bool CheckProperty(string propName, Type returnType, bool isAutoprop, string backFieldName="")
        {

            bool propExists = classToTest.GetType().GetProperty(propName) != null;
            Assert.AreEqual(true, propExists, $"Geen property {propName} gevonden");
            if(!propExists)
                return false;

            var propType = classToTest.GetType().GetProperty(propName).GetMethod.ReturnType;
            Assert.AreEqual(returnType, propType, $"Property {propName} niet van type {returnType.Name}");

            var testAutoProp = IsAutoProp(classToTest.GetType().GetProperty(propName));
            Assert.AreEqual(testAutoProp, isAutoprop, $"Property {propName} is zo te zien een autoprop en geen full prop.");

            //TODO: creepy code.Veronderstel dat backingfield met kleine letter begint én niet met underscore
            if (!isAutoprop)
            {
                if (backFieldName == "")
                    backFieldName = char.ToLower(propName[0]) + propName.Substring(1);
                bool hasBackfield = classToTest.GetType().GetField(backFieldName, BindingFlags.Instance | BindingFlags.NonPublic) != null;
                Assert.AreEqual(true, hasBackfield, $"Geen instantievariabele {backFieldName} gevonden");

            }


            return true;
        }



        // Bron: https://stackoverflow.com/questions/2210309/how-to-find-out-if-a-property-is-an-auto-implemented-property-with-reflection
        private bool IsAutoProp(PropertyInfo info)
        {
            bool mightBe = info.GetGetMethod()
                               .GetCustomAttributes(
                                   typeof(CompilerGeneratedAttribute),
                                   true
                               )
                               .Any();
            if (!mightBe)
            {
                return false;
            }


            bool maybe = info.DeclaringType
                             .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(f => f.Name.Contains(info.Name))
                             .Where(f => f.Name.Contains("BackingField"))
                             .Where(
                                 f => f.GetCustomAttributes(
                                     typeof(CompilerGeneratedAttribute),
                                     true
                                 ).Any()
                             )
                             .Any();

            return maybe;
        }
    }
}
