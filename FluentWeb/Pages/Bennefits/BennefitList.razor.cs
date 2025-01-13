using Microsoft.AspNetCore.Components;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Bennefits.Requests;
using Shared.Models.Bennefits.Responses;
using Shared.Models.Deliverables.Responses;

namespace FluentWeb.Pages.Bennefits;
#nullable disable
public partial class BennefitList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public Guid DeliverableId { get; set; }
    [Parameter]
    [EditorRequired]
    public Guid ProjectId { get; set; }
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }
    [Parameter]
    [EditorRequired]
    public List<BennefitResponse> Items { get; set; }
    string nameFilter;
    public List<BennefitResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateBennefit/{DeliverableId}/{ProjectId}");

    }



    void Edit(BennefitResponse response)
    {
        Navigation.NavigateTo($"/UpdateBennefit/{response.Id}/{ProjectId}");
    }
    public async Task Delete(BennefitResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteBennefitRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = ProjectId,
            };
            var resultDelete = await GenericService.Delete(request);
            if (resultDelete.Succeeded)
            {
                await GetAll.Invoke();
                _snackBar.ShowSuccess(resultDelete.Messages);


            }
            else
            {
                _snackBar.ShowError(resultDelete.Messages);
            }
        }

    }

}
