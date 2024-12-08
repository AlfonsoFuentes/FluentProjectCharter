using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.KnownRisks.Requests;
using Shared.Models.KnownRisks.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.KnownRisks;
public partial class KnownRiskList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public CaseResponse Parent { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<KnownRiskResponse> Items => Parent.KnownRisks;
    public List<KnownRiskResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
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

    public KnownRiskResponse EditResponse { get; set; } = null!;

    void Edit(KnownRiskResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(KnownRiskResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete  {caseResponse.Name}?");
        
       
        if(dialog.Value)
        {
            DeleteKnownRiskRequest request = new()
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
