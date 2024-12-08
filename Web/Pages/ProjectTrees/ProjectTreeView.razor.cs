using Microsoft.AspNetCore.Components;
using Shared.Enums.ExportFiles;
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;
using Shared.StaticClasses;
using Web.Infrastructure.Managers.Generic;
using Web.Infrastructure.Managers.Projects;
#nullable disable
namespace Web.Pages.ProjectTrees;
public partial class ProjectTreeView
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
    public void AddNew()
    {
        _NavigationManager.NavigateTo($"/{StaticClass.Projects.PageName.Create}");
    }
    public void Edit(ProjectResponse Response)
    {
        _NavigationManager.NavigateTo($"/{StaticClass.Projects.PageName.Complete}/{Response.Id}");
    }
    public async Task Delete(ProjectResponse response)
    {
        var result = await DialogService.Confirm($"Delete Project {response.Name}?");
    
        
        if (result.Value)
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
                _snackBar.Add(resultDelete.Messages, Radzen.NotificationSeverity.Success);


            }
            else
            {
                _snackBar.Add(resultDelete.Messages, Radzen.NotificationSeverity.Error);
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
                _snackBar.Add($"Table was exported to {fileType} succesfully ", Radzen.NotificationSeverity.Success);
                



            }
        }
    }

}
