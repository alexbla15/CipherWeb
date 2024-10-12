﻿using System.Reflection;

namespace CipherData.RandomMode
{
    public class RandomGenericRequests : IGenericRequests
    {
        public Task<Tuple<T, ErrorResponse>> Request<T>(T successResult, bool canBadRequest = true, bool canBeNotFound = false, bool canFail = false)
        {
            int result = new Random().Next(1, 4);

            if (canFail)
            {
                var emptyMethod = typeof(T).GetMethod("Empty", BindingFlags.Static | BindingFlags.Public);

                Tuple<T, ErrorResponse> response = result switch
                {
                    1 => new(successResult, ErrorResponse.Success),
                    2 when canBadRequest => new((T)emptyMethod.Invoke(null, null), ErrorResponse.BadRequest),
                    3 when canBeNotFound => new((T)emptyMethod.Invoke(null, null), ErrorResponse.NotFound),
                    _ => new((T)emptyMethod.Invoke(null, null), ErrorResponse.Unauthorized)
                };
                return Task.FromResult(response);
            }
            else
            {
                Tuple<T, ErrorResponse> response = new(successResult, ErrorResponse.Success);
                return Task.FromResult(response);
            }
        }
    }
}