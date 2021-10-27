using System;
using Kadinche.Kassets.Variable;
using TMPro;
using UnityEngine;

namespace Kadinche.Kassets.EventSystem.Sample
{
    public class EventHandler : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private StringVariable _textVariable;
        [SerializeField] private string[] _textsToDisplay = new[]
        {
            "Every", "time", "event", "is", "fired", "a", "word", "is", "added", "to", "make", "a", "full", "sentence."
        };

        private int _index;
        private IDisposable _subscription;

        private void Start()
        {
            _subscription = _gameEvent.Subscribe(OnEventRaised);
        }

        private void OnEventRaised()
        {
            if (_index >= _textsToDisplay.Length)
            {
                _textVariable.Value = "";
                _index = 0;
            }
            
            _textVariable.Value += _textsToDisplay[_index] + " ";
            _index++;
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
        }
    }
}