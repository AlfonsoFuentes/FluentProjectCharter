using FluentWeb.Layout;
using Microsoft.AspNetCore.Components;
using Shared.Enums.ExportFiles;
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;
using Web.Infrastructure.Managers.Generic;
using Web.Infrastructure.Managers.Projects;

namespace FluentWeb.Pages;
#nullable disable
public partial class Home
{
    [CascadingParameter]
    public App App { get; set; }
 
    [Inject]
    private IProjectService Service { get; set; }

    ProjectResponseList Response { get; set; } = new();
    string nameFilter { get; set; } = string.Empty;
    Func<ProjectResponse, bool> fiterexpresion => x =>
       x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase);
    public List<ProjectResponse> FilteredItems => Response.Items.Count == 0 ? new() :
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
        Navigation.NavigateTo("/CreateProject");
    }


    public void Edit(ProjectResponse Response)
    {
        Navigation.NavigateTo($"/UpdateProject/{Response.Id}");
    }
   
   

    public async Task Delete(ProjectResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteProjectRequest request = new()
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
    public async Task Export(ProjectResponse response)
    {
        var resultExport = await Service.ExportPDF(response);
        if (resultExport.Succeeded)
        {

            Console.WriteLine(resultExport.Message);
            var downloadresult = await blazorDownloadFileService.DownloadFile(resultExport.Data.ExportFileName,
              resultExport.Data.Data, contentType: resultExport.Data.ContentType);
            if (downloadresult.Succeeded)
            {
                _snackBar.ShowSuccess($"Project Charter for {response.Name} exported succesfully");

            }
            else
            {
                _snackBar.ShowError($"Project Charter for {response.Name} not exported succesfully");
            }
        }
        else
        {
            _snackBar.ShowError($"Project Charter for {response.Name} not created succesfully");
        }
    }

}
