﻿using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Cases.Validators
{
    public class ValidateCaseRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }

        public string EndPointName => StaticClass.Cases.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Cases.ClassName;
    }
    
}