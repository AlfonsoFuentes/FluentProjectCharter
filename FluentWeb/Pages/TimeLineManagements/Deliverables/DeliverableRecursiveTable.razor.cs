using Microsoft.AspNetCore.Components;
using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Deliverables.Responses.NewResponses;

namespace FluentWeb.Pages.TimeLineManagements.Deliverables;
public partial class DeliverableRecursiveTable
{
    [Parameter]
    public DeliverableResponse SelectedRow { get; set; } = null!;
    [Parameter]
    public DeliverableResponse CreateRow { get; set; } = null!;
    [Parameter]
    public DeliverableResponse EditRow { get; set; } = null!;
    [Parameter]
    public List<DeliverableResponse> Items { get; set; } = new();

    [Parameter]
    public EventCallback<DeliverableResponse> OnEdit { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnDelete { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnDoubleClick { get; set; }

    [Parameter]
    public EventCallback<DeliverableResponse> OnSaveEdit { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnSaveCreate { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnCancelEdit { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnCancelCreate { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnClick { get; set; }



    [Parameter]
    public Func<DeliverableResponse, TasksRelationTypeEnum, Task> OnChangeDependencyType { get; set; } = null!;
    [Parameter]
    public Func<DeliverableResponse, string, Task> OnChangeDuration { get; set; } = null!;
    [Parameter]
    public Func<DeliverableResponse, DateTime?, Task> OnChangeStartDate { get; set; } = null!;
    [Parameter]
    public Func<DeliverableResponse, DateTime?, Task> OnChangeEndDate { get; set; } = null!;
    [Parameter]
    public Func<DeliverableResponse, string, Task> OnChangeDependencies { get; set; } = null!;
    [Parameter]
    public Func<DeliverableResponse, string, Task> OnChangeName { get; set; } = null!;
}
