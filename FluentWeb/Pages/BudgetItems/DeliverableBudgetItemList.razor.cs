using Microsoft.AspNetCore.Components;
using Shared.Enums.BudgetItemTypes;
using Shared.Models.Bennefits.Requests;
using Shared.Models.Bennefits.Responses;
using Shared.Models.BudgetItems;
using Shared.Models.Deliverables.Responses;
using Shared.StaticClasses;

namespace FluentWeb.Pages.BudgetItems;
#nullable disable
public partial class DeliverableBudgetItemList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public Guid DeliverableId { get; set; }
    [Parameter]
    [EditorRequired]
    public Guid ProjectId { get; set; }
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


    [Parameter]
    [EditorRequired]
    public List<IBudgetItemResponse> Items { get; set; }
    string nameFilter;
    public List<IBudgetItemResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNewBudgetItem(string page)
    {
        Navigation.NavigateTo($"/{page}/{DeliverableId}/{ProjectId}");

    }
    void EditBudgetItem(IBudgetItemResponse response)
    {
        
        Navigation.NavigateTo($"/{response.UpadtePageName}/{response.Id}/{ProjectId}");
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
