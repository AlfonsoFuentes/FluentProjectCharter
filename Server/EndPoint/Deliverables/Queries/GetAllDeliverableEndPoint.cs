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
using Shared.Models.Deliverables.Responses.NewResponses;

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
                    // Validación inicial
                    if (!ValidateRequest(request, repository, out var validationError))
                    {
                        return Result<DeliverableResponseList>.Fail(validationError);
                    }

                    try
                    {
                        // Cargar todos los deliverables en una lista plana
                        var rows = await LoadAllDeliverablesFlat(request.ProjectId, repository);
                        if (rows == null || !rows.Any())
                        {
                            return Result<DeliverableResponseList>.Fail(
                                StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Deliverables.ClassLegend));
                        }

                        // Mapear los datos a DeliverableResponse
                        var flattenlist = rows.Select(x => x.MapFlat()).ToList();

                        //Mappear los dependants


                        // Reconstruir la jerarquía
                        var maps = RebuildHierarchy(flattenlist);

                        RebuildDependants(flattenlist, rows);
                        // Validar que haya al menos un elemento raíz
                        if (!maps.Any())
                        {
                            return Result<DeliverableResponseList>.Fail("No deliverables found for the given ProjectId.");
                        }

                        // Construir la respuesta
                        var response = new DeliverableResponseList
                        {

                            Items = maps,
                            ProjectId = request.ProjectId,
                        };

                        return Result<DeliverableResponseList>.Success(response);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.Error.WriteLine($"Hierarchy error: {ex.Message}");
                        return Result<DeliverableResponseList>.Fail($"Data integrity issue: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Unexpected error: {ex}");
                        return Result<DeliverableResponseList>.Fail("An unexpected error occurred while processing the request.");
                    }
                });
            }

            /// <summary>
            /// Valida la solicitud inicial.
            /// </summary>
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

            /// <summary>
            /// Carga todos los deliverables en una lista plana desde la base de datos.
            /// </summary>
            private static async Task<List<Deliverable>> LoadAllDeliverablesFlat(Guid projectId, IQueryRepository repository)
            {
                var cache = $"{StaticClass.Deliverables.Cache.GetAll(projectId)}";
                Func<IQueryable<Deliverable>, IIncludableQueryable<Deliverable, object>> includes = x => x
                .Include(x => x.Dependants)
                .Include(x => x.BudgetItems)
                .Include(x => x.Project);
                Expression<Func<Deliverable, bool>> criteria = x => x.ProjectId == projectId;
                return await repository.GetAllAsync(Cache: cache, Criteria: criteria, Includes: includes);
            }


            /// <summary>
            /// Reconstruye la jerarquía a partir de una lista plana.
            /// </summary>
            private static List<DeliverableResponse> RebuildHierarchy(IEnumerable<DeliverableResponse> flatList)
            {
                if (flatList == null)
                {
                    throw new ArgumentNullException(nameof(flatList), "La lista plana no puede ser nula.");
                }

                var idToItemMap = flatList.ToDictionary(item => item.Id);
                var rootItems = new List<DeliverableResponse>();

                foreach (var item in flatList)
                {
                    if (!item.ParentDeliverableId.HasValue)
                    {
                        // Si ParentDeliverableId es nulo, es una raíz
                        rootItems.Add(item);
                    }
                    else
                    {
                        // Si tiene ParentDeliverableId, buscar el padre en el diccionario
                        if (idToItemMap.TryGetValue(item.ParentDeliverableId.Value, out var parent))
                        {
                            parent.SubDeliverables.Add(item);
                        }
                        else
                        {
                            throw new InvalidOperationException($"El elemento con Id '{item.Id}' tiene un ParentDeliverableId '{item.ParentDeliverableId}' que no existe en la lista.");
                        }
                    }

                }

                return rootItems;
            }

            private static void RebuildDependants(IEnumerable<DeliverableResponse> flatList, List<Deliverable> rows)
            {
                foreach (var row in rows)
                {
                    if (row.Dependants.Any())
                    {
                        var rowmaped = flatList.SingleOrDefault(x => x.Id == row.Id);
                        if (rowmaped != null)
                        {
                            foreach (var dependant in row.Dependants)
                            {
                                var dependantmapped = flatList.SingleOrDefault(x => x.Id == dependant.Id);
                                if (dependantmapped != null)
                                {
                                    rowmaped.Dependants.Add(dependantmapped);
                                }
                            }
                        }

                    }

                }

            }
        }
        private static DeliverableResponse MapFlat(this Deliverable row)
        {
            return new DeliverableResponse
            {
                ProjectId = row.ProjectId,
                DependencyType = string.IsNullOrEmpty(row.DependencyType)
                    ? TasksRelationTypeEnum.FinishStart
                    : TasksRelationTypeEnum.GetType(row.DependencyType),
                StartDate = row.StartDate,
                EndDate = row.EndDate,
                Duration = row.Duration ?? "1d",
                Lag = row.Lag,
                ParentDeliverableId = row.ParentDeliverableId,
                Id = row.Id,
                Name = row.Name,
                Order = row.Order,
                PlanningId = row.PlanningId,
                StartId = row.StartId,
                WBS = row.WBS,
                LabelOrder = row.LabelOrder,
                DependantId = row.DependentantId,
                ShowBudgetItems = row.ShowBudgetItems,
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
