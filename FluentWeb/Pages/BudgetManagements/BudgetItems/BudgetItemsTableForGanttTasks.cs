using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Requests;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.GanttTasks.Responses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems;
#nullable disable
public partial class BudgetItemsTableForGanttTasks
{
   

    public List<IBudgetItemResponse> Items => GanttTask.BudgetItems;

    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Parameter]
    public GanttTaskResponse GanttTask { get; set; }
    public List<IBudgetItemResponse> Selecteds => Items.Where(x => x.Selected).ToList();

    string nameFilter;
    public List<IBudgetItemResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNewBudgetItem(string page)
    {
        Navigation.NavigateTo($"/{page}/{GanttTask.ProjectId}/{GanttTask.Id}");

    }
    void EditBudgetItem(IBudgetItemResponse response)
    {

        Navigation.NavigateTo($"/{response.UpadtePageName}/{response.Id}");
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
                GanttTaskId= GanttTask.Id,


            };
            var resultDelete = await GenericService.Delete(request);
            if (resultDelete.Succeeded)
            {
                await GetAll();
                _snackBar.ShowSuccess(resultDelete.Messages);


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }
    async Task RemoveGroup()
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {Selecteds.Count} Items?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;

        DeleteBudgetItemGroupRequest request = new()
        {
            DeleteGroup = Selecteds.Select(x => x.Id).ToList(),
            ProjectId = GanttTask.ProjectId,
            GanttTaskId = GanttTask.Id,
        };
        var resultDelete = await GenericService.Delete(request);
        if (resultDelete.Succeeded)
        {
            await GetAll();
            _snackBar.ShowSuccess(resultDelete.Messages);


        }
        else
        {
            _snackBar.ShowError(resultDelete.Messages);
        }
    }



}
