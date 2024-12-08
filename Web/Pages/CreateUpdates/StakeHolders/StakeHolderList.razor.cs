using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.StakeHolders.Requests;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.StakeHolders;
public partial class StakeHolderList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public CaseResponse Parent { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<StakeHolderResponse> Items => Parent.StakeHolders;
    public List<StakeHolderResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
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

    public StakeHolderResponse EditResponse { get; set; } = null!;

    void Edit(StakeHolderResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(StakeHolderResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete Asumption {caseResponse.Name}?");
        
       
        if(dialog.Value)
        {
            DeleteStakeHolderRequest request = new()
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
