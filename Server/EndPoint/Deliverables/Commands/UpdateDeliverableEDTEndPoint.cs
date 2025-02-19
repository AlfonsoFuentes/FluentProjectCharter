using Server.Database.Entities.ProjectManagements;
using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses.NewResponses;

namespace Server.EndPoint.Deliverables.Commands
{
    public static class UpdateDeliverableEDTEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.UpdateEDT, async (DeliverableResponseList data, IRepository repository) =>
                {
                    // Validar que se haya proporcionado un ProjectId
                    if (data == null || data.ProjectId == Guid.Empty)
                    {
                        return Result.Fail("Invalid request: Missing or invalid ProjectId.");
                    }

                    // Construir el criterio de búsqueda
                    Expression<Func<Deliverable, bool>> criteria = x => x.ProjectId == data.ProjectId;

                    // Obtener los registros existentes
                    var rows = await repository.GetAllAsync(Criteria: criteria);
                    if (rows == null || !rows.Any())
                    {
                        return Result.Fail("No deliverables found for the given ProjectId.");
                    }
                    List<DeliverableResponse> flatList = new();
                    FlattenItems(data.Items, flatList);
                    foreach (var flat in flatList)
                    {
                        var row = rows.FirstOrDefault(x => x.Id == flat.Id);
                        if (row != null)
                        {
                            flat.Map(row);

                        }

                    }
                    foreach (var row in rows)
                    {
                        row.DependentantId = null;
                        await repository.UpdateAsync(row);
                    }
                    foreach (var dataitem in flatList)
                    {
                        foreach (var item in dataitem.Dependants)
                        {
                            var dependant = rows.FirstOrDefault(x => x.Id == item.Id);
                            if (dependant != null)
                            {
                                dependant.DependentantId = dataitem.Id;
                                await repository.UpdateAsync(dependant);
                            }
                        }
                    }
                    var cache = $"{StaticClass.Deliverables.Cache.GetAll}-{data.ProjectId}";
                    // Guardar los cambios en la base de datos y limpiar la caché
                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(cache);
                    return Result.EndPointResult(
                        result,
                        data.Succesfully,
                        data.Fail);
                });
            }


            private void FlattenItems(IEnumerable<DeliverableResponse> items, List<DeliverableResponse> flatList)
            {
                foreach (var item in items)
                {
                    flatList.Add(item);
                    if (item.SubDeliverables != null && item.SubDeliverables.Any())
                    {
                        FlattenItems(item.SubDeliverables, flatList);
                    }
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
            row.DurationTime = flat.Duration!;
            row.DependencyType = flat.DependencyType.Name;
            row.ParentDeliverableId = flat.ParentDeliverableId;
            row.LabelOrder = flat.LabelOrder;

            return row;
        }
    }


}
