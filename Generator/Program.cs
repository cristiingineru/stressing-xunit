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
            var factCount = 300;
            var file = @"..\..\..\WithGeneratedContent\Generated.cs";

            var fileContent = Generate(factCount);

            File.Delete(file);
            File.WriteAllText(file, fileContent);
        }

        private static string Generate(int factCount)
        {
            var facts = Enumerable
                .Range(0, factCount)
                .Select(GenerateFact)
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

        private static string GenerateFact(int id)
        {
            return @"
        [Fact]
        public void PassingTest" + id + @"()
        {
            Assert.True(true);
        }";
        }
    }
}
