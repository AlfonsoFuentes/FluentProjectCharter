﻿using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Records;
using Shared.Models.BudgetItems.IndividualItems.Alterations.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Alterations.Queries
{
    public static class GetAlterationByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Alterations.EndPoint.GetById, async (GetAlterationByIdRequest request, IQueryRepository Repository) =>
                {
                   

                    Expression<Func<Alteration, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Alterations.Cache.GetById(request.Id);
                    var row = await Repository.GetAsync(Cache: CacheKey, Criteria: Criteria);

                    if (row == null)
                    {
                        return Result.Fail(request.NotFound);
                    }

                    var response = row.Map();
                    return Result.Success(response);

                });
            }
        }
        public static AlterationResponse Map(this Alteration row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,
                GanttTaskId = row.GanttTaskId,
                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
                UnitaryCost = row.UnitaryCost,
                Quantity = row.Quantity,
               
                IsAlteration = row.IsAlteration,
                IsTaxes = row.IsTaxes,
                BudgetUSD = row.BudgetUSD,
                ActualUSD = row.ActualUSD,
                CommitmentUSD = row.CommitmentUSD,
                PotentialUSD = row.PotentialUSD,

            };
        }


    }
}
