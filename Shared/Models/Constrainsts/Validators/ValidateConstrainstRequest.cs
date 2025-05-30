﻿using Shared.Models.FileResults.Generics.Request;

namespace Shared.Models.Constrainsts.Validators
{
    public class ValidateConstrainstRequest : ValidateMessageResponse, IRequest
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
 
        public Guid ProjectId { get; set; }
        public string EndPointName => StaticClass.Constrainsts.EndPoint.Validate;

        public override string Legend => Name;

        public override string ClassName => StaticClass.Constrainsts.ClassName;
    }

}
