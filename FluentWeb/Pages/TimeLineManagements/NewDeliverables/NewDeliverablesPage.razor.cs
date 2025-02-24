using FluentWeb.Pages.TimeLineManagements.GanttCharts;
using FluentWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Enums.DeliverableStatuss;
using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.Deliverables.Mappers;
using Shared.Models.Deliverables.Records;
using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Deliverables.Responses.NewResponses;
using System.Diagnostics;

namespace FluentWeb.Pages.TimeLineManagements.NewDeliverables;
public partial class NewDeliverablesPage
{
    [Parameter]
    public Guid ProjectId { get; set; }

    DeliverableResponseList Response { get; set; } = new();
    public List<DeliverableResponse> Items => Response.OrderedItems.Count == 0 ? new() :Response.OrderedItems;
    DeliverableResponse CreateRow = null!;
    DeliverableResponse EditRow = null!;
    DeliverableResponse SelectedRow = null!;
    // Propiedad para deshabilitar el botón "Up"
    private bool DisableUpButton => SelectedRow == null || !Response.CanMoveUp(SelectedRow);

    string CurrentRowName => SelectedRow == null ? string.Empty : TruncateService.Truncate(SelectedRow.Name, 30);
    // Propiedad para deshabilitar el botón "Down"
    bool DisableDownButton => SelectedRow == null || !Response.CanMoveDown(SelectedRow);
    // Propiedad para deshabilitar el botón "Left"
    private bool DisableLeftButton => SelectedRow == null || Response.FindParent(SelectedRow) == null;
    // Propiedad para deshabilitar el botón "Right"
    private bool DisableRigthButton => SelectedRow == null || !Response.CanMoveRight(SelectedRow);
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<DeliverableResponseListToUpdate, DeliverableGetAll>(new DeliverableGetAll
        {
            ProjectId = ProjectId,
        });

