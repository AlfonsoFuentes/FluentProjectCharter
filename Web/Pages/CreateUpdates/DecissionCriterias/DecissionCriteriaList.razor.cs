using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.DecissionCriterias.Requests;
using Shared.Models.DecissionCriterias.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.DecissionCriterias;
public partial class DecissionCriteriaList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public CaseResponse Parent { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<DecissionCriteriaResponse> Items => Parent.DecissionCriterias;
    public List<DecissionCriteriaResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
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

    public DecissionCriteriaResponse EditResponse { get; set; } = null!;

    void Edit(DecissionCriteriaResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(DecissionCriteriaResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete {caseResponse.Name}?");
        
       
        if(dialog.Value)
        {
            DeleteDecissionCriteriaRequest request = new()
            {
                Id = caseResponse.Id,
                Name = caseResponse.Name,
                ProjectId=caseResponse.ProjectId,
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
