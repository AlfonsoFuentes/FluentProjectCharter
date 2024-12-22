using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Cases.Responses;
using Shared.Models.DecissionCriterias.Requests;
using Shared.Models.DecissionCriterias.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.DecissionCriterias;
#nullable disable
public partial class DecissionCriteriaList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public CaseResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<DecissionCriteriaResponse> Items => Parent == null ? new() : Parent.DecissionCriterias;
    string nameFilter;
    public List<DecissionCriteriaResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateDecissionCriteria/{Parent.Id}/{Parent.ProjectId}");

    }



    void Edit(DecissionCriteriaResponse response)
    {
        Navigation.NavigateTo($"/UpdateDecissionCriteria/{response.Id}/{Parent.ProjectId}");
    }
    public async Task Delete(DecissionCriteriaResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteDecissionCriteriaRequest request = new()
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
