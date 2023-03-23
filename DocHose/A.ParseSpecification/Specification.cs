using System.Text.Json.Serialization;

namespace DocHose.A.ParseSpecification;

public record Specification(
    [property: JsonPropertyName("SourceDocuments")] SourceDocumentsSpec SourceDocumentsSpec = default!,
    [property: JsonPropertyName("TargetDocuments")] IReadOnlyList<TargetDocumentSpec> TargetDocumentsSpec = default!
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
    [property: JsonPropertyName("RelationTypeCode")] string RelationTypeCode = "DIREKTE",
    [property: JsonPropertyName("TargetVersion")] string TargetVersion = -1
);

public record Family(
    [property: JsonPropertyName("FamilyName")] string FamilyName,
    [property: JsonPropertyName("NumberOfHistoricDocuments")] int NumberOfHistoricDocuments = 5,
    [property: JsonPropertyName("NumberOfEffectiveDocuments")] int NumberOfEffectiveDocuments = 1,
    [property: JsonPropertyName("NumberOfFutureDocuments")] int NumberOfFutureDocuments = 2,
    [property: JsonPropertyName("NumberOfIndeterminateDocuments")] int NumberOfIndeterminateDocuments = 1,
    [property: JsonPropertyName("VersionTimespan")] int VersionTimespanInDays = 30
);

public record FamilyDocument(
    [property: JsonPropertyName("NumberOfSections")] NumberOfSections NumberOfSections
);

public record TargetFamilies(
    [property: JsonPropertyName("Count")] DocumentCount Count,
    [property: JsonPropertyName("FamilyDocument")] FamilyDocument FamilyDocument,
    [property: JsonPropertyName("NumberOfHistoricDocuments")] uint NumberOfHistoricDocuments,
    [property: JsonPropertyName("NumberOfEffectiveDocuments")] uint NumberOfEffectiveDocuments,
    [property: JsonPropertyName("NumberOfFutureDocuments")] uint NumberOfFutureDocuments,
    [property: JsonPropertyName("NumberOfIndeterminateDocuments")] uint NumberOfIndeterminateDocuments
);


