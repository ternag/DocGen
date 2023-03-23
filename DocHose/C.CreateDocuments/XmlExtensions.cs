using System.Xml.Linq;

namespace DocHose.C.CreateDocuments;

public static class XmlExtensions
{
    public static XElement GetElementById(this XElement root, XName id)
    {
        var element = root.Descendants().FirstOrDefault(x => x.Attribute("id")?.Value == id);
        if (element == null) throw new ElementNotFoundException(id);
        return element;
    }

    public static void SetElementValue(this XDocument document, XName name, string value)
    {
        var element = document.Descendants(name).SingleOrDefault();
        if (element != null)
            element.Value = value;
    }


    public static XElement AddTitle(this XElement root, string title, XNamespace ns)
    {
        root.GetElementById("doc_title").Value = title;
        return root;
    }


    public static XElement Article(this XElement root, XNamespace ns)
    {
        var article = root.Element(ns + "article");
        if (article == null)
            throw new Exception("article tag is missing in document template");
        return article;
    }

}