﻿using Shared.Models.FileResults.Generics.Reponses;

namespace Shared.Models.Qualitys.Responses
{
    public class QualityResponseList : IResponseAll
    {
        public List<QualityResponse> Items { get; set; } = new();
        public string ProjectName { get; set; } = string.Empty;
    }
}
