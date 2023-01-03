using System.Xml.Linq;

namespace ConsoleApp1.C.CreateDocuments;

public class ElementNotFoundException : Exception
{
    public ElementNotFoundException(XName id) : base($"Element '{id}' not found")
    {
    }
}