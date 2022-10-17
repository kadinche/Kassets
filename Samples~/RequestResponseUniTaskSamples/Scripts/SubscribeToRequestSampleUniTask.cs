using System;
using TMPro;
using UnityEngine;

#if KASSETS_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Kadinche.Kassets.RequestResponseSystem.Sample
{
    public class SubscribeToRequestSampleUniTask : MonoBehaviour
    {
        [SerializeField] private FloatRequestResponseEvent _dummyProcessRequestResponseEvent;
        [SerializeField] private TMP_Text _label;

        private IDisposable _subscription;

        private void Start()
        {
            _subscription = _dummyProcessRequestResponseEvent
                .SubscribeToRequest(value => _label.text = $"Request sent. Request value: {value}");
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}