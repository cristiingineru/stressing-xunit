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
            var theoryCount = 3;
            var memberDataCountForEachTheory = 5;
            var file = @"..\..\..\WithGeneratedContent\Generated.cs";

            var fileContent = Generate(theoryCount, memberDataCountForEachTheory);

            File.Delete(file);
            File.WriteAllText(file, fileContent);
        }

        private static string Generate(int theoryCount, int memberDataCountForEachTheory)
        {
            var facts = Enumerable
                .Range(0, theoryCount)
                .Select(GenerateTheory)
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

        private static List<object[]> _testValues = null;

        public static IEnumerable<object[]> TestValues
        {
            get
            {
                if (_testValues == null)
                {
                    _testValues = Enumerable
                        .Range(0, " + memberDataCountForEachTheory + @")
                        .Select(i => new object[] { i.ToString(), true })
                        .ToList();
                }
                return _testValues;
            }
        }
    }
}";
        }

        private static string GenerateTheory(int id)
        {
            return @"
        [Theory]
        [MemberData(""TestValues"")]
        public void PassingTest" + id + @"(string val1, bool val2)
        {
            Assert.True(true);
        }";
        }
    }
}
