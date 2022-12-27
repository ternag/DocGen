using ConsoleApp1.A.ParseSpecification;

namespace ConsoleApp1.B.BuildModel;

public abstract record SectionModel(string Id);

public record TargetSectionModel(string Id, IReadOnlyList<RelationSpec> RelationsSpec):SectionModel(Id);

public record SourceSectionModel(string Id, Relations Relations):SectionModel(Id);

public abstract record DocumentModel(int Id, string Title, string Fullname);

public record TargetDocumentModel(int Id, string Title, string Fullname, IEnumerable<TargetSectionModel> Sections, IReadOnlyList<RelationSpec> RelationsSpec) : DocumentModel(Id, Title, $"{Fullname}-{Id}");

public record SourceDocumentModel(int Id, string Title, string Fullname, IEnumerable<SourceSectionModel> Sections, Relations Relations) : DocumentModel(Id, Title, $"{Fullname}-{Id}");

public class Relations
{
    public List<StaticRelationModel> StaticRelations { get; } = new();
}

public record StaticRelationModel(string TargetFullname, string RelationTypeCode, string SourceBookmark = "", string TargetBookmark = "");
