# PowershellParser
0.1 Version
-----------
Small dll project to parse, call and execute, from C#, a powershell that uses parameters.

To use it, just create and instanciate your personal C# class and then call the PowershellParser.ExecuteScript() with the object and the powershell path.

Examples can be found in the Unit Test project.

Class definition
----------------
class UnitTestFolder
{
    public string folderPath { get; set; }
}


Usage
-----

UnitTestFolder folder = new UnitTestFolder
{
    folderPath = @"C:\temp\Test Folder"
};

bool result = PowershellParser.ExecuteScript(folder, "YourPowershellPath.ps1");

How it works
------------
The function ExecuteScript accepts as first parameter any object type and will parse for you everything. The class properties will be parsed as the Powershell params, as well the values of the object sent.

Then, the powershell will be executed for you. An execution result "false" means that something failed.

Requierements
-------------
- Only one, it is mandatory that all the class properties are the exactly the same name in the Powershell and the Class definition.
