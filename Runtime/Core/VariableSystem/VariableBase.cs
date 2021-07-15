using Kadinche.Kassets.EventSystem;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kadinche.Kassets.Variable
{
    /// <summary>
    /// Variable System Basics.
    /// </summary>
    /// <typeparam name="T">Type to use on variable system</typeparam>
    public abstract class VariableBase<T> : GameEvent<T>, IVariable<T>
    {
        [Tooltip("Set how variable event behave.\nValue Assign: Raise when value is assigned regardless of value.\nValue Changed: Raise only when value is changed.")]
        [SerializeField] private VariableEventType _variableEventType;

        [Tooltip("If true will reset value when play mode end. Otherwise, keep runtime value.")]
        [SerializeField] private bool _autoResetValue;

        public virtual T Value
        {
            get => _value;
            set => Raise(value);
        }

        public override void Raise(T value)
        {
            if (_variableEventType == VariableEventType.ValueChange && _value.Equals(value))
                return;
            
            base.Raise(value);
        }

        public T InitialValue { get; private set; }

        public static implicit operator T(VariableBase<T> variable) => variable.Value;

        public override string ToString() => Value.ToString();

        public override void OnAfterDeserialize()
        {
            InitialValue = _value;
        }

        /// <summary>
        /// Reset value to InitialValue
        /// </summary>
        public void ResetValue()
        {
            _value = InitialValue;
        }
        
#if UNITY_EDITOR
        private void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            if (_autoResetValue && stateChange == PlayModeStateChange.ExitingPlayMode)
            {
                ResetValue();
            }
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
        
        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }
#endif
    }

    internal enum VariableEventType
    {
        ValueAssign,
        ValueChange
    }
}