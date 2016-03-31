using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StaticTestProject
{
    public class MySlowTheory
    {
        [Theory]
        [MemberData("TestValuesOfMySlowTheory")]
        public void SplitCount(string val1, bool val2)
        {
            Assert.True(true);
        }

        public static IEnumerable<object[]> TestValuesOfMySlowTheory
        {
            get
            {
                var _testValues = Enumerable
                        .Range(0, 3)
                        .Select(i => new object[] { i.ToString(), true })
                        .ToList();
                System.IO.File.AppendAllText("TestValuesCalls.txt", System.DateTime.Now.ToLongTimeString() + " -> about to Thread.Sleep(something) and then return the test values" + System.Environment.NewLine);
                Thread.Sleep(30000);
                return _testValues;
            }
        }
    }
}
