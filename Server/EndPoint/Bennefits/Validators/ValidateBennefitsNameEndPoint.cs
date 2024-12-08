﻿using Shared.Models.Bennefits.Validators;
using Shared.Models.Backgrounds.Validators;

namespace Server.EndPoint.Bennefits.Vallidators
{
    public static class ValidateBennefitsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Bennefits.EndPoint.Validate, async (ValidateBennefitRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Bennefit, bool>> CriteriaId = x => x.DeliverableId == Data.DeliverableId;
                    Func<Bennefit, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Bennefits.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
