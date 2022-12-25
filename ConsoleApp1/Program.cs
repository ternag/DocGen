using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using ConsoleApp1;
using ConsoleApp1.A.ParseSpecification;
using ConsoleApp1.B.BuildModel;
using ConsoleApp1.C.CreateDocuments;

string specificationText = File.ReadAllText("./specification.json");
Specification? specification = SpecificationLoader.Parse(specificationText);

string outputDir = "/temp/GenerateDocPoc/output";
Directory.CreateDirectory(outputDir);

GoDoIt();

/* To do:
X Add static relation(s) to source documents
- Fill in blanks in metadata file.
- Add NumberOfSections to a document
- Write outgoing relations in section 1 of the source document.
- zip and send documents to Fundament
- Build document family
- Build DynamicRelations to target family
- Be able to indicate if static relations should point to a specific bookmark/whole document or if the target of the relation should be randomized within the target document.
*/

void GoDoIt()
{
    var generator = new XDocumentCreator();

    var targetDocumentInfos = Builder.BuildTargetDocuments(specification.TargetDocuments).ToList();
    var sourceDocumentInfos = Builder.BuildSourceDocuments(specification.SourceDocuments);

    StaticRelationBuilder.Create(targetDocumentInfos, sourceDocumentInfos);

    foreach (SourceDocumentInfo doc in sourceDocumentInfos)
    {
        var documentXml = generator.CreateSourceDocuments(doc);
        var metadataXml = generator.CreateMetadata(doc);
        foreach (var relation in doc.Relations.StaticRelations)
        {
            metadataXml.AddStaticRelation(relation);
        }

        SaveDocument(documentXml, metadataXml, doc);
    }

    foreach (var doc in targetDocumentInfos)
    {
        var documentXml = generator.CreateTargetDocuments(doc);
        var metadataXml = generator.CreateMetadata(doc);
        SaveDocument(documentXml, metadataXml, doc);
    }

}

void SaveDocument(XDocument document, XDocument metadata, DocumentInfo info)
{
    string documentDir = Path.Combine(outputDir, $"{info.Fullname}-{info.Id}");
    Directory.CreateDirectory(documentDir);
    document.Save(Path.Combine(documentDir, $"document.xml"));
    metadata.Save(Path.Combine(documentDir, $"metadata.xml"));
}