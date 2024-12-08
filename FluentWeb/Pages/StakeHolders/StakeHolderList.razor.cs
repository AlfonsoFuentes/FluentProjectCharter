using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.StakeHolders.Requests;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.StakeHolders;
#nullable disable
public partial class StakeHolderList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public CaseResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    [EditorRequired]
    public Action Cancel { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<StakeHolderResponse> Items => Parent == null ? new() : Parent.StakeHolders;
    string nameFilter;
    public List<StakeHolderResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    CreateStakeHolderRequest CreateResponse = null!;
    public void AddNew()
    {
        CreateResponse = new()
        {
            ProjectId = Parent.ProjectId,
            CaseId = Parent.Id,
        };
    }



    public void CancelAsync()
    {
        CreateResponse = null!;
        EditResponse = null!;
        Cancel();
    }

    public UpdateStakeHolderRequest EditResponse { get; set; } = null!;

    void Edit(StakeHolderResponse response)
    {
        EditResponse = new()
        {
            Id = response.Id,
            ProjectId = Parent.ProjectId,
            Name = response.Name,
            Role = response.Role,
            Email = response.Email,
            PhoneNumber = response.PhoneNumber,
        };
    }
    public async Task Delete(StakeHolderResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteStakeHolderRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = Parent.Id,
            };
            var resultDelete = await Service.Delete(request);
            if (resultDelete.Succeeded)
            {
                await GetAll.Invoke();
                _snackBar.ShowSuccess(resultDelete.Messages);


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }

}
