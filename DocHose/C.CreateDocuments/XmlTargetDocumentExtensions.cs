using System.Xml.Linq;
using DocHose.B.BuildModel;

namespace DocHose.C.CreateDocuments;

public static class XmlTargetDocumentExtensions
{
    public static XElement AddTargetDocumentInfo(this XElement article, TargetDocumentModel model, XNamespace ns)
    {
        var element = new XElement(ns + "section",
            new XAttribute("id", "doc-info"),
            new XElement(ns + "h1", $"Document Fullname: '{model.Fullname}'"),
            XmlCreate.RelationStats(model.RelationsSpec, "top of document", ns));
        article.Add(element);
        return article;
    }

    public static XElement AddTargetSections(this XElement article, IEnumerable<TargetSectionModel> sections, XNamespace ns)
    {
        foreach (var sectionInfo in sections)
        {
            article.AddTargetSection(sectionInfo, ns);
        }

        return article;
    }

    private static XElement AddTargetSection(this XElement article, TargetSectionModel sectionModel, XNamespace ns)
    {
        var element = new XElement(ns + "section",
            new XAttribute("id", sectionModel.Id),
            new XAttribute("class", sectionModel.Class),
            new XElement(ns + "h1", $"Section id='{sectionModel.Id}'"),
            XmlCreate.RelationStats(sectionModel.RelationsSpec, $"section", ns),
            new XElement(ns + "p", LoremIpsum.GetRandomLength()));
        article.Add(element);
        return article;
    }
}
