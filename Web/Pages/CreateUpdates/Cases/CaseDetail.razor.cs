using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Requests;
using Shared.Models.Cases.Responses;
using Web.Infrastructure.Managers.Generic;

namespace Web.Pages.CreateUpdates.Cases;
#nullable disable
public partial class CaseDetail
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public CaseResponse Case { get; set; }

    [Parameter]
    public Func<Task> GetAll { get; set; }
    public CaseResponse EditResponse { get; set; } = null!;
    [Inject]
    private IGenericService Service { get; set; } = null!;
    void Edit(CaseResponse response)
    {
        EditResponse = response;
    }
    public void CancelAsync()
    {
  
        EditResponse = null!;
        StateHasChanged();
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
