using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using ConsoleApp1;
using ConsoleApp1.A.ParseSpecification;
using ConsoleApp1.B.BuildModel;
using ConsoleApp1.C.CreateDocuments;

string specificationText = File.ReadAllText("./specification.json");
Specification specification = SpecificationLoader.Parse(specificationText);

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

    var targetDocumentModels = Builder.BuildTargetDocuments(specification.TargetDocuments).ToList();
    var sourceDocumentModels = Builder.BuildSourceDocuments(specification.SourceDocuments);

    StaticRelationBuilder.Create(targetDocumentModels, sourceDocumentModels);

    foreach (var sourceDocumentModel in sourceDocumentModels)
    {
        var documentXml = generator.CreateSourceDocuments(sourceDocumentModel);
        var metadataXml = generator.CreateMetadata(sourceDocumentModel);
        metadataXml.AddStaticRelations(sourceDocumentModel.Relations.StaticRelations);
        SaveDocument(documentXml, metadataXml, sourceDocumentModel.Fullname);
    }

    foreach (var targetDocumentModel in targetDocumentModels)
    {
        var documentXml = generator.CreateTargetDocuments(targetDocumentModel);
        var metadataXml = generator.CreateMetadata(targetDocumentModel);
        SaveDocument(documentXml, metadataXml, targetDocumentModel.Fullname);
    }

}

void SaveDocument(XDocument document, XDocument metadata, string fullname)
{
    string documentDir = Path.Combine(outputDir, fullname);
    Directory.CreateDirectory(documentDir);
    document.Save(Path.Combine(documentDir, $"document.xml"));
    metadata.Save(Path.Combine(documentDir, $"metadata.xml"));
}