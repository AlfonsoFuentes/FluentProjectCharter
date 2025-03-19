using FluentWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Models.GanttTasks.Mappers;
using Shared.Models.GanttTasks.Records;
using Shared.Models.GanttTasks.Requests;
using Shared.Models.GanttTasks.Responses;
using System.Diagnostics;

namespace FluentWeb.Pages.TimeLineManagements.DeliverablesGanttTasks;
public partial class TableDeliverableList
{
    [Parameter]
    public Guid ProjectId { get; set; }

  
    DeliverableWithGanttTaskResponseList Response { get; set; } = new();

    async Task GetAll()
    {

        var result = await GenericService.GetAll<DeliverableWithGanttTaskResponseListToUpdate, GanttTaskGetAll>(new GanttTaskGetAll
        {
            ProjectId = ProjectId,
        });

        if (result.Succeeded)
        {

            Response = result.Data.ToReponse();

            StateHasChanged();

        }

    }
    private void OnClickDeliverable(DeliverableWithGanttTaskResponse deliverable)
    {
        SelectedDeliverable = deliverable;
    }
    protected override async Task OnParametersSetAsync()
    {
        if (ProjectId == Guid.Empty) return;
        await GetAll();
    }


    GanttTaskResponse CreateRow = null!;
    GanttTaskResponse EditRow = null!;
    GanttTaskResponse SelectedRow = null!;
    // Propiedad para deshabilitar el botón "Up"
    private bool DisableUpButton => SelectedDeliverable == null ? true : SelectedRow == null || !SelectedDeliverable.CanMoveUp(SelectedRow) || CreateRow != null || EditRow != null;
    DeliverableWithGanttTaskResponse? SelectedDeliverable = null!;
    private bool DisableAddButton => SelectedDeliverable == null ? true : CreateRow != null || EditRow != null;
    string CurrentDeliverableName => SelectedDeliverable == null ? string.Empty : SelectedDeliverable.Name;
    string CurrentRowName
    {
        get
        {
            if (EditRow != null)
            {
                return TruncateService.Truncate(EditRow.Name, 30);
            }

            if (SelectedRow != null)
            {
                return TruncateService.Truncate(SelectedRow.Name, 30);
            }


            return string.Empty;
        }
    }
    // Propiedad para deshabilitar el botón "Down"
    bool DisableDownButton => SelectedDeliverable == null ? true : SelectedRow == null || !SelectedDeliverable.CanMoveDown(SelectedRow) || CreateRow != null || EditRow != null;
    // Propiedad para deshabilitar el botón "Left"
    private bool DisableLeftButton => SelectedDeliverable == null ? true : SelectedRow == null || SelectedDeliverable.FindParent(SelectedRow) == null || CreateRow != null || EditRow != null;
    // Propiedad para deshabilitar el botón "Right"
    private bool DisableRightButton => SelectedDeliverable == null ? true : SelectedRow == null || !SelectedDeliverable.CanMoveRight(SelectedRow) || CreateRow != null || EditRow != null;



    public void AddNew()
    {
        if (SelectedDeliverable == null) return;
        SelectedDeliverable.IsExpanded = true;

        var newDeliverable = SelectedDeliverable.AddGanttTaskResponse(ProjectId, SelectedRow);
        CreateRow = newDeliverable;
        StateHasChanged();
    }

    void CancelEdit(GanttTaskResponse row)
    {
        if (SelectedDeliverable == null) return;
        if (CreateRow == row)
        {
            SelectedDeliverable.RemoveGanttTaskResponse(row);

        }
        row.IsEditing = false;
        SelectedRow = CreateRow == row ? null! : EditRow == row ? row : null!;
        CreateRow = null!;
        EditRow = null!;

    }
    DeliverableWithGanttTaskResponse? FindSelectedDeliverable(GanttTaskResponse row)
    {
        if (Response?.Deliverables == null)
        {
            return null;
        }

        // Itera sobre cada deliverable para encontrar uno cuyo FlatOrderedItems contenga el elemento buscado
        foreach (var deliverable in Response.Deliverables)
        {
            // Verifica que FlatOrderedItems no sea nulo antes de usar Contains
            if (deliverable.FlatOrderedItems != null && deliverable.FlatOrderedItems.Any(x => x.Id == row.Id))
            {
                return deliverable;
            }
        }

        // Si no se encuentra ningún deliverable que contenga el elemento, retorna null
        return null;

    }
    private void RowClick(GanttTaskResponse row)
    {
        if (row.IsEditing) return;
        SelectedDeliverable = FindSelectedDeliverable(row)!;
        SelectedRow = SelectedRow == null ? row : SelectedRow == row ? null! : row;
        CreateRow = null!;
        EditRow = null!;

    }
    private void RowEdit(GanttTaskResponse row)
    {
        if (SelectedDeliverable == null) return;
        SelectedDeliverable.FlatOrderedItems.ForEach(x => { x.IsEditing = false; });

        EditRow = row;
        EditRow.IsEditing = true;

        SelectedRow = null!;

        CreateRow = null!;

    }
    public async Task Delete(GanttTaskResponse model)
    {
        if (SelectedDeliverable == null) return;
        if (model == null) return;

        var dialog = await DialogService.ShowWarningAsync($"Delete {model.Name}?");
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            SelectedRow = null!;
            SelectedDeliverable.RemoveGanttTaskResponse(model);
            var request = new DeleteGanttTaskRequest
            {
                Id = model.Id,
                Name = model.Name,
                ProjectId = ProjectId,
            };

            var resultDelete = await GenericService.Delete(request);

            if (resultDelete.Succeeded)
            {
                await GetAll();


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
        if (SelectedDeliverable == null) return;
        if (SelectedRow == null) return;
        SelectedDeliverable.MoveUp(SelectedRow);
        SelectedDeliverable.Calculate();
        StateHasChanged() ;
        await UpdateResponseAsync();

    }
    async Task Down()
    {
        if (SelectedDeliverable == null) return;
        if (SelectedRow == null) return;
        SelectedDeliverable.MoveDown(SelectedRow);
        SelectedDeliverable.Calculate();
        StateHasChanged();
        await UpdateResponseAsync();

    }
    async Task Right()
    {
        if (SelectedDeliverable == null) return;
        if (SelectedRow == null) return;
        SelectedDeliverable.MoveRight(SelectedRow);
        SelectedDeliverable.Calculate();
        StateHasChanged();
        await UpdateResponseAsync();

    }
    async Task Left()
    {
        if (SelectedDeliverable == null) return;
        if (SelectedRow == null) return;
        SelectedDeliverable.MoveLeft(SelectedRow);
        SelectedDeliverable.Calculate();
        StateHasChanged();
        await UpdateResponseAsync();

    }
    private async Task<bool> UpdateResponseAsync()
    {

        var result = await GenericService.Update(Response.ToUpdate());

        if (result.Succeeded)
        {
            await GetAll();

            EditRow = null!;
            _snackBar.ShowSuccess(result.Messages);
            return true;
        }

        _snackBar.ShowError(result.Messages);
        return false;
    }


    async Task Save(GanttTaskResponse row)
    {
        if (row.IsEditing)
        {
            row.IsEditing = false;
            row.IsCreating = false;

            await UpdateResponseAsync();
        }
    }


}
