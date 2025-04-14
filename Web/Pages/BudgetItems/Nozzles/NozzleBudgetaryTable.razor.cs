using MudBlazor;
using Shared.Models.BudgetItems.IndividualItems.Nozzles.Responses;
using Shared.Models.Templates.NozzleTemplates;
using Web.Pages.ItemsTemplates.NozzleTemplates;

namespace Web.Pages.BudgetItems.Nozzles;
public partial class NozzleBudgetaryTable
{
    [Parameter]
    public List<NozzleResponse> Items { get; set; } = new();

    [Parameter]
    public EventCallback<List<NozzleResponse>> ItemsChanged { get; set; }

    async Task OnItemsChanged()
    {
        await ItemsChanged.InvokeAsync(Items);
    }
 

    async Task Add()
    {
        var parameters = new DialogParameters<NozzleBudgetaryDialog>
        {

        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Small };

        var dialog = await DialogService.ShowAsync<NozzleBudgetaryDialog>("Nozzle Template", parameters, options);
        var result = await dialog.Result;
        if (result != null && !result.Canceled && result.Data != null)
        {
            var model = result.Data as NozzleResponse;
            Items.Add(model!);
            await OnItemsChanged();
            await Validate.InvokeAsync();
            StateHasChanged();
        }

    }
    [Parameter]
    public EventCallback Validate { get; set; }

    
    async Task Edit(NozzleResponse item)
    {
        var parameters = new DialogParameters<NozzleBudgetaryDialog>
        {
             { x => x.Model, item},
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Small };

        var dialog = await DialogService.ShowAsync<NozzleBudgetaryDialog>("Nozzle Template", parameters, options);
        var result = await dialog.Result;
        if (result != null && !result.Canceled && result.Data != null)
        {
            var model = result.Data as NozzleResponse;
            item = model!;
            await OnItemsChanged();
            await Validate.InvokeAsync();
            StateHasChanged();
        }
    }
   
    async Task Delete(NozzleResponse item)
    {
        Items.Remove(item);
        await OnItemsChanged();
        await Validate.InvokeAsync();
    }
}
