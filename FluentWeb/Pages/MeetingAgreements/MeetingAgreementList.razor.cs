using Microsoft.AspNetCore.Components;
using Shared.Models.MeetingAgreements.Requests;
using Shared.Models.MeetingAgreements.Responses;
using Shared.Models.Meetings.Responses;

namespace FluentWeb.Pages.MeetingAgreements;
#nullable disable
public partial class MeetingAgreementList
{
    [CascadingParameter]
    public App App { get; set; }
    [Parameter]
    [EditorRequired]
    public MeetingResponse Parent { get; set; } = new();
    [Parameter]
    [EditorRequired]
    public Func<Task> GetAll { get; set; }



    public List<MeetingAgreementResponse> Items => Parent == null ? new() : Parent.Agreements;
    string nameFilter;
    public List<MeetingAgreementResponse> FilteredItems => string.IsNullOrEmpty(nameFilter) ? Items : Items.Where(x => x.Name.ToLower().Contains(nameFilter)).ToList();

    public void AddNew()
    {
        Navigation.NavigateTo($"/CreateMeetingAgreement/{Parent.Id}/{Parent.ProjectId}");

    }

    void Edit(MeetingAgreementResponse response)
    {
        Navigation.NavigateTo($"/UpdateMeetingAgreement/{response.Id}");
    }


   
    public async Task Delete(MeetingAgreementResponse response)
    {
        var dialog = await DialogService.ShowWarningAsync($"Delete {response.Name}?");
        var result = await dialog.Result;
        var canceled = result.Cancelled;



        if (!canceled)
        {
            DeleteMeetingAgreementRequest request = new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = Parent.Id,
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
