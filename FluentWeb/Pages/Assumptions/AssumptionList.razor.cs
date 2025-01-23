using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Requests;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Requirements.Responses;
using Shared.Models.Scopes.Responses;

namespace FluentWeb.Pages.Assumptions;
#nullable disable
public partial class AssumptionList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]

    public ScopeResponse Parent { get; set; } = null!;

    [Parameter]
    public Guid ParameterProjectId { get; set; }

    Guid ProjectId => Parent == null ? ParameterProjectId : Parent.ProjectId;
    [Parameter]
    public List<AssumptionResponse> ParameterItems { get; set; } = new();
    AssumptionResponse currentAssumption =>
 App.Project == null || App.Project.CurrentCase == null
 || App.Project.CurrentCase.CurrentScope == null ? null :
     FilteredItems.FirstOrDefault(x => x.Id == App.Project.CurrentCase.CurrentScope.CurrentAssumption!.Id);

    public List<AssumptionResponse> Items => Parent == null ? ParameterItems : Parent.Assumptions;
    
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    
    string nameFilter;
    public List<AssumptionResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    Guid? ParentId => Parent == null ? null : Parent.Id;
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateAssumption/{ProjectId}/{ParentId}");
        
    }


    void Edit(AssumptionResponse response)
    {
        Navigation.NavigateTo($"/UpdateAssumption/{response.Id}");
    }
    public async Task Delete(AssumptionResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteAssumptionRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = ProjectId,
            };
            var resultDelete = await GenericService.Delete(request);
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
