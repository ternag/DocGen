using System.Xml.Linq;
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
            GetRelationStats(info.RelationsInfo, ns));

        article.Add(element);

        foreach (var sectionInfo in info.Sections)
        {
            article.Add(CreateSection(sectionInfo, ns));
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

        return document;
    }

    private static XElement GetRelationStats(TmpRelationsInfo info, XNamespace ns)
    {
        return new XElement(ns + "p", $"Relation stats for document: Static ?, Ranged {info.RangedTargetCount}, Single {info.SingleTargetCount}");
    }

    private static XElement GetArticle(XDocument document, XNamespace ns)
    {
        var article = document.Root!.Element(ns + "article");
        if (article == null)
            throw new Exception("article tag is missing in document template");
        return article;
    }

    public XElement CreateSection(SectionInfo sectionInfo, XNamespace ns)
    {
        var element = new XElement(ns + "section",
            new XAttribute("id",
                sectionInfo.Id),
            new XElement(ns + "h1", $"Section id='{sectionInfo.Id}'"),
            new XElement(ns + "p", GetRelationStats(sectionInfo.RelationsInfo, ns)),
            new XElement(ns + "p", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."));
        return element;
    }

    public XDocument CreateMetadata(DocumentInfo info)
    {
        var metadata = new XDocument(_metadataTemplateXml);
        var ns = metadata.Root.GetDefaultNamespace();
        var mf = metadata.Root.GetNamespaceOfPrefix("mf");
        metadata.SetElementValue(mf + "publicid", $"public-{info.Id}");
        metadata.SetElementValue(mf + "date_effect", DateTime.Now.ToString("s"));
        metadata.Descendants(mf + "date_expired").Remove();
        metadata.SetElementValue(mf + "title", info.Title);
        metadata.Descendants(mf + "consolidated_law").Remove();
        metadata.AddClassification("DocumentType", "Love");
        return metadata;
    }


}