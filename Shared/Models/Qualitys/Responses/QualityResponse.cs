﻿namespace Shared.Models.Qualitys.Responses
{
    public class QualityResponse : BaseResponse
    {

        public Guid? StartId { get; set; }
        public Guid? PlanningId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
