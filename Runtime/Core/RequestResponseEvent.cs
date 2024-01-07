using System;
using Kadinche.Kassets.Transaction;

namespace Kadinche.Kassets.RequestResponseSystem
{
    [Obsolete("Renamed RequestResponseEvent to Transaction for naming purpose. Please use TransactionCore derived classes instead.")]
    public abstract class RequestResponseEvent<TRequest, TResponse> : TransactionCore<TRequest, TResponse>
    {
        [Obsolete("To avoid confusion with SubscribeToResponse, use RegisterResponse instead.")]
        public IDisposable SubscribeResponse(Func<TRequest, TResponse> responseFunc, bool overrideResponse = true) =>
            RegisterResponse(responseFunc, overrideResponse);
    }

    public abstract class RequestResponseEvent<T> : TransactionCore<T, T>
    {
    }
}