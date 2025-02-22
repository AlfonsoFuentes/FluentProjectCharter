using FluentWeb.Services;
using Microsoft.AspNetCore.Components;
using Shared.Enums.TasksRelationTypeTypes;
using Shared.Models.Deliverables.Mappers;
using Shared.Models.Deliverables.Records;
using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Deliverables.Responses.NewResponses;
using Shared.Models.Milestones.Responses;
using Shared.Models.Projects.Mappers;

namespace FluentWeb.Pages.TimeLineManagements.Deliverables;

public partial class DeliverableTable
{
    [Parameter]
    public Guid ProjectId { get; set; }

    [Parameter]
    public string IdType { get; set; } = string.Empty;

    [Parameter]
    public Guid Id { get; set; }

    Guid? StartId;
    Guid? PlanningId;

    DeliverableResponseList Response { get; set; } = new();
    public List<DeliverableResponse> Items => Response.OrderedItems.Count == 0 ? new() :
        StartId.HasValue ? Response.OrderedItems.Where(x => x.StartId == StartId).ToList() :
        Response.OrderedItems;

    DeliverableResponse CreateRow = null!;
    DeliverableResponse EditRow = null!;
    DeliverableResponse SelectedRow = null!;

    string CurrentRowName => SelectedRow == null ? string.Empty : TruncateService.Truncate(SelectedRow.Name, 30);

    // Propiedad para deshabilitar el botón "Up"
    private bool DisableUpButton => SelectedRow == null || !Response.CanMoveUp(SelectedRow);

    // Propiedad para deshabilitar el botón "Down"
    bool DisableDownButton => SelectedRow == null || !Response.CanMoveDown(SelectedRow);
    // Propiedad para deshabilitar el botón "Left"
    private bool DisableLeftButton => SelectedRow == null || Response.FindParent(SelectedRow) == null;
    // Propiedad para deshabilitar el botón "Right"
    private bool DisableRigthButton => SelectedRow == null || !Response.CanMoveRight(SelectedRow);


    protected override async Task OnInitializedAsync()
    {
        ValidateIdType();
        await GetAll();
    }

    private void ValidateIdType()
    {
        if (IdType.Equals("Planning", StringComparison.OrdinalIgnoreCase))
        {
            PlanningId = Id;
        }
        else if (IdType.Equals("Start", StringComparison.OrdinalIgnoreCase))
        {
            StartId = Id;
        }
    }

    async Task GetAll()
    {
        var result = await GenericService.GetAll<DeliverableResponseList, DeliverableGetAll>(new DeliverableGetAll
        {
            ProjectId = ProjectId,
        });

        if (result.Succeeded)
        {


            var selectedRowId = SelectedRow?.Id;

            // Actualizar la lista Items
            Response = result.Data;

            // Restaurar SelectedRow si existe
            var FlatOrderedItems = DeliverableHelper.FlattenCompletedOrderedItems(Response.Items);
            SelectedRow = FlatOrderedItems.FirstOrDefault(x => x.Id == selectedRowId)!;

            // Forzar la actualización de la interfaz
            StateHasChanged();
        }
    }

    /// <summary>
    /// Centraliza la gestión de estados para evitar inconsistencias.
    /// </summary>
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

        var create = createRow.ToCreate(StartId, PlanningId);
        var result = await GenericService.Create(create);

        if (result.Succeeded)
        {
            SetState(); // Limpiar todos los estados
            await GetAll();
            Response.Calculate();
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
        await UpdateResponseAsync();

    }

    private async Task<bool> UpdateResponseAsync()
    {
        if (EditRow == null) return false;

        var result = await GenericService.Update(Response);

        if (result.Succeeded)
        {
            await GetAll();
            EditRow = null!;

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
        await Task.Delay(100);

        Response.UpdateRelationType(task, type);
        await UpdateResponseAsync();
        StateHasChanged();
    }
    private async Task UpdateName(DeliverableResponse task, string newValue)
    {
        await Task.Delay(100);
        task.Name = newValue;

        await UpdateResponseAsync();
        StateHasChanged();
    }
    private async Task UpdateDependencies(DeliverableResponse task, string newValue)
    {
        await Task.Delay(100);
        var result = Response.UpdateDependencies(task, newValue);
        if (string.IsNullOrEmpty(result))
        {
            await UpdateResponseAsync();
        }
        else
        {
            _snackBar.ShowError(result);
        }

        StateHasChanged();
    }
    private async Task UpdateLag(DeliverableResponse task, string newValue)
    {
        await Task.Delay(100);
        Response.UpdateLag(task, newValue);
        await UpdateResponseAsync();

        StateHasChanged();
    }
    private async Task UpdateDuration(DeliverableResponse task, string newValue)
    {
        await Task.Delay(100);
        Response.UpdateDuration(task, newValue);
        await UpdateResponseAsync();
        StateHasChanged();
    }
    private async Task UpdateStartDate(DeliverableResponse task, DateTime? newValue)
    {
        await Task.Delay(100);
        Response.UpdateStartDate(task, newValue);
        await UpdateResponseAsync();
        StateHasChanged();

    }
    private async Task UpdateEndDate(DeliverableResponse task, DateTime? newValue)
    {
        await Task.Delay(100);
        Response.UpdateEndDate(task, newValue);
        await UpdateResponseAsync();
        StateHasChanged();
    }

}
