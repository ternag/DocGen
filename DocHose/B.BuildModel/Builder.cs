using DocHose.C.CreateDocuments;
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

public static class FamilyBuilder
{
    public static TargetFamilyModel BuildMemberDocuments(FamilySpec familySpec, DocumentCounter counter)
    {
        var members = Builder.BuildTargetDocuments(familySpec.TargetDocuments, counter);
        var family = new TargetFamilyModel(familySpec.FamilyName, members.ToArray());
        return family.OrderMembers().CreateVersionChain().SetDatesByStatus();
    }

    private static TargetFamilyModel OrderMembers(this TargetFamilyModel family)
    {
        family.MemberDocuments = family.MemberDocuments.OrderBy(x => x.Status).ToArray();
        return family;
    }

    private static TargetFamilyModel SetDatesByStatus(this TargetFamilyModel family)
    {
        family.MemberDocuments.Select()
        return family;
    }

    private static TargetFamilyModel CreateVersionChain(this TargetFamilyModel family)
    {
        if (family.MemberDocuments.Length <= 1) return family;
        for (var i = 1; i < family.MemberDocuments.Length; i++)
        {
            
        }

        return family;
    }
}
