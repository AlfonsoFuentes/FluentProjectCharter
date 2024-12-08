using Microsoft.AspNetCore.Components;
using Shared.Models.Bennefits.Requests;
using Shared.Models.Bennefits.Responses;
using Shared.Models.Deliverables.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.Bennefits;
public partial class BennefitList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public DeliverableResponse Parent { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<BennefitResponse> Items => Parent.Bennefits;
    public List<BennefitResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
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

    public BennefitResponse EditResponse { get; set; } = null!;

    void Edit(BennefitResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(BennefitResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete  {caseResponse.Name}?");
        
       
        if(dialog.Value)
        {
            DeleteBennefitRequest request = new()
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
