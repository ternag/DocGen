using System.Text.Json.Serialization;

namespace ConsoleApp1;

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
    [property: JsonPropertyName("Count")] int Count,
    [property: JsonPropertyName("Sections")] IReadOnlyList<SectionSpec> SectionSpecs
);

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

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public record Document(
    [property: JsonPropertyName("Name")] string Name,
    [property: JsonPropertyName("Relations")] IReadOnlyList<RelationSpec> Relations,
    [property: JsonPropertyName("Sections")] IReadOnlyList<SectionSpec> Sections
);

public record RelationSpec(
    [property: JsonPropertyName("RelationKind")] RelationKind RelationKind,
    [property: JsonPropertyName("RelationTypeCode")] string RelationTypeCode,
    [property: JsonPropertyName("Count")] int Count = 1
);

public record Specification(
    [property: JsonPropertyName("SourceDocuments")] SourceDocuments SourceDocuments,
    [property: JsonPropertyName("TargetFamilies")] TargetFamilies TargetFamilies,
    [property: JsonPropertyName("ManyStaticRelationToOne")] ManyStaticRelationToOne ManyStaticRelationToOne,
    [property: JsonPropertyName("ManyRangeTargetRelationsToOneFamily")] ManyRangeTargetRelationsToOneFamily ManyRangeTargetRelationsToOneFamily,
    [property: JsonPropertyName("ManySingleTargetRelationsToOneFamily")] ManySingleTargetRelationsToOneFamily ManySingleTargetRelationsToOneFamily,
    [property: JsonPropertyName("TargetDocuments")] IReadOnlyList<TargetDocumentSpec> TargetDocuments,
    [property: JsonPropertyName("TargetFamily")] TargetFamily TargetFamily
);

public record SectionSpec
(
    [property: JsonPropertyName("Count")] int Count = 1,
    [property: JsonPropertyName("NumberOfStaticRelations")] int NumberOfStaticRelations = 0,
    [property: JsonPropertyName("NumberOfRangedTargetRelations")] int NumberOfRangedTargetRelations = 0,
    [property: JsonPropertyName("NumberOfSingleTargetRelations")] int NumberOfSingleTargetRelations = 0
);

public record TargetDocumentSpec
(
    [property: JsonPropertyName("Sections")] IReadOnlyList<SectionSpec> SectionSpecs,
    [property: JsonPropertyName("Relations")] IReadOnlyList<RelationSpec> Relations = default!,
    [property: JsonPropertyName("Count")] int Count = 1,
    [property: JsonPropertyName("Title")] string Title  = "Target document",
    [property: JsonPropertyName("NumberOfRangedTargetRelations")] int NumberOfRangedTargetRelations = 0 ,
    [property: JsonPropertyName("NumberOfSingleTargetRelations")] int NumberOfSingleTargetRelations = 0 
);

public record TargetFamily(
    [property: JsonPropertyName("FamilyName")] string FamilyName,
    [property: JsonPropertyName("Document")] Document Document,
    [property: JsonPropertyName("NumberOfHistoricDocuments")] int NumberOfHistoricDocuments = 0,
    [property: JsonPropertyName("NumberOfEffectiveDocuments")] int NumberOfEffectiveDocuments = 0,
    [property: JsonPropertyName("NumberOfFutureDocuments")] int NumberOfFutureDocuments = 0,
    [property: JsonPropertyName("NumberOfIndeterminateDocuments")] int NumberOfIndeterminateDocuments = 0
);

public enum RelationKind
{
    None = 0,
    Static = 1,
    SingleTarget = 2,
    RangedTarget = 3
}