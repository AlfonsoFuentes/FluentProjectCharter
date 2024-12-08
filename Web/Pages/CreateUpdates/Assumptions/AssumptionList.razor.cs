using Microsoft.AspNetCore.Components;
using Shared.Commons;
using Shared.Models.Assumptions.Requests;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Deliverables.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.Assumptions;
public partial class AssumptionList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public DeliverableResponse Parent { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<AssumptionResponse> Items => Parent.Assumptions;
    public List<AssumptionResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
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

    public AssumptionResponse EditResponse { get; set; } = null!;

    void Edit(AssumptionResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(AssumptionResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete Asumption {caseResponse.Name}?");
        
       
        if(dialog.Value)
        {
            DeleteAssumptionRequest request = new()
            {
                Id = caseResponse.Id,
                Name = caseResponse.Name,
                ProjectId = caseResponse.ProjectId,
            };
            var resultDelete = await Service.Delete(request);
            if (resultDelete.Succeeded)
            {
                await GetAll.Invoke();
                _snackBar.Add(resultDelete.Messages, Radzen.NotificationSeverity.Success);


            }
            else
            {
                _snackBar.Add(resultDelete.Messages, Radzen.NotificationSeverity.Error);
            }
        }
    }
}
