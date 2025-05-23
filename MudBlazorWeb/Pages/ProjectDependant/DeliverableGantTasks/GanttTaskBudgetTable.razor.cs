using MudBlazor;
using MudBlazorWeb.Templates;
using Shared.Models.BudgetItemNewGanttTasks.Responses;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.DeliverableGanttTasks.Responses;

namespace MudBlazorWeb.Pages.ProjectDependant.DeliverableGantTasks;
public partial class GanttTaskBudgetTable
{
    [CascadingParameter]
    public DeliverableGanttTaskResponse Model { get; set; } = new();

    [Parameter]
    public List<BudgetItemResponse> SelectedItems { get; set; } = new();
    [Parameter]
    public List<BudgetItemResponse> NonSelectedItems { get; set; } = new();
    [Parameter]
    public EventCallback ValidateAsync { get; set; }
    [Parameter]
    public EventCallback RecreateItems { get; set; }
    async Task SelectedItemChanged(BudgetItemNewGanttTaskResponse selected)
    {
        if (selected.BudgetItem != null)
        {
            selected.BudgetItemId = selected.BudgetItem!.Id;

            if (RecreateItems.HasDelegate)
            {
                await RecreateItems.InvokeAsync();
            }

        }


    }
    public async Task Delete(BudgetItemNewGanttTaskResponse response)
    {
        var parameters = new DialogParameters<DialogTemplate>
        {
            { x => x.ContentText, $"Do you really want to delete {response.NomenclatoreName}?" },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<DialogTemplate>("Delete", parameters, options);
        var result = await dialog.Result;


        if (!result!.Canceled)
        {
            Model.RemoveBudgetItem(response);

        }

    }
    private Task<IEnumerable<BudgetItemResponse>> SearchFromNonSelectedItems(string value, CancellationToken token)
    {
        Func<BudgetItemResponse, bool> Criteria = x => 
        x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase) ||
        x.Nomenclatore.Contains(value, StringComparison.InvariantCultureIgnoreCase)||
        x.Brand.Contains(value, StringComparison.InvariantCultureIgnoreCase);
        IEnumerable<BudgetItemResponse> FilteredItems = string.IsNullOrEmpty(value) ? NonSelectedItems.AsEnumerable() :
            NonSelectedItems.Where(Criteria);
        return Task.FromResult(FilteredItems);
    }
    private Task<IEnumerable<BudgetItemResponse>> SearchFromSelectedItems(string value, CancellationToken token)
    {
        Func<BudgetItemResponse, bool> Criteria = x =>
       x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase) ||
       x.Nomenclatore.Contains(value, StringComparison.InvariantCultureIgnoreCase) ||
       x.Brand.Contains(value, StringComparison.InvariantCultureIgnoreCase);

        IEnumerable<BudgetItemResponse> FilteredItems = string.IsNullOrEmpty(value) ? SelectedItems.AsEnumerable() : SelectedItems.Where(Criteria);
        return Task.FromResult(FilteredItems);
    }
}
