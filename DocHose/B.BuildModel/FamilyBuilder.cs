using DocHose.A.ParseSpecification;

namespace DocHose.B.BuildModel;

public static class FamilyBuilder
{
    public static TargetFamilyModel BuildMemberDocuments(FamilySpec familySpec, DocumentCounter counter)
    {
        var members = Builder.BuildTargetDocuments(familySpec.TargetDocuments, counter);
        var family = new TargetFamilyModel(familySpec.FamilyName, members.ToArray());
        return family.OrderMembers().CreateVersionChain().SetDatesByStatus(familySpec);
    }

    private static TargetFamilyModel OrderMembers(this TargetFamilyModel family)
    {
        family.MemberDocuments = family.MemberDocuments.OrderBy(x => x.Status).ToArray();
        return family;
    }

    private static TargetFamilyModel SetDatesByStatus(this TargetFamilyModel family, FamilySpec familySpec)
    {
        var historic = family.MemberDocuments.Where(x => x.Status == Status.Historic).ToList();
        int startDaysAgo = -historic.Count * familySpec.VersionTimespanInDays;
        DateTime workingDate = DateTime.Today.AddDays(startDaysAgo);
        foreach (var targetDocumentModel in historic)
        {
            targetDocumentModel.EffectiveDate = workingDate;
            targetDocumentModel.RepealDate = workingDate.AddDays(familySpec.VersionTimespanInDays - 1);
            workingDate = workingDate.AddDays(familySpec.VersionTimespanInDays);
        }

        var effective = family.MemberDocuments.Where(x => x.Status == Status.Effective).ToList();
        foreach (var targetDocumentModel in effective)
        {
            targetDocumentModel.EffectiveDate = DateTime.Today;
            targetDocumentModel.RepealDate = null;
        }

        workingDate = workingDate.AddDays(familySpec.VersionTimespanInDays);
        var future = family.MemberDocuments.Where(x => x.Status == Status.Future).ToList();
        foreach (var targetDocumentModel in future)
        {
            targetDocumentModel.EffectiveDate = workingDate;
            targetDocumentModel.RepealDate = null;
            workingDate = workingDate.AddDays(familySpec.VersionTimespanInDays);
        }

        var indeterminate = family.MemberDocuments.Where(x => x.Status == Status.Indeterminate).ToList();
        foreach (var targetDocumentModel in indeterminate)
        {
            targetDocumentModel.EffectiveDate = DateTime.MaxValue;
            targetDocumentModel.RepealDate = null;
        }
        
        return family;
    }

    // TODO: 
    private static TargetFamilyModel CreateVersionChain(this TargetFamilyModel family)
    {
        if (family.MemberDocuments.Length <= 1) return family;
        for (var i = 1; i < family.MemberDocuments.Length; i++)
        {
            
        }

        return family;
    }
}
