﻿using DocHose.C.CreateDocuments;
using DocHose.A.ParseSpecification;

namespace DocHose.B.BuildModel;

public static class StaticRelationBuilder
{
    
    // For now all static relations are added at the top of the source document.
    public static void Create(IEnumerable<TargetDocumentModel> targetDocumentModels, IReadOnlyList<SourceDocumentModel> sourceDocumentModels)
    {
        if (sourceDocumentModels.Count == 0)
        {
            return;
        }

        using var sourceDocumentEnumerator = sourceDocumentModels.GetEnumerator();

        foreach (var targetDocument in targetDocumentModels)
        {
            var staticRelationSpec = targetDocument.RelationsSpec.Where(r => r.RelationKind == RelationKind.Static);
            foreach (var relationSpec in staticRelationSpec)
            {
                CreateStaticRelations(targetDocument, sourceDocumentEnumerator, relationSpec.Count, relationSpec.RelationTypeCode);
            }

            foreach (var sectionModel in targetDocument.Sections)
            {
                staticRelationSpec = sectionModel.RelationsSpec.Where(r => r.RelationKind == RelationKind.Static);
                foreach (var relationSpec in staticRelationSpec)
                {
                    CreateStaticRelations(targetDocument, sourceDocumentEnumerator, relationSpec.Count, relationSpec.RelationTypeCode, sectionModel.Id);
                }
            }
        }
    }

    private static void CreateStaticRelations(
        TargetDocumentModel targetDocument,
        IEnumerator<SourceDocumentModel> sourceDocumentEnumerator,
        int numberOfRelations,
        string relationTypeCode,
        string targetBookmark = "")
    {
        foreach (var _ in numberOfRelations.Range0())
        {
            bool moveOn = sourceDocumentEnumerator.MoveNext();
            if (!moveOn)
            {
                sourceDocumentEnumerator.Reset();
                sourceDocumentEnumerator.MoveNext();
            }

            sourceDocumentEnumerator.Current.Relations.StaticRelations.Add(new StaticRelationModel(targetDocument.Fullname, relationTypeCode, TargetBookmark:targetBookmark));
        }
    }
}
