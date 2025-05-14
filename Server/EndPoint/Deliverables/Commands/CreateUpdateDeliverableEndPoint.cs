namespace Server.EndPoint.Deliverables.Commands
{

    public static class CreateUpdateDeliverableEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.CreateUpdate, async (DeliverableResponse Data, IRepository Repository) =>
                {

                    Deliverable? row = null!;
                    if (Data.Id == Guid.Empty)
                    {
                        var lastorder = await Repository.GetLastOrderAsync<Deliverable, Project>(Data.ProjectId);
                        var project = await Repository.GetByIdAsync<Project>(Data.ProjectId);

                        row = Deliverable.Create(Data.ProjectId, lastorder);
                        

                        await Repository.AddAsync(row);
                    }
                    else
                    {
                        row = await Repository.GetByIdAsync<Deliverable>(Data.Id);
                        if (row == null) { return Result.Fail(Data.NotFound); }
                        await Repository.UpdateAsync(row);
                    }

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Deliverables.Cache.Key(row.Id, row.ProjectId)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Deliverable Map(this DeliverableResponse request, Deliverable row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
