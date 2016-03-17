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
            var file = @"..\..\..\WithGeneratedContent\Generated.cs";
            var fileContent = Generate();
            File.Delete(file);
            File.WriteAllText(file, fileContent);
        }

        private static string Generate()
        {
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
    {
        [Fact]
        public void PassingTest()
        {
            Assert.True(true);
        }
    }
}";
        }
    }
}
