using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using ConsoleApp1;

string specificationText = File.ReadAllText("./specification.json");

Specification? specification = JsonSerializer.Deserialize<Specification>(specificationText);
if (specification == null) throw new NullReferenceException($"{nameof(specification)} is null");

string outputDir = "/temp/GenerateDocPoc/output";
Directory.CreateDirectory(outputDir);

var generator = new XDocumentCreator();

GoDoIt();

/* TODO
- Fill in blanks in metadata file.
- Add relation(s) to source documents
- Add NumberOfSections to a document
- Write outgoing relations in section 1 of the source document.
- zip and send documents to Fundament
- Build document family
- Build DynamicRelations to target family
- Be able to indicate if static relations should point to a specific bookmark/whole document or if the target of the relation should be randomized within the target document.
*/

void GoDoIt()
{
    //specification = SetDefaults(specification);
    var targetDocumentInfos = Builder.BuildTargetDocuments(specification.TargetDocuments);
    var sourceDocumentInfos = Builder.BuildSourceDocuments(specification.SourceDocuments);
    //var sourceDocumentWithStaticRelationsInfos = BuildManyToOneStaticRelations(specification.ManyStaticRelationToOne, sourceDocumentInfos, targetDocumentInfos.First());

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

Specification SetDefaults(Specification specification)
{
    return specification with 
    {
        SourceDocuments = specification.SourceDocuments ?? new SourceDocuments(),
        TargetDocuments = specification.TargetDocuments ?? Array.Empty<TargetDocumentSpec>(),
    };
}

void SaveDocument(XDocument document, XDocument metadata, DocumentInfo info)
{
    string documentDir = Path.Combine(outputDir, $"{info.Fullname}");
    Directory.CreateDirectory(documentDir);
    document.Save(Path.Combine(documentDir, $"document.xml"));
    metadata.Save(Path.Combine(documentDir, $"metadata.xml"));
}