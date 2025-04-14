using Microsoft.AspNetCore.Components;
using Shared.Models.AcceptanceCriterias.Records;
using Shared.Models.AcceptanceCriterias.Responses;
using Shared.Models.Acquisitions.Records;
using Shared.Models.Acquisitions.Responses;
using Shared.Models.Assumptions.Records;
using Shared.Models.Assumptions.Responses;
using Shared.Models.Backgrounds.Records;
using Shared.Models.Backgrounds.Responses;
using Shared.Models.Bennefits.Records;
using Shared.Models.Bennefits.Responses;
using Shared.Models.BudgetItems.Records;
using Shared.Models.BudgetItems.Responses;
using Shared.Models.Communications.Records;
using Shared.Models.Communications.Responses;
using Shared.Models.Constrainsts.Records;
using Shared.Models.Constrainsts.Responses;
using Shared.Models.Deliverables.Records;
using Shared.Models.Deliverables.Responses;
using Shared.Models.ExpertJudgements.Records;
using Shared.Models.ExpertJudgements.Responses;
using Shared.Models.GanttTasks.Mappers;
using Shared.Models.GanttTasks.Records;
using Shared.Models.GanttTasks.Responses;
using Shared.Models.KnownRisks.Records;
using Shared.Models.KnownRisks.Responses;
using Shared.Models.LearnedLessons.Records;
using Shared.Models.LearnedLessons.Responses;
using Shared.Models.Objectives.Records;
using Shared.Models.Objectives.Responses;
using Shared.Models.Projects.Mappers;
using Shared.Models.Projects.Records;
using Shared.Models.Projects.Reponses;
using Shared.Models.Projects.Request;
using Shared.Models.Qualitys.Records;
using Shared.Models.Qualitys.Responses;
using Shared.Models.Requirements.Records;
using Shared.Models.Requirements.Responses;
using Shared.Models.Resources.Records;
using Shared.Models.Resources.Responses;
using Shared.Models.Scopes.Records;
using Shared.Models.Scopes.Responses;
using Shared.Models.StakeHolderInsideProjects.Records;
using Shared.Models.StakeHolderInsideProjects.Responses;
using Shared.Models.StakeHolders.Responses;

namespace FluentWeb.Pages.Projects;
public partial class NewUpdateProject
{
    //UpdateProjectRequest Model = new();
    //[Parameter]
    //public Guid Id { get; set; }

    //bool _loaded = false;

    //protected override async Task OnInitializedAsync()
    //{


    //    var result = await GenericService.GetById<ProjectResponse, GetProjectByIdRequest>(new GetProjectByIdRequest() { Id = Id });

    //    if (result.Succeeded)
    //    {
    //        Model = result.Data.ToUpdate();
    //        var tasks = new List<Task>
    //    {
    //        GetAllStakeHolderInsideProject(),
    //        GetAllExpertJudgement(),
    //        GetAllLearnedLesson(),
    //        GetAllQuality(),
    //        GetAllKnownRisk(),
    //        GetAllDeliverables(),
    //        GetAllCommunication(),
    //        GetAllResource(),
    //        GetAllAcquisition(),
    //        GetAllAssumption(),
    //        GetAllConstrainst(),
    //        GetAllAcceptanceCriteria(),
    //        GetAllRequirement(),
    //        GetAllScope(),
    //        GetAllBennefit(),
    //        GetAllObjective(),
    //        GetAllBackground(),
    //        GetAllBudgetItems(),
           
    //    };

    //        await Task.WhenAll(tasks);
    //    }
    //    _loaded = true;
     
    //}



    //DeliverableResponseList DeliverableResponse { get; set; } = new();
    //async Task GetAllDeliverables()
    //{
    //    if (Id == Guid.Empty) return;
    //    var result = await GenericService.GetAll<DeliverableResponseList, DeliverableGetAll>(new DeliverableGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        DeliverableResponse = result.Data;
    //        await GetAllGantt();


    //    }
    //}
    //DeliverableWithGanttTaskResponseList GanttTaskResponse { get; set; } = new();
    //async Task GetAllGantt()
    //{
    //    if (Id == Guid.Empty) return;
    //    var result = await GenericService.GetAll<DeliverableWithGanttTaskResponseListToUpdate, GanttTaskGetAll>(new GanttTaskGetAll
    //    {
    //        ProjectId = Id,
    //    });

    //    if (result.Succeeded)
    //    {

    //        GanttTaskResponse = result.Data.ToReponse();
    //        StateHasChanged();
    //    }
    //}
    //BackGroundResponseList BackGroundResponseList { get; set; } = new();
    //async Task GetAllBackground()
    //{
    //    var result = await GenericService.GetAll<BackGroundResponseList, BackGroundGetAll>(new BackGroundGetAll() { ProjectId = Id });
    //    if (result.Succeeded)
    //    {
    //        BackGroundResponseList = result.Data;


    //    }
    //}
    //ObjectiveResponseList ObjectiveResponseList = new();
    //async Task GetAllObjective()
    //{
    //    var result = await GenericService.GetAll<ObjectiveResponseList, ObjectiveGetAll>(new ObjectiveGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        ObjectiveResponseList = result.Data;


    //    }
    //}
    //BennefitResponseList BennefitResponseList = new();
    //async Task GetAllBennefit()
    //{
    //    var result = await GenericService.GetAll<BennefitResponseList, BennefitGetAll>(new BennefitGetAll()
    //    {
    //        ProjectId = Id,


    //    });
    //    if (result.Succeeded)
    //    {
    //        BennefitResponseList = result.Data;



