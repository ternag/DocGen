﻿using DocHose.C.CreateDocuments;
using DocHose.A.ParseSpecification;

namespace DocHose.B.BuildModel;

public static class Builder
{
    public static IReadOnlyList<SourceDocumentModel> BuildSourceDocuments(SourceDocumentsSpec sourceDocumentSpecification, DocumentCounter counter)
    {
        List<SourceDocumentModel> result = new List<SourceDocumentModel>(sourceDocumentSpecification.Count);
        foreach (int i in sourceDocumentSpecification.Count.Range1())
        {
            var documentInfo = new SourceDocumentModel(counter.GetNextId(),
                $"{sourceDocumentSpecification.Title} // No. {i} of {sourceDocumentSpecification.Count}",
                sourceDocumentSpecification.Fullname,
                BuildSourceSectionsInfo(sourceDocumentSpecification.SectionSpecs),
                new Relations());
            result.Add(documentInfo);
        }
        return result;
    }
    
    public static IEnumerable<TargetDocumentModel> BuildTargetDocuments(IEnumerable<TargetDocumentSpec> targetDocumentsSpecification, DocumentCounter counter)
    {
        var targetDocumentSpecs = targetDocumentsSpecification.ToList();
        int totalNumberOfTargetDocuments = targetDocumentSpecs.Sum(x => x.Count);
        
        foreach (var targetDocumentSpec in targetDocumentSpecs)
        {
            foreach (var _ in targetDocumentSpec.Count.Range1())
            {
                yield return BuildTargetDocumentModel(targetDocumentSpec, counter.GetNextId(), totalNumberOfTargetDocuments);
            }
        }
    }

    public static TargetDocumentModel BuildTargetDocumentModel(TargetDocumentSpec targetDocumentSpec, int documentId, int totalNumberOfTargetDocuments)
    {
        return new TargetDocumentModel(documentId, 
            $"{targetDocumentSpec.Title} // No. {documentId} of {totalNumberOfTargetDocuments}", 
            targetDocumentSpec.Fullname, 
            targetDocumentSpec.Status,
            BuildTargetSectionsInfo(targetDocumentSpec.SectionSpecs), 
            targetDocumentSpec.Relations);
    }

    private static IEnumerable<TargetSectionModel> BuildTargetSectionsInfo(IEnumerable<SectionSpec> sections)
    {
        int sectionId = 0;
        foreach (var section in sections)
        {
            foreach (var _ in section.Count.Range0())
            {
                yield return new TargetSectionModel($"s{sectionId}", section.Class, section.Relations);
                sectionId++;
            }
        }
    }

    public static IEnumerable<SourceSectionModel> BuildSourceSectionsInfo(IReadOnlyList<SectionSpec> sections)
    {
        int sectionId = 0;
        foreach (var section in sections)
        {
            foreach (var _ in section.Count.Range0())
            {
                yield return new SourceSectionModel($"s{sectionId}", section.Class, new Relations());
                sectionId++;
            }
        }
    }
}
