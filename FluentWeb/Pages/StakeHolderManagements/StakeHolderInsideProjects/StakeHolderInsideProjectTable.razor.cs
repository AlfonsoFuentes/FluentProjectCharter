using FluentWeb.Services;
using Microsoft.AspNetCore.Components;
using Shared.Enums.StakeHolderTypes;
using Shared.Models.Projects.Mappers;
using Shared.Models.StakeHolderInsideProjects.Mappers;
using Shared.Models.StakeHolderInsideProjects.Records;
using Shared.Models.StakeHolderInsideProjects.Requests;
using Shared.Models.StakeHolderInsideProjects.Responses;
using Shared.Models.StakeHolders.Responses;
using Shared.StaticClasses;
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

    protected override async Task OnInitializedAsync()
    {
        await UpdateStakeHolder();

        await GetAll();


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

            GetSelectedRowFromItems();
        }
    }

    void AddNew()
    {

        CreateRow = new()
        {
            ProjectId = ProjectId,
        };
        //Si EditRow esta creada se desaparece Editrow 
        stakeHolder = string.Empty;
        EditRow = null!;
        Items.Add(CreateRow);
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
    private void HandleRowClick(StakeHolderInsideProjectResponse row)
    {
        SelectedRow = row == null ? null : SelectedRow == null ? row : SelectedRow.Id == row.Id ? SelectedRow : row;
        //Si EditRow es diferente al seleccionado se vuelve null para desaparecer la caja de texto
        EditRow = EditRow == null ? null : SelectedRow == null ? null : EditRow.Id != SelectedRow.Id ? null : EditRow;



    }
    private void HandleRowDoubleClick(StakeHolderInsideProjectResponse row)
    {
        EditRow = row == null ? null : SelectedRow == null ? row : SelectedRow.Id == row.Id ? SelectedRow : row;
        stakeHolder = EditRow == null ? string.Empty : EditRow.StakeHolder == null ? string.Empty : EditRow.StakeHolder.Name;
        //Si CreateRow esta creada se elimina del listado y se vueleve null
        CancelCreate();

    }
    async Task Update(StakeHolderInsideProjectResponse model)
    {

        var result = await GenericService.Update(model.ToUpdate());
        if (result.Succeeded)
        {

            await GetAll();
            EditRow = null!;
        }
    }

    bool DisableSaveButton(StakeHolderInsideProjectResponse model)
    {
        return model.StakeHolder == null || model.Role.Id == StakeHolderRoleEnum.None.Id;

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
            DeleteStakeHolderInsideProjectRequest request = new()
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
    void AddStakeHolder()
    {
        Navigation.NavigateTo(StaticClass.StakeHolders.PageName.Create);
    }
    
   

}
