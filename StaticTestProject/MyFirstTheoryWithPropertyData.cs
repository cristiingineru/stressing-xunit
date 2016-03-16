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

        private static List<object[]> _splitCountDataValues = null;

        public static IEnumerable<object[]> SplitCountData
        {
            get
            {
                if (_splitCountDataValues == null)
                {
                    _splitCountDataValues = Enumerable
                        .Range(0, 5000)
                        .Select(i => new object[] { i.ToString(), 1 })
                        .ToList();
                }
                return _splitCountDataValues;
            }
        }
    }
}
