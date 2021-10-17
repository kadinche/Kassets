using System;
using TMPro;
using UnityEngine;

namespace Kadinche.Kassets.EventSystem.Sample
{
    public class EventSubscriber : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private TMP_Text _displayText;
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
                _displayText.text = "";
                _index = 0;
            }
            
            _displayText.text += _textsToDisplay[_index] + " ";
            _index++;
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
        }
    }
}