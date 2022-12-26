using System.Xml.Linq;
using AutoFixture;
using ConsoleApp1;
using ConsoleApp1.A.ParseSpecification;
using ConsoleApp1.B.BuildModel;
using ConsoleApp1.C.CreateDocuments;
using FluentAssertions;
using Xunit.Abstractions;

namespace TestProject1;

public class XmlTests
{
    private readonly ITestOutputHelper _output;

    public XmlTests(ITestOutputHelper output)
    {
        _output = output;
    }

    //[Fact(Skip = "no assert - only output")]
    [Fact]
    public void Metadata()
    {
        XDocumentCreator sut = new XDocumentCreator();

        IFixture fixture = new Fixture();

        var sourceDocument = fixture.Create<SourceDocumentInfo>();
        var targetDocument = fixture.Create<TargetDocumentInfo>();

        var staticRelationInfo = new StaticRelationInfo(
            targetDocument,
            "DIREKTE",
            "sourceBM",
            "targetBM");

        var metadata = sut.CreateMetadata(sourceDocument);

        metadata.AddStaticRelation(staticRelationInfo);

        _output.WriteLine(metadata.ToString());
    }


    /// <summary>
    /// can be removed if it is annoying
    /// </summary>
    [Fact]
    public void GivenRelationSpec_CountIsCorrect()
    {
        RelationSpec[] relations = {
            new(Count: 2)
            ,new(Count: 10)
            ,new(Count: 3, RelationKind.SingleTarget)
            ,new(Count: 10, RelationKind.SingleTarget)
            ,new(Count: 4, RelationKind.RangedTarget)
            ,new(Count: 10, RelationKind.RangedTarget)
        };
        
        var actual = XDocumentCreator.GetRelationStats(relations, XNamespace.None, "test");

        actual.Value.Should().Contain("Static 12");
        actual.Value.Should().Contain("Single target 13");
        actual.Value.Should().Contain("Ranged target 14");
        
        _output.WriteLine(actual.ToString());
    }
}