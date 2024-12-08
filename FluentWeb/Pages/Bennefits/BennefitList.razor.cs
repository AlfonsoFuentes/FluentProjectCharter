using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.Bennefits.Requests;
using Shared.Models.Bennefits.Responses;
using Web.Infrastructure.Managers.Generic;
using Shared.Models.Scopes.Responses;
using Shared.Models.Deliverables.Responses;

namespace FluentWeb.Pages.Bennefits;
#nullable disable
public partial class BennefitList
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
    public List<BennefitResponse> Items => Parent == null ? new() : Parent.Bennefits;
    string nameFilter;
    public List<BennefitResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    CreateBennefitRequest CreateResponse = null!;
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

    public UpdateBennefitRequest EditResponse { get; set; } = null!;

    void Edit(BennefitResponse response)
    {
        EditResponse = new()
        {
            Id = response.Id,
            ProjectId = Parent.ProjectId,
            Name = response.Name,
        };
    }
    public async Task Delete(BennefitResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteBennefitRequest request = new()
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
