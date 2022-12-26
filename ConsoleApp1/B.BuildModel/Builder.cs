using ConsoleApp1.A.ParseSpecification;
using ConsoleApp1.C.CreateDocuments;

namespace ConsoleApp1.B.BuildModel;

public static class Builder
{
    public static IReadOnlyList<SourceDocumentInfo> BuildSourceDocuments(SourceDocuments sourceDocumentSpecification)
    {
        List<SourceDocumentInfo> result = new List<SourceDocumentInfo>((int)sourceDocumentSpecification.Count);
        foreach (int i in sourceDocumentSpecification.Count.Range1())
        {
            var documentInfo = new SourceDocumentInfo(i, $"Source document No. {i} of {sourceDocumentSpecification.Count}", sourceDocumentSpecification.Fullname, BuildSourceSectionsInfo(sourceDocumentSpecification.SectionSpecs), new Relations());
            result.Add(documentInfo);
        }
        return result;
    }

    public static IEnumerable<TargetDocumentInfo> BuildTargetDocuments(IReadOnlyList<TargetDocumentSpec> targetDocumentsSpecification)
    {
        int documentId = 0;
        foreach (TargetDocumentSpec targetDocument in targetDocumentsSpecification)
        {
            yield return BuildTargetDocumentInfo(targetDocument, documentId, 0);
            documentId++;
        }
    }

    public static TargetDocumentInfo BuildTargetDocumentInfo(TargetDocumentSpec targetDocumentSpec, int documentId, int totalNumberOfTargetDocuments)
    {
        var title = targetDocumentSpec.Title.Replace("{x}", documentId.ToString());
        return new TargetDocumentInfo(documentId, title, targetDocumentSpec.Fullname, BuildTargetSectionsInfo(targetDocumentSpec.SectionSpecs), targetDocumentSpec.Relations);
    }

    private static IEnumerable<TargetSectionInfo> BuildTargetSectionsInfo(IReadOnlyList<SectionSpec> sections)
    {
        int sectionId = 0;
        foreach (var section in sections)
        {
            foreach (var _ in section.Count.Range0())
            {
                yield return new TargetSectionInfo($"s{sectionId}", section.Relations);
                sectionId++;
            }
        }
    }

    public static IEnumerable<SourceSectionInfo> BuildSourceSectionsInfo(IReadOnlyList<SectionSpec> sections)
    {
        int sectionId = 0;
        foreach (var section in sections)
        {
            foreach (var _ in section.Count.Range0())
            {
                yield return new SourceSectionInfo($"s{sectionId}", new Relations());
                sectionId++;
            }
        }
    }
}