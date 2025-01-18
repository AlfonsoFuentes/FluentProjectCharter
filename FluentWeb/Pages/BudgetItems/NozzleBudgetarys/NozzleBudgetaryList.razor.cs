using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Nozzles.Responses;

namespace FluentWeb.Pages.BudgetItems.NozzleBudgetarys;
public partial class NozzleBudgetaryList
{


    [Parameter]
    public List<NozzleResponse> Items { get; set; } = new();

    [Parameter]
    public EventCallback<List<NozzleResponse>> ItemsChanged { get; set; }

    async Task OnItemsChanged()
    {
        await ItemsChanged.InvokeAsync(Items);
    }
    NozzleResponse EditItem = null!;
    NozzleResponse CreateItem = null!;

    void Add()
    {
        CreateItem = new() { Id = Guid.NewGuid() };
      
    }
    [Parameter]
    public EventCallback Validate { get; set; }

    [Parameter]
    public bool IsAbleTodAdd { get; set; } = true;
    void Edit(NozzleResponse item)
    {
        EditItem= item;
    }
    void CancelEdit()
    {
        EditItem = null!;
    }
    async Task EditItemOk()
    {       
        await OnItemsChanged();
        await Validate.InvokeAsync();
        EditItem = null!;
    }
    void CancelAdd()
    {
        CreateItem = null!;
        StateHasChanged();
    }
    async Task AddItemOk()
    {
    

        Items.Add(CreateItem);
        CreateItem = null!;
        await OnItemsChanged();
        await Validate.InvokeAsync();
    }
    async Task Delete(NozzleResponse item)
    {
        Items.Remove(item);
        await OnItemsChanged();
        await Validate.InvokeAsync();
    }

}
