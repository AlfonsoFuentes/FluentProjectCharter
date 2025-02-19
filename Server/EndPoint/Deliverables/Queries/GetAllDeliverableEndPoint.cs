using Server.Database.Entities.ProjectManagements;
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



                        // Reconstruir la jerarquía
                        var maps = RebuildHierarchy(flattenlist);

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
                var cache = $"{StaticClass.Deliverables.Cache.GetAll}-{projectId}";
                Func<IQueryable<Deliverable>, IIncludableQueryable<Deliverable, object>> includes = x => x.Include(x => x.Dependants);
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
                Duration = string.IsNullOrEmpty(row.DurationTime) ? "1d" : row.DurationTime,
            
                ParentDeliverableId = row.ParentDeliverableId,
                Id = row.Id,
                Name = row.Name,
                Order = row.Order,
                PlanningId = row.PlanningId,
                StartId = row.StartId,
                WBS = row.WBS,
                LabelOrder = row.LabelOrder,
                DependantId = row.DependentantId,
                Dependants = row.Dependants == null || row.Dependants.Count == 0 ? new() : row.Dependants.Select(x => x.Map()).ToList(),

            };
        }
    }
}
