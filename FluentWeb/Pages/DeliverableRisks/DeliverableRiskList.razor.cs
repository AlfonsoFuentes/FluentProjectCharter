using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.DeliverableRisks.Requests;
using Shared.Models.DeliverableRisks.Responses;
using Web.Infrastructure.Managers.Generic;
using Shared.Models.Scopes.Responses;
using Shared.Models.Deliverables.Responses;

namespace FluentWeb.Pages.DeliverableRisks;
#nullable disable
public partial class DeliverableRiskList
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
    public List<DeliverableRiskResponse> Items => Parent == null ? new() : Parent.DeliverableRisks;
    string nameFilter;
    public List<DeliverableRiskResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();
    CreateDeliverableRiskRequest CreateResponse = null!;
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

    public UpdateDeliverableRiskRequest EditResponse { get; set; } = null!;

    void Edit(DeliverableRiskResponse response)
    {
        EditResponse = new()
        {
            Id = response.Id,
            ProjectId = Parent.ProjectId,
            Name = response.Name,
        };
    }
    public async Task Delete(DeliverableRiskResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteDeliverableRiskRequest request = new()
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
