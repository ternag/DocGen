using System.Text.Json.Serialization;

namespace ConsoleApp1.A.ParseSpecification;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RelationKind
{
    None = 0,
    Static = 1,
    SingleTarget = 2,
    RangedTarget = 3
}