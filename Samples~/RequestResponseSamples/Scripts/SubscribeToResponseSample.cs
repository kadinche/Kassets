using System;
using TMPro;
using UnityEngine;

namespace Kadinche.Kassets.RequestResponseSystem.Sample
{
    public class SubscribeToResponseSample : MonoBehaviour
    {
        [SerializeField] private FloatRequestResponseEvent _dummyProcessRequestResponseEvent;
        [SerializeField] private TMP_Text _label;

        private IDisposable _subscription;

        private void Start()
        {
            _subscription = _dummyProcessRequestResponseEvent
                .SubscribeToResponse(value => _label.text = $"Response received. Response value: {value}");
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}