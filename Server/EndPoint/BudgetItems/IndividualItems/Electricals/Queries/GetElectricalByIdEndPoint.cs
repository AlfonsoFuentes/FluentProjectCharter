﻿using Server.Database.Entities.BudgetItems.Commons;
using Server.EndPoint.Projects.Queries;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Records;
using Shared.Models.BudgetItems.IndividualItems.Electricals.Responses;

namespace Server.EndPoint.BudgetItems.IndividualItems.Electricals.Queries
{
    public static class GetElectricalByIdEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Electricals.EndPoint.GetById, async (GetElectricalByIdRequest request, IQueryRepository Repository) =>
                {
                    //Func<IQueryable<Electrical>, IIncludableQueryable<Electrical, object>> Includes = x => x.Include(x => x.Deliverable!);

                    //;

                    Expression<Func<Electrical, bool>> Criteria = x => x.Id == request.Id;

                    string CacheKey = StaticClass.Electricals.Cache.GetById(request.Id);
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
        public static ElectricalResponse Map(this Electrical row)
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
                BudgetUSD = row.BudgetUSD,
          
            };
        }


    }
}
