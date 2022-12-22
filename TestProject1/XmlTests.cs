using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using Xunit.Abstractions;

namespace TestProject1
{
    public class XmlTests
    {
        private readonly ITestOutputHelper _output;

        public XmlTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Metadata()
        {
            XDocumentCreator sut = new XDocumentCreator();

            var sourceDocument = new SourceDocumentInfo(1, "test", Array.Empty<SectionInfo>());
            var targetDocument = new TargetDocumentInfo(5, "Target5", new List<SectionInfo>(), new RelationsInfo(0,0,0));

            var staticRelationInfo = new StaticRelationInfo(12,
                sourceDocument,
                targetDocument,
                "DIREKTE",
                "sourceBM",
                "targetBM");

            var metadata = sut.CreateMetadata(sourceDocument);

            metadata.AddStaticRelation(staticRelationInfo);

            _output.WriteLine(metadata.ToString());
        }
    }
}
