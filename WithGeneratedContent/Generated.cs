
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
        [Theory]
        [MemberData("TestValues")]
        public void PassingTest0(string val1, bool val2)
        {
            Assert.True(true);
        }
        [Theory]
        [MemberData("TestValues")]
        public void PassingTest1(string val1, bool val2)
        {
            Assert.True(true);
        }
        [Theory]
        [MemberData("TestValues")]
        public void PassingTest2(string val1, bool val2)
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
                        .Range(0, 5)
                        .Select(i => new object[] { i.ToString(), true })
                        .ToList();
                }
                return _testValues;
            }
        }
    }
}