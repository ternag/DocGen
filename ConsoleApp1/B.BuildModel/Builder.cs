using ConsoleApp1.A.ParseSpecification;
using ConsoleApp1.C.CreateDocuments;

namespace ConsoleApp1.B.BuildModel;

public static class Builder
{
    public static IReadOnlyList<SourceDocumentModel> BuildSourceDocuments(SourceDocuments sourceDocumentSpecification)
    {
        List<SourceDocumentModel> result = new List<SourceDocumentModel>((int)sourceDocumentSpecification.Count);
        foreach (int i in sourceDocumentSpecification.Count.Range1())
        {
            var documentInfo = new SourceDocumentModel(i, $"Source document No. {i} of {sourceDocumentSpecification.Count}", sourceDocumentSpecification.Fullname, BuildSourceSectionsInfo(sourceDocumentSpecification.SectionSpecs), new Relations());
            result.Add(documentInfo);
        }
        return result;
    }

    public static IEnumerable<TargetDocumentModel> BuildTargetDocuments(IReadOnlyList<TargetDocumentSpec> targetDocumentsSpecification)
    {
        int documentId = 0;
        foreach (TargetDocumentSpec targetDocument in targetDocumentsSpecification)
        {
            yield return BuildTargetDocumentInfo(targetDocument, documentId, 0);
            documentId++;
        }
    }

    public static TargetDocumentModel BuildTargetDocumentInfo(TargetDocumentSpec targetDocumentSpec, int documentId, int totalNumberOfTargetDocuments)
    {
        var title = targetDocumentSpec.Title.Replace("{x}", documentId.ToString());
        return new TargetDocumentModel(documentId, title, targetDocumentSpec.Fullname, BuildTargetSectionsInfo(targetDocumentSpec.SectionSpecs), targetDocumentSpec.Relations);
    }

    private static IEnumerable<TargetSectionModel> BuildTargetSectionsInfo(IReadOnlyList<SectionSpec> sections)
    {
        int sectionId = 0;
        foreach (var section in sections)
        {
            foreach (var _ in section.Count.Range0())
            {
                yield return new TargetSectionModel($"s{sectionId}", section.Relations);
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
                yield return new SourceSectionModel($"s{sectionId}", new Relations());
                sectionId++;
            }
        }
    }
}