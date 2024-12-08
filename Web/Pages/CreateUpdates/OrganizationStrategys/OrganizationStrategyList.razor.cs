using Microsoft.AspNetCore.Components;
using Shared.Models.OrganizationStrategies.Requests;
using Shared.Models.OrganizationStrategies.Responses;
using Web.Infrastructure.Managers.Generic;
#nullable disable
namespace Web.Pages.CreateUpdates.OrganizationStrategys;
public partial class OrganizationStrategyList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    public Func<Task> GetAll { get; set; }

    [Inject]
    private IGenericService Service { get; set; } = null!;
    [Parameter]
    public List<OrganizationStrategyResponse> Items { get; set; }
    public List<OrganizationStrategyResponse> FilteredItems => nameFilter == string.Empty ? Items : Items.Where(x => x.Name.Contains(nameFilter)).ToList();
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

    public OrganizationStrategyResponse EditResponse { get; set; } = null!;

    void Edit(OrganizationStrategyResponse response)
    {
        EditResponse = response;
    }

    async Task Delete(OrganizationStrategyResponse caseResponse)
    {
        var dialog = await DialogService.Confirm($"Delete  {caseResponse.Name}?");
        
       
        if(dialog.Value)
        {
            DeleteOrganizationStrategyRequest request = new()
            {
                Id = caseResponse.Id,
                Name = caseResponse.Name,
      
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
