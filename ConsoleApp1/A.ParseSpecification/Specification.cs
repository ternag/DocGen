﻿using System.Text.Json.Serialization;

namespace ConsoleApp1.A.ParseSpecification;

public record FamilyDocument(
    [property: JsonPropertyName("NumberOfSections")] NumberOfSections NumberOfSections
);

public record ManyRangeTargetRelationsToOneFamily(
    [property: JsonPropertyName("Generate")] bool Generate,
    [property: JsonPropertyName("RelationTypeCode")] string RelationTypeCode,
    [property: JsonPropertyName("TargetStartMember")] string TargetStartMember,
    [property: JsonPropertyName("TargetEndMember")] string TargetEndMember,
    [property: JsonPropertyName("TargetWithinDocument")] TargetWithinDocument TargetWithinDocument
);

public record ManySingleTargetRelationsToOneFamily(
    [property: JsonPropertyName("Generate")] bool Generate,
    [property: JsonPropertyName("RelationTypeCode")] string RelationTypeCode,
    [property: JsonPropertyName("TargetMember")] string TargetMember,
    [property: JsonPropertyName("TargetWithinDocument")] TargetWithinDocument TargetWithinDocument
);

public record ManyStaticRelationToOne(
    [property: JsonPropertyName("Generate")] bool Generate,
    [property: JsonPropertyName("RelationTypeCode")] string RelationTypeCode,
    [property: JsonPropertyName("PercentToTargetDocument")] Percent PercentToTargetDocument,
    [property: JsonPropertyName("PercentToTargetSections")] Percent PercentToTargetSections
);

public record SourceDocuments(
    [property: JsonPropertyName("Count")] int Count = 1,
    IReadOnlyList<SectionSpec> SectionSpecs = default!,
    [property: JsonPropertyName("Fullname")] string Fullname = "SourceDocument"
)
{
    [property: JsonPropertyName("Sections")] 
    public IReadOnlyList<SectionSpec> SectionSpecs { get; init; } = SectionSpecs ?? new List<SectionSpec>() { new SectionSpec()};
};

public record TargetFamilies(
    [property: JsonPropertyName("Count")] DocumentCount Count,
    [property: JsonPropertyName("FamilyDocument")] FamilyDocument FamilyDocument,
    [property: JsonPropertyName("NumberOfHistoricDocuments")] uint NumberOfHistoricDocuments,
    [property: JsonPropertyName("NumberOfEffectiveDocuments")] uint NumberOfEffectiveDocuments,
    [property: JsonPropertyName("NumberOfFutureDocuments")] uint NumberOfFutureDocuments,
    [property: JsonPropertyName("NumberOfIndeterminateDocuments")] uint NumberOfIndeterminateDocuments
);

public record TargetWithinDocument(
    [property: JsonPropertyName("PercentToTargetDocument")] Percent PercentToTargetDocument,
    [property: JsonPropertyName("PercentToTargetSections")] Percent PercentToTargetSections
);

public record RelationSpec(
    [property: JsonPropertyName("Count")] int Count = 1,
    [property: JsonPropertyName("RelationKind")] RelationKind RelationKind = RelationKind.Static,
    [property: JsonPropertyName("RelationTypeCode")] string RelationTypeCode = "DIREKTE"
);

public record Specification(
    [property: JsonPropertyName("SourceDocuments")] SourceDocuments SourceDocuments = default!,
    [property: JsonPropertyName("TargetDocuments")] IReadOnlyList<TargetDocumentSpec> TargetDocuments = default!
);

public record SectionSpec
(
    [property: JsonPropertyName("Count")] int Count = 1,
    IReadOnlyList<RelationSpec> Relations = default!
){
    [property: JsonPropertyName("Relations")]
    public IReadOnlyList<RelationSpec> Relations { get; init; } = Relations ?? new[] { new RelationSpec() };
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
    [property: JsonPropertyName("Sections")] 
    public IReadOnlyList<SectionSpec> SectionSpecs { get; init; } = SectionSpecs ?? new[] { new SectionSpec() };

    [property: JsonPropertyName("Relations")]
    public IReadOnlyList<RelationSpec> Relations { get; init; } = Relations ?? new[] { new RelationSpec() };
};

public record TargetFamily(
    [property: JsonPropertyName("FamilyName")] string FamilyName,
    [property: JsonPropertyName("NumberOfHistoricDocuments")] int NumberOfHistoricDocuments = 0,
    [property: JsonPropertyName("NumberOfEffectiveDocuments")] int NumberOfEffectiveDocuments = 0,
    [property: JsonPropertyName("NumberOfFutureDocuments")] int NumberOfFutureDocuments = 0,
    [property: JsonPropertyName("NumberOfIndeterminateDocuments")] int NumberOfIndeterminateDocuments = 0
);