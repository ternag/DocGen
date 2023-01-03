using System.Text.Json;

namespace ConsoleApp1.A.ParseSpecification;

public static class SpecificationLoader
{
    public static Specification Parse(string specificationText)
    {
        Specification? specification = JsonSerializer.Deserialize<Specification>(specificationText);
        if (specification == null) throw new NullReferenceException($"{nameof(specification)} is null");
        return specification;
    }
}