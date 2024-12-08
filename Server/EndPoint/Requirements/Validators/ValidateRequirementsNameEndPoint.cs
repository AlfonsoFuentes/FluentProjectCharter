﻿using Shared.Models.Requirements.Validators;
using Shared.Models.Backgrounds.Validators;

namespace Server.EndPoint.Requirements.Vallidators
{
    public static class ValidateRequirementsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Requirements.EndPoint.Validate, async (ValidateRequirementRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Requirement, bool>> CriteriaId = x => x.DeliverableId == Data.DeliverableId;
                    Func<Requirement, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Requirements.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
