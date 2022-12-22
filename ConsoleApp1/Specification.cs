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

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public record Document(
    [property: JsonPropertyName("Name")] string Name,
    [property: JsonPropertyName("Relations")] IReadOnlyList<Relation> Relations,
    [property: JsonPropertyName("Sections")] IReadOnlyList<Section> Sections
);

public record Relation(
    [property: JsonPropertyName("Count")] int Count,
    [property: JsonPropertyName("RelationKind")] string RelationKind,
    [property: JsonPropertyName("RelationTypeCode")] string RelationTypeCode
);

public record Specification(
    [property: JsonPropertyName("SourceDocuments")] SourceDocuments SourceDocuments,
    [property: JsonPropertyName("TargetFamilies")] TargetFamilies TargetFamilies,
    [property: JsonPropertyName("ManyStaticRelationToOne")] ManyStaticRelationToOne ManyStaticRelationToOne,
    [property: JsonPropertyName("ManyRangeTargetRelationsToOneFamily")] ManyRangeTargetRelationsToOneFamily ManyRangeTargetRelationsToOneFamily,
    [property: JsonPropertyName("ManySingleTargetRelationsToOneFamily")] ManySingleTargetRelationsToOneFamily ManySingleTargetRelationsToOneFamily,
    [property: JsonPropertyName("TargetDocuments")] IReadOnlyList<TargetDocument> TargetDocuments,
    [property: JsonPropertyName("TargetFamily")] TargetFamily TargetFamily
);

public record Section
{
    [property: JsonPropertyName("Count")]
    public int Count { get; init; } = 1;

    [property: JsonPropertyName("NumberOfStaticRelations")]
    public int NumberOfStaticRelations { get; init; }

    [property: JsonPropertyName("NumberOfRangedTargetRelations")]
    public int NumberOfRangedTargetRelations { get; init; }

    [property: JsonPropertyName("NumberOfSingleTargetRelations")]
    public int NumberOfSingleTargetRelations { get; init; }
}

public record TargetDocument
{
    [property: JsonPropertyName("Count")] public int Count { get; init; } = 1;
    [property: JsonPropertyName("Title")] public string Title { get; init; } = "Target document";
    [property: JsonPropertyName("NumberOfStaticRelations")] public int NumberOfStaticRelations { get; init; }
    [property: JsonPropertyName("NumberOfRangedTargetRelations")]  public int NumberOfRangedTargetRelations { get; init; }
    [property: JsonPropertyName("NumberOfSingleTargetRelations")]  public int NumberOfSingleTargetRelations { get; init; }
    [property: JsonPropertyName("Sections")]  public IReadOnlyList<Section> Sections { get; init; } = new List<Section>();
}
public record TargetFamily(
    [property: JsonPropertyName("FamilyName")] string FamilyName,
    [property: JsonPropertyName("Document")] Document Document,
    [property: JsonPropertyName("NumberOfHistoricDocuments")] int NumberOfHistoricDocuments,
    [property: JsonPropertyName("NumberOfEffectiveDocuments")] int NumberOfEffectiveDocuments,
    [property: JsonPropertyName("NumberOfFutureDocuments")] int NumberOfFutureDocuments,
    [property: JsonPropertyName("NumberOfIndeterminateDocuments")] int NumberOfIndeterminateDocuments
);

