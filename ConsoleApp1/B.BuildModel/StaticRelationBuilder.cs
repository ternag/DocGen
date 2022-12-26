using ConsoleApp1.A.ParseSpecification;

namespace ConsoleApp1.B.BuildModel;

public static class StaticRelationBuilder
{
    internal static void Create(IEnumerable<TargetDocumentInfo> targetDocumentInfos, IReadOnlyList<SourceDocumentInfo> sourceDocuments)
    {
        if (sourceDocuments.Count == 0)
        {
            return;
        }

        using var sourceDocumentEnumerator = sourceDocuments.GetEnumerator();

        foreach (var targetDocument in targetDocumentInfos)
        {
            var staticRelationSpec = targetDocument.RelationsSpec.Where(r => r.RelationKind == RelationKind.Static);
            foreach (var item in staticRelationSpec)
            {
                CreateStaticRelations(targetDocument, sourceDocumentEnumerator, item.Count, item.RelationTypeCode);
            }
        }
    }

    private static void CreateStaticRelations(TargetDocumentInfo targetDocument, IEnumerator<SourceDocumentInfo> sourceDocumentEnumerator, int numberOfRelations, string relationTypeCode)
    {
        for (int i = 0; i < numberOfRelations; i++)
        {
            bool moveOn = sourceDocumentEnumerator.MoveNext();
            if (!moveOn)
            {
                sourceDocumentEnumerator.Reset();
                sourceDocumentEnumerator.MoveNext();
            }

            sourceDocumentEnumerator.Current.Relations.StaticRelations.Add(new StaticRelationInfo(targetDocument, relationTypeCode));
        }
    }
}