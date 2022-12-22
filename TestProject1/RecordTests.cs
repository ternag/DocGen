using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
            
            TargetDocument? target = JsonSerializer.Deserialize<TargetDocument>(json);

            target.Should().NotBeNull();
            target?.Count.Should().Be(1);
        }
    }
}
