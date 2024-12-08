﻿using Shared.Models.KnownRisks.Validators;
using Shared.Models.Backgrounds.Validators;

namespace Server.EndPoint.KnownRisks.Vallidators
{
    public static class ValidateKnownRisksNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.KnownRisks.EndPoint.Validate, async (ValidateKnownRiskRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<KnownRisk, bool>> CriteriaId = x => x.CaseId == Data.CaseId;
                    Func<KnownRisk, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.KnownRisks.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
