﻿using Shared.Models.Constrainsts.Validators;
using Shared.Models.Backgrounds.Validators;

namespace Server.EndPoint.Constrainsts.Vallidators
{
    public static class ValidateConstrainstsNameEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Constrainsts.EndPoint.Validate, async (ValidateConstrainstRequest Data, IQueryRepository Repository) =>
                {
                    Expression<Func<Constrainst, bool>> CriteriaId = x => x.DeliverableId == Data.DeliverableId;
                    Func<Constrainst, bool> CriteriaExist = x => Data.Id == null ?
                    x.Name.Equals(Data.Name) : x.Id != Data.Id.Value && x.Name.Equals(Data.Name);
                    string CacheKey = StaticClass.Constrainsts.Cache.GetAll;

                    return await Repository.AnyAsync(Cache: CacheKey, CriteriaExist: CriteriaExist, CriteriaId: CriteriaId);
                });


            }
        }



    }

}