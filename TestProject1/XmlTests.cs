using AutoFixture;
using ConsoleApp1;
using ConsoleApp1.B.BuildModel;
using ConsoleApp1.C.CreateDocuments;
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
}