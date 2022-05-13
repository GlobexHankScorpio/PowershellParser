using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using UnitTests.Classes;

namespace UnitTests
{
    [TestClass]
    public class UnitTestExecutePowershell
    {
        [TestMethod]
        public void CreateFolder()
        {
            UnitTestFolder folder = new UnitTestFolder
            {
                folderPath = @"C:\temp\Test Folder"
            };

            bool result = PowershellParser.PowershellParser.ExecuteScript(folder, $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\Tests scripts\\Create folder.ps1");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateFile()
        {
            UnitTestFile folder = new UnitTestFile
            {
                filePath = @"C:\temp\Test Folder\test.txt"
            };

            bool result = PowershellParser.PowershellParser.ExecuteScript(folder, $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\Tests scripts\\Create file.ps1");
            Assert.IsTrue(result);
        }
    }
}
