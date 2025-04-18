using Shared.Models.BudgetItems.IndividualItems.Engineerings.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Engineerings.Queries
{
    public static class GetEngineeringByIdEndPoint
    {

        public static EngineeringResponse Map(this Engineering row)
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
                BudgetUSD = row.BudgetUSD,
    
            };
        }


    }
}
