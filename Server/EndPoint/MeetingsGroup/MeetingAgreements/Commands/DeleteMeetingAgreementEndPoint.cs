﻿using Shared.Models.MeetingAgreements.Requests;

namespace Server.EndPoint.MeetingsGroup.MeetingAgreements.Commands
{
    public static class DeleteMeetingAgreementEndPoint
    {
        public class EndPoint : IEndPoint
        {
            public void MapEndPoint(IEndpointRouteBuilder app)
            {
                app.MapPost(StaticClass.MeetingAgreements.EndPoint.Delete, async (DeleteMeetingAgreementRequest Data, IRepository Repository) =>
                {
                    var row = await Repository.GetByIdAsync<MeetingAgreement>(Data.Id);
                    if (row == null) { return Result.Fail(Data.NotFound); }


                    var cache = GetCacheKeys(row, Data.ProjectId);

                    await Repository.RemoveAsync(row);

                    var result = await Repository.Context.SaveChangesAndRemoveCacheAsync(cache);
                    return Result.EndPointResult(result,
                        Data.Succesfully,
                        Data.Fail);

                });
            }
            private string[] GetCacheKeys(MeetingAgreement row, Guid ProjectId)
            {
                List<string> cacheKeys = [
                    .. StaticClass.Projects.Cache.Key(ProjectId),
                    .. StaticClass.Meetings.Cache.Key(row.MeetingId),
                
                ];
                return cacheKeys.Where(key => !string.IsNullOrEmpty(key)).ToArray();
            }
        }
    }
}
