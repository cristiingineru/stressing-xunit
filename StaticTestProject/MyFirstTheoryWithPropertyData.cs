using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace StaticTestProject
{
    public class MyFirstTheoryWithPropertyData
    {
        [Theory, MemberData("SplitCountData")]
        public void SplitCount(string input, int expectedCount)
        {
            var actualCount = input.Split(' ').Count();
            Assert.Equal(expectedCount, actualCount);
        }

        public static IEnumerable<object[]> SplitCountData
        {
            get
            {
                return Enumerable
                    .Range(0, 10000)
                    .Select(i => new object[] { i.ToString(), 1 });
            }
        }
    }
}
