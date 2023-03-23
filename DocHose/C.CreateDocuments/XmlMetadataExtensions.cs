using System.Xml.Linq;
using DocHose.B.BuildModel;

namespace DocHose.C.CreateDocuments;

public static class XmlMetadataExtensions
{
    public static void AddClassification(this XElement root, string taxonomyName, string taxonKey, XNamespace ns)
    {
        XElement? classifications = root.Descendants(ns + "Classifications").SingleOrDefault();
        XElement classification = new XElement(ns + "Classification", new XAttribute("TaxonomyName", taxonomyName), new XAttribute("TaxonKey", taxonKey));
        if (classifications != null) classifications.Add(classification);
    }

    public static void AddStaticRelations(this XDocument metadata, SourceDocumentModel sourceDocumentModel)
    {
        foreach (var relation in sourceDocumentModel.Relations.StaticRelations)
        {
            metadata.AddStaticRelation(relation);
        }
        foreach (var relation in sourceDocumentModel.Sections.SelectMany(x => x.Relations.StaticRelations))
        {
            metadata.AddStaticRelation(relation);
        }
    }
    
    // <DocumentDocumentRelation RelationTypeCode="DIREKTE" SourceBookmark="p1-1" TargetFullName="71769" TargetBookmark="p1-1" />
    // <DocumentFamilySingleRelation RelationTypeCode="FAMILY" TargetFamilyName="73901_family" TargetStatus="Effective" />
    // <DocumentFamilyRangedRelation RelationTypeCode = "FAMILY" TargetFamilyName="71769_family" TargetStartFullName="71769_v1" />
    // <DocumentFamilyRangedRelation RelationTypeCode = "FAMILY" TargetFamilyName="71769_family" TargetStartFullName="71769_v1" TargetEndFullName="71769_v20" />
    public static void AddStaticRelation(this XDocument document, StaticRelationModel relation)
    {
        document.AddStaticRelation(relation.RelationTypeCode,
            relation.TargetFullname,
            relation.SourceBookmark,
            relation.TargetBookmark);
    }

    public static void AddStaticRelation(this XDocument document, string relationTypeCode, string targetFullname, string sourceBookmark = "", string targetBookmark = "")
    {
        if (document.Root == null) throw new Exception("Root element not found");
        var ns = document.Root?.GetDefaultNamespace() ?? XNamespace.None;
        
        var relations = document.Root?.Descendants(ns + "Relations").SingleOrDefault();
        
        var relation = new XElement(ns + "DocumentDocumentRelation",
            new XAttribute("RelationTypeCode", relationTypeCode),
            new XAttribute("TargetFullName", targetFullname));

        if (!string.IsNullOrWhiteSpace(sourceBookmark))
            relation.SetAttributeValue("SourceBookmark", sourceBookmark);

        if (!string.IsNullOrWhiteSpace(targetBookmark))
            relation.SetAttributeValue("TargetBookmark", targetBookmark);

        relations?.Add(relation);
    }
    
}
