﻿using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Resources.Requests
{
    public class UpdateResourceRequest : UpdateMessageResponse, IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string EndPointName => StaticClass.Resources.EndPoint.Update;
     
        public override string Legend => Name;

        public override string ClassName => StaticClass.Resources.ClassName;
    }
}
