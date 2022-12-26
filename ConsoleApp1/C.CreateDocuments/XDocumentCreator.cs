using System.Xml.Linq;
using ConsoleApp1.A.ParseSpecification;
using ConsoleApp1.B.BuildModel;

namespace ConsoleApp1.C.CreateDocuments;

public class XDocumentCreator
{
    readonly XDocument _metadataTemplateXml = XDocument.Load("./Templates/metadata_template.xml");
    readonly XDocument _documentTemplateXml = XDocument.Load("./Templates/document_template.xml");

    public XDocument CreateTargetDocuments(TargetDocumentInfo info)
    {
        var document = new XDocument(_documentTemplateXml);
        var ns = document.Root!.GetDefaultNamespace();

        var docDescription = document.GetElementById("doc_title");
        if (docDescription != null) docDescription.Value = info.Title;

        XElement? article = GetArticle(document, ns);

        var element = new XElement(ns + "section",
            new XAttribute("id", "doc-info"),
            new XElement(ns + "h1", $"Document '{info.Fullname}'"),
            GetRelationStats(info.RelationsSpec, ns, "top of document"));

        article.Add(element);

        foreach (var sectionInfo in info.Sections)
        {
            article.Add(CreateTargetSection(sectionInfo, ns));
        }

        return document;
    }

    public XDocument CreateSourceDocuments(SourceDocumentInfo info)
    {
        var document = new XDocument(_documentTemplateXml);
        var ns = document.Root!.GetDefaultNamespace();

        var docDescription = document.GetElementById("doc_title");
        if (docDescription != null) docDescription.Value = info.Title;

        XElement? article = GetArticle(document, ns);

        // TODO add sections
        
        return document;
    }

    public static XElement GetRelationStats(IReadOnlyList<RelationSpec> specifications, XNamespace ns, string context)
    {
        var staticCount = specifications.Where(x => x.RelationKind == RelationKind.Static).Sum(x => x.Count);
        var singleCount = specifications.Where(x => x.RelationKind == RelationKind.SingleTarget).Sum(x => x.Count);
        var rangedCount = specifications.Where(x => x.RelationKind == RelationKind.RangedTarget).Sum(x => x.Count);

        return new XElement(ns + "p", $"Relation stats for {context}: Static {staticCount}, Single target {singleCount}, Ranged target {rangedCount}");
    }

    private static XElement GetArticle(XDocument document, XNamespace ns)
    {
        var article = document.Root!.Element(ns + "article");
        if (article == null)
            throw new Exception("article tag is missing in document template");
        return article;
    }

    public XElement CreateTargetSection(TargetSectionInfo sectionInfo, XNamespace ns)
    {
        var element = new XElement(ns + "section",
            new XAttribute("id",
                sectionInfo.Id),
            new XElement(ns + "h1", $"Section id='{sectionInfo.Id}'"),
            GetRelationStats(sectionInfo.RelationsSpec, ns, $"section"),
            new XElement(ns + "p", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."));
        return element;
    }

    public XDocument CreateMetadata(DocumentInfo info)
    {
        var metadata = new XDocument(_metadataTemplateXml);
        var ns = metadata.Root?.GetDefaultNamespace() ?? "urn:schultz.dk:rhinestone:1.0:metadata:1.0";
        var mf = metadata.Root?.GetNamespaceOfPrefix("mf") ?? "urn:schultz.dk:mayflower:1.0:metadata:1.0";
        metadata.SetElementValue(mf + "publicid", $"public-{info.Id}");
        metadata.SetElementValue(mf + "date_effect", DateTime.Now.ToString("s"));
        metadata.Descendants(mf + "date_expired").Remove();
        metadata.SetElementValue(mf + "title", info.Title);
        metadata.Descendants(mf + "consolidated_law").Remove();
        metadata.AddClassification("DocumentType", "Love");
        return metadata;
    }
}