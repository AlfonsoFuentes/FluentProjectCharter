﻿using Server.Database.Entities.ProjectManagements;
using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.Deliverables.Records;
using System.Diagnostics;
using Server.EndPoint.BudgetItems.IndividualItems.Valves.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Alterations.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Structurals.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Foundations.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Equipments.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Electricals.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Pipes.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Instruments.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.EHSs.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Paintings.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Taxs.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.Testings.Queries;
using Server.EndPoint.BudgetItems.IndividualItems.EngineeringDesigns.Queries;
namespace Server.EndPoint.Deliverables.Queries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class GetAllDeliverableEndPoint2
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost($"{StaticClass.Deliverables.EndPoint.GetAll}2", async (DeliverableGetAll request, IQueryRepository repository) =>
                {
                    // Validación inicial
                    if (!ValidateRequest(request, repository, out var validationError))
                    {
                        return Result<DeliverableResponseListToUpdate>.Fail(validationError);
                    }

                    Stopwatch sw = Stopwatch.StartNew();
                    Stopwatch swtotal = Stopwatch.StartNew();
                    // Cargar todos los deliverables en una lista plana
                    var rows = await LoadAllDeliverablesFlat(request.ProjectId, repository);
                    sw.Stop();

                    var elapse1 = sw.Elapsed;
                    sw.Start();
                    if (rows == null || !rows.Any())
                    {
                        return Result<DeliverableResponseListToUpdate>.Fail(
                            StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Deliverables.ClassLegend));
                    }

                    // Mapear los datos a DeliverableResponse
                    var flattenlist = rows.Select(x => x.MapFlat()).ToList();
                    sw.Stop();

                    var elapse2 = sw.Elapsed;
                    sw.Start();
                    // Reconstruir la jerarquía
                    var maps = RebuildHierarchy(flattenlist);
                    sw.Stop();
                    var elapse3 = sw.Elapsed;
                    sw.Start();
                    RebuildDependants(flattenlist, rows);
                    sw.Stop();
                    var elapse4 = sw.Elapsed;
                    sw.Start();
                    swtotal.Stop();
                    var elapsetotal = swtotal.Elapsed;
                    // Validar que haya al menos un elemento raíz
                    if (!maps.Any())
                    {
                        return Result<DeliverableResponseListToUpdate>.Fail("No deliverables found for the given ProjectId.");
                    }

                    // Construir la respuesta
                    var response = new DeliverableResponseListToUpdate
                    {

                        Items = maps,
                        ProjectId = request.ProjectId,
                    };

                    return Result<DeliverableResponseListToUpdate>.Success(response);
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

                var cache = StaticClass.Deliverables.Cache.GetAll(projectId);
                Func<IQueryable<Deliverable>, IIncludableQueryable<Deliverable, object>> includes = x => x
                .Include(x => x.Dependants)
                .Include(x => x.BudgetItems)
                ;
                Expression<Func<Deliverable, bool>> criteria = x => x.ProjectId == projectId;
                return await repository.GetAllAsync(Cache: cache, Criteria: criteria, Includes: includes);
            }


            /// <summary>
            /// Reconstruye la jerarquía a partir de una lista plana.
            /// </summary>
            private static List<DeliverableResponse> RebuildHierarchy(List<DeliverableResponse> flatList)
            {
                if (flatList == null)
                {
                    throw new ArgumentNullException(nameof(flatList), "La lista plana no puede ser nula.");
                }

                var idToItemMap = flatList.ToDictionary(item => item.Id);
                var rootItems = new List<DeliverableResponse>();
                for (int i = 0; i < flatList.Count; i++)
                {
                    var item = flatList[i];
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

            private static void RebuildDependants(List<DeliverableResponse> flatList, List<Deliverable> rows)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    var row = rows[i];
                    if (row.Dependants.Any())
                    {
                        var rowmaped = flatList.SingleOrDefault(x => x.Id == row.Id);
                        if (rowmaped != null)
                        {
                            for (int j = 0; j < row.Dependants.Count; j++)
                            {
                                var dependant = row.Dependants[j];
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
            //public void MapEndPoint2(IEndpointRouteBuilder app)
            //{
            //    app.MapPost(StaticClass.Deliverables.EndPoint.GetAll, async (DeliverableGetAll request, IQueryRepository repository) =>
            //    {
            //        // Validación inicial
            //        if (!ValidateRequest(request, repository, out var validationError))
            //        {
            //            return Result<DeliverableResponseListToUpdate>.Fail(validationError);
            //        }

            //        try
            //        {
            //            Stopwatch sw = Stopwatch.StartNew();
            //            // Cargar todos los deliverables en una lista plana
            //            var rows = await LoadAllDeliverablesFlat(request.ProjectId, repository);
            //            sw.Stop();

            //            var elapse1 = sw.Elapsed;
            //            sw.Start();
            //            if (rows == null || !rows.Any())
            //            {
            //                return Result<DeliverableResponseListToUpdate>.Fail(
            //                    StaticClass.ResponseMessages.ReponseNotFound(StaticClass.Deliverables.ClassLegend));
            //            }

            //            // Mapear los datos a DeliverableResponse
            //            var flattenlist = rows.Select(x => x.MapFlat()).ToList();
            //            sw.Stop();

            //            var elapse2 = sw.Elapsed;
            //            sw.Start();
            //            // Reconstruir la jerarquía
            //            var maps = RebuildHierarchy(flattenlist);
            //            sw.Stop();
            //            var elapse3 = sw.Elapsed;
            //            sw.Start();
            //            RebuildDependants(flattenlist, rows);
            //            sw.Stop();
            //            var elapse4 = sw.Elapsed;
            //            sw.Start();
            //            // Validar que haya al menos un elemento raíz
            //            if (!maps.Any())
            //            {
            //                return Result<DeliverableResponseListToUpdate>.Fail("No deliverables found for the given ProjectId.");
            //            }

            //            // Construir la respuesta
            //            var response = new DeliverableResponseListToUpdate
            //            {

            //                Items = maps,
            //                ProjectId = request.ProjectId,
            //            };

            //            return Result<DeliverableResponseListToUpdate>.Success(response);
            //        }
            //        catch (InvalidOperationException ex)
            //        {
            //            Console.Error.WriteLine($"Hierarchy error: {ex.Message}");
            //            return Result<DeliverableResponseListToUpdate>.Fail($"Data integrity issue: {ex.Message}");
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.Error.WriteLine($"Unexpected error: {ex}");
            //            return Result<DeliverableResponseListToUpdate>.Fail("An unexpected error occurred while processing the request.");
            //        }
            //    });
            //}
        }
      
    }
}
