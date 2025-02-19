﻿using Server.Database.Entities.ProjectManagements;
using Shared.Models.Communications.Requests;

namespace Server.EndPoint.Communications.Commands
{

    public static class CreateCommunicationEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Communications.EndPoint.Create, async (CreateCommunicationRequest Data, IRepository Repository) =>
                {
                    if (!Data.PlanningId.HasValue && !Data.StartId.HasValue)
                        return Result.Fail();
                    var lastorder = await Repository.GetLastOrderAsync<Communication, Project>(Data.ProjectId);

                    var row = Communication.Create(Data.ProjectId, Data.StartId, Data.PlanningId, lastorder);

                    await Repository.AddAsync(row);



                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Communications.Cache.Key(row.Id)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Communication Map(this CreateCommunicationRequest request, Communication row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
