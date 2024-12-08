﻿using Shared.Models.Backgrounds.Validators;

namespace Server.EndPoint.BackGrounds.Vallidators
{
    public static class ValidateBackGroundNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.BackGrounds.EndPoint.Validate, async (ValidateBackGroundRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<BackGround, bool>> CriteriaId = x => x.CaseId == Data.CaseId;
                    Func<BackGround, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.BackGrounds.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}
