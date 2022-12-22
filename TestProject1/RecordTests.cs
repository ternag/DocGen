using ConsoleApp1;
using System.Text.Json;
using FluentAssertions;
using Xunit.Abstractions;

namespace TestProject1
{
    public class RecordTests
    {
        private readonly ITestOutputHelper _output;

        public RecordTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void AlternativeConstructor()
        {
            string json = @"{
              ""Name"": ""Target document {x}"",
              ""NumberOfRelations"": 6000,
              ""Sections"": [
                {
                  ""Name"": ""First Section"",
                  ""Bookmark"": ""fb"",
                  ""NumberOfRelations"": 10
                }
              ]
            }";
            
            TargetDocumentSpec? target = JsonSerializer.Deserialize<TargetDocumentSpec>(json);

            target.Should().NotBeNull();
            target?.Count.Should().Be(1);
        }


    }
}
