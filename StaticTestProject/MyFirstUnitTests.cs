using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StaticTestProject
{
    public class MyFirstUnitTests
    {
        [Fact]
        public void PassingTest()
        {
            Assert.True(true);
        }
    }
}
