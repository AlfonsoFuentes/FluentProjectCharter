using Shared.Models.BudgetItems.IndividualItems.Contingencys.Responses;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Records;
using Shared.Models.BudgetItems.IndividualItems.Foundations.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Contingencys.Queries
{
    public static class GetContingencyByIdEndPoint
    {
     
        public static ContingencyResponse Map(this Contingency row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,

                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
           
                ActualUSD = row.ActualUSD,
                CommitmentUSD = row.CommitmentUSD,
                PotentialUSD = row.PotentialUSD,
            };
        }


    }
}
