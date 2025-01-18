using Microsoft.AspNetCore.Components;
using Shared.Models.Bennefits.Requests;
using Shared.Models.BudgetItems;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Projects.Reponses;
using static Shared.StaticClasses.StaticClass;
#nullable disable

namespace FluentWeb.Pages.BudgetItems;
public partial class ProjectBudgetItemsList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public ProjectResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }



    public List<IBudgetItemResponse> Items => Parent == null ? new() : Parent.BudgetItems;
    string nameFilter;
    public List<IBudgetItemResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
  
    void EditBudgetItem(IBudgetItemResponse response)
    {

        Navigation.NavigateTo($"/{response.UpadtePageName}/{response.Id}/{Parent.Id}");
    }
    public async Task Delete(IBudgetItemResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteBudgetItemRequest request = new()
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
