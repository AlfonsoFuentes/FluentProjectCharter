using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.Scopes.Requests;
using Shared.Models.Scopes.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.Scopes;
#nullable disable
public partial class ScopeList
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
    public List<ScopeResponse> Items => Parent == null ? new() : Parent.Scopes;
    string nameFilter;
    public List<ScopeResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateScope/{Parent.Id}/{Parent.ProjectId}");

    }
    ScopeResponse currentScope => App.Project == null || App.Project.CurrentCase == null ? null : 
        FilteredItems.FirstOrDefault(x => x.Id == App.Project.CurrentCase.CurrentScope.Id);


    void Edit(ScopeResponse response)
    {
        Navigation.NavigateTo($"/UpdateScope/{response.Id}/{Parent.ProjectId}");
    }
    public async Task Delete(ScopeResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteScopeRequest request = new()
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
