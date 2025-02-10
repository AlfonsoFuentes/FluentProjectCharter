using Microsoft.AspNetCore.Components;
using Shared.Models.Templates.NozzleTemplates;

namespace FluentWeb.Pages.BudgetManagements.BudgetItems.NozzleTemplates;
public partial class NozzleTemplateList
{
    [Parameter]
    public bool EnableAdd { get; set; } = true;

    [Parameter]
    public List<NozzleTemplateResponse> Items { get; set; } = new();

    [Parameter]
    public EventCallback<List<NozzleTemplateResponse>> ItemsChanged { get; set; }

    async Task OnItemsChanged()
    {
        await ItemsChanged.InvokeAsync(Items);
    }
    NozzleTemplateResponse EditItem = null!;
    NozzleTemplateResponse CreateItem = null!;

    void Add()
    {
        CreateItem = new() { Id = Guid.NewGuid() };
      
    }
    [Parameter]

    public EventCallback Validate { get; set; }
    void Edit(NozzleTemplateResponse item)
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
    async Task Delete(NozzleTemplateResponse item)
    {
        Items.Remove(item);
        await OnItemsChanged();
        await Validate.InvokeAsync();
    }

}
