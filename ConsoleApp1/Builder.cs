namespace ConsoleApp1;

public static class Builder
{
    public static IEnumerable<StaticRelationInfo> BuildManyToOneStaticRelations(ManyStaticRelationToOne specification, IEnumerable<SourceDocumentInfo> sourceDocumentInfos, TargetDocumentInfo targetDocumentInfo)
    {
        if (specification.Generate == false) yield break;

        int i = 0;

        foreach (SourceDocumentInfo documentInfo in sourceDocumentInfos)
        {
            yield return new StaticRelationInfo(i, documentInfo, targetDocumentInfo, specification.RelationTypeCode);
            i++;
        }
    }


    //public static IReadOnlyList<SourceDocumentInfo> BuildSourceDocumentInfo(SourceDocuments sourceDocumentSpecification)
    //{
    //    List<SourceDocumentInfo> result = new List<SourceDocumentInfo>((int)sourceDocumentSpecification.Count);
    //    foreach (int i in sourceDocumentSpecification.Count.Range1())
    //    {
    //        var documentInfo = new SourceDocumentInfo(i, $"Source document No. {i} of {sourceDocumentSpecification.Count}", BuildSectionsInfo());
    //        result.Add(documentInfo);
    //    }
    //    return result;
    //}

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
        return new TargetDocumentInfo(documentId, title, BuildSectionsInfo(targetDocumentSpec.SectionSpecs), new RelationsInfo(targetDocumentSpec.NumberOfStaticRelations, targetDocumentSpec.NumberOfRangedTargetRelations, targetDocumentSpec.NumberOfSingleTargetRelations));
    }

    public static IEnumerable<SectionInfo> BuildSectionsInfo(IReadOnlyList<SectionSpec> sections)
    {
        int sectionId = 0;
        foreach (var section in sections)
        {
            foreach (var unused in section.Count.Range0())
            {
                yield return new SectionInfo($"BM{sectionId}", new RelationsInfo(section.NumberOfStaticRelations, section.NumberOfRangedTargetRelations, section.NumberOfSingleTargetRelations));
                sectionId++;
            }
        }
    }
}