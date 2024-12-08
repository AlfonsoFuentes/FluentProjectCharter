using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Requests;
using Shared.Models.Cases.Responses;
using Shared.Models.IdentityModels.Responses.Audit;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.OrganizationStrategies.Responses;
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;
using Web.Infrastructure.Managers.Generic;
using Web.Infrastructure.Managers.OrganizationStrategys;

namespace FluentWeb.Pages.BusinessCases;
#nullable disable
public partial class BussinesCaseList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public ProjectResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    [EditorRequired]
    public Action Cancel { get; set; }
    [Inject]
    private IOrganizationStrategyService ServiceOrganizationStrategy { get; set; }
    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<CaseResponse> Items => Parent == null ? new() : Parent.Cases;
    string nameFilter;
    public List<CaseResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    CreateCaseRequest CreateResponse = null!;
    OrganizationStrategyResponseList OrganizationResponse = new();
    protected override async Task OnParametersSetAsync()
    {
        await UpdateOrganizationStrategy();
    }
    async Task UpdateOrganizationStrategy()
    {
        var result = await ServiceOrganizationStrategy.GetAll();
        if (result.Succeeded)
        {
            OrganizationResponse = result.Data;
        }
    }
    public void AddNew()
    {
        CreateResponse = new()
        {
            ProjectId = Parent.Id,
        };
    }
    CreateOrganizationStrategyRequest CreateOrganizationStrategyRequest = null!;
    public void AddOrganizationStrategy()
    {
        CreateOrganizationStrategyRequest = new()
        {

        };
    }
    void CancelOrganizationStrategy()
    {
        CreateOrganizationStrategyRequest = null!;
        StateHasChanged();
    }
    bool GetOrganizationStrategyFromCreate(OrganizationStrategyResponse response)
    {
        if (CreateResponse != null)
        {
            CreateResponse.OrganizationStrategy = response;
        }
        if (EditResponse != null)
        {
            EditResponse.OrganizationStrategy = response;
        }
        StateHasChanged();
        return false;
    }
    public void CancelAsync()
    {
        CreateResponse = null!;
        EditResponse = null!;
        Cancel();
    }

    public UpdateCaseRequest EditResponse { get; set; } = null!;

    void Edit(CaseResponse response)
    {
        EditResponse = new()
        {
            Id = response.Id,
            ProjectId = Parent.Id,
            Name = response.Name,
            OrganizationStrategy = response.OrganizationStrategy,
        };
    }
    public async Task Delete(CaseResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteCaseRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = Parent.Id,
            };
            var resultDelete = await Service.Delete(request);
            if (resultDelete.Succeeded)
            {
                await GetAll.Invoke();
                _snackBar.ShowSuccess(resultDelete.Messages);


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }

}
