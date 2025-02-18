﻿using Server.Database.Entities.ProjectManagements;
using Server.Repositories;
using Shared.Models.Deliverables.Requests;

namespace Server.EndPoint.Deliverables.Commands
{

    public static class CreateDeliverableEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.Deliverables.EndPoint.Create, async (CreateDeliverableRequest Data, IRepository Repository) =>
                {
                    if (!Data.PlanningId.HasValue && !Data.StartId.HasValue)
                        return Result.Fail();
                    var lastorder = await Repository.GetLastOrderAsync<Deliverable, Project>(Data.ProjectId);

                    var row = Deliverable.Create(Data.ProjectId, Data.StartId, Data.PlanningId, lastorder);

                    await Repository.AddAsync(row);

                    Data.Map(row);
                    var cache = $"{StaticClass.Deliverables.Cache.GetAll}-{Data.ProjectId}";
                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);

                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);


                });
            }

        }


        static Deliverable Map(this CreateDeliverableRequest request, Deliverable row)
        {
            row.Name = request.Name;
            row.WBS = request.WBS;
            row.Order = request.Order;
            row.StartDate = request.StartDate;
            row.EndDate = request.EndDate;
            row.DependencyType = request.DependencyType;
            row.DurationTime = request.Duration;
            row.LabelOrder = request.LabelOrder;

            return row;
        }

    }



}
