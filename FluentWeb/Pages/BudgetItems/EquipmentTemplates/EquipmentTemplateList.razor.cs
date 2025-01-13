using Microsoft.AspNetCore.Components;
using Shared.Models.Templates.Equipments.Records;
using Shared.Models.Templates.Equipments.Requests;
using Shared.Models.Templates.Equipments.Responses;

#nullable disable
namespace FluentWeb.Pages.BudgetItems.EquipmentTemplates;
public partial class EquipmentTemplateList
{
    [CascadingParameter]
    public App App { get; set; }

    [Parameter]
    public List<EquipmentTemplateResponse> Items { get; set; } = new();
    string nameFilter;
    Func<EquipmentTemplateResponse, bool> Criteria => x =>
    x.Type.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.SubType.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.Brand.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.Reference.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.Model.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.InternalMaterial.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.ExternalMaterial.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase)
    ;
    public List<EquipmentTemplateResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items :
        Items.Where(Criteria).ToList();

    bool ByParameter { get; set; } = false;

    [Parameter]
    public Action<EquipmentTemplateResponse> SendToForm { get; set; }
    protected override async Task OnInitializedAsync()
    {

        if (Items.Count == 0)
            await GetAll();
        else ByParameter = true;
    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<EquipmentTemplateResponseList, EquipmentTemplateGetAll>(new EquipmentTemplateGetAll());
        if (result.Succeeded)
        {
            Items = result.Data.Items;
        }
    }
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateEquipmentTemplate");

    }

    void OnSendToForm(EquipmentTemplateResponse response)
    {
        SendToForm(response);
    }

    void Edit(EquipmentTemplateResponse response)
    {
        Navigation.NavigateTo($"/UpdateEquipmentTemplate/{response.Id}");
    }
    async Task Copy(EquipmentTemplateResponse response)
    {
        CreateEquipmentTemplateRequest request = new()
        {
            BrandResponse = response.BrandResponse,
            ExternalMaterial = response.ExternalMaterial,
            InternalMaterial = response.InternalMaterial,
            Model = response.Model,
            Name = response.Name,
            Nozzles = response.Nozzles,
            Reference = response.Reference,
            SubType = response.SubType,
            TagLetter = response.TagLetter,
            Type = response.Type,
            Value = response.Value,



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
    public async Task Delete(EquipmentTemplateResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteEquipmentTemplateRequest request = new()
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
