using Microsoft.AspNetCore.Components;
using Shared.Commons;
using Shared.Models.DeliverableRisks.Requests;
using Shared.Models.DeliverableRisks.Responses;
using Shared.Models.Deliverables.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.DeliverableRisks;
public partial class DeliverableRiskList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public DeliverableResponse Parent { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<DeliverableRiskResponse> Items => Parent.DeliverableRisks;
    public List<DeliverableRiskResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
    bool AddItem = false;

    [Parameter]
    public string TableName { get; set; }

    public void CancelAsync()
    {
        AddItem = false;
        EditResponse = null!;
        StateHasChanged();
    }
    void Add()
    {
        AddItem = true;
    }

    public DeliverableRiskResponse EditResponse { get; set; } = null!;

    void Edit(DeliverableRiskResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(DeliverableRiskResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete {caseResponse.Name}?");
        
       
        if(dialog.Value)
        {
            DeleteDeliverableRiskRequest request = new()
            {
                Id = caseResponse.Id,
                Name = caseResponse.Name,
                ProjectId = caseResponse.ProjectId,
            };
            var resultDelete = await Service.Delete(request);
            if (resultDelete.Succeeded)
            {
                _snackBar.Add(resultDelete.Messages, Radzen.NotificationSeverity.Success);
              
                await GetAll.Invoke();
            }
            else
            {
                _snackBar.Add(resultDelete.Messages, Radzen.NotificationSeverity.Error);


            }
        }
    }
}
