using Microsoft.Extensions.Caching.Memory;
using Server.Database.Entities.ProjectManagements;
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
using Shared.Models.Deliverables.Records;
using System.Collections.Concurrent;
using System.Diagnostics;
namespace Server.EndPoint.Deliverables.Queries
{


    public static class GetAllDeliverableEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.GetAll, async (DeliverableGetAll request, IQueryRepository repository) =>
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
                            return Result<DeliverableResponseListToUpdate>.Fail("No deliverables found.");
                        }

                        // Mapear en paralelo
                        var flatList = await MapFlatInParallelAsync(rows);

                        // Reconstruir jerarquía en paralelo
                        var hierarchy = RebuildHierarchyInParallel(flatList);

                        // Reconstruir dependencias en paralelo
                        RebuildDependantsInParallel(flatList, rows);

                        sw.Stop();

                        Console.WriteLine($"Consulting Deliverable GetAll By Project {sw.ElapsedMilliseconds} ms");
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

                if (cache == null)
                {
                    return null!;
                }
                Func<IQueryable<Deliverable>, IIncludableQueryable<Deliverable, object>> includes = x => x
                   .Include(x => x.Dependants)
                   .Include(x => x.BudgetItems);

                Expression<Func<Deliverable, bool>> criteria = x => x.ProjectId == projectId;
                var data = await repository.GetAllAsync(
                    Cache: cache,
                    Criteria: criteria,
                    Includes: includes
                );


                return data;
            }

            private static async Task<List<DeliverableResponse>> MapFlatInParallelAsync(List<Deliverable> rows)
            {
                var tasks = rows.Select(async row =>
                {
                    return await Task.Run(() => row.MapFlat());
                });

                // Convertir el resultado de Task.WhenAll (arreglo) a una lista
                return (await Task.WhenAll(tasks)).ToList();
            }

            private static List<DeliverableResponse> RebuildHierarchyInParallel(List<DeliverableResponse> flatList)
            {
                var idToItemMap = flatList.ToDictionary(item => item.Id);
                var rootItems = new ConcurrentBag<DeliverableResponse>();

                Parallel.ForEach(flatList, item =>
                {
                    if (!item.ParentDeliverableId.HasValue)
                    {
                        rootItems.Add(item);
                    }
                    else if (idToItemMap.TryGetValue(item.ParentDeliverableId.Value, out var parent))
                    {
                        lock (parent.SubDeliverables)
                        {
                            parent.SubDeliverables.Add(item);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"ParentDeliverableId '{item.ParentDeliverableId}' not found.");
                    }
                });

                return rootItems.ToList();
            }

            private static void RebuildDependantsInParallel(List<DeliverableResponse> flatList, List<Deliverable> rows)
            {
                var flatListDict = flatList.ToDictionary(x => x.Id);

                Parallel.ForEach(rows.Where(r => r.Dependants.Any()), row =>
                {
                    if (flatListDict.TryGetValue(row.Id, out var mappedRow))
                    {
                        foreach (var dependant in row.Dependants)
                        {
                            if (flatListDict.TryGetValue(dependant.Id, out var mappedDependant))
                            {
                                lock (mappedRow.Dependants)
                                {
                                    mappedRow.Dependants.Add(mappedDependant);
                                }
                            }
                        }
                    }
                });
            }
        }
    }

    // Extension method para mapear un Deliverable a DeliverableResponse
    public static class DeliverableExtensions
    {
        public static DeliverableResponse MapParallel(this Deliverable row)
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

                Alterations = row.BudgetItems?.OfType<Alteration>().Select(x => x.Map()).ToList() ?? new(),
                Structurals = row.BudgetItems?.OfType<Structural>().Select(x => x.Map()).ToList() ?? new(),
                Foundations = row.BudgetItems?.OfType<Foundation>().Select(x => x.Map()).ToList() ?? new(),
                Equipments = row.BudgetItems?.OfType<Equipment>().Select(x => x.Map()).ToList() ?? new(),

                Valves = row.BudgetItems?.OfType<Valve>().Select(x => x.Map()).ToList() ?? new(),
                Electricals = row.BudgetItems?.OfType<Electrical>().Select(x => x.Map()).ToList() ?? new(),
                Pipings = row.BudgetItems?.OfType<Pipe>().Select(x => x.Map()).ToList() ?? new(),
                Instruments = row.BudgetItems?.OfType<Instrument>().Select(x => x.Map()).ToList() ?? new(),

                EHSs = row.BudgetItems?.OfType<EHS>().Select(x => x.Map()).ToList() ?? new(),
                Paintings = row.BudgetItems?.OfType<Painting>().Select(x => x.Map()).ToList() ?? new(),
                Taxes = row.BudgetItems?.OfType<Tax>().Select(x => x.Map()).ToList() ?? new(),
                Testings = row.BudgetItems?.OfType<Testing>().Select(x => x.Map()).ToList() ?? new(),

                EngineeringDesigns = row.BudgetItems?.OfType<EngineeringDesign>().Select(x => x.Map()).ToList() ?? new(),
                ProjectName = row.Project?.Name ?? string.Empty,
            };
        }
    }
}
