﻿using Shared.Commons;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Web.Infrastructure.ExtensionMethods
{
    public static class ResultExtensions
    {
        public static async Task<IResult<T>> ToResult<T>(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();

            try
            {
                var responseObject = JsonSerializer.Deserialize<Result<T>>(responseAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                });
                return responseObject!;
            }
            catch (Exception ex)
            {
                string exm = ex.Message;
            }
            return Result<T>.Fail("Something went wrong in last operation");
        }

        public static async Task<IResult> ToResult(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            try
            {
                var responseObject = JsonSerializer.Deserialize<Result>(responseAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                });
                return responseObject!;
            }
            catch (Exception ex)
            {
                string exm = ex.Message;
            }
            return Result.Fail("Something went wrong in last operation");

        }

        public static async Task<PaginatedResult<T>> ToPaginatedResult<T>(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<PaginatedResult<T>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return responseObject!;
        }
        public static async Task<T> ToObject<T>(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            try
            {
                var responseObject = JsonSerializer.Deserialize<T>(responseAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true

                }
                );
                return responseObject!;
            }
            catch (Exception ex)
            {
                string exm = ex.Message;
            }
            return default!;
        }
    }
}