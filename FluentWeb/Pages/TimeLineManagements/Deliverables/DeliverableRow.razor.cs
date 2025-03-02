using Microsoft.AspNetCore.Components;
using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.Deliverables.Mappers;
using Shared.Models.Deliverables.Responses;
using static UnitSystem.Amount;

namespace FluentWeb.Pages.TimeLineManagements.Deliverables;
public partial class DeliverableRow
{
    [Parameter]
    public DeliverableResponseList Response { get; set; } = new();
    [Parameter]
    public EventCallback<DeliverableResponseList> ResponseChanged { get; set; }
    [Parameter]
    public DeliverableResponse Row { get; set; } = new();


    [Parameter]
    public EventCallback<DeliverableResponse> OnClick { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnEdit { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnSave { get; set; }

    [Parameter]
    public EventCallback<DeliverableResponse> OnCancel { get; set; }
    [Parameter]
    public EventCallback<DeliverableResponse> OnDelete { get; set; }
    [Parameter]
    public Func<Task> GetAll { get; set; } = null!;
    private bool DisableSaveButton(DeliverableResponse task)
    {
        // Lógica para deshabilitar el botón de guardar si es necesario
        return string.IsNullOrEmpty(task?.Name);
    }

    public async Task OnToggleTask(DeliverableResponse task)
    {

        task.IsExpanded = !task.IsExpanded;
        await ResponseChanged.InvokeAsync(Response);
     
        var result=await GenericService.Update(task.ToExpand());
        if (result.Succeeded)
        {

        }
    }
    public async Task OnChangeName(string name)
    {

        Row.Name = name;
        await ResponseChanged.InvokeAsync(Response);

    }
    public async Task OnChangeStartDate(DateTime? newdate)
    {
        Response.UpdateStartDate(Row, newdate);

        await ResponseChanged.InvokeAsync(Response);

    }
    public async Task OnChangeEndDate(DateTime? newdate)
    {
        Response.UpdateEndDate(Row, newdate);

        await ResponseChanged.InvokeAsync(Response);

    }
    public async Task OnChangeDuration(string newValue)
    {

        Response.UpdateDuration(Row, newValue);
        await ResponseChanged.InvokeAsync(Response);

    }
    public async Task OnChangeLag(string newValue)
    {

        Response.UpdateLag(Row, newValue);
        await ResponseChanged.InvokeAsync(Response);

    }
    async Task OnChangeDependencyType(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var selectedId))
        {
            var selectedType = TasksRelationTypeEnum.GetType(selectedId);
            Response.UpdateRelationType(Row, selectedType);
            await ResponseChanged.InvokeAsync(Response);
        }

       
    }
    public async Task OnChangeDependences(string newValue)
    {

        Response.UpdateDependencies(Row, newValue);
        await ResponseChanged.InvokeAsync(Response);

    }


}
