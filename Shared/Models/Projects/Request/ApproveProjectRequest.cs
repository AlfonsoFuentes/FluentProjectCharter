﻿using Shared.Enums.CostCenter;
using Shared.Enums.Focuses;
using Shared.Enums.ProjectNeedTypes;
using Shared.Models.FileResults.Generics.Request;
using Shared.Models.StakeHolders.Responses;

namespace Shared.Models.Projects.Request
{
    public class ApproveProjectRequest : UpdateMessageResponse, IRequest
    {
        public string EndPointName => StaticClass.Projects.EndPoint.Approve;
        public override string ClassName => StaticClass.Projects.ClassName;
        public override string Legend => Name;
        public string ProjectNumber { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
 
        public DateTime? InitialProjectDate { get; set; } = DateTime.Today;
        public double PercentageTaxProductive { get; set; }
        public bool IsProductiveAsset { get; set; } = true;
        public ProjectNeedTypeEnum ProjectNeedType { get; set; } = ProjectNeedTypeEnum.None;
        public double PercentageEngineering { get; set; }
        public double PercentageContingency { get; set; }
        public FocusEnum Focus { get; set; } = FocusEnum.None;
        public CostCenterEnum CostCenter { get; set; } = CostCenterEnum.None;
        public ProjectStatusEnum Status { get; set; } = ProjectStatusEnum.Approved;

        public int BudgetItems {  get; set; } = 0;
    }
}
