using Microsoft.AspNetCore.Components;
using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.Milestones.Responses;

namespace FluentWeb.Pages.TimeLineManagements.Milestones;
public partial class SubMilestonesList
{
    [Parameter]
    public List<MilestoneResponse> CompleteItems { get; set; } = new();

    [Parameter]
    public List<MilestoneResponse> Items { get; set; } = new();
    [Parameter]
    public EventCallback<MilestoneResponse> OnSelectModel { get; set; }
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; } = null!;

    async Task OnClick(MilestoneResponse Model)
    {
        if (Model != null)
        {
            await OnSelectModel.InvokeAsync(Model);
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

    async Task OnChangeStartDate(MilestoneResponse Model,DateTime? startDate)
    {
        if (Model != null)
        {
            await ChangeStartDate.Invoke(Model, startDate);
        }

    }
    async Task OnChangeEndDate(MilestoneResponse Model, DateTime? startDate)
    {
        if (Model != null)
        {
            await ChangeEndDate.Invoke(Model, startDate);
        }

    }
    async Task OnChangeDuration(MilestoneResponse Model, string duration)
    {
        if (Model != null)
        {
            await ChangeDuration.Invoke(Model, duration);
        }

    }
    async Task OnChangeName(MilestoneResponse Model, string duration)
    {
        if (Model != null)
        {
            await ChangeName.Invoke(Model, duration);
        }

    }
    async Task OnChangeDependencyType(MilestoneResponse Model, ChangeEventArgs args)
    {
        if (args == null) return;
        if (Model != null)
        {
            var DependencyType = TasksRelationTypeEnum.GetType(args!.Value!.ToString()!);
            await ChangeDependencyType.Invoke(Model, DependencyType);
        }


    }
    async Task OnChangeDependency(MilestoneResponse Model, string newDependency)
    {
        var dependencyModel = Items.FirstOrDefault(x => x.Name.Equals(newDependency));
        if (Model != null)
        {
            await ChangeDependency.Invoke(Model, dependencyModel);
        }

    }

}
