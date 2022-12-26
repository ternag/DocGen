using ConsoleApp1.A.ParseSpecification;

namespace ConsoleApp1.B.BuildModel;

public record SectionInfo(string Id, TmpRelationsInfo RelationsInfo);

public abstract record DocumentInfo(int Id, string Title, IEnumerable<SectionInfo> Sections, string Fullname);

public record TargetDocumentInfo(int Id, string Title, string Fullname, IEnumerable<SectionInfo> Sections, IReadOnlyList<RelationSpec> RelationsSpec, TmpRelationsInfo RelationsInfo) 
    : DocumentInfo(Id, Title, Sections, $"{Fullname}-{Id}");

public record StaticRelationInfo(TargetDocumentInfo TargetDocument, string RelationTypeCode, string SourceBookmark = "", string TargetBookmark = "");

public record TmpRelationsInfo(int RangedTargetCount, int SingleTargetCount);

public record SourceDocumentInfo(int Id, string Title, string Fullname, IEnumerable<SectionInfo> Sections, Relations Relations) : DocumentInfo(Id, Title, Sections,  $"{Fullname}-{Id}");

public class Relations
{
    public List<StaticRelationInfo> StaticRelations { get; } = new();
}