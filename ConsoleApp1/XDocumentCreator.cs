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

        

        var article = document.Root?.Element("article");
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
        var ns = metadata.Root.GetDefaultNamespace();
        var mf = metadata.Root.GetNamespaceOfPrefix("mf");
        metadata.SetElementValue(mf+"publicid", $"public-{info.Id}");
        metadata.SetElementValue(mf+"date_effect", DateTime.Now.ToString("s"));
        metadata.Descendants(mf+"date_expired").Remove();
        metadata.SetElementValue(mf+"title", info.Title);
        metadata.Descendants(mf+ "consolidated_law").Remove();
        metadata.AddClassification("DocumentType", "Love");
        return metadata;
    }

}