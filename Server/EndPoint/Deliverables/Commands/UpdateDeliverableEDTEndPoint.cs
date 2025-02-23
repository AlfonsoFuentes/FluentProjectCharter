using DocumentFormat.OpenXml.Spreadsheet;
using Server.Database.Entities.ProjectManagements;
using Server.Repositories;
using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses.NewResponses;
using System.Diagnostics;

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
                    Stopwatch sw = Stopwatch.StartNew();
                    // Validar que se haya proporcionado un ProjectId
                    if (data == null || data.ProjectId == Guid.Empty)
                    {
                        return Result.Fail("Invalid request: Missing or invalid ProjectId.");
                    }
                    sw.Stop();
                    var elpase1 = sw.Elapsed;
                    sw.Restart();
                    // Construir el criterio de búsqueda
                    Expression<Func<Deliverable, bool>> criteria = x => x.ProjectId == data.ProjectId;
                    sw.Stop();
                    var elpase2 = sw.Elapsed;
                    sw.Restart();
                    // Obtener los registros existentes
                    var rows = await repository.GetAllAsync(Criteria: criteria);
                    sw.Stop();
                    var elpased3 = sw.Elapsed;
                    sw.Restart();
                    if (rows == null || !rows.Any())
                    {
                        return Result.Fail("No deliverables found for the given ProjectId.");
                    }

                    await UpdateMapped(repository, rows, data.FlatOrderedItems);
                    sw.Stop();
                    var elpased4 = sw.Elapsed;
                    sw.Restart();
                    await UpdateDependencies(repository, rows, data.FlatOrderedItems);
                    sw.Stop();
                    var elpased5 = sw.Elapsed;
                    sw.Restart();
                    var cache = StaticClass.Deliverables.Cache.GetAll(data.ProjectId);
                    // Guardar los cambios en la base de datos y limpiar la caché
                    var result = await repository.Context.SaveChangesAndRemoveCacheAsync(cache);
                    sw.Stop();
                    var elpased6 = sw.Elapsed;

                    return Result.EndPointResult(
                        result,
                        data.Succesfully,
                        data.Fail);
                });
            }
            async Task UpdateDependencies(IRepository repository, List<Deliverable> rows, List<DeliverableResponse> flatlist)
            {
                //for (int i = 0; i < rows.Count; i++)
                //{
                //    var row = rows[i];
                //    row.DependentantId = null;
                //    await repository.UpdateAsync(row);
                //}

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

            async Task UpdateMapped(IRepository repository, List<Deliverable> rows, List<DeliverableResponse> flatlist)
            {
                for (int i = 0; i < flatlist.Count; i++)
                {
                    var flat = flatlist[i];
                    var row = rows.FirstOrDefault(x => x.Id == flat.Id);
                    if (row != null)
                    {
                        flat.Map(row);
                        await repository.UpdateAsync(row);
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
            row.Duration = flat.Duration;
            row.Lag = flat.Lag;
            row.DependencyType = flat.DependencyType.Name;
            row.ParentDeliverableId = flat.ParentDeliverableId;
            row.LabelOrder = flat.LabelOrder;
            row.DependentantId = null;
            return row;
        }
    }


}
