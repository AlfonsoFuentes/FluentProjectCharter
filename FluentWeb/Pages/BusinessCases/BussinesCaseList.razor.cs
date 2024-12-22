using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Responses;
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

    [Inject]
    private IOrganizationStrategyService ServiceOrganizationStrategy { get; set; }

    public List<CaseResponse> Items => Parent == null ? new() : Parent.Cases;
    string nameFilter;
    public List<CaseResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateCase/{Parent.Id}");

    }
    void CancelAsync()
    {

    }


    void Edit(CaseResponse response)
    {
        Navigation.NavigateTo($"/UpdateCase/{response.Id}/{Parent.Id}");
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
            var resultDelete = await GenericService.Delete(request);
            if (resultDelete.Succeeded)
            {

                _snackBar.ShowSuccess(resultDelete.Messages);
                Navigation.NavigateTo("/");

            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }

}
