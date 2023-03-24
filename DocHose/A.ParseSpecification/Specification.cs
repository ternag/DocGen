using System.Text.Json.Serialization;

namespace DocHose.A.ParseSpecification;

public record Specification(
    [property: JsonPropertyName("SourceDocuments")] SourceDocumentsSpec SourceDocumentsSpec = default!,
    [property: JsonPropertyName("TargetDocuments")] IReadOnlyList<TargetDocumentSpec> TargetDocumentsSpec = default!,
    [property: JsonPropertyName("Family")] FamilySpec FamilySpec = default!
);

public record SourceDocumentsSpec(
    [property: JsonPropertyName("Count")] int Count = 1,
    [property: JsonPropertyName("Title")] string Title = "Source document",
    [property: JsonPropertyName("Fullname")] string Fullname = "SourceDocument",
    IReadOnlyList<SectionSpec> SectionSpecs = default!
)
{
    [property: JsonPropertyName("Sections")] public IReadOnlyList<SectionSpec> SectionSpecs { get; init; } = SectionSpecs ?? new List<SectionSpec>() { new SectionSpec() };
};

public record TargetDocumentSpec
(
    [property: JsonPropertyName("Count")] int Count = 1,
    [property: JsonPropertyName("Title")] string Title = "Target document",
    [property: JsonPropertyName("Fullname")] string Fullname = "TargetDocument",
    [property: JsonPropertyName("Status")] Status Status = Status.Effective,
    IReadOnlyList<SectionSpec> SectionSpecs = default!,
    IReadOnlyList<RelationSpec> Relations = default!
)
{
    [property: JsonPropertyName("Sections")] public IReadOnlyList<SectionSpec> SectionSpecs { get; init; } = SectionSpecs ?? new[] { new SectionSpec() };
    [property: JsonPropertyName("Relations")] public IReadOnlyList<RelationSpec> Relations { get; init; } = Relations ?? new[] { new RelationSpec() };
};

public record SectionSpec
(
    [property: JsonPropertyName("Count")] int Count = 1, 
    IReadOnlyList<RelationSpec> Relations = default!,
    [property: JsonPropertyName("Class")] string Class = "paragraf"
)
{
    [property: JsonPropertyName("Relations")] public IReadOnlyList<RelationSpec> Relations { get; init; } = Relations ?? new[] { new RelationSpec() };
};

public record RelationSpec(
    [property: JsonPropertyName("Count")] int Count = 1,
    [property: JsonPropertyName("RelationKind")] RelationKind RelationKind = RelationKind.Static,
    [property: JsonPropertyName("RelationTypeCode")] string RelationTypeCode = "DIREKTE"
);

public record FamilySpec(
    [property: JsonPropertyName("FamilyName")] string FamilyName,
    [property: JsonPropertyName("VersionTimespanInDays")] int VersionTimespanInDays = 30,
    IReadOnlyList<TargetDocumentSpec> TargetDocuments = default!
)
{
    [property: JsonPropertyName("TargetDocuments")] public IReadOnlyList<TargetDocumentSpec> TargetDocuments { get; init; } = TargetDocuments ?? new[] { new TargetDocumentSpec() };
};
