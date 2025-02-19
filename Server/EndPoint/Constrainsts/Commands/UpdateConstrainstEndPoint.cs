using Server.Database.Entities.ProjectManagements;
using Shared.Models.Constrainsts.Requests;


namespace Server.EndPoint.Constrainsts.Commands
{
    public static class UpdateConstrainstEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Constrainsts.EndPoint.Update, async (UpdateConstrainstRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<Constrainst>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
            private string[] GetCacheKeys(Constrainst row)
            {
                List<string> cacheKeys = [
                 
                    .. StaticClass.Constrainsts.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Constrainst Map(this UpdateConstrainstRequest request, Constrainst row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