    //    }
    //}
    //ScopeResponseList ScopeResponseList = new();
    //async Task GetAllScope()
    //{
    //    var result = await GenericService.GetAll<ScopeResponseList, ScopeGetAll>(new ScopeGetAll()
    //    {

    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        ScopeResponseList = result.Data;



    //    }
    //}
    //RequirementResponseList RequirementResponseList = new();
    //async Task GetAllRequirement()
    //{
    //    var result = await GenericService.GetAll<RequirementResponseList, RequirementGetAll>(new RequirementGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        RequirementResponseList = result.Data;


    //    }
    //}
    //AcceptanceCriteriaResponseList AcceptanceCriteriaResponseList = new();
    //async Task GetAllAcceptanceCriteria()
    //{
    //    var result = await GenericService.GetAll<AcceptanceCriteriaResponseList, AcceptanceCriteriaGetAll>(new AcceptanceCriteriaGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        AcceptanceCriteriaResponseList = result.Data;


    //    }
    //}
    //ConstrainstResponseList ConstrainstResponseList = new();
    //async Task GetAllConstrainst()
    //{
    //    var result = await GenericService.GetAll<ConstrainstResponseList, ConstrainstGetAll>(new ConstrainstGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        ConstrainstResponseList = result.Data;


    //    }
    //}

    //AssumptionResponseList AssumptionResponseList = new();
    //async Task GetAllAssumption()
    //{
    //    var result = await GenericService.GetAll<AssumptionResponseList, AssumptionGetAll>(new AssumptionGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        AssumptionResponseList = result.Data;



    //    }
    //}
    //AcquisitionResponseList AcquisitionResponseList = new();
    //async Task GetAllAcquisition()
    //{
    //    var result = await GenericService.GetAll<AcquisitionResponseList, AcquisitionGetAll>(new AcquisitionGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        AcquisitionResponseList = result.Data;


    //    }
    //}

    //CommunicationResponseList CommunicationResponseList = new();
    //async Task GetAllCommunication()
    //{
    //    var result = await GenericService.GetAll<CommunicationResponseList, CommunicationGetAll>(new CommunicationGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        CommunicationResponseList = result.Data;



    //    }
    //}
    //ResourceResponseList ResourceResponseList = new();
    //async Task GetAllResource()
    //{
    //    var result = await GenericService.GetAll<ResourceResponseList, ResourceGetAll>(new ResourceGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        ResourceResponseList = result.Data;



    //    }
    //}
    //KnownRiskResponseList KnownRiskResponseList = new();
    //async Task GetAllKnownRisk()
    //{
    //    var result = await GenericService.GetAll<KnownRiskResponseList, KnownRiskGetAll>(new KnownRiskGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        KnownRiskResponseList = result.Data;


    //    }
    //}
    //QualityResponseList QualityResponseList = new();
    //async Task GetAllQuality()
    //{
    //    var result = await GenericService.GetAll<QualityResponseList, QualityGetAll>(new QualityGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        QualityResponseList = result.Data;

    //    }
    //}
    //LearnedLessonResponseList LearnedLessonResponseList = new();
    //async Task GetAllLearnedLesson()
    //{
    //    var result = await GenericService.GetAll<LearnedLessonResponseList, LearnedLessonGetAll>(new LearnedLessonGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        LearnedLessonResponseList = result.Data;



    //    }
    //}
    //ExpertJudgementResponseList ExpertJudgementResponseList = new();
    //async Task GetAllExpertJudgement()
    //{
    //    var result = await GenericService.GetAll<ExpertJudgementResponseList, ExpertJudgementGetAll>(new ExpertJudgementGetAll()
    //    {
    //        ProjectId = Id,

    //    });
    //    if (result.Succeeded)
    //    {
    //        ExpertJudgementResponseList = result.Data;


    //    }
    //}
    //BudgetItemResponseList BudgetItemResponseList = new();
    //async Task GetAllBudgetItems()
    //{
    //    var result = await GenericService.GetAll<BudgetItemResponseList, BudgetItemGetAll>(new BudgetItemGetAll()
    //    {
    //        ProjectId = Id
    //    });
    //    if (result.Succeeded)
    //    {
    //        BudgetItemResponseList = result.Data;
    //    }
    //}
    //StakeHolderInsideProjectResponseList StakeHolderInsideProjectResponseList = new();
    //async Task GetAllStakeHolderInsideProject()
    //{

    //    var result = await GenericService.GetAll<StakeHolderInsideProjectResponseList, StakeHolderInsideProjectGetAll>(
    //        new StakeHolderInsideProjectGetAll() { ProjectId = Id });
    //    if (result.Succeeded)
    //    {
    //        StakeHolderInsideProjectResponseList = result.Data;

    //        await UpdateStakeHolder();
    //    }

    //}
    //[Inject]
    //private IStakeHolderService StakeHolderService { get; set; } = null!;
    //StakeHolderResponseList StakeHolderResponseList = new();
    //async Task UpdateStakeHolder()
    //{
    //    var result = await StakeHolderService.GetAll();
    //    if (result.Succeeded)
    //    {

    //        var allStakeHolders = result.Data.Items;
    //        var selectedStakeHolderIds = StakeHolderInsideProjectResponseList.Items.Select(item => item.StakeHolder.Id).ToHashSet();
    //        StakeHolderResponseList.Items = allStakeHolders
    //            .Where(stakeHolder => !selectedStakeHolderIds.Contains(stakeHolder.Id))
    //            .ToList();
    //    }
    //}
   
    

}
