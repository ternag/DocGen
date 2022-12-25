using System.Xml.Linq;
using ConsoleApp1.B.BuildModel;

namespace ConsoleApp1.C.CreateDocuments;

public static class XmlExtensions
{
    public static XElement? GetElementById(this XDocument document, XName id)
    {
        return document.Descendants().FirstOrDefault(x => x.Attribute("id")?.Value == id);
    }

    public static void SetElementValue(this XDocument document, XName name, string value)
    {
        var element = document.Descendants(name).SingleOrDefault();
        if (element != null)
            element.Value = value;

    }

    public static void AddClassification(this XDocument document, string taxonomyName, string taxonKey)
    {
        XNamespace ns = document.Root.GetDefaultNamespace();
        XElement? classifications = document.Root.Descendants(ns + "Classifications").SingleOrDefault();
        XElement classification = new XElement(ns + "Classification", new XAttribute("TaxonomyName", taxonomyName), new XAttribute("TaxonKey", taxonKey));
        if(classifications != null) classifications.Add(classification);
    }

    public static void AddStaticRelation(this XDocument document, StaticRelationInfo relation)
    {
        document.AddStaticRelation(relation.RelationTypeCode,
            relation.TargetDocument.Fullname,
            relation.SourceBookmark,
            relation.TargetBookmark);
    }

    // <DocumentDocumentRelation RelationTypeCode="DIREKTE" SourceBookmark="p1-1" TargetFullName="71769" TargetBookmark="p1-1" />
    public static void AddStaticRelation(this XDocument document, string relationTypeCode, string targetFullname, string sourceBookmark = "", string targetBookmark = "")
    {
        var ns = document.Root?.GetDefaultNamespace();
        var relations = document.Root?.Descendants(ns + "Relations").SingleOrDefault();

        var relation = new XElement(ns + "DocumentDocumentRelation", 
            new XAttribute("RelationTypeCode", relationTypeCode), 
            new XAttribute("TargetFullName", targetFullname));

        if(!string.IsNullOrWhiteSpace(sourceBookmark))
            relation.SetAttributeValue("SourceBookmark", sourceBookmark);

        if(!string.IsNullOrWhiteSpace(targetBookmark))
            relation.SetAttributeValue("TargetBookmark", targetBookmark);

        relations?.Add(relation);
    }

    
    // <DocumentFamilySingleRelation RelationTypeCode="FAMILY" TargetFamilyName="73901_family" TargetStatus="Effective" />
    // <DocumentFamilyRangedRelation RelationTypeCode = "FAMILY" TargetFamilyName="71769_family" TargetStartFullName="71769_v1" />
    // <DocumentFamilyRangedRelation RelationTypeCode = "FAMILY" TargetFamilyName="71769_family" TargetStartFullName="71769_v1" TargetEndFullName="71769_v20" />

    public static IEnumerable<int> Range0(this int value)
    {
        return Enumerable.Range(0, value);
    }

    public static IEnumerable<int> Range1(this int value)
    {
        return Enumerable.Range(1, value);
    }
}