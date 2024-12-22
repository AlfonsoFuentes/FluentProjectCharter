﻿using Shared.Models.AppStates.Requests;


namespace Server.EndPoint.AppStates.Commands
{
    public static partial class UpdateCaseScopeStateEndPoint
    {
        public class EndPointProject : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AppStates.EndPoint.UpdateCaseScope, async (UpdateActiveStateRequest Data, IRepository Repository) =>
                {
                    Expression<Func<Case, bool>> Criteria = x => x.Id ==
                   Data.CaseId;

                    var row = await Repository.GetAsync(Criteria: Criteria);
                    if (row == null)
                    {
                        return Result.Fail();
                    }
                    await Repository.UpdateAsync(row);
                    row.ScopeId = Data.ScopeId;

                    List<string> cache = [.. StaticClass.AppStates.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());


                    return Result.EndPointResult(result,
                        "",
                        "");



                });
            }
        }

    }

}
