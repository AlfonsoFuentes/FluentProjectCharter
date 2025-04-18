﻿using Shared.Models.BudgetItems.IndividualItems.Paintings.Records;
using Shared.Models.BudgetItems.IndividualItems.Paintings.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Paintings.Queries
{
    public static class GetPaintingByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Paintings.EndPoint.GetById, async (GetPaintingByIdRequest request, IQueryRepository Repository) =>
                {
                    Func<IQueryable<Painting>, IIncludableQueryable<Painting, object>> Includes = x => null!;

                    ;

                    Expression<Func<Painting, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Paintings.Cache.GetById(request.Id);
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
        public static PaintingResponse Map(this Painting row)
        {
            return new()
            {
                Id = row.Id,
                Name = row.Name,

                ProjectId = row.ProjectId,
                Nomenclatore = row.Nomenclatore,
                UnitaryCost = row.UnitaryCost,
                Quantity = row.Quantity,
                ActualUSD = row.ActualUSD,
                CommitmentUSD = row.CommitmentUSD,
                PotentialUSD = row.PotentialUSD,
                BudgetUSD = row.BudgetUSD,
               
            };
        }


    }
}
