using Microsoft.AspNetCore.Components;
using Shared.Models.Templates.Instruments.Records;
using Shared.Models.Templates.Instruments.Requests;
using Shared.Models.Templates.Instruments.Responses;

#nullable disable
namespace FluentWeb.Pages.BudgetItems.InstrumentTemplates;
public partial class InstrumentTemplateList
{
    [CascadingParameter]
    public App App { get; set; }

    [Parameter]
    public List<InstrumentTemplateResponse> Items { get; set; } = new();
    string nameFilter;
    Func<InstrumentTemplateResponse, bool> Criteria => x =>
    x.Type.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||

    x.Brand.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
      x.SignalType.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.Model.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.Material.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase) ||
    x.Reference.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase)
    ;
    public List<InstrumentTemplateResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items :
        Items.Where(Criteria).ToList();

    bool ByParameter { get; set; } = false;

    [Parameter]
    public Action<InstrumentTemplateResponse> SendToForm { get; set; }
    protected override async Task OnInitializedAsync()
    {

        if (Items.Count == 0)
            await GetAll();
        else ByParameter = true;
    }
    async Task GetAll()
    {
        var result = await GenericService.GetAll<InstrumentTemplateResponseList, InstrumentTemplateGetAll>(new InstrumentTemplateGetAll());
        if (result.Succeeded)
        {
            Items = result.Data.Items;
        }
    }
    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateInstrumentTemplate");

    }

    void OnSendToForm(InstrumentTemplateResponse response)
    {
        SendToForm(response);
    }

    void Edit(InstrumentTemplateResponse response)
    {
        Navigation.NavigateTo($"/UpdateInstrumentTemplate/{response.Id}");
    }
    async Task Copy(InstrumentTemplateResponse response)
    {
        CreateInstrumentTemplateRequest request = new()
        {
            BrandResponse = response.BrandResponse,
  
            Model = response.Model,
            Name = response.Name,
            Nozzles = response.Nozzles,

            Type = response.Type,
            Value = response.Value,
        
            Material = response.Material,
  
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
    public async Task Delete(InstrumentTemplateResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteInstrumentTemplateRequest request = new()
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
