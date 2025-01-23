using Microsoft.AspNetCore.Components;
using Shared.Models.Requirements.Requests;
using Shared.Models.Requirements.Responses;
using Shared.Models.Scopes.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.Requirements;
#nullable disable
public partial class RequirementList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public ScopeResponse Parent { get; set; }

    [Parameter]
    public Guid ParameterProjectId { get; set; }

   
    Guid ProjectId => Parent == null ? ParameterProjectId : Parent.ProjectId;
    [Parameter]
    public List<RequirementResponse> ParameterItems { get; set; } = new();
    RequirementResponse currentRequirement =>
 App.Project == null || App.Project.CurrentCase == null
 || App.Project.CurrentCase.CurrentScope == null ? null :
     FilteredItems.FirstOrDefault(x => x.Id == App.Project.CurrentCase.CurrentScope.CurrentRequirement!.Id);

    public List<RequirementResponse> Items => Parent == null ? ParameterItems : Parent.Requirements;

    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    
    [Inject]
    private IGenericService Service { get; set; } = null!;

    string nameFilter;
    public List<RequirementResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    Guid? ParentId => Parent == null ? null : Parent.Id;
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateRequirement/{ProjectId}/{ParentId}");



    }

    void Edit(RequirementResponse response)
    {
        Navigation.NavigateTo($"/UpdateRequirement/{response.Id}");
    }
    public async Task Delete(RequirementResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteRequirementRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = ProjectId,
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
