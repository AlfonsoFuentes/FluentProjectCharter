using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Electricals.Requests;

namespace FluentWeb.Pages.BudgetItems.Electricals;
public partial class CreateElectrical
{
    CreateElectricalRequest Model = new();
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
