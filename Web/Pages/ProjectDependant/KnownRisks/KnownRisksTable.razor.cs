using MudBlazor;
using Newtonsoft.Json.Linq;
using Shared.Models.Acquisitions.Responses;
using Shared.Models.KnownRisks.Mappers;
using Shared.Models.KnownRisks.Records;
using Shared.Models.KnownRisks.Requests;
using Shared.Models.KnownRisks.Responses;
using Shared.Models.Projects.Reponses;
using Shared.StaticClasses;
using Web.Templates;

namespace Web.Pages.ProjectDependant.KnownRisks;
public partial class KnownRisksTable
{

    [Parameter]
    public ProjectResponse Project { get; set; } = null!;
    public List<KnownRiskResponse> Items { get; set; } = new();
    string nameFilter = string.Empty;
    public Func<KnownRiskResponse, bool> Criteria => x => x.Name.Contains(nameFilter, StringComparison.InvariantCultureIgnoreCase);
    public List<KnownRiskResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items :
        Items.Where(Criteria).ToList();
    protected override async Task OnParametersSetAsync()
    {
        await GetAll();
    }
    async Task GetAll()
    {
        if (Project == null) return;
        var result = await GenericService.GetAll<KnownRiskResponseList, KnownRiskGetAll>(new KnownRiskGetAll()
        {
            ProjectId = Project.Id, 
        });
        if (result.Succeeded)
        {
            Items = result.Data.Items;
        }
    }
    public async Task AddNew()
    {

        var parameters = new DialogParameters<KnownRiskDialog>
        {
            { x => x.Model, new(){ProjectId=Project.Id } },
        };

        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = await DialogService.ShowAsync<KnownRiskDialog>("KnownRisk", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
            StateHasChanged();
        }
    }



    async Task Edit(KnownRiskResponse response)
    {


        var parameters = new DialogParameters<KnownRiskDialog>
        {

              { x => x.Model, response },
        };
        var options = new DialogOptions() { MaxWidth = MaxWidth.Medium };


        var dialog = await DialogService.ShowAsync<KnownRiskDialog>("KnownRisk", parameters, options);
        var result = await dialog.Result;
        if (result != null)
        {
            await GetAll();
        }
    }
    public async Task Delete(KnownRiskResponse response)
    {
        var parameters = new DialogParameters<DialogTemplate>
        {
            { x => x.ContentText, $"Do you really want to delete {response.Name}? This process cannot be undone." },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<DialogTemplate>("Delete", parameters, options);
        var result = await dialog.Result;


        if (!result!.Canceled)
        {
            DeleteKnownRiskRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,

            };
            var resultDelete = await GenericService.Post(request);
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
    HashSet<KnownRiskResponse> SelecteItems = null!;
    public async Task DeleteGroup()
    {
        if (SelecteItems == null) return;
        var parameters = new DialogParameters<DialogTemplate>
        {
            { x => x.ContentText, $"Do you really want to delete this {SelecteItems.Count} Items? This process cannot be undone." },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<DialogTemplate>("Delete", parameters, options);
        var result = await dialog.Result;


        if (!result!.Canceled)
        {
            DeleteGroupKnownRiskRequest request = new()
            {
                SelecteItems = SelecteItems,
                ProjectId = Project.Id,

            };
            var resultDelete = await GenericService.Post(request);
            if (resultDelete.Succeeded)
            {
                await GetAll();
                _snackBar.ShowSuccess(resultDelete.Messages);
                SelecteItems = null!;

            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }
    KnownRiskResponse SelectedRow = null!;
    bool DisableUpButton => SelectedRow == null ? true : SelectedRow.Order == 1;
    bool DisableDownButton => SelectedRow == null ? true : SelectedRow.Order == LastOrder;
    public int LastOrder => Items.Count == 0 ? 1 : Items.MaxBy(x => x.Order)!.Order;
    void RowClicked(KnownRiskResponse item)
    {
        SelectedRow = SelectedRow == null ? SelectedRow = item : SelectedRow = null!;
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
