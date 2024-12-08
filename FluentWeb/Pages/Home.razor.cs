using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Enums.ExportFiles;
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;
using Shared.StaticClasses;
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
    [Inject]
    private IGenericService GenericService { get; set; } = null!;
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
    CreateProjectRequest CreateResponse = null!;
    public void AddNew()
    {
        CreateResponse = new()
        {

        };
    }
    UpdateProjectRequest EditResponse = null!;

    public void Edit(ProjectResponse Response)
    {
        EditResponse = new()
        {
            Id = Response.Id,
            Name = Response.Name,
        };
    }
    void CancelAsync()
    {
        EditResponse = null!;
        CreateResponse = null!;
        StateHasChanged();
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
