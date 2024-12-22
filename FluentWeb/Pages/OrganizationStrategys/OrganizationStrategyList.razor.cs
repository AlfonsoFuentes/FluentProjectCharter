using Microsoft.AspNetCore.Components;
using Shared.Enums.ExportFiles;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.OrganizationStrategies.Responses;
using Web.Infrastructure.Managers.OrganizationStrategys;
#nullable disable
namespace FluentWeb.Pages.OrganizationStrategys;
public partial class OrganizationStrategyList
{
    [CascadingParameter]
    public App App { get; set; }
    [Inject]
    private IOrganizationStrategyService Service { get; set; }

    OrganizationStrategyResponseList Response { get; set; } = new();
    string nameFilter { get; set; } = string.Empty;
    Func<OrganizationStrategyResponse, bool> fiterexpresion => x =>
       x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase);
    public List<OrganizationStrategyResponse> FilteredItems => Response.Items.Count == 0 ? new() :
        Response.Items.Where(fiterexpresion).ToList();

    protected override async Task OnInitializedAsync()
    {
        await UpdateAll();
    }
    async Task UpdateAll()
    {

        var result = await Service.GetAll();
        if (result.Succeeded)
        {
            Response = result.Data;
            StateHasChanged();
        }
    }

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateOrganizationStrategy");
    }
    void Edit(OrganizationStrategyResponse response)
    {
        Navigation.NavigateTo($"/UpdateOrganizationStrategy/{response.Id}");
    }
    
    void CancelAsync()
    {
       
        StateHasChanged();
    }
    public async Task Delete(OrganizationStrategyResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteOrganizationStrategyRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
            };
            var resultDelete = await GenericService.Delete(request);
            if (resultDelete.Succeeded)
            {
                await UpdateAll();
                _snackBar.ShowSuccess(resultDelete.Messages);


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }
    public async Task Export(ExportFileType fileType)
    {
        var resultExport = await Service.Export(fileType, FilteredItems.ToList());
        if (resultExport.Succeeded)
        {
            var downloadresult = await blazorDownloadFileService.DownloadFile(resultExport.Data.ExportFileName,
              resultExport.Data.Data, contentType: resultExport.Data.ContentType);
            if (downloadresult.Succeeded)
            {
                _snackBar.ShowSuccess($"Table was exported to {fileType} succesfully ");




            }
        }
    }
}
