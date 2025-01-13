using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Instruments.Requests;

namespace FluentWeb.Pages.BudgetItems.Instruments;
public partial class CreateInstrument
{
    CreateInstrumentRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid DeliverableId { get; set; }
    protected override void OnInitialized()
    {
        Model.ProjectId = ProjectId;
        Model.DeliverableId = DeliverableId;
    }
   
}
