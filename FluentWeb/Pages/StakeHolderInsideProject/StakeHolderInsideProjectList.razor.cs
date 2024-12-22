using Microsoft.AspNetCore.Components;
using Shared.Models.Projects.Reponses;
using Shared.Models.StakeHolders.Requests;
using Shared.Models.StakeHolders.Responses;
#nullable disable
namespace FluentWeb.Pages.StakeHolderInsideProject;
public partial class StakeHolderInsideProjectList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public ProjectResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }



    public List<StakeHolderInsideProjectResponse> Items => Parent == null ? new() : Parent.StakeHolders;
    string nameFilter;
    public List<StakeHolderInsideProjectResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateStakeHolderInsideProject/{Parent.Id}");

    }

    void Edit(StakeHolderInsideProjectResponse response)
    {
        Navigation.NavigateTo($"/UpdateStakeHolderInsideProject/{response.Id}/{Parent.Id}");
    }



    public async Task Delete(StakeHolderInsideProjectResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteStakeHolderInsideProjectRequest request = new()
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
