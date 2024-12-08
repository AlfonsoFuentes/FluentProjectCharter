﻿using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Deliverables.Validators
{
   
    public class ValidateDeliverableRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ScopeId { get; set; }

        public string EndPointName => StaticClass.Deliverables.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Deliverables.ClassName;
    }
}