using DocHose.A.ParseSpecification;

namespace DocHose.B.BuildModel;

public class SourceDocumentModel : DocumentModel
{
    public SourceDocumentModel(int id, string title, string fullname, IEnumerable<SourceSectionModel> sections, Relations relations) : base(id, title, $"{fullname}-{id}")
    {
        Sections = sections;
        Relations = relations;
    }

    public IEnumerable<SourceSectionModel> Sections { get; }
    public Relations Relations { get; }
}

public class TargetDocumentModel : DocumentModel
{
    public TargetDocumentModel(int id, string title, string fullname, Status status, IEnumerable<TargetSectionModel> sections, IReadOnlyList<RelationSpec> relationSpecs) : base(id, title, $"{fullname}-{id}")
    {
        Sections = sections;
        RelationSpecsSpec = relationSpecs;
        Status = status;
    }

    public Status Status { get; }
    public DateTime EffectiveDate { get; set; }
    public DateTime? RepealDate { get; set; }
    public IEnumerable<TargetSectionModel> Sections { get; }
    public IReadOnlyList<RelationSpec> RelationSpecsSpec { get; }
}

public class TargetFamilyModel
{
    public TargetFamilyModel(string familyName, TargetDocumentModel[] memberDocuments)
    {
        FamilyName = familyName;
        MemberDocuments = memberDocuments;
    }

    public string FamilyName { get; }
    public TargetDocumentModel[] MemberDocuments { get; set; }
}

public abstract class DocumentModel
{
    protected DocumentModel(int id, string title, string fullname)
    {
        Id = id;
        Title = title;
        Fullname = fullname;
    }

    public int Id { get; }
    public string Title { get; }
    public string Fullname { get; }
}

public record SourceSectionModel(string Id, string Class, Relations Relations):SectionModel(Id, Class);

public record TargetSectionModel(string Id, string Class, IReadOnlyList<RelationSpec> RelationsSpec):SectionModel(Id, Class);

public abstract record SectionModel(string Id, string Class);

public class Relations
{
    public List<StaticRelationModel> StaticRelations { get; } = new();
}

public record StaticRelationModel(string TargetFullname, string RelationTypeCode, string SourceBookmark = "", string TargetBookmark = "");
