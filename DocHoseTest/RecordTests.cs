using System.Text.Json;
using DocHose.A.ParseSpecification;

namespace DocHoseTest;

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

    [Fact]
    public void GivenDefaultSourceDocument_DefaultValuesShouldBeCorrect()
    {
        var sut = new SourceDocumentsSpec();

        sut.SectionSpecs.Should().NotBeNull();
        sut.SectionSpecs.Count.Should().Be(1);
        sut.SectionSpecs[0].Count.Should().Be(1);
    }

    [Fact]
    public void GivenDefaultTargetDocumentSpec_DefaultValuesShouldBeCorrect()
    {
        var sut = new TargetDocumentSpec();

        // Sections
        sut.SectionSpecs.Should().NotBeNull();
        sut.SectionSpecs.Count.Should().Be(1);
        sut.SectionSpecs[0].Count.Should().Be(1);
        
        // Relations
        sut.Relations.Should().NotBeNull();
        sut.Relations.Count.Should().Be(1);
        sut.Relations[0].RelationKind.Should().Be(RelationKind.Static);
        sut.Relations[0].RelationTypeCode.Should().Be("DIREKTE");
        sut.Relations[0].Count.Should().Be(1);
    }

}