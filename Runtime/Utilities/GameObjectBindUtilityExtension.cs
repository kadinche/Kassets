using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;

namespace Kadinche.Kassets.Utilities
{
    public static class GameObjectBindUtilityExtension
    {
        public static void BindActivation(this GameObject source, BoolVariable activationFlag, bool reversedFlag = false, CancellationToken cancellationToken = default)
        {
            var token = cancellationToken == CancellationToken.None
                ? source.GetCancellationTokenOnDestroy()
                : cancellationToken;
            
            activationFlag.Subscribe(value => source.SetActive(reversedFlag ? !value : value), token);
        }

        public static void BindActivation(this GameObjectVariable source, BoolVariable activationFlag, bool reversedFlag = false,
            CancellationToken cancellationToken = default) =>
            BindActivation(source.Value, activationFlag, reversedFlag, cancellationToken);
    }
}