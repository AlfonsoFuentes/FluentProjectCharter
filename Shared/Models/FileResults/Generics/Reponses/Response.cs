﻿using Shared.Models.BaseResponses;

namespace Shared.Models.FileResults.Generics.Reponses
{
    public interface IResponse
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
    public class Response<T> : BaseResponse where T : IResponse
    {
        public T Value { get; set; } = default!;


    }
}