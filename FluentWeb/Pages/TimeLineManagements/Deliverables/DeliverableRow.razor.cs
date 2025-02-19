using Microsoft.AspNetCore.Components;
using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.Deliverables.Responses.NewResponses;

namespace FluentWeb.Pages.TimeLineManagements.Deliverables;
public partial class DeliverableRow
{
    [Parameter]
    public DeliverableResponse SelectedRow { get; set; } = null!;
    [Parameter]
    public DeliverableResponse EditRow { get; set; } = null!;
    [Parameter]
    public DeliverableResponse CreateRow { get; set; } = null!;
    [Parameter]
    public DeliverableResponse Item { get; set; } = null!;

    [Parameter]
    public int Level { get; set; }

    [Parameter]
    public EventCallback<DeliverableResponse> OnEdit { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnSaveEdit { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnSaveCreate { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnCancelEdit { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnCancelCreate { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnDelete { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnDoubleClick { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnClick { get; set; }
    bool DisableSaveButton(DeliverableResponse model)
    {
        return string.IsNullOrEmpty(model.Name) ? true : false;

    }
    

    async Task ChangeDependencyType(DeliverableResponse model, TasksRelationTypeEnum args)
    {
        if (args == null) return;
        if (model != null)
        {
            var DependencyType = args;
            await OnChangeDependencyType.Invoke(model, DependencyType);
        }


    }
    [Parameter]
    public Func<DeliverableResponse, TasksRelationTypeEnum, Task> OnChangeDependencyType { get; set; } = null!;

    [Parameter]
    public Func<DeliverableResponse, string, Task> OnChangeDuration { get; set; } = null!;
    [Parameter]
    public Func<DeliverableResponse, string, Task> OnChangeName { get; set; } = null!;

    [Parameter]
    public Func<DeliverableResponse, string, Task> OnChangeDependencies { get; set; } = null!;

    [Parameter]
    public Func<DeliverableResponse, DateTime?, Task> OnChangeStartDate { get; set; } = null!;
    [Parameter]
    public Func<DeliverableResponse, DateTime?, Task> OnChangeEndDate { get; set; } = null!;

}
