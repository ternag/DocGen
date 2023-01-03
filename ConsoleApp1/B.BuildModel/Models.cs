using ConsoleApp1.A.ParseSpecification;

namespace ConsoleApp1.B.BuildModel;

public record SourceDocumentModel(int Id, string Title, string Fullname, IEnumerable<SourceSectionModel> Sections, Relations Relations) : DocumentModel(Id, Title, $"{Fullname}-{Id}");

public record TargetDocumentModel(int Id, string Title, string Fullname, IEnumerable<TargetSectionModel> Sections, IReadOnlyList<RelationSpec> RelationsSpec) : DocumentModel(Id, Title, $"{Fullname}-{Id}");

public abstract record DocumentModel(int Id, string Title, string Fullname);

public record SourceSectionModel(string Id, string Class, Relations Relations):SectionModel(Id, Class);

public record TargetSectionModel(string Id, string Class, IReadOnlyList<RelationSpec> RelationsSpec):SectionModel(Id, Class);

public abstract record SectionModel(string Id, string Class);

public class Relations
{
    public List<StaticRelationModel> StaticRelations { get; } = new();
}

public record StaticRelationModel(string TargetFullname, string RelationTypeCode, string SourceBookmark = "", string TargetBookmark = "");
