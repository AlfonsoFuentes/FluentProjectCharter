﻿

using Shared.Models.FileResults.Generics.Records;
using Shared.Models.FileResults.Generics.Reponses;
using Shared.Models.FileResults.Generics.Request;

namespace Web.Infrastructure.Managers.Generic
{
    public interface IGenericService : IManager
    {
        Task<IResult> Create<T>(T request) where T : class, IRequest;
        Task<bool> Validate<T>(T request) where T : class, IRequest;
        Task<IResult> Update<T>(T request) where T : class, IRequest;

        Task<IResult> UpdateState<T>(T request) where T : class, IUpdateStateResponse;

        Task<IResult> Delete<T>(T request) where T : class, IRequest;
        Task<IResult<TResponse>> GetById<TResponse, TRequest>(TRequest request)
            where TResponse : class, IResponse
            where TRequest : class, IGetById;

        Task<IResult<TResponse>> GetAll<TResponse, TRequest>(TRequest request)
           where TResponse : class, IResponseAll
           where TRequest : class, IGetAll;

    }
    public class GenericService : IGenericService
    {
        IHttpClientService http;

        public GenericService(IHttpClientService http)
        {
            this.http = http;
        }

        public async Task<IResult> Create<T>(T request) where T : class, IRequest
        {
            var result = await http.PostAsJsonAsync(request.EndPointName, request);
            return await result.ToResult();
        }

        public async Task<IResult> Update<T>(T request) where T : class, IRequest
        {
            var result = await http.PostAsJsonAsync(request.EndPointName, request);
            return await result.ToResult();
        }
        public async Task<IResult> Delete<T>(T request) where T : class, IRequest
        {
            var result = await http.PostAsJsonAsync(request.EndPointName, request);
            return await result.ToResult();
        }
        public async Task<bool> Validate<T>(T request) where T : class, IRequest
        {
            var result = await http.PostAsJsonAsync(request.EndPointName, request);
            return await result.ToObject<bool>();
        }
        public async Task<IResult<TResponse>> GetById<TResponse, TRequest>(TRequest request)
            where TResponse : class, IResponse
            where TRequest : class, IGetById
        {

            var result = await http.PostAsJsonAsync(request.EndPointName, request);
            return await result.ToResult<TResponse>();
        }
        public async Task<IResult<TResponse>> GetAll<TResponse, TRequest>(TRequest request)
           where TResponse : class, IResponseAll
           where TRequest : class, IGetAll
        {

            var result = await http.PostAsJsonAsync(request.EndPointName, request);
            return await result.ToResult<TResponse>();
        }

        public async Task<IResult> UpdateState<T>(T request) where T : class, IUpdateStateResponse
        {
            var result = await http.PostAsJsonAsync(request.EndPointName, request);
            return await result.ToResult();
        }
    }
}
