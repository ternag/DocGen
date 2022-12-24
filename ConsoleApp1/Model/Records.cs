
using ConsoleApp1;

public record SectionInfo(string Id, TmpRelationsInfo relationsInfo);

public abstract record DocumentInfo(int Id, string Title, IEnumerable<SectionInfo> Sections)
{
    public abstract string Fullname { get; }
}

public record TargetDocumentInfo(int Id, string Title, IEnumerable<SectionInfo> Sections, IReadOnlyList<RelationSpec> relationsSpec, TmpRelationsInfo relationsInfo) : DocumentInfo(Id, Title, Sections)
{
    public override string Fullname => $"TargetDocument-{Id}";
}

public record StaticRelationInfo(int Id, SourceDocumentInfo SourceDocument, TargetDocumentInfo TargetDocument, string RelationTypeCode, string SourceBookmark = "", string TargetBookmark = "");

public record TmpRelationsInfo(int RangedTargetCount, int SingleTargetCount);


public record SourceDocumentInfo(int Id, string Title, IEnumerable<SectionInfo> Sections, Relations Relations) : DocumentInfo(Id, Title, Sections)
{
    public override string Fullname => $"SourceDocument-{Id}";
}

public class Relations
{
    public List<StaticRelationInfo> StaticRelations { get; init; } = new List<StaticRelationInfo>();
}
