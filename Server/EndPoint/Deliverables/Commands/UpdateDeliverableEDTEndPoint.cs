using Server.Database.Entities.ProjectManagements;

namespace Server.EndPoint.Deliverables.Commands
{
    public static class UpdateDeliverableEDTEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.UpdateEDT, async (DeliverableResponseListToUpdate data, IRepository repository) =>
                {
                    
                    // Validar que se haya proporcionado un ProjectId
                    if (data == null || data.ProjectId == Guid.Empty)
                    {
                        return Result.Fail("Invalid request: Missing or invalid ProjectId.");
                    }
                    
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> includes = x => x.Include(x => x.Deliverables);
                    // Construir el criterio de búsqueda
                    Expression<Func<Project, bool>> criteria = x => x.Id == data.ProjectId;
                   
                    // Obtener los registros existentes
                    var project = await repository.GetAsync(Criteria: criteria, Includes: includes);
                    if (project == null) return Result.Fail("Invalid request: Missing or invalid ProjectId.");
                    var rows = project.Deliverables;
                   
                    if (rows == null || !rows.Any())
                    {
                        return Result.Fail("No deliverables found for the given ProjectId.");
                    }

                    await UpdateMapped(repository, project, data.FlatOrderedItems);
                    
                    await UpdateDependencies(repository, project, data.FlatOrderedItems);
                   
                    var cache = StaticClass.Deliverables.Cache.GetAll(data.ProjectId);
                    // Guardar los cambios en la base de datos y limpiar la caché
                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(cache);
                  

                    return Result.EndPointResult(
                        result,
                        data.Succesfully,
                        data.Fail);
                });
            }
            async Task UpdateDependencies(IRepository repository, Project project, List<DeliverableResponse> flatlist)
            {

                var rows = project.Deliverables;
                var itemswithdependants = flatlist.Where(x => x.Dependants.Any()).ToList();
                for (int i = 0; i < itemswithdependants.Count; i++)
                {
                    var dataitem = itemswithdependants[i];
                    for (int j = 0; j < dataitem.Dependants.Count; j++)
                    {
                        var item = dataitem.Dependants[j];
                        var dependant = rows.SingleOrDefault(x => x.Id == item.Id);
                        if (dependant != null)
                        {
                            dependant.DependentantId = dataitem.Id;
                            await repository.UpdateAsync(dependant);
                        }
                    }
                }

            }

            async Task UpdateMapped(IRepository repository, Project project, List<DeliverableResponse> flatlist)
            {
                var rows = project.Deliverables;
                for (int i = 0; i < flatlist.Count; i++)
                {
                    var flat = flatlist[i];
                    var row = rows.FirstOrDefault(x => x.Id == flat.Id);
                    if (row == null)
                    {
                        row = Deliverable.Create(project.Id);
                        await repository.AddAsync(row);
                    }
                    else
                    {
                        await repository.UpdateAsync(row);
                    }
                    flat.Map(row);
                }

            }


        }
        public static Deliverable Map(this DeliverableResponse flat, Deliverable row)
        {

            row.Order = flat.Order;
            row.WBS = flat.WBS;
            row.Name = flat.Name;
            row.StartDate = flat.StartDate;
            row.EndDate = flat.EndDate;
            row.Duration = flat.Duration;
            row.Lag = flat.Lag;
            row.DependencyType = flat.DependencyType.Name;
            row.ParentDeliverableId = flat.ParentDeliverableId;
            row.LabelOrder = flat.LabelOrder;
            row.DependentantId = null;
            row.IsExpanded = flat.IsExpanded;
            return row;
        }
    }


}
