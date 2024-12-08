using Microsoft.AspNetCore.Components;
using Shared.Models.Constrainsts.Requests;
using Shared.Models.Constrainsts.Responses;
using Shared.Models.Deliverables.Responses;
using Web.Infrastructure.Managers.Generic;

namespace FluentWeb.Pages.Constraints;
#nullable disable
public partial class ConstraintList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public DeliverableResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    [EditorRequired]
    public Action Cancel { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<ConstrainstResponse> Items => Parent == null ? new() : Parent.Constrainsts;
    string nameFilter;
    public List<ConstrainstResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    CreateConstrainstRequest CreateResponse = null!;
    public void AddNew()
    {
        CreateResponse = new()
        {
            ProjectId = Parent.ProjectId,
            DeliverableId=Parent.Id,
        };
    }



    public void CancelAsync()
    {
        CreateResponse = null!;
        EditResponse = null!;
        Cancel();
    }

    public UpdateConstrainstRequest EditResponse { get; set; } = null!;

    void Edit(ConstrainstResponse response)
    {
        EditResponse = new()
        {
            Id = response.Id,
            ProjectId = Parent.ProjectId,
            Name = response.Name,
        };
    }
    public async Task Delete(ConstrainstResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteConstrainstRequest request = new()
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
