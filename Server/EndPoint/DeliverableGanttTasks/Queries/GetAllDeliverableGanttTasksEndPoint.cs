using Shared.Models.DeliverableGanttTasks.Records;
using Shared.Models.DeliverableGanttTasks.Responses;

namespace Server.EndPoint.DeliverableGanttTasks.Queries
{

    public static class GetAllDeliverableGanttTasksEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.DeliverableGanttTasks.EndPoint.GetAll, async (GetAllDeliverableGanttTask request, IQueryRepository repository) =>
                {
                    try
                    {
                       
                        if (!ValidateRequest(request, repository, out var validationError))
                        {
                            return Result<DeliverableGanttTaskResponseList>.Fail(validationError);
                        }

                        //Cargar datos
                        var project = await LoadAllGanttTasksFlat(request.ProjectId, repository);
                        if (project == null)
                        {
                            return Result<DeliverableGanttTaskResponseList>.Fail(request.NotFound);
                        }

                        DeliverableGanttTaskResponseList response = new();
                        response.ProjectId = request.ProjectId;
                        response.InitialProjectStartDate = project.StartDate;
                        response.ProjectName = project.Name;
                        foreach (var deliverable in project.Deliverables)
                        {
                            DeliverableGanttTaskResponse deliverableDto = new()
                            {
                                IsDeliverable = true,
                                InternalOrder = deliverable.InternalOrder,
                                MainOrder = deliverable.MainOrder,
                                StartDate = deliverable.StartDate,
                                EndDate = deliverable.EndDate,
                                Id = deliverable.Id,
                                Name = deliverable.Name,
                                DeliverableId = deliverable.Id,
                                DurationInDays = deliverable.DurationInDays,
                                DurationUnit = deliverable.DurationUnit,
                                DurationInUnit = deliverable.DurationInUnit,


                            };
                            response.Items.Add(deliverableDto);
                            var flatList = MapFlatInParallelAsync(deliverable.NewGanttTasks);
                           

                            response.Items.AddRange(flatList);



                        }

                      

                        return Result<DeliverableGanttTaskResponseList>.Success(response);

                    }
                    catch (Exception ex)
                    {
                        return Result<DeliverableGanttTaskResponseList>.Fail($"An error occurred: {ex.Message}");
                    }
                });

            }

            private static List<DeliverableGanttTaskResponse> MapFlatInParallelAsync(List<NewGanttTask> rows)
            {

                var tasks = rows.Select(row => row.MapParallel()).ToList();


                return tasks.OrderBy(x => x.MainOrder).ToList();
            }




            private static bool ValidateRequest(GetAllDeliverableGanttTask request, IQueryRepository repository, out string errorMessage)
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

            private static async Task<Project?> LoadAllGanttTasksFlat(Guid projectId, IQueryRepository repository)
            {
                var cache = StaticClass.DeliverableGanttTasks.Cache.GetAll(projectId);

                if (cache == null)
                {
                    return null!;
                }
                Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x
                .Include(x => x.Deliverables).ThenInclude(x => x.NewGanttTasks)
                       ;

                Expression<Func<Project, bool>> criteria = x => x.Id == projectId;
                var project = await repository.GetAsync(Cache: cache, Criteria: criteria, Includes: includes);





                return project!;
            }
        }
        public static DeliverableGanttTaskResponse MapParallel(this NewGanttTask row)
        {
            return new DeliverableGanttTaskResponse
            {
                IsDeliverable = false,
                StartDate = row.StartDate,
                EndDate = row.EndDate,
                DurationInDays = row.DurationInDays,
                DurationUnit = row.DurationUnit,
                LagInDays = row.LagInDays,
                LagUnit = row.LagUnit,
                LagInUnits = row.LagInUnits,
                DurationInUnit = row.DurationInUnit,
                DependencyType = TasksRelationTypeEnum.GetType(row.DependencyType),
                Id = row.Id,
                Name = row.Name,
                DeliverableId = row.DeliverableId,
                TaskParentId = row.ParentId == null ? row.DeliverableId : row.ParentId,
                InternalOrder = row.InternalOrder,
                MainOrder = row.MainOrder,
                ParentWBS = row.ParentWBS,
                LoadDependencyList = row.DependencyList,
                IsParentDeliverable = row.ParentId == null,
                //LabelOrder = row.LabelOrder,
                //DependantId = row.DependentantId,
                ////ShowBudgetItems = row.ShowBudgetItems,
                //Alterations = row.BudgetItems?.OfType<Alteration>().Select(x => x.Map()).ToList() ?? new(),
                //Structurals = row.BudgetItems?.OfType<Structural>().Select(x => x.Map()).ToList() ?? new(),
                //Foundations = row.BudgetItems?.OfType<Foundation>().Select(x => x.Map()).ToList() ?? new(),
                //Equipments = row.BudgetItems?.OfType<Equipment>().Select(x => x.Map()).ToList() ?? new(),

                //Valves = row.BudgetItems?.OfType<Valve>().Select(x => x.Map()).ToList() ?? new(),
                //Electricals = row.BudgetItems?.OfType<Electrical>().Select(x => x.Map()).ToList() ?? new(),
                //Pipings = row.BudgetItems?.OfType<Pipe>().Select(x => x.Map()).ToList() ?? new(),
                //Instruments = row.BudgetItems?.OfType<Instrument>().Select(x => x.Map()).ToList() ?? new(),

                //EHSs = row.BudgetItems?.OfType<EHS>().Select(x => x.Map()).ToList() ?? new(),
                //Paintings = row.BudgetItems?.OfType<Painting>().Select(x => x.Map()).ToList() ?? new(),
                //Taxes = row.BudgetItems?.OfType<Tax>().Select(x => x.Map()).ToList() ?? new(),
                //Testings = row.BudgetItems?.OfType<Testing>().Select(x => x.Map()).ToList() ?? new(),

                //EngineeringDesigns = row.BudgetItems?.OfType<EngineeringDesign>().Select(x => x.Map()).ToList() ?? new(),

            };
        }


    }
}
