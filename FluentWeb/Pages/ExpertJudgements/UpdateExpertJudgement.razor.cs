using Microsoft.AspNetCore.Components;
using Shared.Enums.StakeHolderTypes;
using Shared.Models.ExpertJudgements.Records;
using Shared.Models.ExpertJudgements.Requests;
using Shared.Models.ExpertJudgements.Responses;
using Shared.Models.StakeHolders.Responses;
using Web.Infrastructure.Managers.StakeHolders;
#nullable disable
namespace FluentWeb.Pages.ExpertJudgements;
public partial class UpdateExpertJudgement
{
    UpdateExpertJudgementRequest Model = new();
    [Parameter]
    public Guid ProjectId { get; set; }
    [Parameter]
    public Guid Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await UpdateStakeHolder();
        var result = await GenericService.GetById<ExpertJudgementResponse, GetExpertJudgementByIdRequest>(
            new GetExpertJudgementByIdRequest() { Id = Id, ProjectId= ProjectId });

        if (result.Succeeded)
        {
            Model = new()
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                ProjectId = ProjectId,
                CaseId=result.Data.CaseId,
                Expert = result.Data.Expert == null ? null : result.Data.Expert,
            };
            expert = Model.Expert == null ? null : Model.Expert.Name;
        }
    }


    [Inject]
    private IStakeHolderService StakeHolderService { get; set; }
    StakeHolderResponseList StakeHolderResponseList = new();

    async Task UpdateStakeHolder()
    {
        var result = await StakeHolderService.GetAll();
        if (result.Succeeded)
        {
            StakeHolderResponseList = result.Data;
        }
    }
    void AddStakeHolder()
    {
        Navigation.NavigateTo($"/CreateStakeHolder");
    }

}
