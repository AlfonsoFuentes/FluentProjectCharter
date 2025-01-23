using Microsoft.AspNetCore.Components;
using Shared.Models.AcceptanceCriterias.Requests;
using Shared.Models.AcceptanceCriterias.Responses;
using Shared.Models.Scopes.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.AcceptanceCriterias;
#nullable disable
public partial class AcceptanceCriteriaList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]

    [EditorRequired]
    public ScopeResponse Parent { get; set; } = new();

    public List<AcceptanceCriteriaResponse> Items => Parent == null ? new() : Parent.AcceptanceCriterias;

    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


    [Inject]
    private IGenericService Service { get; set; } = null!;
  
    string nameFilter;
    public List<AcceptanceCriteriaResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateAcceptanceCriteria/{Parent.ProjectId}/{Parent.Id}");

    }

    void Edit(AcceptanceCriteriaResponse response)
    {
        Navigation.NavigateTo($"/UpdateAcceptanceCriteria/{response.Id}/{Parent.ProjectId}");
    }
    public async Task Delete(AcceptanceCriteriaResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteAcceptanceCriteriaRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = Parent.ProjectId,
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
