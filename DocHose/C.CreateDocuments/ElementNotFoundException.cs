using System.Xml.Linq;

namespace DocHose.C.CreateDocuments;

public class ElementNotFoundException : Exception
{
    public ElementNotFoundException(XName id) : base($"Element '{id}' not found")
    {
    }
}