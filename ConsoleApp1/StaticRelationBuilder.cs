using ConsoleApp1.A.ParseSpecification;
using ConsoleApp1.B.BuildModel;

namespace ConsoleApp1;

public static class StaticRelationBuilder
{
    internal static void Create(IEnumerable<TargetDocumentInfo> targetDocumentInfos, IReadOnlyList<SourceDocumentInfo> sourceDocuments)
    {
        foreach(var targetDocument in targetDocumentInfos)
        {
            IEnumerable<RelationSpec> staticRelationSpec = targetDocument.RelationsSpec.Where(r => r.RelationKind == RelationKind.Static);
            foreach (var item in staticRelationSpec)
            {
                CreateStaticRelations(targetDocument, sourceDocuments, item.Count, item.RelationTypeCode);
            }
        }
    }

    private static void CreateStaticRelations(TargetDocumentInfo targetDocument, IReadOnlyList<SourceDocumentInfo> sourceDocuments, int numberOfRelations, string relationTypeCode)
    {
        if(sourceDocuments.Count == 0) { return; }

        IEnumerator<SourceDocumentInfo> s = sourceDocuments.GetEnumerator();

        for (int i=0; i < numberOfRelations; i++)
        {
            bool moveOn = s.MoveNext();
            if (!moveOn)
            {
                s.Reset();
                s.MoveNext();
            }
            s.Current.Relations.StaticRelations.Add(new StaticRelationInfo(i, targetDocument, relationTypeCode));
        }
    }
}