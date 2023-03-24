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


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Historic = 0,
    Effective = 1,
    Future = 2,
    Indeterminate = 3
}
