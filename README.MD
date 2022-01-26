Probably walking down an a well throden path, but still: let me do this :)

The OOPClassBasicsTesterLibrary contains a ...library (!) to use wile writing unit tests to check for basic class making skills. For example, it allows you to check if the user created the correct properties and following the correct naming, returntype, and whether the get/set combo works as expected.

(Again, I'm pretty sure things like Roslyn will also do this)

Usage:

1. Add the OOPClassBacisTesterLibrary (dll) to your unit test project
2. Make sure an empty class with the correct name already exists in the project to test 
3. In your Unit test class make sure to create the analyzer and give it an instant of the class to test (static classes/methods cant be tested...yet ) 
``TimsEpicClassAnalyzer tester = new TimsEpicClassAnalyzer(new StudentClassToMake());``
4. Start creating tests 

Current methods:

* **CheckFullProperty**
* **CheckAutoProperty**
* **TestPropGetSet**: test if the get and set work by checking of the expected value is return from the get after setting it.