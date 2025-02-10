﻿using Shared.Models.KnownRisks.Requests;
using Shared.Models.KnownRisks.Responses;

namespace Shared.Models.KnownRisks.Mappers
{
    public static class KnownRiskMapper
    {
        public static ChangeKnownRiskOrderDowmRequest ToDown(this KnownRiskResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,
                Order = response.Order,


            };
        }
        public static ChangeKnownRiskOrderUpRequest ToUp(this KnownRiskResponse response)
        {
            return new()
            {
                ProjectId = response.ProjectId,
                Id = response.Id,
                Name = response.Name,
                Order = response.Order,
            };
        }
        public static UpdateKnownRiskRequest ToUpdate(this KnownRiskResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,
            };
        }
        public static CreateKnownRiskRequest ToCreate(this KnownRiskResponse response, Guid? startid, Guid? planid)
        {
            return new()
            {

                Name = response.Name,
                ProjectId = response.ProjectId,
                StartId = startid,
                PlanningId = planid
            };
        }
    }

}
