using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Models.BudgetItems;
using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Milestones.Responses;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems;
#nullable disable
public partial class BudgetItemsTable
{


    [Parameter]
    public Guid ProjectId { get; set; } = new();

    BudgetItemResponseList Response = new();
    async Task GetAll()
    {
        var result = await GenericService.GetAll<BudgetItemResponseList, BudgetItemGetAll>(new BudgetItemGetAll()
        {
            ProjectId = ProjectId
        });
        if (result.Succeeded)
        {
            Response = result.Data;
        }
    }
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
    }

    public List<IBudgetItemResponse> Selecteds => Response.Items.Where(x => x.Selected).ToList();

    string nameFilter;
    public List<IBudgetItemResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Response.Items : Response.Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    public void AddNewBudgetItem(string page)
    {
        Navigation.NavigateTo($"/{page}/{ProjectId}");

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
            ProjectId = ProjectId,
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
