using FluentWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Models.Objectives.Mappers;
using Shared.Models.Objectives.Records;
using Shared.Models.Objectives.Requests;
using Shared.Models.Objectives.Responses;
using Shared.Models.Projects.Mappers;

namespace FluentWeb.Pages.ScopeManagements.Objectives;
#nullable disable
public partial class ObjectiveTable
{
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public string IdType { get; set; }
    [Parameter]
    public Guid Id { get; set; }

    Guid? StartId { get; set; } = null;
    Guid? PlanningId { get; set; } = null;
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
    public List<ObjectiveResponse> Items { get; set; } = new();


    string nameFilter { get; set; } = string.Empty;
    Func<ObjectiveResponse, bool> fiterexpresion => x =>
       x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase);
    public List<ObjectiveResponse> FilteredItems => Items.Count == 0 ? new() : Items.Where(fiterexpresion).ToList();

    ObjectiveResponse CreateRow = null!;
    ObjectiveResponse EditRow = null!;
    ObjectiveResponse SelectedRow = null!;
    string CurrentRowName => SelectedRow == null ? string.Empty : TruncateService.Truncate(SelectedRow.Name, 30);
    bool DisableUpButton => SelectedRow == null ? true : SelectedRow.Order == 1;
    bool DisableDownButton => SelectedRow == null ? true : SelectedRow.Order == LastOrder;
    public int LastOrder => Items.Count == 0 ? 1 : Items.MaxBy(x => x.Order).Order;
    protected override async Task OnInitializedAsync()
    {
        ValidateIdType();
        await GetAll();
    }

    bool DisableSaveButton(ObjectiveResponse model)
    {
        return string.IsNullOrEmpty(model.Name) ? true : false;

    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<ObjectiveResponseList, ObjectiveGetAll>(new ObjectiveGetAll()
        {
            ProjectId = ProjectId,

        });
        if (result.Succeeded)
        {
            Items = StartId.HasValue ? result.Data.Items.Where(x => x.StartId == StartId).ToList() : result.Data.Items;

            GetSelectedRowFromItems();
        }
    }
    public void AddNew()
    {
        CreateRow = new()
        {
            ProjectId = ProjectId,
        };
        //Si EditRow esta creada se desaparece Editrow 
        EditRow = null!;
        Items.Add(CreateRow);
    }
    public async Task Create()
    {
        if (CreateRow == null) return;
        CreateObjectiveRequest create = CreateRow.ToCreate(StartId, PlanningId);
        var result = await GenericService.Create(create);
        if (result.Succeeded)
        {
            CreateRow = null;
            await GetAll();

        }
    }
    void CancelCreate()
    {
        if (CreateRow == null) return;

        Items.Remove(CreateRow);
        CreateRow = null;
    }
    void CancelEdit()
    {
        if (EditRow == null) return;
        EditRow = null;
    }
    private void HandleRowClick(FluentDataGridRow<ObjectiveResponse> row)
    {
        SelectedRow = row.Item == null ? null : SelectedRow == null ? row.Item : SelectedRow.Id == row.Item.Id ? SelectedRow : row.Item;
        //Si EditRow es diferente al seleccionado se vuelve null para desaparecer la caja de texto
        EditRow = EditRow == null ? null : SelectedRow == null ? null : EditRow.Id != SelectedRow.Id ? null : EditRow;



    }
    private void HandleRowDoubleClick(FluentDataGridRow<ObjectiveResponse> row)
    {
        EditRow = row.Item == null ? null : SelectedRow == null ? row.Item : SelectedRow.Id == row.Item.Id ? SelectedRow : row.Item;
        //Si CreateRow esta creada se elimina del listado y se vueleve null
        CancelCreate();

    }


    async Task Update(ObjectiveResponse model)
    {

        var result = await GenericService.Update(model.ToUpdate());
        if (result.Succeeded)
        {

            await GetAll();
            EditRow = null!;
        }
    }
    void GetSelectedRowFromItems()
    {
        SelectedRow = SelectedRow == null ? null : Items.FirstOrDefault(x => x.Id == SelectedRow.Id);
    }
    public async Task Delete()
    {
        if (SelectedRow == null) return;
        var dialog = await DialogService.ShowWarningAsync($"Delete {SelectedRow.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteObjectiveRequest request = new()
            {
                Id = SelectedRow.Id,
                Name = SelectedRow.Name,
            };
            var resultDelete = await GenericService.Delete(request);
            if (resultDelete.Succeeded)
            {
                await GetAll();
                _snackBar.ShowSuccess(resultDelete.Messages);


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


        var result = await GenericService.Update(SelectedRow.ToUp());
        if (result.Succeeded)
        {
            await GetAll();
        }
    }
    async Task Down()
    {
        if (SelectedRow == null) return;

        var result = await GenericService.Update(SelectedRow.ToDown());

        if (result.Succeeded)
        {
            await GetAll();
        }
    }
}
