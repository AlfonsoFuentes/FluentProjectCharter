﻿using Server.Database.Entities.ProjectManagements;
using Shared.Models.AcceptanceCriterias.Requests;

namespace Server.EndPoint.AcceptanceCriterias.Commands
{

    public static class CreateAcceptanceCriteriaEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.AcceptanceCriterias.EndPoint.Create, async (CreateAcceptanceCriteriaRequest Data, IRepository Repository) =>
                {
                   
                    var lastorder = await Repository.GetLastOrderAsync<AcceptanceCriteria, Project>(Data.ProjectId);

                    var row = AcceptanceCriteria.Create(Data.ProjectId,lastorder);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.AcceptanceCriterias.Cache.Key(row.Id, row.ProjectId)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static AcceptanceCriteria Map(this CreateAcceptanceCriteriaRequest request, AcceptanceCriteria row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
