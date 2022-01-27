*Probably walking down an a well-trodden path, but still: let me do this :)* 

# What's this?

The OOPClassBasicsTesterLibrary contains a ...library (!) to use wile writing unit tests to check for basic class making skills. For example, it allows you to check if the user created the correct properties and following the correct naming, returntype, and whether the get/set combo works as expected.

(Again, I'm pretty sure things like Roslyn will also do this)



# Usage:

1. Add the OOPClassBacisTesterLibrary (nuget) to your unit test project: see  [https://www.nuget.org/packages/OOPClassBasicsTesterLibrary/](https://www.nuget.org/packages/OOPClassBasicsTesterLibrary/)
2. Make sure an empty class with the correct name already exists in the project to test 
3. In your Unit test class make sure to create the analyzer and give it an instant of the class to test (static classes/methods cant be tested...yet ) 
``TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new StudentClassToMake());``
4. Start creating tests 

## Current existing checks

* **CheckFullProperty**
* **CheckAutoProperty**
* **TestPropGetSet**: test if the get and set work by checking of the expected value is return from the get after setting it.
* **TestBackingFieldProp**: test if a backing field of a given name receives the correct information from a given property

* **CheckMethod**: Check if the method is compliant to the basic requirements (name, returntype, parameters)
* **TestMethod**: test if the method (optional: given a certain set of parameters) return the expected result.

# Why does this exist?

We use this in our basic object oriented programming classes C# (at the AP University Collega, informatics bachelor degree). In the first labs, student need to learn to write classes that adhere to some basic requirements (correct properties, naming, etc). This library allows the lecture to write unit tests the student can then run to check if his class meets the minimal requirements.

Imagine the student needs to write a class with the following requirements:

## Example 1:Full property

* It has a full property called ``Diameter`` (type ``int``) with a backingfield called ``diameter``. (if the backing field has the same name as the full property, but starts with small char, no additional info needs to be passed to the method).
* It has a full property called ``Toppings`` (type ``string``) and it uses a backingfield called ``tops``.


```cs       
tester.CheckFullProperty("Diameter", typeof(int));
tester.CheckFullProperty("Toppings", typeof(string),"tops");

//Test if the backing field gets its value from the property (second parameter)
tester.TestBackingFieldProp("Toppings, "tops", "pineapple", "pineapple");
```

## Example 2: Autoproperty

* It has an autoproperty called ``IsPizza`` of type ``bool``.

```cs
tester.CheckAutoProperty("IsPizza", typeof(bool));
```

## Example 3 : Full property with check in setter

* It has a full property called ``Price``, with a backing field ``price``. Type = ``double``.
* Only positive value can be set. If a negative value is set, nothing happens and the property retains its current value.

```cs
string priceProp = "Price";
if(tester.CheckFullProperty(priceProp, typeof(double)))
{
    double starttop = 66.68;
    
    //test if we can save a positieve number. We expect the same number if we call the get.
    tester.TestPropGetSet(priceProp, starttop, starttop); 
    
    //if we try to set a negative number, we expect the previous value (startop) to be returned after calling the get
    tester.TestPropGetSet(priceProp, -1.0, starttop); 
}
```

## Example 4: Methods

* The class has a method called ``Compute`` that should return an ``int`` and expects two paramers of type ``int`` and ``string``.
* Running the method with parameters ``3`` and ``hello`` should return the value ``5``.
* The method should print the second parameter to the console.

```cs
if (tester.CheckMethod("Compute", typeof(int), new Type[] { typeof(int), typeof(string) }))
{
    var arguments = new object[] { 3 , "hello" };
    string consoleOutput = tester.TestMethod("Compute", arguments, 5);
    Assert.AreEquals("hello",consoleOutput);
}
```