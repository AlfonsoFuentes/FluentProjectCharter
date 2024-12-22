using Shared.Models.AppStates.Records;
using Shared.Models.AppStates.Requests;
using Shared.Models.AppStates.Responses;

namespace Web.Infrastructure.Managers.AppStates
{
    public interface IAppStateService : IManager
    {
        Task<IResult<ActiveAppResponse>> GetInitialProjectState();
        Task<IResult<ActiveAppResponse>> GetProjectState(ActiveAppResponse response);
        Task<IResult> UpdateActiveProjectState(ActiveAppResponse response);
        Task<IResult> UpdateCaseScope(ActiveAppResponse response);
        Task<IResult> UpdateCaseTab(ActiveAppResponse response);
        Task<IResult> UpdateDeliverableTab(ActiveAppResponse response);
        Task<IResult> UpdateProjectCase(ActiveAppResponse response);
        Task<IResult> UpdateProjectTab(ActiveAppResponse response);
        Task<IResult> UpdateScopeDeliverable(ActiveAppResponse response);
        Task<IResult> UpdateScopeTab(ActiveAppResponse response);
    }
    public class AppStateService : IAppStateService
    {
        IHttpClientService http;

        public AppStateService(IHttpClientService http)
        {
            this.http = http;
        }


        public async Task<IResult> UpdateActiveProjectState(ActiveAppResponse response)
        {
            UpdateActiveStateRequest request = response.MapRequest();
            var result = await http.PostAsJsonAsync(StaticClass.AppStates.EndPoint.UpdateActiveProject, request);
            return await result.ToResult();
        }
        public async Task<IResult> UpdateProjectTab(ActiveAppResponse response)
        {
            UpdateActiveStateRequest request = response.MapRequest();
            var result = await http.PostAsJsonAsync(StaticClass.AppStates.EndPoint.UpdateProjectTab, request);
            return await result.ToResult();
        }
        public async Task<IResult> UpdateProjectCase(ActiveAppResponse response)
        {
            UpdateActiveStateRequest request = response.MapRequest();
            var result = await http.PostAsJsonAsync(StaticClass.AppStates.EndPoint.UpdateProjectCase, request);
            return await result.ToResult();
        }
        public async Task<IResult> UpdateCaseTab(ActiveAppResponse response)
        {
            UpdateActiveStateRequest request = response.MapRequest();
            var result = await http.PostAsJsonAsync(StaticClass.AppStates.EndPoint.UpdateCaseTab, request);
            return await result.ToResult();
        }
        public async Task<IResult> UpdateCaseScope(ActiveAppResponse response)
        {
            UpdateActiveStateRequest request = response.MapRequest();
            var result = await http.PostAsJsonAsync(StaticClass.AppStates.EndPoint.UpdateCaseScope, request);
            return await result.ToResult();
        }
        public async Task<IResult> UpdateScopeTab(ActiveAppResponse response)
        {
            UpdateActiveStateRequest request = response.MapRequest();
            var result = await http.PostAsJsonAsync(StaticClass.AppStates.EndPoint.UpdateScopeTab, request);
            return await result.ToResult();
        }
        public async Task<IResult> UpdateScopeDeliverable(ActiveAppResponse response)
        {
            UpdateActiveStateRequest request = response.MapRequest();
            var result = await http.PostAsJsonAsync(StaticClass.AppStates.EndPoint.UpdateScopeDeliverable, request);
            return await result.ToResult();
        }
        public async Task<IResult> UpdateDeliverableTab(ActiveAppResponse response)
        {
            UpdateActiveStateRequest request = response.MapRequest();
            var result = await http.PostAsJsonAsync(StaticClass.AppStates.EndPoint.UpdateDeliverableTab, request);
            return await result.ToResult();
        }

        public async Task<IResult<ActiveAppResponse>> GetProjectState(ActiveAppResponse response)
        {
            GetAppStateByIdRequest request = new() { ProjectId = response.ProjectId };
            var result = await http.PostAsJsonAsync(StaticClass.AppStates.EndPoint.GetById, request);
            return await result.ToResult<ActiveAppResponse>();
        }

        public async Task<IResult<ActiveAppResponse>> GetInitialProjectState()
        {
            GetAppStateByIdInitialRequest request = new();
            var result = await http.PostAsJsonAsync(StaticClass.AppStates.EndPoint.GetByIdInitial, request);
            return await result.ToResult<ActiveAppResponse>();
        }
    }
    static class MapperResponseRequest
    {
        public static UpdateActiveStateRequest MapRequest(this ActiveAppResponse response)
        {
            return new()
            {
                CaseId = response.CaseId,
                CaseTab = response.CaseTab,
                DeliverableId = response.DeliverableId,
                DeliverableTab = response.DeliverableTab,
                ProjectId = response.ProjectId,
                ProjectTab = response.ProjectTab,
                ScopeId = response.ScopeId,
                ScopeTab = response.ScopeTab,
            };
        }
    }
}
