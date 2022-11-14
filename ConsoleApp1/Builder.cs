using ConsoleApp1;

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


    public static IEnumerable<SourceDocumentInfo> BuildSourceDocumentInfo(SourceDocuments sourceDocumentSpecification)
    {
        List<SourceDocumentInfo> result = new List<SourceDocumentInfo>((int)sourceDocumentSpecification.Count);
        foreach (int i in sourceDocumentSpecification.Count.Range1())
        {
            var documentInfo = new SourceDocumentInfo(i, $"Source document No. {i} of {sourceDocumentSpecification.Count}", BuildSectionsInfo(sourceDocumentSpecification));
            result.Add(documentInfo);
        }
        return result;
    }


    public static IEnumerable<TargetDocumentInfo> BuildTargetDocumentInfo(TargetDocuments targetDocumentSpecification)
    {
        foreach (int i in targetDocumentSpecification.Count.Range1())
        {
            var documentInfo = new TargetDocumentInfo(i, $"Target document No. {i} of {targetDocumentSpecification.Count}", BuildSectionsInfo(targetDocumentSpecification));
            yield return documentInfo;
        }
    }

    public static IEnumerable<SectionInfo> BuildSectionsInfo(Documents specification)
    {
        foreach (int i in specification.NumberOfSections.Range1())
        {
            yield return new SectionInfo($"s{i}");
        }
    }

}