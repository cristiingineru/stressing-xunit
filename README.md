# Stressing xUnit

I was curious to see how xUnit behaves under stressing conditions so I generated some benchmarks to compare it with mstest.
 
Most of the benchmarks were generated and run on my personal laptop, but soon I realized the memory is a serious bottle neck so in order to have conclusive results some of the benchmarks were (re)run on a more powerfull machine. Each test is one line long containing a single Assert.IsTrue(true) or Assert.True(true) depending on which framework is being benchmarked. There is no actual production code being tested or instrumented.
 
Here are some examples on how the generated tests look like:
* [[Fact]](../blob/facts/WithGeneratedContent/Generated.cs)
* [[TestMethod]](../blob/testmethods/WithGeneratedContent/Generated.cs)
* [[Theory, InlineData()]](../blob/theories-with-inlinedata/WithGeneratedContent/Generated.cs)
* [[Theory, MemberData()]](../blob/theories-with-memberdata/WithGeneratedContent/Generated.cs)
 
### The versions used to run these benchmarks
* Visual Studio 2015 14.0.23107.0
* xUnit 2.1.0
* xUnit runner console and Visual Studio 2.1.0
* ReSharper Ultimate 10.0.2
* xUnit.net Test Support for ReSharper 10 2.3.4
 
### Number of tests used for stressing
20000
 
 
### [TestMethod] vs. [Fact]
 
**[TestMethod]s** with **VS Test Explorer** is the best combination in terms of time to run a random test, and memory usage (300-500 MB). The discovery is triggered only when a build occurs and the runner is always responsive.
 
**[TestMethod]s** with **R# Unit Test Explorer** is just a little bit slower in terms of discovery, but close to the previous combination regarding the time to run a random test. For some reason the time required to run all the tests is rapidly degrading with their count. When dealing with lots of tests sometimes VS hangs.
 
**[Fact]s** with **R# Unit Test Explorer** is a fast combination in terms of discovery, but slower at running a random test because a new discovery is triggered each time. The memory consumption is considerably higher (700-1000 MB) and most of the time the runner fails to run all the tests.
 
**[Fact]s** with **VS Test Explorer** is a slow combination, but it's not hanging/crashing.
 
 
### [Theory]
 
**[Theory]s** with **R# Unit Test Explorer**: discovery is super fast but incomplete: a complete run attempt is required to populate the full test case list. A new complete discovery is being performed each time a random test starts and all MemberData are being re-evaluated to get the test case list. The refresh button doesn't work so when the MemberData is changed a new run has to be initiated to update the test case list. This combination is using a lot of memory (1000-2000 MB).
 
**[Theory]s** with **VS Test Explorer** is slow most of the time. Sometimes before running a random test even 2-3 minutes pass, then a new discovery is being performed. Less than 1000 MB of memory are being used by this combination.
 
 
## Conclusions
 
When dealing with a small test project, it doesn't matter that much which framework or runner is used. While going big **R# Unit Test Explorer** tend to use a lot of memory. **[Fact]s** are a clean way of dealing with enumerations, but they come at a price: (1) discovery is being performed each time a test run is initiated and (2) extensive memory usage.
 
 
## Conclusions (part 2)
 
There is an xUnit setting that can be used to prevent rediscovering all **[Theory]s** when running a single one or a **[Fact]**: [xunit.preEnumerateTheories](http://xunit.github.io/docs/configuring-with-xml.html).
 
While using **R# Unit Test Explorer** this setting is speeding up the start time by preventing useless discoveries. The tests are displayed as before, but all the test cases from the same **[Theory]** are run by xUnit even though a single one is requested to run from the UI. When test cases part of multiple **[Theory]s** are triggered to run then the properties spawning the enumerations of those **[Theory]s** are being run in parallel, so they should be thread safe.
 
While using **VS Test Explorer** this setting is making discovery faster, but the test cases to be grouped as a single test. However the results are reported individually for each test case.
 

--- 
Final conclusion... When dealing with a large test suite of xUnit **[Fact]s** then **R# Unit Test Explorer** has to be used... as long as the memory allows it. If the tests take a lot of time to complete then make sure all the test case enumerations are super fast so preEnumerateTheories is not needed.
