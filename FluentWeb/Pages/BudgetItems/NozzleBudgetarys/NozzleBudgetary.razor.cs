using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Nozzles.Responses;
using Shared.Models.Templates.NozzleTemplates;

namespace FluentWeb.Pages.BudgetItems.NozzleBudgetarys;
public partial class NozzleBudgetary
{
    [Parameter]
    public NozzleResponse Model { get; set; } = new();
    [Parameter]
    public EventCallback<NozzleResponse> ModelChanged { get; set; }
    async Task OnModelChanged()
    {
        await ModelChanged.InvokeAsync(Model);
    }
    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public Action Cancel { get; set; } = null!;
    [Parameter]
    public EventCallback Ok { get; set; }

    void OnCancel()
    {
        Cancel();
    }
    async Task OnOk()
    {
        await Ok.InvokeAsync();
    }
}
