﻿using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Milestones.Requests
{
    public class ChangeMilestoneOrderDowmRequest : UpdateMessageResponse, IRequest
    {
   
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Milestones.EndPoint.UpdateDown;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Milestones.ClassName;
    }
}
