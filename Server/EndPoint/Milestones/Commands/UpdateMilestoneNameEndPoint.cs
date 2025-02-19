using Shared.Models.Milestones.Requests;

namespace Server.EndPoint.Milestones.Commands
{

    public static class UpdateMilestoneNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Milestones.EndPoint.UpdateName, async (UpdateMilestoneNameRequest Data, IRepository Repository, IQueryRepository QueryRepository) =>
                {
                    Expression<Func<Milestone, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;

                    Func<Milestone, bool> CriteriaExist = x => x.Id != Data.Id && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Milestones.Cache.GetAll;

                    var validation = await QueryRepository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);

                    if (validation) return Result.Fail($"{Data.Name} already exist!");

                    var row = await Repository.GetByIdAsync<Milestone>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private string[] GetCacheKeys(Milestone row)
            {
                List<string> cacheKeys = [
                 
                  
                    .. StaticClass.Milestones.Cache.Key(row.Id)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static Milestone Map(this UpdateMilestoneNameRequest request, Milestone row)
        {
            row.Name = request.Name;


            return row;
        }

    }

}
