﻿using Server.Database.Entities.ProjectManagements;
using System.Threading;

namespace Server.EndPoint.Bennefits.Commands
{

    public static class CreateBennefitEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Bennefits.EndPoint.Create, async (CreateBennefitRequest Data, IRepository Repository) =>
                {
                    
                    var lastorder = await Repository.GetLastOrderAsync<Bennefit, Project>(Data.ProjectId);

                    var row = Bennefit.Create(Data.ProjectId,lastorder);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    List<string> cache = [.. StaticClass.Bennefits.Cache.Key(row.Id, row.ProjectId)];

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache.ToArray());

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });


            }
        }


        static Bennefit Map(this CreateBennefitRequest request, Bennefit row)
        {
            row.Name = request.Name;
            return row;
        }

    }

}
