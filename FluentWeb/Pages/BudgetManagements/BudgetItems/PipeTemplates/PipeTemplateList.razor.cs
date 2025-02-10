using Microsoft.AspNetCore.Components;
using Shared.Models.Templates.Pipings.Records;
using Shared.Models.Templates.Pipings.Requests;
using Shared.Models.Templates.Pipings.Responses;

#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.PipeTemplates;
public partial class PipeTemplateList
{
    [CascadingParameter]
    public App App { get; set; }

    [Parameter]
    public List<PipeTemplateResponse> Items { get; set; } = new();
    string nameFilter;
    Func<PipeTemplateResponse, bool> Criteria => x =>
    x.Diameter.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.Class.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.Brand.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.Material.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase);
    public List<PipeTemplateResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items :
        Items.Where(Criteria).ToList();

    bool ByParameter { get; set; } = false;

    [Parameter]
    public Action<PipeTemplateResponse> SendToForm { get; set; }
    protected override async Task OnInitializedAsync()
    {

        if (Items.Count == 0)
            await GetAll();
        else ByParameter = true;
    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<PipeTemplateResponseList, PipeTemplateGetAll>(new PipeTemplateGetAll());
        if (result.Succeeded)
        {
            Items = result.Data.Items;
        }
    }
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreatePipeTemplate");

    }

    void OnSendToForm(PipeTemplateResponse response)
    {
        SendToForm(response);
    }

    void Edit(PipeTemplateResponse response)
    {
        Navigation.NavigateTo($"/UpdatePipeTemplate/{response.Id}");
    }
    async Task Copy(PipeTemplateResponse response)
    {
        CreatePipeTemplateRequest request = new()
        {
            BrandResponse = response.BrandResponse,
            Material = response.Material,
            Insulation = response.Insulation,
            Class = response.Class,
            Diameter = response.Diameter,
            EquivalentLenghPrice = response.EquivalentLenghPrice,
            LaborDayPrice = response.LaborDayPrice,





        };
        var result = await GenericService.Create(request);

        if (result.Succeeded)
        {
            await GetAll();
            _snackBar.ShowSuccess(result.Messages);
        }
        else
        {
            _snackBar.ShowError(result.Messages);
        }
    }
    public async Task Delete(PipeTemplateResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeletePipeTemplateRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,

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
}
