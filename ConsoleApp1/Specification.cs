// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

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

public record Specification(
    [property: JsonPropertyName("SourceDocuments")] SourceDocuments SourceDocuments,
    [property: JsonPropertyName("TargetDocuments")] TargetDocuments TargetDocuments,
    [property: JsonPropertyName("Families")] TargetFamilies TargetFamilies,
    [property: JsonPropertyName("ManyStaticRelationToOne")] ManyStaticRelationToOne ManyStaticRelationToOne,
    [property: JsonPropertyName("ManyRangeTargetRelationsToOneFamily")] ManyRangeTargetRelationsToOneFamily ManyRangeTargetRelationsToOneFamily,
    [property: JsonPropertyName("ManySingleTargetRelationsToOneFamily")] ManySingleTargetRelationsToOneFamily ManySingleTargetRelationsToOneFamily
);

public record SourceDocuments(
    [property: JsonPropertyName("Count")] int Count,
    [property: JsonPropertyName("NumberOfSections")] int NumberOfSections
);

public record TargetDocuments(
    [property: JsonPropertyName("Count")] int Count,
    [property: JsonPropertyName("NumberOfSections")] int NumberOfSections
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