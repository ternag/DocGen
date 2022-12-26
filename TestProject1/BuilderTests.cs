using ConsoleApp1;
using ConsoleApp1.A.ParseSpecification;
using ConsoleApp1.B.BuildModel;
using FluentAssertions;
using Xunit.Abstractions;

namespace TestProject1
{
    public class BuilderTests
    {
        private readonly ITestOutputHelper _output;

        public BuilderTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void GivenSectionSpecification_ReturnsExpectedNumberOfSections()
        {
            var testData = new List<SectionSpec> { new() { Count = 1 }, new() { Count = 6 }, new() { Count = 110 } };
            IEnumerable<SectionModel> sectionInfos = Builder.BuildSourceSectionsInfo(testData);

            sectionInfos.Count().Should().Be(117);
        }
    }
}
