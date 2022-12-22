using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1;

internal static class Builder
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


    public static TargetDocumentInfo BuildTargetDocumentInfo(TargetDocument targetDocument, int documentId, int totalNumberOfTargetDocuments)
    {
        var title = targetDocument.Title.Replace("{x}", documentId.ToString());
        return new TargetDocumentInfo(documentId, title, BuildSectionsInfo(targetDocument.Sections));
    }

    public static IEnumerable<SectionInfo> BuildSectionsInfo(IReadOnlyList<Section> sections)
    {
        int sectionId = 0;
        foreach (var section in sections)
        {
            foreach (var i in section.Count.Range0())
            {
                yield return new SectionInfo($"BM{sectionId}");
                sectionId++;
            }
        }
    }

    public static IEnumerable<TargetDocumentInfo> BuildTargetDocuments(IReadOnlyList<TargetDocument> targetDocumentsSpecification)
    {
        int documentId = 0;
        foreach (TargetDocument targetDocument in targetDocumentsSpecification)
        {
            yield return BuildTargetDocumentInfo(targetDocument, documentId, 0);
            documentId++;
        }
    }
}