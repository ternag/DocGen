using System.Xml.Linq;
using DocHose.A.ParseSpecification;

namespace DocHose.C.CreateDocuments;

public static class XmlCreate
{
    public static XElement RelationStats(IReadOnlyList<RelationSpec> specifications, string context, XNamespace ns)
    {
        var staticCount = specifications.Where(x => x.RelationKind == RelationKind.Static).Sum(x => x.Count);
        var singleCount = specifications.Where(x => x.RelationKind == RelationKind.SingleTarget).Sum(x => x.Count);
        var rangedCount = specifications.Where(x => x.RelationKind == RelationKind.RangedTarget).Sum(x => x.Count);

        return new XElement(ns + "p", $"Relation stats for {context}: Static {staticCount}, Single target {singleCount}, Ranged target {rangedCount}");
    }

}