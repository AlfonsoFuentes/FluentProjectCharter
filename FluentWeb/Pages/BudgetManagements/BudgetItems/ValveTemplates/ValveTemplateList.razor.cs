using Microsoft.AspNetCore.Components;
using Shared.Models.Templates.Valves.Records;
using Shared.Models.Templates.Valves.Requests;
using Shared.Models.Templates.Valves.Responses;

#nullable disable
namespace FluentWeb.Pages.BudgetManagements.BudgetItems.ValveTemplates;
public partial class ValveTemplateList
{
    [CascadingParameter]
    public App App { get; set; }

    [Parameter]
    public List<ValveTemplateResponse> Items { get; set; } = new();
    string nameFilter;
    Func<ValveTemplateResponse, bool> Criteria => x =>
    x.Type.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||

    x.Brand.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||

    x.Model.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.Material.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.Diameter.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase)
    ;
    public List<ValveTemplateResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items :
        Items.Where(Criteria).ToList();

    bool ByParameter { get; set; } = false;

    [Parameter]
    public Action<ValveTemplateResponse> SendToForm { get; set; }
    protected override async Task OnInitializedAsync()
    {

        if (Items.Count == 0)
            await GetAll();
        else ByParameter = true;
    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<ValveTemplateResponseList, ValveTemplateGetAll>(new ValveTemplateGetAll());
        if (result.Succeeded)
        {
            Items = result.Data.Items;
        }
    }
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateValveTemplate");

    }

    void OnSendToForm(ValveTemplateResponse response)
    {
        SendToForm(response);
    }

    void Edit(ValveTemplateResponse response)
    {
        Navigation.NavigateTo($"/UpdateValveTemplate/{response.Id}");
    }
    async Task Copy(ValveTemplateResponse response)
    {
        CreateValveTemplateRequest request = new()
        {
            BrandResponse = response.BrandResponse,
            ActuatorType = response.ActuatorType,
            Model = response.Model,
            Name = response.Name,
            Nozzles = response.Nozzles,
     
            Type = response.Type,
            Value = response.Value,
            Diameter = response.Diameter,
            FailType = response.FailType,
            HasFeedBack = response.HasFeedBack,
            Material = response.Material,
            PositionerType = response.PositionerType,
            SignalType = response.SignalType,
             



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
    public async Task Delete(ValveTemplateResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteValveTemplateRequest request = new()
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
