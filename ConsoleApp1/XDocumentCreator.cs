using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ConsoleApp1;

public class XDocumentCreator
{
    XDocument metadataTemplateXml = XDocument.Load("./Templates/metadata_template.xml");
    XDocument documentTemplateXml = XDocument.Load("./Templates/document_template.xml");

    public XDocument CreateDocument(DocumentInfo info)
    {
        var document = new XDocument(documentTemplateXml);
        var docDescription = document.GetElementById("doc_description");
        if (docDescription != null) docDescription.Value = info.Title;

        if (document.Root == null) throw new NullReferenceException(nameof(document.Root));

        var article = document.Root.Element("article");
        if (article == null) throw new NullReferenceException(nameof(article));

        article.Add();

        return document;
    }

    public XElement CreateSection(SectionInfo sectionInfo)
    {
        var element = new XElement("section", new XAttribute("id", $"BM{sectionInfo.Id}"));

        return element;
    }

    public XDocument CreateMetadata(DocumentInfo info)
    {
        var metadata = new XDocument(metadataTemplateXml);
        if (metadata.Root == null) throw new NullReferenceException(nameof(metadata.Root));
        var ns = metadata.Root.GetDefaultNamespace();
        var mf = metadata.Root.GetNamespaceOfPrefix("mf");

        if (mf == null) throw new NullReferenceException(nameof(mf));

        metadata.SetElementValue(mf+"publicid", $"public-{info.Id}");
        metadata.SetElementValue(mf+"date_effect", DateTime.Now.ToString("s"));
        metadata.Descendants(mf+"date_expired").Remove();
        metadata.SetElementValue(mf+"title", info.Title);
        metadata.Descendants(mf+ "consolidated_law").Remove();
        metadata.AddClassification("DocumentType", "Love");
        return metadata;
    }

}