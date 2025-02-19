using Microsoft.AspNetCore.Components;
using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.Milestones.Responses;

namespace FluentWeb.Pages.TimeLineManagements.Milestones;
public partial class MilestoneTable
{
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; } = null!;
    public List<MilestoneResponse> Items => AllItems.Where(x => string.IsNullOrEmpty(x.ParentTaskName)).ToList();

    [Parameter]
    public List<MilestoneResponse> AllItems { get; set; } = new();

    [Parameter]
    public MilestoneResponse SelectedRow { get; set; } = null!;
    [Parameter]
    public EventCallback<MilestoneResponse> SelectedRowChanged { get; set; }


    // Método para actualizar la fecha final
    [Parameter]
    public Func<MilestoneResponse, DateTime?, Task> ChangeStartDate { get; set; } = null!;

    [Parameter]
    public Func<MilestoneResponse, DateTime?, Task> ChangeEndDate { get; set; } = null!;

    [Parameter]
    public Func<MilestoneResponse, string, Task> ChangeDuration { get; set; } = null!;
    [Parameter]
    public Func<MilestoneResponse, string, Task> ChangeName { get; set; } = null!;

    [Parameter]
    public Func<MilestoneResponse, MilestoneResponse?, Task> ChangeDependency { get; set; } = null!;
    [Parameter]
    public Func<MilestoneResponse, TasksRelationTypeEnum, Task> ChangeDependencyType { get; set; } = null!;


}
