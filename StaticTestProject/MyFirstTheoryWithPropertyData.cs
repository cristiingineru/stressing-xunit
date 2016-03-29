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
        [Theory]
        [MemberData("TestValues")]
        public void SplitCount(string val1, bool val2)
        {
            Assert.True(true);
        }

        private static List<object[]> _testValues = null;

        public static IEnumerable<object[]> TestValues
        {
            get
            {
                if (_testValues == null)
                {
                    _testValues = Enumerable
                        .Range(0, 3)
                        .Select(i => new object[] { i.ToString(), true })
                        .ToList();
                }
                System.IO.File.AppendAllText("TestValuesCalls.txt", System.DateTime.Now.ToShortTimeString() + System.Environment.NewLine);
                return _testValues;
            }
        }
    }
}
