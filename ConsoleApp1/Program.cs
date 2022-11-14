﻿using System.Collections;
using System.Text.Json;
using System.Xml.Linq;
using ConsoleApp1;
using static System.Collections.Specialized.BitVector32;

string specificationText = File.ReadAllText("./specification.json");

Specification? specification = JsonSerializer.Deserialize<Specification>(specificationText);
if (specification == null) throw new NullReferenceException($"{nameof(specification)} is null");

string outputDir = "/temp/GenerateDocPoc/output";
Directory.CreateDirectory(outputDir);

var generator = new XDocumentCreator();

GoDoIt();

/* TODO
- Fill in blanks in metadata file.
- Add NumberOfSections to a document
- Write write outgoing relations in section 1 of the source document.
- zip and send documents to Fundament
- Build document family
- Build DynamicRelations to target family
- Be able to indicate if static relations should point to a specific bookmark/whole document or if the target of the relation should be randomized within the target document.
*/

void GoDoIt()
{
    var targetDocumentInfos = Builder.BuildTargetDocumentInfo(specification.TargetDocuments);
    var sourceDocumentInfos = Builder.BuildSourceDocumentInfo(specification.SourceDocuments);
    var staticRelationInfos = Builder.BuildManyToOneStaticRelations(specification.ManyStaticRelationToOne, sourceDocumentInfos, targetDocumentInfos.First());

    foreach (var relation in staticRelationInfos)
    {
        var documentXml = generator.CreateDocument(relation.SourceDocument);
        var metadataXml = generator.CreateMetadata(relation.SourceDocument);
        metadataXml.AddStaticRelation(relation);
        SaveDocument(documentXml, metadataXml, relation.SourceDocument);
    }

    foreach (var doc in targetDocumentInfos)
    {
        var documentXml = generator.CreateDocument(doc);
        var metadataXml = generator.CreateMetadata(doc);
        SaveDocument(documentXml, metadataXml, doc);
    }

}




void SaveDocument(XDocument document, XDocument metadata, DocumentInfo info)
{
    string documentDir = Path.Combine(outputDir, $"{info.Fullname}");
    Directory.CreateDirectory(documentDir);
    document.Save(Path.Combine(documentDir, $"document.xml"));
    metadata.Save(Path.Combine(documentDir, $"metadata.xml"));
}
public record SectionInfo(string Id);

public abstract record DocumentInfo(int Id, string Title, IEnumerable<SectionInfo> Sections)
{
    public abstract string Fullname { get; }
}

public record SourceDocumentInfo(int Id, string Title, IEnumerable<SectionInfo> Sections) : DocumentInfo(Id, Title, Sections)
{
    public override string Fullname => $"SourceDocument-{Id}";
}
public record TargetDocumentInfo(int Id, string Title, IEnumerable<SectionInfo> Sections) : DocumentInfo(Id, Title, Sections)
{
    public override string Fullname => $"TargetDocument-{Id}";
}

public record StaticRelationInfo(int Id, SourceDocumentInfo SourceDocument, TargetDocumentInfo TargetDocument, string RelationTypeCode, string SourceBookmark = "", string TargetBookmark = "");