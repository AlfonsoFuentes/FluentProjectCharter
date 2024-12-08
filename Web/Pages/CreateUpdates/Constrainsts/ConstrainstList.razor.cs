using Microsoft.AspNetCore.Components;
using Shared.Models.Constrainsts.Requests;
using Shared.Models.Constrainsts.Responses;
using Shared.Models.Deliverables.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.Constrainsts;
public partial class ConstrainstList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public DeliverableResponse Parent { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<ConstrainstResponse> Items => Parent.Constraints;
    public List<ConstrainstResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
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

    public ConstrainstResponse EditResponse { get; set; } = null!;

    void Edit(ConstrainstResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(ConstrainstResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete {caseResponse.Name}?");
        
       
        if(dialog.Value)
        {
            DeleteConstrainstRequest request = new()
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
