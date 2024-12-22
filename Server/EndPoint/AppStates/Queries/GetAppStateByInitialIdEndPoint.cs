using Shared.Models.AppStates.Records;
using Shared.Models.AppStates.Responses;

namespace Server.EndPoint.AppStates.Queries
{
    public static class GetAppStateByInitialIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AppStates.EndPoint.GetByIdInitial, async (GetAppStateByIdInitialRequest request, IRepository Repository) =>
                {


                    var rows = await Repository.GetAllAsync<AppState>();
                    if (rows == null || rows.Count == 0){

                        return Result.Fail();
                    }
                    var appstate = rows.First();

                    var response = appstate.Map();
                    return Result.Success(response);



                });
            }
        }
        static ActiveAppResponse Map(this AppState row)
        {
            return new ActiveAppResponse
            {
                ProjectId = row.ActiveProjectId,
               
            };
        }


    }
}
