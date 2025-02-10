﻿using Shared.Models.Objectives.Requests;
using Shared.Models.Objectives.Responses;

namespace Shared.Models.Objectives.Mappers
{
    public static class ObjectiveMapper
    {
        public static ChangeObjectiveOrderDowmRequest ToDown(this ObjectiveResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,
                Order = response.Order,


            };
        }
        public static ChangeObjectiveOrderUpRequest ToUp(this ObjectiveResponse response)
        {
            return new()
            {
                ProjectId = response.ProjectId,
                Id = response.Id,
                Name = response.Name,
                Order = response.Order,
            };
        }
        public static UpdateObjectiveRequest ToUpdate(this ObjectiveResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,
                ProjectId = response.ProjectId,
            };
        }
        public static CreateObjectiveRequest ToCreate(this ObjectiveResponse response, Guid? startid,Guid?plannid)
        {
            return new()
            {

                Name = response.Name,
                ProjectId = response.ProjectId,
                StartId = startid,
                PlanningId=plannid,
         
            };
        }
    }

}
