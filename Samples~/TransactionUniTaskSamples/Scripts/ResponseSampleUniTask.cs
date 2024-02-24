using System;
using Kadinche.Kassets.Transaction;
using UnityEngine;
using Random = UnityEngine.Random;

#if KASSETS_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Kadinche.Kassets.RequestResponseSystem.Sample
{
    public class ResponseSampleUniTask : MonoBehaviour
    {
        [SerializeField] private FloatTransaction _dummyProcessTransaction;

#if KASSETS_UNITASK
        private IDisposable _subscription;

        private void Start()
        {
            _subscription = _dummyProcessTransaction.RegisterResponse(ProcessRequest);
        }

        private async UniTask<float> ProcessRequest(float requestValue)
        {
            var token = this.GetCancellationTokenOnDestroy();
            var delay = requestValue + Random.value * 3;
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
            return delay;
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
#else
        private void Start()
        {
            Debug.LogError("UniTask not found. Please import UniTask first");
        }
#endif
    }
}