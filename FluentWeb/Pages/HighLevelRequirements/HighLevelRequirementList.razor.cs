using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.HighLevelRequirements.Requests;
using Shared.Models.HighLevelRequirements.Responses;
using Shared.Models.Projects.Reponses;

namespace FluentWeb.Pages.HighLevelRequirements;
#nullable disable
public partial class HighLevelRequirementList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public ProjectResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }



    public List<HighLevelRequirementResponse> Items => Parent == null ? new() : Parent.HighLevelRequirements;
    string nameFilter;
    public List<HighLevelRequirementResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateHighLevelRequirement/{Parent.Id}");

    }

    void Edit(HighLevelRequirementResponse response)
    {
        Navigation.NavigateTo($"/UpdateHighLevelRequirement/{response.Id}/{Parent.Id}");
    }


   
    public async Task Delete(HighLevelRequirementResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteHighLevelRequirementRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = Parent.Id,
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
