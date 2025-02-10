﻿using Shared.Models.FileResults.Generics.Records;

namespace Shared.Models.Bennefits.Records
{
    public class BennefitGetAll : IGetAll
    {
    
        public string EndPointName => StaticClass.BackGrounds.EndPoint.GetAll;
        public Guid ProjectId { get; set; }
    }
}
