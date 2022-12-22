using System.Xml.Linq;

namespace ConsoleApp1;

public class XDocumentCreator
{
    readonly XDocument _metadataTemplateXml = XDocument.Load("./Templates/metadata_template.xml");
    readonly XDocument _documentTemplateXml = XDocument.Load("./Templates/document_template.xml");

    public XDocument CreateDocument(DocumentInfo info)
    {
        var document = new XDocument(_documentTemplateXml);
        var ns = document.Root!.GetDefaultNamespace();

        var docDescription = document.GetElementById("doc_description");
        if (docDescription != null) docDescription.Value = info.Title;

        var article = document.Root!.Element(ns + "article");
        
        if (article == null) 
            throw new Exception("article tag is missing in document template");

        if(info is TargetDocumentInfo targetDocInfo)
        {
            var element = new XElement(ns + "section",
                new XAttribute("id", "doc-info"),
                new XElement(ns + "h1", $"Document '{targetDocInfo.Fullname}'"),
                new XElement(ns + "p", $"Relation stats for document: Static {targetDocInfo.relationsInfo.StaticRelationCount}, Ranged {targetDocInfo.relationsInfo.RangedTargetCount}, Single {targetDocInfo.relationsInfo.SingleTargetCount}"));

            article.Add(element);
        }

        foreach (var sectionInfo in info.Sections)
        {
            article.Add(CreateSection(sectionInfo, ns));
        }

        return document;
    }

    public XElement CreateSection(SectionInfo sectionInfo, XNamespace ns)
    {
        var element = new XElement(ns + "section",
            new XAttribute("id",
                sectionInfo.Id),
            new XElement(ns + "h1",
                $"Section id='{sectionInfo.Id}'"),
            new XElement(ns + "p", $"Relation stats: Static {sectionInfo.relationsInfo.StaticRelationCount}, Ranged {sectionInfo.relationsInfo.RangedTargetCount}, Single {sectionInfo.relationsInfo.SingleTargetCount}"),
            new XElement(ns + "p",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."));
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