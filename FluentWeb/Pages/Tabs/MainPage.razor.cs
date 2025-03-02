using Microsoft.AspNetCore.Components;
using Shared.Models.Projects.Reponses;
using Web.Infrastructure.Managers.Projects;

namespace FluentWeb.Pages.Tabs;
public partial class MainPage
{
    

    [Parameter]
    public ProjectResponse Project { get; set; } = null!;
  
    [Inject]
    private IProjectService ProjectService { get; set; } = null!;

    async Task ExportProjectCharter()
    {

        var resultExport = await ProjectService.ExportProjectCharter(Project);
        if (resultExport.Succeeded)
        {

            Console.WriteLine(resultExport.Message);
            var downloadresult = await blazorDownloadFileService.DownloadFile(resultExport.Data.ExportFileName,
              resultExport.Data.Data, contentType: resultExport.Data.ContentType);
            if (downloadresult.Succeeded)
            {
                _snackBar.ShowSuccess($"Project Charter for {Project.Name} exported succesfully");

            }
            else
            {
                _snackBar.ShowError($"Project Charter for {Project.Name} not exported succesfully");
            }
        }
        else
        {
            _snackBar.ShowError($"Project Charter for {Project.Name} not created succesfully");
        }
    }
    async Task ExportProjectExecution()
    {

        var resultExport = await ProjectService.ExportProjectPlann(Project);
        if (resultExport.Succeeded)
        {

            Console.WriteLine(resultExport.Message);
            var downloadresult = await blazorDownloadFileService.DownloadFile(resultExport.Data.ExportFileName,
              resultExport.Data.Data, contentType: resultExport.Data.ContentType);
            if (downloadresult.Succeeded)
            {
                _snackBar.ShowSuccess($"Project Charter for {Project.Name} exported succesfully");

            }
            else
            {
                _snackBar.ShowError($"Project Charter for {Project.Name} not exported succesfully");
            }
        }
        else
        {
            _snackBar.ShowError($"Project Charter for {Project.Name} not created succesfully");
        }
    }


}
