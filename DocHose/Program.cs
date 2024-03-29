﻿using System.Xml.Linq;
using DocHose.A.ParseSpecification;
using DocHose.B.BuildModel;
using DocHose.C.CreateDocuments;

namespace DocHose;

/* To do:
X Specify static relations in target document
X Add static relation(s) to source document metadata
X Add NumberOfSections to a document
- Make SourceDocumentSpec a list in Specification record
- Build document family (how to specify family?)
- Build DynamicRelations to target family
- Be able to indicate if source of relation should be a section bookmark
- Fill in blanks in metadata file.
- zip and send documents to Fundament
- Be able to specify section body in specification.
*/

public static class Program
{
    public static void Main(string[] args)
    {
        string specificationText = File.ReadAllText("./specification.json");
        Specification specification = SpecificationLoader.Parse(specificationText);

        string outputDir = "/temp/GenerateDocPoc/output";
        Directory.CreateDirectory(outputDir);

        GenerateDocuments(specification, outputDir);
    }

    private static void GenerateDocuments(Specification specification, string outputDir)
    {
        var generator = new XDocumentCreator();

        var targetDocumentModels = Builder.BuildTargetDocuments(specification.TargetDocumentsSpec).ToList();
        var sourceDocumentModels = Builder.BuildSourceDocuments(specification.SourceDocumentsSpec);

        StaticRelationBuilder.Create(targetDocumentModels, sourceDocumentModels);

        foreach (var sourceDocumentModel in sourceDocumentModels)
        {
            var documentXml = generator.CreateSourceDocuments(sourceDocumentModel);
            var metadataXml = generator.CreateMetadata(sourceDocumentModel);
            metadataXml.AddStaticRelations(sourceDocumentModel);
            SaveDocument(documentXml, metadataXml, sourceDocumentModel.Fullname, outputDir);
        }

        foreach (var targetDocumentModel in targetDocumentModels)
        {
            var documentXml = generator.CreateTargetDocuments(targetDocumentModel);
            var metadataXml = generator.CreateMetadata(targetDocumentModel);
            SaveDocument(documentXml, metadataXml, targetDocumentModel.Fullname, outputDir);
        }
    }

    private static void SaveDocument(XDocument document, XDocument metadata, string fullname, string outputDir)
    {
        string documentDir = Path.Combine(outputDir, fullname);
        Directory.CreateDirectory(documentDir);
        document.Save(Path.Combine(documentDir, $"document.xml"));
        metadata.Save(Path.Combine(documentDir, $"metadata.xml"));
    }
}
