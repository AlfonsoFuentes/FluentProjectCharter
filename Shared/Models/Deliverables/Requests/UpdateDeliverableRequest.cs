﻿using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Requests
{
    public class UpdateDeliverableRequest : UpdateMessageResponse, IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Deliverables.EndPoint.Update;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Deliverables.ClassName;
    }
}
