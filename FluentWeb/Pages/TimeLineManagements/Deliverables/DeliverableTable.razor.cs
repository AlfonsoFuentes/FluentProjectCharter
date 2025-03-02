using FluentWeb.Pages.TimeLineManagements.NewNewDeliverables;
using FluentWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Models.Deliverables.Mappers;
using Shared.Models.Deliverables.Records;
using Shared.Models.Deliverables.Requests;
using Shared.Models.Deliverables.Responses;
using System.Diagnostics;

namespace FluentWeb.Pages.TimeLineManagements.Deliverables;
public partial class DeliverableTable
{
    [Parameter]
    public Guid ProjectId { get; set; }
    public int WindowWidth { get; set; } = 1024; // Ancho inicial por defecto
    public int WindowHeight { get; set; } = 768; // Altura inicial por defecto

    private DotNetObjectReference<DeliverableTable> _dotNetRef = null!;
    DeliverableResponseList Response { get; set; } = new();
    public List<DeliverableResponse> Items => Response.OrderedItems.Count == 0 ? new() : Response.OrderedItems;
    DeliverableResponse CreateRow = null!;
    DeliverableResponse EditRow = null!;
    DeliverableResponse SelectedRow = null!;
    // Propiedad para deshabilitar el botón "Up"
    private bool DisableUpButton => SelectedRow == null || !Response.CanMoveUp(SelectedRow) || CreateRow != null || EditRow != null;

    private bool DisableAddButton => CreateRow != null || EditRow != null;
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
    bool DisableDownButton => SelectedRow == null || !Response.CanMoveDown(SelectedRow) || CreateRow != null || EditRow != null;
    // Propiedad para deshabilitar el botón "Left"
    private bool DisableLeftButton => SelectedRow == null || Response.FindParent(SelectedRow) == null || CreateRow != null || EditRow != null;
    // Propiedad para deshabilitar el botón "Right"
    private bool DisableRightButton => SelectedRow == null || !Response.CanMoveRight(SelectedRow) || CreateRow != null || EditRow != null;
    protected override async Task OnInitializedAsync()
    {
        await GetAll();

    }
    async Task GetAll()
    {
        Stopwatch sw = Stopwatch.StartNew();
        var result = await GenericService.GetAll<DeliverableResponseListToUpdate, DeliverableGetAll>(new DeliverableGetAll
        {
            ProjectId = ProjectId,
        });

        if (result.Succeeded)
        {
            sw.Stop();
            var elapse1 = sw.Elapsed;
            
            var selectedRowId = SelectedRow?.Id;

            // Actualizar la lista Items
            Response = result.Data.ToResponse();
            StateHasChanged();
            Response.CalculateColumnWidths();
            SelectedRow = Response.FlatOrderedItems.FirstOrDefault(x => x.Id == selectedRowId)!;
            // Forzar la actualización de la interfaz
            StateHasChanged();
        }

    }

    public void AddNew()
    {
        var newDeliverable = Response.AddDeliverableResponse(ProjectId, SelectedRow);
        CreateRow = newDeliverable;
    }
    public async Task Create(DeliverableResponse createRow)
    {
        if (createRow == null) return;

        var create = createRow.ToCreate();
        var result = await GenericService.Create(create);

        if (result.Succeeded)
        {
            _snackBar.ShowSuccess(result.Messages);
            await GetAll();
            CreateRow = null!;

        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }
    }
    void CancelEdit(DeliverableResponse row)
    {
        if (CreateRow == row)
        {
            Response.RemoveDeliverableResponse(row);

        }
        row.IsEditing = false;
        SelectedRow = CreateRow == row ? null! : EditRow == row ? row : null!;
        CreateRow = null!;
        EditRow = null!;

    }
    private void RowClick(DeliverableResponse row)
    {
        if (row.IsEditing) return;

        SelectedRow = SelectedRow == null ? row : SelectedRow == row ? null! : row;
        CreateRow = null!;
        EditRow = null!;

    }
    private void RowEdit(DeliverableResponse row)
    {
        Response.FlatOrderedItems.ForEach(x => { x.IsEditing = false; });

        EditRow = row;
        EditRow.IsEditing = true;

        SelectedRow = null!;

        CreateRow = null!;

    }
    public async Task Delete(DeliverableResponse model)
    {
        if (model == null) return;

        var dialog = await DialogService.ShowWarningAsync($"Delete {model.Name}?");
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            SelectedRow = null!;
            Response.RemoveDeliverableResponse(model);
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


    async Task Save(DeliverableResponse row)
    {
        if (row.IsEditing)
        {
            row.IsEditing = false;
            row.IsCreating = false;

            await UpdateResponseAsync();
        }
    }



    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            var dimensions = await _jsRuntime.InvokeAsync<Dictionary<string, int>>("getWindowDimensions");

            UpdateWindowDimensions(dimensions);



        }
    }


    string StyleContaniner => $"height: {WindowHeight - 100}px;";
    [JSInvokable]
    public void UpdateWindowDimensions(Dictionary<string, int> dimensions)
    {
        WindowWidth = dimensions["width"];
        WindowHeight = dimensions["height"];
        StateHasChanged();
    }

    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }



}
