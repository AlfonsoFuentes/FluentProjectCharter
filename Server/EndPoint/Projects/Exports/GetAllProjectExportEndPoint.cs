
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Shared.Models.AcceptanceCriterias.Responses;
using System.Text;
namespace Server.EndPoint.Projects.Exports
{
    public static class GetAllProjectExportEndPoint
    {

        public class EndPoint : IEndPoint
        {
            ProjectResponse response = null!;
            byte[] CPLogo = null!;
            byte[] PMLogo = null!;

            StringBuilder mesajes = new StringBuilder();
            void GetImageData(IWebHostEnvironment host)
            {
                var path = host.WebRootPath;

                if (path == null)
                {
                    mesajes.Append("path not found");

                    return;
                }
                Console.WriteLine(path);
                var rutaImagen = Path.Combine(path, "Assets/CPLogo.PNG");
                CPLogo = System.IO.File.ReadAllBytes(rutaImagen);

                mesajes.Append($"CPLogo: created");
                rutaImagen = Path.Combine(path, "Assets/PMLogo.PNG");
                PMLogo = System.IO.File.ReadAllBytes(rutaImagen);
                mesajes.Append($"PMLogo: created");
            }
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.Export, (ProjectGetAllExport request, [FromServices] IWebHostEnvironment host) =>
                {

                    mesajes.Append("Ingresado a exportar");
                    GetImageData(host);

                    response = request.ProjectResponse;
                    var responsePDF = CreatePDF(request.ProjectResponse);


                    return Result<Shared.Models.FileResults.FileResult>.Success(responsePDF, mesajes.ToString());
                });
            }
            Shared.Models.FileResults.FileResult CreatePDF(ProjectResponse response)
            {
                byte[] pdfBytes = GenerateReportBytes(response);

                Shared.Models.FileResults.FileResult newresult = new()
                {
                    Data = pdfBytes,
                    ExportFileName = $"Project Charter {response.Name}.pdf",
                    ContentType = Shared.Models.FileResults.FileResult.pdfContentType,
                };

                return newresult;
            }
            byte[] GenerateReportBytes(ProjectResponse response)
            {
                byte[] reportBytes;
                Document document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(30);

                        page.Size(PageSizes.Letter.Portrait());
                        page.Margin(0.8f, QuestPDF.Infrastructure.Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));
                        page.Header().ShowOnce().Row(row =>
                        {
                            if (CPLogo == null)
                            {
                                row.ConstantItem(100).Column(col =>
                                {
                                    col.Item().AlignCenter().Text("Colgate Palmolive").FontColor("#422ef2").Bold().FontSize(16).Italic();

                                });
                            }
                            else
                            {
                                row.ConstantItem(100).Image(CPLogo);
                            }

                            row.RelativeItem().Column(col =>
                            {
                                col.Item().AlignCenter().Text("Confidential").FontSize(14);

                            });
                            if (PMLogo == null)
                            {

                                row.ConstantItem(100).Column(col =>
                                {
                                    col.Item().AlignCenter().Text("Project Management").FontColor("#422ef2").Bold().FontSize(16).Italic();

                                });
                            }
                            else
                            {
                                row.ConstantItem(100).Image(PMLogo);
                            }

                        });
                        page.Footer().AlignRight().Text(txt =>
                        {
                            txt.Span("Page ").FontSize(10);
                            txt.CurrentPageNumber().FontSize(10);
                            txt.Span(" of ").FontSize(10);
                            txt.TotalPages().FontSize(10);
                        });
                        page.Content().PaddingVertical(10).Column(col1 =>
                        {
                            col1.Item().PaddingBottom(15).Column(col2 =>
                            {
                                col2.Item().Background(Colors.Grey.Lighten2).Text("Project Charter Statement").FontSize(20).AlignCenter();
                            });
                            col1.Item().Element(ProjectNameContent);
                            col1.Item().Element(ProjectDescriptionContent);

                            col1.Item().Element(ProjectManagerContent);
                            col1.Item().Element(SponsorContent);
                            col1.Item().Element(InitialBudgetContent);

                            col1.Item().Element(HighLevelContent);
                            col1.Item().Element(StakeHoldersContent);
                            col1.Item().Element(BusinsesCaseContent);
                            col1.Item().Element(ProjectRequirementsContent);
                            col1.Item().Element(ProjectAssumptionContent);
                            col1.Item().Element(ProjectConstrainstsContent);

                            col1.Item().LineHorizontal(0.5f);

                            col1.Item().Element(SignContent);
                        });

                    });
                });

                reportBytes = document.GeneratePdf();

                return reportBytes;
            }

            void ProjectNameContent(IContainer container)
            {
                container.Column(col1 =>
                {

                    col1.Item().PaddingBottom(5).Column(col2 =>
                    {
                        col2.Item().Text(txt =>
                        {
                            txt.Span("Project Name: ").SemiBold().FontSize(20);
                            txt.Span(response.Name).SemiBold().FontSize(20);
                        });
                    });
                });
            }
            void ProjectDescriptionContent(IContainer container)
            {
                container.Column(col1 =>
                {

                    if (!string.IsNullOrEmpty(response.ProjectDescription))
                    {
                        col1.Item().PaddingBottom(5).Column(col2 =>
                        {
                            col2.Item().Text(txt =>
                            {
                                txt.Span("Description:").SemiBold().FontSize(16);

                            });
                        });
                        col1.Item().PaddingBottom(15).Column(col2 =>
                        {
                            col2.Item().Text(txt =>
                            {
                                txt.Span(response.ProjectDescription).FontSize(11);

                            });
                        });

                    }
                });
            }
            void ProjectManagerContent(IContainer container)
            {
                if (response.Manager == null) return;
                container.Column(col1 =>
                {

                    col1.Item().PaddingBottom(5).Column(col2 =>
                    {
                        col2.Item().Text(txt =>
                        {
                            txt.Span("Project Manager: ").FontSize(11);
                            txt.Span(response.Manager.Name).FontSize(11);
                        });
                    });
                });
            }
            void SponsorContent(IContainer container)
            {
                if (response.Sponsor == null) return;
                container.Column(col1 =>
                {

                    col1.Item().PaddingBottom(15).Column(col2 =>
                    {
                        col2.Item().Text(txt =>
                        {
                            txt.Span("Sponsor: ").FontSize(11);
                            txt.Span(response.Sponsor.Name).FontSize(11);
                        });
                    });
                });
            }
            void InitialBudgetContent(IContainer container)
            {
                if (response.InitialBudget == 0) return;
                container.Column(col1 =>
                {

                    col1.Item().PaddingBottom(15).Column(col2 =>
                    {
                        col2.Item().Text(txt =>
                        {
                            txt.Span("Initial Budget, USD: ").FontSize(11);
                            string value = String.Format(new System.Globalization.CultureInfo("en-US"), "{0:C0}", response.InitialBudget);
                            txt.Span(value).FontSize(11).SemiBold();
                        });
                    });
                });
            }
            void HighLevelContent(IContainer container)
            {
                if (response.HighLevelRequirements.Count == 0) return;
                container.Column(col1 =>
                {

                    col1.Item().PaddingBottom(10).Column(col2 =>
                    {
                        col2.Item().Text(txt =>
                        {
                            txt.Span("High Level Requirements").FontSize(12).SemiBold();

                        });
                    });
                    col1.Item().PaddingBottom(10).Column(col2 =>
                    {
                        foreach (var row in response.HighLevelRequirements)
                        {
                            col2.Item().Text(txt =>
                            {
                                txt.Span($"- {row.Name}").FontSize(11);

                            });

                        }

                    });
                });
            }
            void StakeHoldersContent(IContainer container)
            {
                if (response.StakeHolders.Count == 0) return;
                container.Column(col1 =>
                {

                    col1.Item().PaddingBottom(10).Column(col2 =>
                    {
                        col2.Item().Text(txt =>
                        {
                            txt.Span("StakeHolders").FontSize(12).SemiBold();

                        });
                    });
                    col1.Item().PaddingBottom(10).Table(table => GetStakeHoldersTable(table));
                });
            }
            void BusinsesCaseContent(IContainer container)
            {
                if (response.Cases.Count == 0) return;
                container.Column(col1 =>
                {

                    col1.Item().PaddingBottom(10).Column(col2 =>
                    {
                        col2.Item().Text(txt =>
                        {
                            txt.Span("Business Cases:").FontSize(12).SemiBold();

                        });
                    });
                    var intcase = 0;

                    col1.Item().TranslateX(10).Column(col2 =>
                    {
                        intcase++;
                        foreach (var row in response.Cases)
                        {
                            col1.Item().TranslateX(10).Column(col => GetCase(col, row));

                        }

                    });
                });
            }
            void ProjectAssumptionContent(IContainer container)
            {
                if (response.Assumptions.Count == 0) return;
                container.Column(col1 =>
                {

                    col1.Item().PaddingBottom(10).Column(col2 =>
                    {
                        col2.Item().Text(txt =>
                        {
                            txt.Span("General Assumption:").FontSize(12).SemiBold();

                        });
                    });
                  

                    col1.Item().TranslateX(10).Column(col2 =>
                    {
                        GetAssumption(col2, response.Assumptions);

                    });
                });
            }
            void ProjectConstrainstsContent(IContainer container)
            {
                if (response.Constrainsts.Count == 0) return;
                container.Column(col1 =>
                {

                    col1.Item().PaddingBottom(10).Column(col2 =>
                    {
                        col2.Item().Text(txt =>
                        {
                            txt.Span("General Constrainsts:").FontSize(12).SemiBold();

                        });
                    });
       

                    col1.Item().TranslateX(10).Column(col2 =>
                    {
                        GetConstrainst(col2, response.Constrainsts);


                    });
                });
            }
            void ProjectRequirementsContent(IContainer container)
            {
                if (response.Requirements.Count == 0) return;
                container.Column(col1 =>
                {

                    col1.Item().PaddingBottom(10).Column(col2 =>
                    {
                        col2.Item().Text(txt =>
                        {
                            txt.Span("General Constrainsts:").FontSize(12).SemiBold();

                        });
                    });


                    col1.Item().TranslateX(10).Column(col2 =>
                    {
                        GetRequirements(col2, response.Requirements);


                    });
                });
            }
            void SignContent(IContainer container)
            {

                container.Column(col1 =>
                {

                    col1.Item().PaddingBottom(10).Column(col2 =>
                    {
                        col2.Item().Text(txt =>
                        {
                            txt.Span("Approvals").FontSize(12).SemiBold();

                        });
                    });
                    col1.Item().Table(table => GetSign(table));
                });
            }
            ColumnDescriptor GetCase(ColumnDescriptor col2, CaseResponse row)
            {


                col2.Item().PaddingBottom(10).Text(txt =>
                {
                    txt.Span($"- {row.Name}").FontSize(11).SemiBold();


                    col2.Item().TranslateX(10).PaddingBottom(5).Column(col3 => GetBackground(col3, row.BackGrounds));
                    col2.Item().TranslateX(10).PaddingBottom(5).Column(col3 => GetExperts(col3, row.ExpertJudgements));
                    col2.Item().TranslateX(10).PaddingBottom(5).Column(col3 => GetKnowrisks(col3, row.KnownRisks));
                    col2.Item().TranslateX(10).PaddingBottom(5).Column(col3 => GetSucessfull(col3, row.SucessfullFactors));
                    col2.Item().TranslateX(10).PaddingBottom(5).Column(col3 => GetDecission(col3, row.DecissionCriterias));
                    col2.Item().TranslateX(10).PaddingBottom(5).Column(col3 => GetScopes(col3, row.Scopes));

                });

                return col2;
            }
            ColumnDescriptor GetBackground(ColumnDescriptor col3, List<BackGroundResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Backgrounds: ").FontSize(10).SemiBold();

                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });

                }

                return col3;
            }
            ColumnDescriptor GetExperts(ColumnDescriptor col3, List<ExpertJudgementResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Expert Judgements: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });

                }

                return col3;
            }
            ColumnDescriptor GetKnowrisks(ColumnDescriptor col3, List<KnownRiskResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Known risk: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });

                }

                return col3;
            }
            ColumnDescriptor GetSucessfull(ColumnDescriptor col3, List<SucessfullFactorResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Sucessfull factors: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });

                }

                return col3;
            }
            ColumnDescriptor GetDecission(ColumnDescriptor col3, List<DecissionCriteriaResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Decission criterias: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });

                }

                return col3;
            }
            ColumnDescriptor GetScopes(ColumnDescriptor col3, List<ScopeResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Scopes: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });
                    col3.Item().TranslateX(25).PaddingBottom(5).Column(col4 => GetDeliverable(col4, row.Deliverables));
                    col3.Item().TranslateX(25).PaddingBottom(5).Column(col4 => GetRequirements(col4, row.Requirements));
                    col3.Item().TranslateX(25).PaddingBottom(5).Column(col4 => GetAssumption(col4, row.Assumptions));
                    col3.Item().TranslateX(25).PaddingBottom(5).Column(col4 => GetConstrainst(col4, row.Constrainsts));
                    col3.Item().TranslateX(25).PaddingBottom(5).Column(col4 => GetDeliverableRisk(col4, row.DeliverableRisks));
                    col3.Item().TranslateX(25).PaddingBottom(5).Column(col4 => GetAcceptanceCriteria(col4, row.AcceptanceCriterias));
                    col3.Item().TranslateX(25).PaddingBottom(5).Column(col4 => GetBennefits(col4, row.Bennefits));

                }

                return col3;
            }
            ColumnDescriptor GetDeliverable(ColumnDescriptor col3, List<DeliverableResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Deliverables: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });
                   

                }

                return col3;
            }
            ColumnDescriptor GetAssumption(ColumnDescriptor col3, List<AssumptionResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Assumptions: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });

                }

                return col3;
            }
            ColumnDescriptor GetRequirements(ColumnDescriptor col3, List<RequirementResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Requirements: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });

                }

                return col3;
            }
            ColumnDescriptor GetDeliverableRisk(ColumnDescriptor col3, List<DeliverableRiskResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Deliverable risks: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });

                }

                return col3;
            }
            ColumnDescriptor GetConstrainst(ColumnDescriptor col3, List<ConstrainstResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Constrainst: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });

                }

                return col3;
            }
            ColumnDescriptor GetBennefits(ColumnDescriptor col3, List<BennefitResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Bennefits: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });

                }

                return col3;
            }
            ColumnDescriptor GetAcceptanceCriteria(ColumnDescriptor col3, List<AcceptanceCriteriaResponse> rows)
            {
                if (rows.Count == 0) return col3;
                col3.Item().TranslateX(10).PaddingBottom(5).Text(txt =>
                {
                    txt.Span($"Acceptance Criterias: ").FontSize(10).SemiBold();
                });
                foreach (var row in rows)
                {

                    col3.Item().TranslateX(20).PaddingBottom(5).Text(txt =>
                    {
                        txt.Span($"- {row.Name}").FontSize(10);

                    });

                }

                return col3;
            }
            TableDescriptor GetSign(TableDescriptor tabla)
            {
                tabla.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);

                });

                tabla.Header(header =>
                {
                    header.Cell().Border(0.5f).BorderColor("#D9D9D9")
                    .Padding(4).Text("Name").Bold();

                    header.Cell().Border(0.5f).BorderColor("#D9D9D9")
                   .Padding(4).Text("Role").Bold();

                    header.Cell().Border(0.5f).BorderColor("#D9D9D9")
                   .Padding(4).Text("Sign").Bold();

                    header.Cell().Border(0.5f).BorderColor("#D9D9D9")
                    .Padding(4).Text("Sign date").Bold();


                });
                
                foreach (var expert in response.ProjectSigninList)
                {
                    tabla.Cell().Border(0.5f).BorderColor("#D9D9D9")
                    .Padding(4).Text(expert.Name).FontSize(10);

                    tabla.Cell().Border(0.5f).BorderColor("#D9D9D9")
                    .Padding(4).Text("Expert").FontSize(10);

                    tabla.Cell().Border(0.5f).BorderColor("#D9D9D9")
                    .Padding(4).Text(string.Empty).FontSize(10);

                    tabla.Cell().Border(0.5f).BorderColor("#D9D9D9")
                    .Padding(4).Text(string.Empty).FontSize(10);
                }
               
                return tabla;
            }

            TableDescriptor GetStakeHoldersTable(TableDescriptor tabla)
            {
                tabla.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);


                });

                tabla.Header(header =>
                {
                    header.Cell()
                    .Padding(4).Text("Name").Bold();

                    header.Cell()
                   .Padding(4).Text("Role").Bold();




                });
                if (response.Sponsor != null)
                {
                    tabla.Cell()
                   .Padding(4).Text(response.Sponsor.Name).FontSize(10);

                    tabla.Cell()
                    .Padding(4).Text("Sponsor").FontSize(10);


                }
                if (response.Manager != null)
                {
                    tabla.Cell()
                   .Padding(4).Text(response.Manager.Name).FontSize(10);

                    tabla.Cell()
                    .Padding(4).Text("Manager").FontSize(10);


                }
                foreach (var caseitem in response.Cases)
                {
                    
                    foreach (var expert in caseitem.ExpertJudgements)
                    {
                        tabla.Cell()
                        .Padding(4).Text(expert.Expert!.Name).FontSize(10);

                        tabla.Cell()
                        .Padding(4).Text("Expert").FontSize(10);

                    }
                }
                foreach (var stakeholder in response.StakeHolders)
                {
                    tabla.Cell()
                        .Padding(4).Text(stakeholder.Name).FontSize(10);

                    tabla.Cell()
                    .Padding(4).Text(stakeholder.Role.Name).FontSize(10);


                }
                return tabla;
            }
        }


    }
}
