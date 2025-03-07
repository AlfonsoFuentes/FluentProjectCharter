using FluentWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Models.Qualitys.Mappers;
using Shared.Models.Qualitys.Records;
using Shared.Models.Qualitys.Requests;
using Shared.Models.Qualitys.Responses;
using Shared.Models.Projects.Mappers;

namespace FluentWeb.Pages.MinorManagements.Qualitys;
#nullable disable
public partial class QualityTable
{
    [Parameter]
    public Guid ProjectId { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
       
        await GetAll();
    }

    public List<QualityResponse> Items { get; set; } = new();
    string nameFilter { get; set; } = string.Empty;
    Func<QualityResponse, bool> fiterexpresion => x =>
       x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase);
    public List<QualityResponse> FilteredItems => Items.Count == 0 ? new() : Items.Where(fiterexpresion).ToList();

    QualityResponse CreateRow = null!;
    QualityResponse EditRow = null!;
    QualityResponse SelectedRow = null!;
    string CurrentRowName => SelectedRow == null ? string.Empty : TruncateService.Truncate(SelectedRow.Name, 30);
    bool DisableUpButton => SelectedRow == null ? true : SelectedRow.Order == 1;
    bool DisableDownButton => SelectedRow == null ? true : SelectedRow.Order == LastOrder;
    public int LastOrder => Items.Count == 0 ? 1 : Items.MaxBy(x => x.Order).Order;

    bool DisableSaveButton(QualityResponse model)
    {
        return string.IsNullOrEmpty(model.Name) ? true : false;

    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<QualityResponseList, QualityGetAll>(new QualityGetAll()
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
        CreateQualityRequest create = CreateRow.ToCreate();
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
    private void HandleRowClick(FluentDataGridRow<QualityResponse> row)
    {
        SelectedRow = row.Item == null ? null : SelectedRow == null ? row.Item : SelectedRow.Id == row.Item.Id ? SelectedRow : row.Item;
        //Si EditRow es diferente al seleccionado se vuelve null para desaparecer la caja de texto
        EditRow = EditRow == null ? null : SelectedRow == null ? null : EditRow.Id != SelectedRow.Id ? null : EditRow;



    }
    private void HandleRowDoubleClick(FluentDataGridRow<QualityResponse> row)
    {
        EditRow = row.Item == null ? null : SelectedRow == null ? row.Item : SelectedRow.Id == row.Item.Id ? SelectedRow : row.Item;
        //Si CreateRow esta creada se elimina del listado y se vueleve null
        CancelCreate();

    }


    async Task Update(QualityResponse model)
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
            DeleteQualityRequest request = new()
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
