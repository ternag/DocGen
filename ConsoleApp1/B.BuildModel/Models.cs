using ConsoleApp1.A.ParseSpecification;

namespace ConsoleApp1.B.BuildModel;

public abstract record SectionInfo(string Id);

public record TargetSectionInfo(string Id, IReadOnlyList<RelationSpec> RelationsSpec):SectionInfo(Id);

public record SourceSectionInfo(string Id, Relations Relations):SectionInfo(Id);

public abstract record DocumentInfo(int Id, string Title, string Fullname);

public record TargetDocumentInfo(int Id, string Title, string Fullname, IEnumerable<TargetSectionInfo> Sections, IReadOnlyList<RelationSpec> RelationsSpec) : DocumentInfo(Id, Title, $"{Fullname}-{Id}");

public record SourceDocumentInfo(int Id, string Title, string Fullname, IEnumerable<SourceSectionInfo> Sections, Relations Relations) : DocumentInfo(Id, Title, $"{Fullname}-{Id}");

public class Relations
{
    public List<StaticRelationInfo> StaticRelations { get; } = new();
}

public record StaticRelationInfo(TargetDocumentInfo TargetDocument, string RelationTypeCode, string SourceBookmark = "", string TargetBookmark = "");
