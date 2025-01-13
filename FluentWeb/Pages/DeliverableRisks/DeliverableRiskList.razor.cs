using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.DeliverableRisks.Requests;
using Shared.Models.DeliverableRisks.Responses;
using Web.Infrastructure.Managers.Generic;
using Shared.Models.Scopes.Responses;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Assumptions.Responses;

namespace FluentWeb.Pages.DeliverableRisks;
#nullable disable
public partial class DeliverableRiskList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public Guid DeliverableId { get; set; }
    [Parameter]
    [EditorRequired]
    public Guid ProjectId { get; set; }
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }


    [Parameter]
    [EditorRequired]
    public List<DeliverableRiskResponse> Items { get; set; }
    string nameFilter;
    public List<DeliverableRiskResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateDeliverableRisk/{DeliverableId}/{ProjectId}");

    }



    void Edit(DeliverableRiskResponse response)
    {
        Navigation.NavigateTo($"/UpdateDeliverableRisk/{response.Id}/{ProjectId}");
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
                ProjectId = ProjectId,
            };
            var resultDelete = await GenericService.Delete(request);
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
