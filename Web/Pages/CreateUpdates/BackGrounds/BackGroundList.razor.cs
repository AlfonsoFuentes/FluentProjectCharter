using Microsoft.AspNetCore.Components;
using Shared.Models.Backgrounds.Requests;
using Shared.Models.Backgrounds.Responses;
using Shared.Models.Cases.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.BackGrounds;
public partial class BackGroundList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public CaseResponse Parent { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<BackGroundResponse> Items => Parent.BackGrounds;
    public List<BackGroundResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
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

    public BackGroundResponse EditResponse { get; set; } = null!;

    void Edit(BackGroundResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(BackGroundResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete {caseResponse.Name}?");
        
       
        if(dialog.Value)
        {
            DeleteBackGroundRequest request = new()
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
