using System.Text.Json;
using AutoFixture;
using ConsoleApp1;
using ConsoleApp1.A.ParseSpecification;
using ConsoleApp1.B.BuildModel;
using FluentAssertions;
using Xunit.Abstractions;

namespace TestProject1
{
    public class BuilderTests
    {
        private readonly ITestOutputHelper _output;
        private IFixture _fixture;

        public BuilderTests(ITestOutputHelper output)
        {
            _output = output;
            _fixture = new Fixture();
        }

        [Fact]
        public void GivenSectionSpecification_ReturnsExpectedNumberOfSections()
        {
            var testData = new List<SectionSpec> { new() { Count = 1 }, new() { Count = 6 }, new() { Count = 110 } };
            IEnumerable<SectionModel> sectionInfos = Builder.BuildSourceSectionsInfo(testData);

            sectionInfos.Count().Should().Be(117);
        }

        [Fact]
        public void GivenSourceDocumentSpec_ModelIsCorrect()
        {
            IReadOnlyList<SourceDocumentModel> actual = Builder.BuildSourceDocuments(SourceDocumentSpec);

            actual.Should().NotBeNull();
            actual.Count.Should().Be(3);
            actual[0].Id.Should().Be(1);
            actual[1].Id.Should().Be(2);
            actual[2].Id.Should().Be(3);
            actual[0].Fullname.Should().Be("KNOWN_FULLNAME-1");
            actual[1].Fullname.Should().Be("KNOWN_FULLNAME-2");
            actual[2].Fullname.Should().Be("KNOWN_FULLNAME-3");
            actual[0].Title.Should().Be("KNOWN TITLE // No. 1 of 3");
            actual[1].Title.Should().Be("KNOWN TITLE // No. 2 of 3");
            actual[2].Title.Should().Be("KNOWN TITLE // No. 3 of 3");
            actual[0].Sections.Count().Should().Be(2);
            actual[1].Sections.Count().Should().Be(2);
            actual[2].Sections.Count().Should().Be(2);
            actual[0].Sections.ToList()[0].Id.Should().Be("s0");
            actual[0].Sections.ToList()[1].Id.Should().Be("s1");

            _output.WriteLine(JsonSerializer.Serialize(actual));
        }

        [Fact]
        public void GivenTargetDocumentSpec_ModelIsCorrect()
        {
            List<TargetDocumentModel> actual = Builder.BuildTargetDocuments(new[] { TargetDocumentSpec }).ToList();

            actual.Should().NotBeNull();
            actual.Count.Should().Be(3);
            actual[0].Id.Should().Be(1);
            actual[1].Id.Should().Be(2);
            actual[2].Id.Should().Be(3);
            actual[0].Fullname.Should().Be("KNOWN_FULLNAME-1");
            actual[1].Fullname.Should().Be("KNOWN_FULLNAME-2");
            actual[2].Fullname.Should().Be("KNOWN_FULLNAME-3");
            actual[0].Title.Should().Be("KNOWN TITLE // No. 1 of 3");
            actual[1].Title.Should().Be("KNOWN TITLE // No. 2 of 3");
            actual[2].Title.Should().Be("KNOWN TITLE // No. 3 of 3");
            actual[0].Sections.Count().Should().Be(2);
            actual[1].Sections.Count().Should().Be(2);
            actual[2].Sections.Count().Should().Be(2);
            actual[0].Sections.ToList()[0].Id.Should().Be("s0");
            actual[0].Sections.ToList()[1].Id.Should().Be("s1");

            _output.WriteLine(JsonSerializer.Serialize(actual));
        }

        [Fact]
        public void Given()
        {
            IReadOnlyList<SourceDocumentModel> sourceDocumentModels = Builder.BuildSourceDocuments(SourceDocumentSpec);
            IEnumerable<TargetDocumentModel> targetDocumentModels = Builder.BuildTargetDocuments(new[] { TargetDocumentSpec });

            StaticRelationBuilder.Create(targetDocumentModels, sourceDocumentModels);

            sourceDocumentModels[0].Relations.StaticRelations.Count.Should().Be(9);

            _output.WriteLine(JsonSerializer.Serialize(sourceDocumentModels, new JsonSerializerOptions { WriteIndented = true }));
        }

        private SourceDocumentsSpec SourceDocumentSpec => new SourceDocumentsSpec(
            3,
            "KNOWN TITLE",
            "KNOWN_FULLNAME",
            new[]
            {
                new SectionSpec(2,
                    new[]
                    {
                        new RelationSpec(4, RelationKind.Static, "KNOWN_RTC")
                    }
                )
            });

        private TargetDocumentSpec TargetDocumentSpec => new TargetDocumentSpec(
            3,
            "KNOWN TITLE",
            "KNOWN_FULLNAME",
            new[]
            {
                new SectionSpec(2, new[] { new RelationSpec(3, RelationKind.Static, "KNOWN_RTC_STATIC") })
            },
            new[]
            {
                new RelationSpec(3, RelationKind.Static, "KNOWN_RTC_STATIC"),
                new RelationSpec(5, RelationKind.RangedTarget, "KNOWN_RTC_RANGED"),
                new RelationSpec(7, RelationKind.SingleTarget, "KNOWN_RTC_SINGLE")
            }
        );
    }
}
