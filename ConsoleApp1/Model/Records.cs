
using ConsoleApp1;

public record SectionInfo(string Id, TmpRelationsInfo RelationsInfo);

public abstract record DocumentInfo(int Id, string Title, IEnumerable<SectionInfo> Sections, string Fullname);

public record TargetDocumentInfo(int Id,
    string Title,
    string Fullname,
    IEnumerable<SectionInfo> Sections,
    IReadOnlyList<RelationSpec> RelationsSpec,
    TmpRelationsInfo RelationsInfo) : DocumentInfo(Id,
    Title,
    Sections,
    Fullname);

public record StaticRelationInfo(int Id,
    TargetDocumentInfo TargetDocument,
    string RelationTypeCode,
    string SourceBookmark = "",
    string TargetBookmark = "");

public record TmpRelationsInfo(int RangedTargetCount, int SingleTargetCount);

public record SourceDocumentInfo(int Id,
    string Title,
    string Fullname,
    IEnumerable<SectionInfo> Sections,
    Relations Relations) : DocumentInfo(Id,
    Title,
    Sections,
    Fullname);

public class Relations
{
    public List<StaticRelationInfo> StaticRelations { get; init; } = new List<StaticRelationInfo>();
}
