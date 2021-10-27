using System;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem.Sample
{
    public class ResponseSample : MonoBehaviour
    {
        [SerializeField] private FloatRequestResponseEvent _dummyProcessRequestResponseEvent;

        private IDisposable _subscription;

        private void Start()
        {
            _subscription = _dummyProcessRequestResponseEvent.SubscribeResponse(ProcessRequest);
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