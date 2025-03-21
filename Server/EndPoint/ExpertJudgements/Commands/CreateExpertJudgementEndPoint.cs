﻿using Server.Database.Entities.ProjectManagements;
using Shared.Models.ExpertJudgements.Requests;

namespace Server.EndPoint.ExpertJudgements.Commands
{

    public static class CreateExpertJudgementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.ExpertJudgements.EndPoint.Create, async (CreateExpertJudgementRequest Data, IRepository Repository) =>
                {
                  
                    var lastorder = await Repository.GetLastOrderAsync<ExpertJudgement, Project>(Data.ProjectId);

                    var row = ExpertJudgement.Create(Data.ProjectId,lastorder);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(GetCacheKeys(row));

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }
            private string[] GetCacheKeys(ExpertJudgement row)
            {
                List<string> cacheKeys = [
                    .. StaticClass.ExpertJudgements.Cache.Key(row.Id, row.ProjectId)
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }


        static ExpertJudgement Map(this CreateExpertJudgementRequest request, ExpertJudgement row)
        {
            row.Name = request.Name;
            row.ExpertId = request.Expert == null ? null : request.Expert.Id;
            return row;
        }

    }

}
