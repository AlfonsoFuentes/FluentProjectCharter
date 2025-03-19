using FluentWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Models.Backgrounds.Responses;
using Shared.Models.Constrainsts.Mappers;
using Shared.Models.Constrainsts.Records;
using Shared.Models.Constrainsts.Requests;
using Shared.Models.Constrainsts.Responses;
using Shared.Models.Projects.Mappers;

namespace FluentWeb.Pages.ScopeManagements.Constrainsts;
#nullable disable
public partial class ConstrainstTable
{
    [Parameter]
    public Guid ProjectId { get; set; }
    protected override async Task OnParametersSetAsync()
    {
        if (ProjectId == Guid.Empty) return;
        await GetAll();
    }

    public List<ConstrainstResponse> Items { get; set; } = new();
    string nameFilter { get; set; } = string.Empty;
    Func<ConstrainstResponse, bool> fiterexpresion => x =>
       x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase);
    public List<ConstrainstResponse> FilteredItems => Items.Count == 0 ? new() : Items.Where(fiterexpresion).ToList();

    ConstrainstResponse CreateRow = null!;
    ConstrainstResponse EditRow = null!;
    ConstrainstResponse SelectedRow = null!;
    string CurrentRowName => SelectedRow == null ? string.Empty : TruncateService.Truncate(SelectedRow.Name, 30);
    bool DisableUpButton => SelectedRow == null ? true : SelectedRow.Order == 1;
    bool DisableDownButton => SelectedRow == null ? true : SelectedRow.Order == LastOrder;
    public int LastOrder => Items.Count == 0 ? 1 : Items.MaxBy(x => x.Order).Order;

    bool DisableSaveButton(ConstrainstResponse model)
    {
        return string.IsNullOrEmpty(model.Name) ? true : false;

    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<ConstrainstResponseList, ConstrainstGetAll>(new ConstrainstGetAll()
        {
            ProjectId = ProjectId,

        });
        if (result.Succeeded)
        {
            Items = result.Data.Items;


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
        CreateConstrainstRequest create = CreateRow.ToCreate();
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
    private void HandleRowClick(FluentDataGridRow<ConstrainstResponse> row)
    {
        SelectedRow = row.Item == null ? null : SelectedRow == null ? row.Item : SelectedRow.Id == row.Item.Id ? SelectedRow : row.Item;
        //Si EditRow es diferente al seleccionado se vuelve null para desaparecer la caja de texto
        EditRow = EditRow == null ? null : SelectedRow == null ? null : EditRow.Id != SelectedRow.Id ? null : EditRow;



    }
    private void HandleRowDoubleClick(FluentDataGridRow<ConstrainstResponse> row)
    {
        EditRow = row.Item == null ? null : SelectedRow == null ? row.Item : SelectedRow.Id == row.Item.Id ? SelectedRow : row.Item;
        //Si CreateRow esta creada se elimina del listado y se vueleve null
        CancelCreate();

    }


    async Task Update(ConstrainstResponse model)
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
    void Edit(ConstrainstResponse model)
    {
        EditRow = model;
        SelectedRow = null;
        CreateRow = null;
    }
    public async Task Delete(ConstrainstResponse model)
    {
     
        var dialog = await DialogService.ShowWarningAsync($"Delete {model.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteConstrainstRequest request = new()
            {
                Id = model.Id,
                Name = model.Name,
                 
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
