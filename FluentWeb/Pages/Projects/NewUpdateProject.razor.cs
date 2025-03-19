using Microsoft.AspNetCore.Components;
using Shared.Models.Projects.Records;
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.StakeHolders;
using Web.Infrastructure.Managers;
using Shared.StaticClasses;
using Shared.Models.Projects.Mappers;

namespace FluentWeb.Pages.Projects;
public partial class NewUpdateProject
{
    UpdateProjectRequest Model = new();
    [Parameter]
    public Guid Id { get; set; }

    bool _loaded = false;   
    protected override async Task OnInitializedAsync()
    {
        await UpdateStakeHolder();
        await LoadFromLocalStorage();
        if (Model == null)
        {
            var result = await GenericService.GetById<ProjectResponse, GetProjectByIdRequest>(new GetProjectByIdRequest() { Id = Id });

            if (result.Succeeded)
            {
                Model = result.Data.ToUpdate();
            }

        }
        _loaded = true;
    }
    [Inject]
    private IStakeHolderService StakeHolderService { get; set; } = null!;
    StakeHolderResponseList StakeHolderResponseList = new();

    async Task UpdateStakeHolder()
    {
        var result = await StakeHolderService.GetAll();
        if (result.Succeeded)
        {
            StakeHolderResponseList = result.Data;
        }
    }

    void AddStakeHolder()
    {
        SaveModelToLocalStorage().ContinueWith(_ =>
        {
            Navigation.NavigateTo("/CreateStakeHolder");
        });
    }
    private async Task SaveModelToLocalStorage()
    {
        await _localModelStorage.SaveToLocalStorage(Model);
    }
    async Task LoadFromLocalStorage()
    {
        Model = await _localModelStorage.LoadFromLocalStorage(Model) ?? null!;
    }
}