        if (result.Succeeded)
        {


            var selectedRowId = SelectedRow?.Id;

            // Actualizar la lista Items
            Response = result.Data.ToResponse();

            // Restaurar SelectedRow si existe
            var FlatOrderedItems = DeliverableHelper.FlattenCompletedOrderedItems(Response.Items);
            SelectedRow = FlatOrderedItems.FirstOrDefault(x => x.Id == selectedRowId)!;

            // Forzar la actualización de la interfaz
            StateHasChanged();
        }
        
    }
    private void SetState(DeliverableResponse? createRow = null, DeliverableResponse? editRow = null, DeliverableResponse? selectedRow = null)
    {
        // Limpiar todos los estados antes de aplicar nuevos valores
        CreateRow = null!;
        EditRow = null!;
        SelectedRow = null!;

        // Aplicar los nuevos estados
        CreateRow = createRow!;
        EditRow = editRow!;
        SelectedRow = selectedRow!;

        // Forzar la actualización de la interfaz
        StateHasChanged();
    }
    
    public void AddNew()
    {
        var newDeliverable = Response.AddDeliverableResponse(ProjectId);
        SetState(createRow: newDeliverable); // Activar el modo de creación
    }
    public async Task Create(DeliverableResponse createRow)
    {
        if (createRow == null) return;

        var create = createRow.ToCreate();
        var result = await GenericService.Create(create);

        if (result.Succeeded)
        {
            SetState(); // Limpiar todos los estados
            Response.Calculate();
            await GetAll();
           
            StateHasChanged();
        }
    }
    void CancelCreate()
    {
        if (CreateRow != null)
        {
            Response.RemoveDeliverableResponse(CreateRow);
            SetState(); // Limpiar todos los estados
        }
    }
    void CancelEdit()
    {
        SetState(); // Limpiar todos los estados
    }
    private void RowClick(DeliverableResponse row)
    {
        if (row == null) return;

        // Alternar la selección de la fila
        var newSelectedRow = SelectedRow?.Id == row.Id ? null : row;

        // Limpiar EditRow si no coincide con la fila seleccionada
        var newEditRow = EditRow?.Id == row?.Id ? EditRow : null;

        SetState(editRow: newEditRow, selectedRow: newSelectedRow, createRow: CreateRow);
    }
    public async Task Delete(DeliverableResponse model)
    {
        if (model == null) return;

        var dialog = await DialogService.ShowWarningAsync($"Delete {model.Name}?");
        var result = await dialog.Result;

        if (!result.Cancelled)
        {


            var request = new DeleteDeliverableRequest
            {
                Id = model.Id,
                Name = model.Name,
                ProjectId = ProjectId,
            };

            var resultDelete = await GenericService.Delete(request);

            if (resultDelete.Succeeded)
            {
                await GetAll();
                Response.Calculate();

                StateHasChanged();
            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }
    }
    async Task Up()
    {
        if (SelectedRow == null) return;
        Response.MoveUp(SelectedRow);
        Response.Calculate();
        await UpdateResponseAsync();

    }
    async Task Down()
    {
        if (SelectedRow == null) return;
        Response.MoveDown(SelectedRow);
        Response.Calculate();
        await UpdateResponseAsync();

    }
    async Task Right()
    {
        if (SelectedRow == null) return;
        Response.MoveRight(SelectedRow);
        Response.Calculate();
        await UpdateResponseAsync();

    }
    async Task Left()
    {
        if (SelectedRow == null) return;
        Response.MoveLeft(SelectedRow);
        Response.Calculate();
        StateHasChanged();
        await UpdateResponseAsync();

    }
    private async Task<bool> UpdateResponseAsync()
    {
        if (CreateRow != null) return false;
        Stopwatch sw = Stopwatch.StartNew();
        var result = await GenericService.Update(Response.ToUpdate());
        sw.Stop();
        var elpase1 = sw.Elapsed;
        sw.Restart();
        if (result.Succeeded)
        {
            await GetAll();
            sw.Stop();
            var elpase2 = sw.Elapsed;

            EditRow = null!;
            _snackBar.ShowSuccess(result.Messages);
            return true;
        }

        _snackBar.ShowError(result.Messages);
        return false;
    }
    void EditItem(DeliverableResponse item)
    {
        SetState(editRow: item, selectedRow: item); // Activar el modo de edición
    }
    private async Task UpdateDependencyType(DeliverableResponse task, TasksRelationTypeEnum type)
    {


        Response.UpdateRelationType(task, type);
        StateHasChanged();
        //await UpdateResponseAsync();

    }
    private async Task UpdateName(DeliverableResponse task, string newValue)
    {

        task.Name = newValue;
        StateHasChanged();
        //await UpdateResponseAsync();
        StateHasChanged();
    }
    private async Task UpdateDependencies(DeliverableResponse task, string newValue)
    {

        var result = Response.UpdateDependencies(task, newValue);
        StateHasChanged();
        if (string.IsNullOrEmpty(result))
        {
            //await UpdateResponseAsync();
        }
        else
        {
            _snackBar.ShowError(result);
        }

        StateHasChanged();
    }
    private async Task UpdateLag(DeliverableResponse task, string newValue)
    {

        Response.UpdateLag(task, newValue);
        StateHasChanged();
        //await UpdateResponseAsync();

        StateHasChanged();
    }
    private async Task UpdateDuration(DeliverableResponse task, string newValue)
    {

        Response.UpdateDuration(task, newValue);
        StateHasChanged();
        //await UpdateResponseAsync();
        StateHasChanged();
    }
    private async Task UpdateStartDate(DeliverableResponse task, DateTime? newValue)
    {

        Response.UpdateStartDate(task, newValue);
        StateHasChanged();
        //await UpdateResponseAsync();
        StateHasChanged();

    }
    private async Task UpdateEndDate(DeliverableResponse task, DateTime? newValue)
    {

        Response.UpdateEndDate(task, newValue);
        StateHasChanged();
        //await UpdateResponseAsync();
        StateHasChanged();
    }
    private RenderFragment RenderTaskRow(DeliverableResponse task, int level) => builder =>
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "gantt-sidebar-item");
        builder.AddAttribute(2, "style", $"padding-left: {level * 20}px");

        if (task.OrderedSubDeliverables.Any())
        {
            builder.OpenElement(3, "span");
            builder.AddAttribute(4, "class", "expand-icon");
            builder.AddAttribute(5, "onclick", EventCallback.Factory.Create(this, () => ToggleTask(task)));
            builder.AddContent(6, task.IsExpanded ? "▼" : "►");
            builder.CloseElement();
        }

        builder.AddContent(7, task.Name);
        builder.CloseElement();

        if (task.IsExpanded)
        {
            foreach (var subtask in task.OrderedSubDeliverables)
            {
                builder.AddContent(8, RenderTaskRow(subtask, level + 1));
            }
        }
    };
    private int WindowWidth { get; set; }
    private DotNetObjectReference<NewDeliverablesPage> _dotNetRef = null!;
    private void ToggleTask(DeliverableResponse task)
    {
        task.IsExpanded = !task.IsExpanded;
        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && Items.Count > 0)
        {
            var dimensions = await _jsRuntime.InvokeAsync<Dictionary<string, int>>("getWindowDimensions");
            WindowWidth = dimensions["width"];
            _dotNetRef = DotNetObjectReference.Create(this);
            await _jsRuntime.InvokeVoidAsync("onWindowResize", _dotNetRef);

            // Asegúrate de que los elementos estén presentes antes de sincronizar el desplazamiento
            await _jsRuntime.InvokeVoidAsync("syncScroll");
            await InvokeAsync(async () =>
            {
                StateHasChanged();
                await Task.Delay(100); // Espera breve para asegurar que el DOM esté actualizado
                await _jsRuntime.InvokeVoidAsync("syncRowHeights");
            });
        }
    }

    [JSInvokable]
    public void UpdateWindowDimensions(Dictionary<string, int> dimensions)
    {
        WindowWidth = dimensions["width"];
        StateHasChanged();
    }

    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }

    private int ColumnWidth => Math.Max(60, (int)(WindowWidth / Response.Duration.Days));

    private string GetDeliverableColor(DeliverableStatus status)
    {
        return status switch
        {
            DeliverableStatus.Completed => "#4CAF50",    // Verde
            DeliverableStatus.InProgress => "#2196F3",   // Azul
            DeliverableStatus.Delayed => "#f44336",      // Rojo
            DeliverableStatus.OnHold => "#FFA726",       // Naranja
            _ => "#9E9E9E"                               // Gris para otros estados
        };
    }
}
