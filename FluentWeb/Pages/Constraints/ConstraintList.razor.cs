using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Constrainsts.Requests;
using Shared.Models.Constrainsts.Responses;
using Shared.Models.Scopes.Responses;

namespace FluentWeb.Pages.Constraints;
#nullable disable
public partial class ConstraintList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public ScopeResponse Parent { get; set; } = null!;

    [Parameter]
    public Guid ParameterProjectId { get; set; }

    Guid ProjectId => Parent == null ? ParameterProjectId : Parent.ProjectId;
    [Parameter]
    public List<ConstrainstResponse> ParameterItems { get; set; } = new();

    ConstrainstResponse currentConstraint =>
 App.Project == null || App.Project.CurrentCase == null
 || App.Project.CurrentCase.CurrentScope == null ? null :
     FilteredItems.FirstOrDefault(x => x.Id == App.Project.CurrentCase.CurrentScope.CurrentConstrainst!.Id);

    public List<ConstrainstResponse> Items => Parent == null ? ParameterItems : Parent.Constrainsts;
  
    [Parameter]
    public Func<Task> GetAll { get; set; }
    Guid? ParentId => Parent == null ? null : Parent.Id;


    string nameFilter;
    public List<ConstrainstResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateConstrainst/{ProjectId}/{ParentId}");

    }



    void Edit(ConstrainstResponse response)
    {
        Navigation.NavigateTo($"/UpdateConstrainst/{response.Id}");
    }
    public async Task Delete(ConstrainstResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteConstrainstRequest request = new()
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
