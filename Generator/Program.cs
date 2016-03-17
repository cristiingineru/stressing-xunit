using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var factCount = 3;
            var file = @"..\..\..\WithGeneratedContent\Generated.cs";

            var fileContent = Generate(factCount);

            File.Delete(file);
            File.WriteAllText(file, fileContent);
        }

        private static string Generate(int factCount)
        {
            var testMethods = Enumerable
                .Range(0, factCount)
                .Select(GenerateTestMethod)
                .Aggregate(string.Empty, string.Concat);

            return @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StaticTestProject
{
    [TestClass]
    public class Generated
    {" + testMethods + @"
    }
}";
        }

        private static string GenerateTestMethod(int id)
        {
            return @"
        [TestMethod]
        public void PassingTest" + id + @"()
        {
            Assert.IsTrue(true);
        }";
        }
    }
}
