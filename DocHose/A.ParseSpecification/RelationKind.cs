using System.Text.Json.Serialization;

namespace DocHose.A.ParseSpecification;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RelationKind
{
    None = 0,
    Static = 1,
    SingleTarget = 2,
    RangedTarget = 3
}