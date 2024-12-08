using Microsoft.AspNetCore.Components;
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;
using Web.Infrastructure.Managers.Generic;
using Web.Infrastructure.Managers.Projects;
using static Shared.StaticClasses.StaticClass;

namespace Web.Pages.ProjectTrees;
#nullable disable
public partial class ProjectDetailView
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public ProjectResponse Response { get; set; } = new();
   

    [Inject]
    private IGenericService GenericService { get; set; } = null!;
    [Inject]
    private IProjectService Service { get; set; } = null!;
    protected override async Task OnInitializedAsync()
    {
        await GetAll();
    }
    public async Task GetAll()
    {
        var result = await Service.GetById(Response.Id);
        if (result.Succeeded)
        {
            Response = result.Data;
            StateHasChanged();

        }
    }
    void EditProject()
    {
        EditProjectForm = true;
    }
    public bool EditProjectForm = false;


    void CancelEdit()
    {
        EditProjectForm = false;
        StateHasChanged();
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
                await GetAll();
                _snackBar.Add(resultDelete.Messages, Radzen.NotificationSeverity.Success);
              


            }
            else
            {
                _snackBar.Add(resultDelete.Messages, Radzen.NotificationSeverity.Error);
               
            }
        }

    }
}
