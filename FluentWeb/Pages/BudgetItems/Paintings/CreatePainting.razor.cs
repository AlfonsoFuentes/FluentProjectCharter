using Microsoft.AspNetCore.Components;
using Shared.Models.BudgetItems.Paintings.Requests;

namespace FluentWeb.Pages.BudgetItems.Paintings;
public partial class CreatePainting
{
    CreatePaintingRequest Model = new();
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
