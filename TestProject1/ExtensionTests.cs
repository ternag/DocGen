using ConsoleApp1;
using ConsoleApp1.C.CreateDocuments;
using FluentAssertions;
using Xunit.Abstractions;

namespace TestProject1;

public class ExtensionTests
{
    private readonly ITestOutputHelper _output;

    public ExtensionTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Range1_ContainsCorrectRange()
    {
        var enumerable = 5.Range1();
        enumerable.Should().Equal(1, 2, 3, 4, 5);
    }

    [Fact]
    public void Range0_ContainsCorrectRange()
    {
        var enumerable = 5.Range0();
        enumerable.Should().Equal(0, 1, 2, 3, 4);
    }
}