﻿using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Models.BudgetItems.IndividualItems.Structurals.Records;
using Shared.Models.BudgetItems.IndividualItems.Structurals.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Structurals.Queries
{
    public static class GetStructuralByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Structurals.EndPoint.GetById, async (GetStructuralByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Structural>, IIncludableQueryable<Structural, object>> Includes = x => null!;

                    ;

                    Expression<Func<Structural, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Structurals.Cache.GetById(request.Id);
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
        public static StructuralResponse Map(this Structural row)
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
                ActualUSD = row.ActualUSD,
                CommitmentUSD = row.CommitmentUSD,
                PotentialUSD = row.PotentialUSD,
            };
        }


    }
}
