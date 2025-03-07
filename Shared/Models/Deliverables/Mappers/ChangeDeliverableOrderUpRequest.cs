﻿using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Mappers
{
    public class ChangeDeliverableOrderUpRequest : UpdateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public string EndPointName => StaticClass.Deliverables.EndPoint.UpdateUp;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Deliverables.ClassName;
    }
}
