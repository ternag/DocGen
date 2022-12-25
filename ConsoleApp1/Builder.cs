namespace ConsoleApp1;

public static class Builder
{
    public static IReadOnlyList<SourceDocumentInfo> BuildSourceDocuments(SourceDocuments sourceDocumentSpecification)
    {
        List<SourceDocumentInfo> result = new List<SourceDocumentInfo>((int)sourceDocumentSpecification.Count);
        foreach (int i in sourceDocumentSpecification.Count.Range1())
        {
            var documentInfo = new SourceDocumentInfo(i, $"Source document No. {i} of {sourceDocumentSpecification.Count}", sourceDocumentSpecification.Fullname, BuildSectionsInfo(sourceDocumentSpecification.SectionSpecs), new Relations());
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
        return new TargetDocumentInfo(documentId, title, targetDocumentSpec.Fullname, BuildSectionsInfo(targetDocumentSpec.SectionSpecs), targetDocumentSpec.Relations, new TmpRelationsInfo(targetDocumentSpec.NumberOfRangedTargetRelations, targetDocumentSpec.NumberOfSingleTargetRelations));
    }

    public static IEnumerable<SectionInfo> BuildSectionsInfo(IReadOnlyList<SectionSpec> sections)
    {
        int sectionId = 0;
        foreach (var section in sections)
        {
            foreach (var unused in section.Count.Range0())
            {
                yield return new SectionInfo($"s{sectionId}", new TmpRelationsInfo(section.NumberOfRangedTargetRelations, section.NumberOfSingleTargetRelations));
                sectionId++;
            }
        }
    }
}