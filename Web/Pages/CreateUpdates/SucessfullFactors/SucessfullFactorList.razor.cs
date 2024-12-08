using Microsoft.AspNetCore.Components;
using Shared.Models.Cases.Responses;
using Shared.Models.SucessfullFactors.Requests;
using Shared.Models.SucessfullFactors.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.SucessfullFactors;
public partial class SucessfullFactorList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public CaseResponse Parent { get; set; } = new();
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    public List<SucessfullFactorResponse> Items => Parent.SucessfullFactors;
    public List<SucessfullFactorResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
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

    public SucessfullFactorResponse EditResponse { get; set; } = null!;

    void Edit(SucessfullFactorResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(SucessfullFactorResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete SucessfullFactor {caseResponse.Name}?");
      
        
       
        if(dialog.Value)
        {
            DeleteSucessfullFactorRequest request = new()
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
