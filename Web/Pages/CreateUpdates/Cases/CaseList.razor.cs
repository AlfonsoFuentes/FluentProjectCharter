using Microsoft.AspNetCore.Components;
using Shared.Models.BaseResponses;
using Shared.Models.Cases.Requests;
using Shared.Models.Cases.Responses;
using Shared.Models.Projects.Reponses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.Cases;
public partial class CaseList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public ProjectResponse Parent { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<CaseResponse> Items => Parent.Cases;
    public List<CaseResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
    bool AddItem = false;

   

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
   
    public CaseResponse EditResponse { get; set; } = null!;
  
    void Edit(CaseResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(CaseResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete Business case {caseResponse.Name}?");
        
       
        if(dialog.Value)
        {
            DeleteCaseRequest request = new()
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
