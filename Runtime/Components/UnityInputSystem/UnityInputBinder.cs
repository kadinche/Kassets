#if KASSETS_INPUTSYSTEM

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Kadinche.Kassets.EventSystem;
using Kadinche.Kassets.VariableSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Kadinche.Kassets.UnityInputSystem
{
    [Flags]
    internal enum EventInputTypeEnum
    {
        Started = 1,
        Performed = 1 << 1,
        Canceled = 1 << 2
    }
    
    public class UnityInputBinder : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActionAsset;
        [Tooltip("Will bind InputActionAsset's enable with this MonoBehavior's")]
        [SerializeField] private bool _bindEnable;
        [SerializeField] private List<UnityEventInputBindInfo> _unityEventsToBind;
        [SerializeField] private List<EventInputBindInfo> _eventsToBind;
        [SerializeField] private List<PressedEventInputBindInfo> _pressEventsToBind;

        private void OnEnable()
        {
            if (_bindEnable)
                _inputActionAsset.Enable();
            BindEvents();
        }

        private void OnDisable()
        {
            if (_bindEnable)
                _inputActionAsset.Disable();
            UnbindEvents();
        }

        private void Start() => BindPressEvents();

        private void BindEvents()
        {
            foreach (var eventInfo in _unityEventsToBind)
            {
                var action = eventInfo.actionName;
                var inputAction = _inputActionAsset[action];
                if (inputAction == null) continue;
                
                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Started))
                    inputAction.started += eventInfo.BindEvent;
                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Performed))
                    inputAction.performed += eventInfo.BindEvent;
                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Canceled))
                    inputAction.canceled += eventInfo.BindEvent;
            }

            foreach (var eventInfo in _eventsToBind)
            {
                var action = eventInfo.actionName;
                var inputAction = _inputActionAsset[action];
                if (inputAction == null) continue;

                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Started))
                    inputAction.started += eventInfo.BindEvent;
                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Performed))
                    inputAction.performed += eventInfo.BindEvent;
                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Canceled))
                    inputAction.canceled += eventInfo.BindEvent;
            }
        }
        
        private void UnbindEvents()
        {
            foreach (var eventInfo in _unityEventsToBind)
            {
                var action = eventInfo.actionName;
                var inputAction = _inputActionAsset[action];
                if (inputAction == null) continue;
                
                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Started))
                    inputAction.started -= eventInfo.BindEvent;
                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Performed))
                    inputAction.performed -= eventInfo.BindEvent;
                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Canceled))
                    inputAction.canceled -= eventInfo.BindEvent;
            }
            
            foreach (var eventInfo in _eventsToBind)
            {
                var action = eventInfo.actionName;
                var inputAction = _inputActionAsset[action];
                if (inputAction == null) continue;
                
                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Started))
                    inputAction.started -= eventInfo.BindEvent;
                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Performed))
                    inputAction.performed -= eventInfo.BindEvent;
                if (eventInfo.eventInputType.HasFlag(EventInputTypeEnum.Canceled))
                    inputAction.canceled -= eventInfo.BindEvent;
            }
        }

        private void BindPressEvents()
        {
            var token = this.GetCancellationTokenOnDestroy();
            
            foreach (var eventInfo in _pressEventsToBind)
            {
                var action = eventInfo.actionName;
                var inputAction = _inputActionAsset[action];
                if (inputAction == null) continue;

                UniTaskAsyncEnumerable.EveryValueChanged(inputAction, a => a.ReadValue<float>() > eventInfo.treshold)
                    .Subscribe(eventInfo.SetPressed, token);
            }
        }
    }
    
    [Serializable]
    internal class UnityEventInputBindInfo
    {
        public string actionName;
        public EventInputTypeEnum eventInputType = EventInputTypeEnum.Performed;
        public UnityEvent eventToFire;

        public void BindEvent(InputAction.CallbackContext context) => eventToFire.Invoke();
    }

    [Serializable]
    internal class EventInputBindInfo
    {
        public GameEvent eventToFire;
        public string actionName;
        public EventInputTypeEnum eventInputType = EventInputTypeEnum.Performed;

        public void BindEvent(InputAction.CallbackContext context)
        {
            if (eventToFire is BoolEvent boolEvent)
            {
                var value = context.ReadValue<bool>();
                boolEvent.Raise(value);
            }
            else if (eventToFire is FloatEvent floatEvent)
            {
                var value = context.ReadValue<float>();
                floatEvent.Raise(value);
            }
            else if (eventToFire is IntEvent intEvent)
            {
                var value = context.ReadValue<int>();
                intEvent.Raise(value);
            }
            else if (eventToFire is Vector2Event vector2Event)
            {
                var value = context.ReadValue<Vector2>();
                vector2Event.Raise(value);
            }
            else
            {
                eventToFire.Raise();
            }
        }
    }

    [Serializable]
    internal class PressedEventInputBindInfo
    {
        public BoolVariable pressedVariable;
        public string actionName;
        public float treshold = 0.5f;

        public void SetPressed(bool pressed) => pressedVariable.Value = pressed;
    }
}

#endif
