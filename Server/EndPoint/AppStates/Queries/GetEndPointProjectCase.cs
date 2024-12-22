using Shared.Models.AppStates.Records;

namespace Server.EndPoint.AppStates.Queries
{
    public static partial class GetAppStateByIdEndPoint
    {
        public class GetEndPointProjectCase : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AppStates.EndPoint.GetByIdProjectCase, async (GetAppStateByIdRequest request, IRepository Repository) =>
                {

                    Expression<Func<AppState, bool>> Criteria = x => x.ActiveProjectId == request.ActiveProjectId&&x.CaseAcordionActive==request.CaseAcordionActive;

                    Func<IQueryable<AppState>, IIncludableQueryable<AppState, object>> Includes = x => x.Include(x => x.AppStateDeliverables);

                    var row = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);

                    if (row == null)
                    {
                        return Result.Fail();
                    }

                    var response = row.MapProjectCase();
                    return Result.Success(response);

                });
            }
        }

    }
  
}
