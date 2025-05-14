namespace Server.EndPoint.Projects.Commands
{
    
    public static class UnApproveProjectEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Projects.EndPoint.UnApprove, async (UnApproveProjectRequest Data, IRepository Repository, IQueryRepository queryRepository) =>
                {
                    var row = await Repository.GetByIdAsync<Project>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }
                    await Repository.UpdateAsync(row);
                    Expression<Func<Contingency, bool>> CriteriaContingency = x => x.ProjectId == Data.Id;

                    Expression<Func<Engineering, bool>> CriteriaEngineering = x => x.ProjectId == Data.Id;

                    var contingency=await Repository.GetAsync(Criteria: CriteriaContingency);
                    var engineering = await Repository.GetAsync(Criteria: CriteriaEngineering);
                    if (contingency != null)
                    {
                        await Repository.RemoveAsync(contingency);
                    }
                    if (engineering != null)
                    {
                        await Repository.RemoveAsync(engineering);
                    }
                    row.Status = ProjectStatusEnum.Created.Id;
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(StaticClass.Projects.Cache.Key(row.Id));


                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }

        }

       


    }

}
