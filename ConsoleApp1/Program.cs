using System.Collections;
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
- Add relation(s) to source documents
- Add NumberOfSections to a document
- Write write outgoing relations in section 1 of the source document.
- zip and send documents to Fundament
- Build document family
- Build DynamicRelations to target family
- Be able to indicate if static relations should point to a specific bookmark/whole document or if the target of the relation should be randomized within the target document.
*/

void GoDoIt()
{
    var targetDocumentInfos = BuildTargetDocumentInfo(specification.TargetDocuments);
    var sourceDocumentInfos = BuildSourceDocumentInfo(specification.SourceDocuments);
    var staticRelationInfos = BuildManyToOneStaticRelations(specification.ManyStaticRelationToOne, sourceDocumentInfos, targetDocumentInfos.First());

    

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


IEnumerable<StaticRelationInfo> BuildManyToOneStaticRelations(ManyStaticRelationToOne specification, IEnumerable<SourceDocumentInfo> sourceDocumentInfos, TargetDocumentInfo targetDocumentInfo)
{
    if (specification.Generate == false) yield break;

    int i = 0;
    
    foreach (SourceDocumentInfo documentInfo in sourceDocumentInfos)
    {
        yield return new StaticRelationInfo(i, documentInfo, targetDocumentInfo, specification.RelationTypeCode);
        i++;
    }
}


IReadOnlyList<SourceDocumentInfo> BuildSourceDocumentInfo(SourceDocuments sourceDocumentSpecification)
{
    List<SourceDocumentInfo> result = new List<SourceDocumentInfo>((int)sourceDocumentSpecification.Count);
    foreach (int i in sourceDocumentSpecification.Count.Range1())
    {
        var documentInfo = new SourceDocumentInfo(i, $"Source document No. {i} of {sourceDocumentSpecification.Count}", BuildSectionsInfo(sourceDocumentSpecification));
        result.Add(documentInfo);
    }
    return result;
}


IEnumerable<TargetDocumentInfo> BuildTargetDocumentInfo(TargetDocuments targetDocumentSpecification)
{
    foreach (int i in targetDocumentSpecification.Count.Range1())
    {
        var documentInfo = new TargetDocumentInfo(i, $"Target document No. {i} of {targetDocumentSpecification.Count}", BuildSectionsInfo(targetDocumentSpecification));
        yield return documentInfo;
    }
}

IEnumerable<SectionInfo> BuildSectionsInfo(Documents specification)
{
    foreach (int i in specification.NumberOfSections.Range1())
    {
        yield return new SectionInfo($"BM{i}");
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


