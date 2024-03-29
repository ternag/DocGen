﻿using System.Xml.Linq;
using DocHose.B.BuildModel;

namespace DocHose.C.CreateDocuments;

public static class XmlSourceDocumentExtensions
{
    public static XElement AddSourceDocumentInfo(this XElement article, DocumentModel model, XNamespace ns)
    {
        var element = new XElement(ns + "section",
            new XAttribute("id", "doc-info"),
            new XElement(ns + "h1", $"Document Fullname: '{model.Fullname}'"));
        article.Add(element);
        return article;
    }

    public static XElement AddSourceSections(this XElement article, IEnumerable<SourceSectionModel> sections, XNamespace ns)
    {
        foreach (var sourceSectionInfo in sections)
        {
            article.AddSourceSection(sourceSectionInfo, ns);
        }

        return article;
    }

    public static XElement AddSourceSection(this XElement article, SourceSectionModel sectionModel, XNamespace ns)
    {
        var section = new XElement(ns + "section",
            new XAttribute("id", sectionModel.Id),
            new XAttribute("class", sectionModel.Class),
            new XElement(ns + "h1", $"Section id='{sectionModel.Id}'"),
            new XElement(ns + "p", LoremIpsum.GetRandomLength()));
        article.Add(section);
        return section;
    }
}
