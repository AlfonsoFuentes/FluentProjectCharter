using Microsoft.AspNetCore.Components;
using Web.Infrastructure.Managers.Generic;
using Shared.Models.Deliverables.Responses;
using Shared.Models.Requirements.Requests;
using Shared.Models.Requirements.Responses;
#nullable disable
namespace Web.Pages.CreateUpdates.Requirements;
public partial class RequirementList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public DeliverableResponse Parent { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<RequirementResponse> Items => Parent.Requirements;
    public List<RequirementResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
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

    public RequirementResponse EditResponse { get; set; } = null!;

    void Edit(RequirementResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(RequirementResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete {caseResponse.Name}?");
        
       
        if(dialog.Value)
        {
            DeleteRequirementRequest request = new()
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
