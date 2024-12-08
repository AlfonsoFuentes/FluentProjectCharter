using Shared.Models.Constrainsts.Requests;

namespace Server.EndPoint.Constrainsts.Commands
{

    public static class CreateConstrainstEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Constrainsts.EndPoint.Create, async (CreateConstrainstRequest Data, IRepository Repository) =>
                {
                    var row = Constrainst.Create(Data.DeliverableId);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [..StaticClass.Projects.Cache.Key(Data.ProjectId), .. StaticClass.Constrainsts.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Constrainst Map(this CreateConstrainstRequest request, Constrainst row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
