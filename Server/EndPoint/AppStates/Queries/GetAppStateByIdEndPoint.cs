using Shared.Models.AppStates.Records;
using Shared.Models.AppStates.Responses;

namespace Server.EndPoint.AppStates.Queries
{
    public static partial class GetAppStateByIdEndPoint
    {
        public class GetEndPointProject : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AppStates.EndPoint.GetById, async (GetAppStateByIdRequest request, IRepository Repository) =>
                {
                    Func<IQueryable<Project>, IIncludableQueryable<Project, object>> Includes = x => x
                    .Include(x => x.Cases).ThenInclude(x => x.Scopes).ThenInclude(x => x.Deliverables);

                    ActiveAppResponse response = new ActiveAppResponse();
                    Expression<Func<Project, bool>> Criteria = x => x.Id == request.ProjectId;

                    response.ProjectId = request.ProjectId;
                    var queryproject = await Repository.GetAsync(Criteria: Criteria, Includes: Includes);

                    if (queryproject == null) return Result.Success(response);

                    response.CaseId = queryproject.CaseId;
                    response.ProjectTab = queryproject.ProjectTab;

                    if (queryproject.CaseId == null) return Result.Success(response);


                    var querycase = queryproject.Cases.FirstOrDefault(x => x.Id == queryproject.CaseId);

                    if (querycase == null) return Result.Success(response);

                    response.CaseTab = querycase.CaseTab;
                    response.ScopeId = querycase.ScopeId;

                    if (querycase.ScopeId == null) return Result.Success(response);

                    var queryscope = querycase.Scopes.FirstOrDefault(x => x.Id == querycase.ScopeId);

                    if (queryscope == null) return Result.Success(response);
                 
                    response.ScopeTab = queryscope.ScopeTab;
                    response.DeliverableId = queryscope.DeliverableId;

                    if (queryscope.DeliverableId == null) return Result.Success(response);


                    var querydeliverable = queryscope.Deliverables.FirstOrDefault(x => x.Id == queryscope.DeliverableId);
                    
                    if (querydeliverable == null) return Result.Success(response);
                    
                    response.DeliverableTab = querydeliverable.DeliverableTab;

                    return Result.Success(response);

                });
            }

        }


    }

}
