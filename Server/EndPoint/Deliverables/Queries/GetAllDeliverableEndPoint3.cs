using Server.Database.Entities.ProjectManagements;
using Shared.Models.Deliverables.Records;
using Server.EndPoint.BudgetItems.IndividualItems.Alterations.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.EHSs.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Electricals.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.EngineeringDesigns.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Equipments.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Foundations.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Instruments.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Paintings.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Pipes.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Structurals.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Taxs.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Testings.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Valves.Queries;
using Shared.Enums.TasksRelationTypeTypes;
using System.Diagnostics;
namespace Server.EndPoint.Deliverables.Queries
{
    public static class GetAllDeliverableEndPoint3
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost($"{StaticClass.Deliverables.EndPoint.GetAll}3", async (DeliverableGetAll request, IQueryRepository repository) =>
                {
                    try
                    {
                        Stopwatch sw = Stopwatch.StartNew();
                        // Validación inicial
                        if (!ValidateRequest(request, repository, out var validationError))
                        {
                            return Result<DeliverableResponseListToUpdate>.Fail(validationError);
                        }

                        // Cargar datos
                        var rows = await LoadAllDeliverablesFlat(request.ProjectId, repository);
                        if (rows == null || !rows.Any())
                        {
                            return Result<DeliverableResponseListToUpdate>.Fail(
                                StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Deliverables.ClassLegend));
                        }

                        // Mapear y reconstruir jerarquía
                        var flatList = rows.Select(x => x.MapFlat()).ToList();
                        var hierarchy = RebuildHierarchy(flatList);
                        RebuildDependants(flatList, rows);

                        // Validar jerarquía
                        if (!hierarchy.Any())
                        {
                            return Result<DeliverableResponseListToUpdate>.Fail("No deliverables found for the given ProjectId.");
                        }
                        sw.Stop();
                        var elapse4 = sw.Elapsed;
                        // Construir respuesta
                        var response = new DeliverableResponseListToUpdate
                        {
                            Items = hierarchy,
                            ProjectId = request.ProjectId,
                        };

                        return Result<DeliverableResponseListToUpdate>.Success(response);
                    }
                    catch (Exception ex)
                    {
                        return Result<DeliverableResponseListToUpdate>.Fail($"An error occurred: {ex.Message}");
                    }
                });
            }

            private static bool ValidateRequest(DeliverableGetAll request, IQueryRepository repository, out string errorMessage)
            {
                if (request == null || repository == null)
                {
                    errorMessage = "Invalid request: Request or repository is null.";
                    return false;
                }
                if (request.ProjectId == Guid.Empty)
                {
                    errorMessage = "Invalid request: Missing or invalid ProjectId.";
                    return false;
                }
                errorMessage = null!;
                return true;
            }

            private static async Task<List<Deliverable>> LoadAllDeliverablesFlat(Guid projectId, IQueryRepository repository)
            {
                var cache = StaticClass.Deliverables.Cache.GetAll(projectId);
                Func<IQueryable<Deliverable>, IIncludableQueryable<Deliverable, object>> includes = x => x
                    .Include(x => x.Dependants)
                    .Include(x => x.BudgetItems);

                Expression<Func<Deliverable, bool>> criteria = x => x.ProjectId == projectId;
                return await repository.GetAllAsync(Cache: cache, Criteria: criteria, Includes: includes);
            }

            private static List<DeliverableResponse> RebuildHierarchy(List<DeliverableResponse> flatList)
            {
                var idToItemMap = flatList.ToDictionary(item => item.Id);
                var rootItems = new List<DeliverableResponse>();

                foreach (var item in flatList)
                {
                    if (!item.ParentDeliverableId.HasValue)
                    {
                        rootItems.Add(item);
                    }
                    else if (idToItemMap.TryGetValue(item.ParentDeliverableId.Value, out var parent))
                    {
                        parent.SubDeliverables.Add(item);
                    }
                    else
                    {
                        throw new InvalidOperationException($"ParentDeliverableId '{item.ParentDeliverableId}' not found.");
                    }
                }

                return rootItems;
            }

            private static void RebuildDependants(List<DeliverableResponse> flatList, List<Deliverable> rows)
            {
                foreach (var row in rows.Where(r => r.Dependants.Any()))
                {
                    var mappedRow = flatList.SingleOrDefault(x => x.Id == row.Id);
                    if (mappedRow != null)
                    {
                        foreach (var dependant in row.Dependants)
                        {
                            var mappedDependant = flatList.SingleOrDefault(x => x.Id == dependant.Id);
                            if (mappedDependant != null)
                            {
                                mappedRow.Dependants.Add(mappedDependant);
                            }
                        }
                    }
                }
            }
        }
        public static DeliverableResponse MapFlat(this Deliverable row)
        {
            return new DeliverableResponse
            {
                ProjectId = row.ProjectId,
                DependencyType = string.IsNullOrEmpty(row.DependencyType)
                    ? TasksRelationTypeEnum.FinishStart
                    : TasksRelationTypeEnum.GetType(row.DependencyType),
                StartDate = row.StartDate,
                EndDate = row.EndDate,
                Duration = row.Duration,
                Lag = row.Lag,
                ParentDeliverableId = row.ParentDeliverableId,
                Id = row.Id,
                Name = row.Name,
                Order = row.Order,
                IsExpanded = row.IsExpanded,
                WBS = row.WBS,
                LabelOrder = row.LabelOrder,
                DependantId = row.DependentantId,

                Alterations = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Alteration>().Select(x => x.Map()).ToList(),
                Structurals = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Structural>().Select(x => x.Map()).ToList(),
                Foundations = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Foundation>().Select(x => x.Map()).ToList(),
                Equipments = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Equipment>().Select(x => x.Map()).ToList(),

                Valves = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Valve>().Select(x => x.Map()).ToList(),
                Electricals = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Electrical>().Select(x => x.Map()).ToList(),
                Pipings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Pipe>().Select(x => x.Map()).ToList(),
                Instruments = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Instrument>().Select(x => x.Map()).ToList(),

                EHSs = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<EHS>().Select(x => x.Map()).ToList(),
                Paintings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Painting>().Select(x => x.Map()).ToList(),
                Taxes = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Tax>().Select(x => x.Map()).ToList(),
                Testings = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<Testing>().Select(x => x.Map()).ToList(),

                EngineeringDesigns = row.BudgetItems == null || row.BudgetItems.Count == 0 ? new() : row.BudgetItems.OfType<EngineeringDesign>().Select(x => x.Map()).ToList(),
                ProjectName = row.Project == null ? string.Empty : row.Project.Name,

            };
        }
    }
}
