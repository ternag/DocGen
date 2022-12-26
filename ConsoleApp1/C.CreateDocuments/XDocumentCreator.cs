using System.Xml.Linq;
using ConsoleApp1.B.BuildModel;

namespace ConsoleApp1.C.CreateDocuments;

public class XDocumentCreator
{
    readonly XDocument _metadataTemplateXml = XDocument.Load("./Templates/metadata_template.xml");
    readonly XDocument _documentTemplateXml = XDocument.Load("./Templates/document_template.xml");

    public XDocument CreateTargetDocuments(TargetDocumentModel model)
    {
        var document = new XDocument(_documentTemplateXml);
        if (document.Root == null) throw new Exception("Root element not found in document template");
        var ns = document.Root!.GetDefaultNamespace();
        document.Root
            .AddTitle(model.Title, ns)
            .Article(ns)
            .AddTargetDocumentInfo(model, ns)
            .AddTargetSections(model.Sections, ns);
        return document;
    }

    public XDocument CreateSourceDocuments(SourceDocumentModel model)
    {
        var document = new XDocument(_documentTemplateXml);
        if (document.Root == null) throw new Exception("Root element not found in document template");
        var ns = document.Root!.GetDefaultNamespace();
        document.Root
            .AddTitle(model.Title, ns)
            .Article(ns)
            .AddSourceDocumentInfo(model, ns)
            .AddSourceSections(model.Sections, ns);
        return document;
    }

    public XDocument CreateMetadata(DocumentModel model)
    {
        var metadata = new XDocument(_metadataTemplateXml);
        var root = metadata.Root;
        if(root == null)  throw new Exception("Root element not found in metadata template");
        var ns = metadata.Root?.GetDefaultNamespace() ?? "urn:schultz.dk:rhinestone:1.0:metadata:1.0";
        var mf = metadata.Root?.GetNamespaceOfPrefix("mf") ?? "urn:schultz.dk:mayflower:1.0:metadata:1.0";
        metadata.SetElementValue(mf + "publicid", $"public-{model.Id}");
        metadata.SetElementValue(mf + "date_effect", DateTime.Now.ToString("s"));
        metadata.Descendants(mf + "date_expired").Remove();
        metadata.SetElementValue(mf + "title", model.Title);
        metadata.Descendants(mf + "consolidated_law").Remove();
        root.AddClassification("DocumentType", "Love", ns);
        return metadata;
    }
}