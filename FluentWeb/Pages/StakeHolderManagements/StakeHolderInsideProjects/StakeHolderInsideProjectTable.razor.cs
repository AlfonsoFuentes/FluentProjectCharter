using FluentWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Enums.StakeHolderTypes;
using Shared.Enums.TasksRelationTypes;
using Shared.Models.Projects.Mappers;
using Shared.Models.StakeHolderInsideProjects.Mappers;
using Shared.Models.StakeHolderInsideProjects.Records;
using Shared.Models.StakeHolderInsideProjects.Requests;
using Shared.Models.StakeHolderInsideProjects.Responses;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.StakeHolders;
using static Shared.StaticClasses.StaticClass;

namespace FluentWeb.Pages.StakeHolderManagements.StakeHolderInsideProjects;
#nullable disable
public partial class StakeHolderInsideProjectTable
{
    [Parameter]
    public Guid ProjectId { get; set; }


    public List<StakeHolderInsideProjectResponse> Items { get; set; } = new();

    [Inject]
    private IStakeHolderService StakeHolderService { get; set; }
    StakeHolderResponseList StakeHolderResponseList = new();

    string nameFilter { get; set; } = string.Empty;
    Func<StakeHolderInsideProjectResponse, bool> fiterexpresion => x =>
      x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase);
    public List<StakeHolderInsideProjectResponse> FilteredItems => Items.Count == 0 ? new() : Items.Where(fiterexpresion).ToList();
    async Task UpdateStakeHolder()
    {
        var result = await StakeHolderService.GetAll();
        if (result.Succeeded)
        {

            var allStakeHolders = result.Data.Items;
            var selectedStakeHolderIds = Items.Select(item => item.StakeHolder.Id).ToHashSet();
            StakeHolderResponseList.Items = allStakeHolders
                .Where(stakeHolder => !selectedStakeHolderIds.Contains(stakeHolder.Id))
                .ToList();
        }
    }


    protected override async Task OnParametersSetAsync()
    {
        if (ProjectId == Guid.Empty) return;

        await UpdateStakeHolder();
        await LoadFromLocalStorage();
        if (Items == null)
        {
            await GetAll();
        }
    }

    StakeHolderInsideProjectResponse CreateRow = null!;
    StakeHolderInsideProjectResponse EditRow = null!;
    StakeHolderInsideProjectResponse SelectedRow = null!;
    string CurrentRowName => SelectedRow == null ? string.Empty : TruncateService.Truncate(SelectedRow.Name, 20);

    async Task GetAll()
    {

        var result = await GenericService.GetAll<StakeHolderInsideProjectResponseList, StakeHolderInsideProjectGetAll>(
            new StakeHolderInsideProjectGetAll() { ProjectId = ProjectId });
        if (result.Succeeded)
        {
            Items = result.Data.Items;


        }
        GetSelectedRowFromItems();
    }

    void AddNew()
    {

        CreateRow = new()
        {
            ProjectId = ProjectId,
        };
        //Si EditRow esta creada se desaparece Editrow 
        //stakeHolder = string.Empty;
        EditRow = null!;
        Items.Insert(0, CreateRow);
    }
    public async Task Create()
    {
        if (CreateRow == null) return;
        CreateStakeHolderInsideProjectRequest create = CreateRow.ToCreate();
        var result = await GenericService.Create(create);
        if (result.Succeeded)
        {
            CreateRow = null;
            await GetAll();

        }
        else
        {
            _snackBar.ShowError(result.Messages);
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
    private void HandleRowClick(FluentDataGridRow<StakeHolderInsideProjectResponse> row)
    {
        SelectedRow = row.Item == null ? null : SelectedRow == null ? row.Item : SelectedRow.Id == row.Item.Id ? SelectedRow : row.Item;
        //Si EditRow es diferente al seleccionado se vuelve null para desaparecer la caja de texto
        EditRow = EditRow == null ? null : SelectedRow == null ? null : EditRow.Id != SelectedRow.Id ? null : EditRow;



    }
   

    async Task Update(StakeHolderInsideProjectResponse model)
    {

        var result = await GenericService.Update(model.ToUpdate());
        if (result.Succeeded)
        {

            await GetAll();
            EditRow = null!;
        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }
    }

    bool DisableSaveButton(StakeHolderInsideProjectResponse model)
    {
        return model.StakeHolder == null || model.Role.Id == StakeHolderRoleEnum.None.Id;

    }
    void Edit(StakeHolderInsideProjectResponse model)
    {
        EditRow = model;
        SelectedRow = null;
        CreateRow = null;
    }


    void GetSelectedRowFromItems()
    {
        SelectedRow = SelectedRow == null ? null : Items.FirstOrDefault(x => x.Id == SelectedRow.Id);
    }
    public async Task Delete(StakeHolderInsideProjectResponse response)
    {

        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteStakeHolderInsideProjectRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = ProjectId
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

    void AddStakeHolder()
    {
        SaveModelToLocalStorage().ContinueWith(_ =>
        {
            Navigation.NavigateTo("/CreateStakeHolder");
        });
    }
    private async Task SaveModelToLocalStorage()
    {
        await _localModelStorage.SaveToLocalStorage(Items);
    }
    async Task LoadFromLocalStorage()
    {
        Items = await _localModelStorage.LoadFromLocalStorage(Items) ?? null!;
    }
    void OnChangeStakeHolderInsideProject(ChangeEventArgs e, StakeHolderInsideProjectResponse response)
    {
        if (Guid.TryParse(e.Value?.ToString(), out Guid selectedId))
        {
            var selected = StakeHolderResponseList.Items.First(x => x.Id == selectedId);
            response.StakeHolder = selected;
        }



    }
    void OnChangeRole(ChangeEventArgs e, StakeHolderInsideProjectResponse response)
    {
        if (int.TryParse(e.Value?.ToString(), out int selectedId))
        {
            var selected = StakeHolderRoleEnum.List.First(x => x.Id == selectedId);
            response.Role = selected;
        }



    }

}
