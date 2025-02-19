using Microsoft.AspNetCore.Components;
using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.Milestones.Responses;
using System.Reflection.Metadata;

namespace FluentWeb.Pages.TimeLineManagements.Milestones;
public partial class MilestoneRow
{
  
    [Parameter]
    public MilestoneResponse Model { get; set; } = new();
    [Parameter]
    public EventCallback<MilestoneResponse> SelectedRowChanged { get; set; }

    [Parameter]
    public MilestoneResponse SelectedRow { get; set; } = new();
    async Task OnClick(MilestoneResponse model)
    {
        if (model != null)
        {
            await SelectedRowChanged.InvokeAsync(model);
        }
    }
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
    [Parameter]
    [EditorRequired]
    public List<MilestoneResponse> AllItems { get; set; } = new();
    IEnumerable<MilestoneResponse> SubTasks => GetSubtasks();

    bool IsSelected => SelectedRow == null ? false : SelectedRow.Id == Model.Id;
    [Parameter]
    public int IndentLevel { get; set; }
    public string BackgroundColor => IsSelected ? $"#f0f0f0" : $"white";
    async Task OnChangeStartDate(MilestoneResponse model, DateTime? startDate)
    {
        if (model != null)
        {
            await ChangeStartDate.Invoke(model, startDate);
        }

    }
    async Task OnChangeEndDate(MilestoneResponse model, DateTime? startDate)
    {
        if (model != null)
        {
            await ChangeEndDate.Invoke(model, startDate);
        }

    }
    async Task OnChangeDuration(MilestoneResponse model, string duration)
    {
        if (model != null)
        {
            await ChangeDuration.Invoke(model, duration);
        }

    }
    async Task OnChangeName(MilestoneResponse model, string duration)
    {
        if (model != null)
        {
            await ChangeName.Invoke(model, duration);
        }

    }
    async Task OnChangeDependencyType(MilestoneResponse model, ChangeEventArgs args)
    {
        if (args == null) return;
        if (model != null)
        {
            var DependencyType = TasksRelationTypeEnum.GetType(args!.Value!.ToString()!);
            await ChangeDependencyType.Invoke(model, DependencyType);
        }


    }
    async Task OnChangeDependency(MilestoneResponse model, string newDependency)
    {
        var dependencyModel = AllItems.FirstOrDefault(x => x.Name.Equals(newDependency));
        if (model != null)
        {
            await ChangeDependency.Invoke(model, dependencyModel);
        }

    }
    private IEnumerable<MilestoneResponse> GetSubtasks()
    {
        return AllItems.Where(t => t.ParentTaskName == Model.Name);
    }
}
