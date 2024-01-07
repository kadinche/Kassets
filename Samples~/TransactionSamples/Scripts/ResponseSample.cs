using System;
using UnityEngine;

namespace Kadinche.Kassets.Transaction.Sample
{
    public class ResponseSample : MonoBehaviour
    {
        [SerializeField] private FloatTransaction _dummyProcessTransaction;

        private IDisposable _subscription;

        private void Start()
        {
            _subscription = _dummyProcessTransaction.RegisterResponse(ProcessRequest);
        }

        private float ProcessRequest(float requestValue)
        {
            var startTime = DateTime.Now;
            var loopCounter = 0;
            for (var i = 0; i < 999999999; i++)
            {
                loopCounter++;
            }
            var responseValue = (float)(DateTime.Now - startTime).TotalSeconds;
            Debug.Log($"looped {loopCounter} times.");
            return responseValue;
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}