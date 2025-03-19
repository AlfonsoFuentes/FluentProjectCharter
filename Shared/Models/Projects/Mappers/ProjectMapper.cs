using Shared.Models.Projects.Reponses;

namespace Shared.Models.Projects.Mappers
{
    public static class ProjectMapper
    {
        public static ChangeProjectOrderDowmRequest ToDown(this ProjectResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,

                Order = response.Order,


            };
        }
        public static ChangeProjectOrderUpRequest ToUp(this ProjectResponse response)
        {
            return new()
            {

                Id = response.Id,
                Name = response.Name,
                Order = response.Order,
            };
        }
        public static UpdateProjectNameRequest ToUpdateName(this ProjectResponse response)
        {
            return new()
            {
                Id = response.Id,
                Name = response.Name,

            };
        }
        public static CreateProjectRequest ToCreate(this ProjectResponse response)
        {
            return new()
            {
                Name = response.Name,
                CostCenter = response.CostCenter,
                Focus = response.Focus,
                InitialProjectDate = response.InitialProjectDate,
                IsProductiveAsset = response.IsProductive,
                PercentageContingency = response.PercentageContingency,
                PercentageEngineering = response.PercentageEngineering,
                PercentageTaxProductive = response.PercentageTaxes,
                ProjectNeedType = response.ProjectNeedType,
                Status = response.Status,

            };
        }
        public static UpdateProjectRequest ToUpdate(this ProjectResponse response)
        {
            return new()
            {
                Name = response.Name,
                CostCenter = response.CostCenter,
                Focus = response.Focus,
                InitialProjectDate = response.InitialProjectDate,
                IsProductiveAsset = response.IsProductive,
                PercentageContingency = response.PercentageContingency,
                PercentageEngineering = response.PercentageEngineering,
                PercentageTaxProductive = response.PercentageTaxes,
                ProjectNeedType = response.ProjectNeedType,
                Status = response.Status,
                Id = response.Id

            };
        }
        public static ApproveProjectRequest ToApprove(this ProjectResponse response)
        {
            return new()
            {
                Name = response.Name,

                InitialProjectDate = response.InitialProjectDate,
                IsProductiveAsset = response.IsProductive,
                PercentageContingency = response.PercentageContingency,
                PercentageEngineering = response.PercentageEngineering,
                PercentageTaxProductive = response.PercentageTaxes,
                ProjectNeedType = response.ProjectNeedType,
                Status = response.Status,
                Id = response.Id,
                CostCenter = response.CostCenter,
                Focus = response.Focus,

            };
        }
    }

}
