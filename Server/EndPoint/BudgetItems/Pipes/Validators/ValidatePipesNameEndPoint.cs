﻿using Server.Database.Entities.BudgetItems.Commons;
using Shared.Models.BudgetItems.Pipes.Validators;

namespace Server.EndPoint.Pipes.Validators
{
    public static class ValidatePipesNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Pipes.EndPoint.Validate, async (ValidatePipeRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Pipe, bool>> CriteriaId = x => x.ProjectId == Data.ProjectId;
                    Func<Pipe, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Pipes.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
