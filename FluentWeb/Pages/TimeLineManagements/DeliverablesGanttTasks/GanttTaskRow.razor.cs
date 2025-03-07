using Microsoft.AspNetCore.Components;
using Shared.Enums.TasksRelationTypes;
using Shared.Models.GanttTasks.Mappers;
using Shared.Models.GanttTasks.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.TimeLineManagements.DeliverablesGanttTasks;
public partial class GanttTaskRow
{
    [Parameter]
    public DeliverableWithGanttTaskResponse Deliverable { get; set; } = new();
    [Parameter]
    public EventCallback<DeliverableWithGanttTaskResponse> DeliverableChanged { get; set; }
    [Parameter]
    public GanttTaskResponse Task { get; set; } = new();


    [Parameter]
    public EventCallback<GanttTaskResponse> OnClick { get; set; }
    [Parameter]
    public EventCallback<GanttTaskResponse> OnEdit { get; set; }
    [Parameter]
    public EventCallback<GanttTaskResponse> OnSave { get; set; }

    [Parameter]
    public EventCallback<GanttTaskResponse> OnCancel { get; set; }
    [Parameter]
    public EventCallback<GanttTaskResponse> OnDelete { get; set; }
    [Parameter]
    public Func<Task> GetAll { get; set; } = null!;
    private bool DisableSaveButton(GanttTaskResponse task)
    {
        // L�gica para deshabilitar el bot�n de guardar si es necesario
        return string.IsNullOrEmpty(task?.Name);
    }

    public async Task OnToggleTask(GanttTaskResponse task)
    {

        task.IsExpanded = !task.IsExpanded;
        await DeliverableChanged.InvokeAsync(Deliverable);

        var result = await GenericService.Update(task.ToExpand());
        if (result.Succeeded)
        {

        }
    }
    public async Task OnChangeName(string name)
    {

        Task.Name = name;
        await DeliverableChanged.InvokeAsync(Deliverable);

    }
    public async Task OnChangeStartDate(DateTime? newdate)
    {
        Deliverable.UpdateStartDate(Task, newdate);

        await DeliverableChanged.InvokeAsync(Deliverable);

    }
    public async Task OnChangeEndDate(DateTime? newdate)
    {
        Deliverable.UpdateEndDate(Task, newdate);

        await DeliverableChanged.InvokeAsync(Deliverable);

    }
    public async Task OnChangeDuration(string newValue)
    {

        Deliverable.UpdateDuration(Task, newValue);
        await DeliverableChanged.InvokeAsync(Deliverable);

    }
    public async Task OnChangeLag(string newValue)
    {

        Deliverable.UpdateLag(Task, newValue);
        await DeliverableChanged.InvokeAsync(Deliverable);

    }
    async Task OnChangeDependencyType(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var selectedId))
        {
            var selectedType = TasksRelationTypeEnum.GetType(selectedId);
            Deliverable.UpdateRelationType(Task, selectedType);
            await DeliverableChanged.InvokeAsync(Deliverable);
        }


    }
    public async Task OnChangeDependences(string newValue)
    {

        Deliverable.UpdateDependencies(Task, newValue);
        await DeliverableChanged.InvokeAsync(Deliverable);

    }



}
