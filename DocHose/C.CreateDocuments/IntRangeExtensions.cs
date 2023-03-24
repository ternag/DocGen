namespace DocHose.C.CreateDocuments;

public static class IntRangeExtensions
{
    public static IEnumerable<int> Range0(this int value)
    {
        return Enumerable.Range(0, value);
    }

    public static IEnumerable<int> Range1(this int value)
    {
        return Enumerable.Range(1, value);
    }
}
