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
            var theoryCount = 100;
            var inlineDataCountForEachTheory = 5;
            var file = @"..\..\..\WithGeneratedContent\Generated.cs";

            var fileContent = Generate(theoryCount, inlineDataCountForEachTheory);

            File.Delete(file);
            File.WriteAllText(file, fileContent);
        }

        private static string Generate(int theoryCount, int inlineDataCountForEachTheory)
        {
            var facts = Enumerable
                .Range(0, theoryCount)
                .Select(i => GenerateTheory(i, inlineDataCountForEachTheory))
                .Aggregate(string.Empty, string.Concat);

            return @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StaticTestProject
{
    public class Generated
    {" + facts + @"
    }
}";
        }

        private static string GenerateTheory(int id, int inlineDataCountForEachTheory)
        {
            var inlineDatas = Enumerable
                .Range(0, inlineDataCountForEachTheory)
                .Select(i => Environment.NewLine + "        " + "[InlineData(" + i + ")]")
                .Aggregate(string.Concat);

            return @"
        [Theory]" + inlineDatas + @"
        public void PassingTest" + id + @"(int value)
        {
            Assert.True(true);
        }";
        }
    }
}
