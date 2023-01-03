using System.Xml.Linq;
using ConsoleApp1.A.ParseSpecification;
using ConsoleApp1.B.BuildModel;
using ConsoleApp1.C.CreateDocuments;

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

        var sourceDocument = fixture.Create<SourceDocumentModel>();
        var targetDocument = fixture.Create<TargetDocumentModel>();

        var staticRelationInfo = new StaticRelationModel(
            targetDocument.Fullname,
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
        
        var actual = XmlCreate.RelationStats(relations, "test", XNamespace.None);

        actual.Value.Should().Contain("Static 12");
        actual.Value.Should().Contain("Single target 13");
        actual.Value.Should().Contain("Ranged target 14");
        
        _output.WriteLine(actual.ToString());
    }
}
