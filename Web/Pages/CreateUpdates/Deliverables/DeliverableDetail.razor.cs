using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.Deliverables.Responses;
#nullable disable

namespace Web.Pages.CreateUpdates.Deliverables;
public partial class DeliverableDetail
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public DeliverableResponse Deliverable { get; set; }

    [Parameter]
    public Func<Task> GetAll { get; set; }

}
