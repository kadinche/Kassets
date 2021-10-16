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
        [SerializeField] internal VariableEventType _variableEventType;

        [Tooltip("If true will reset value when play mode end. Otherwise, keep runtime value.")]
        [SerializeField] protected bool _autoResetValue;

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

        public T InitialValue { get; protected set; }

        /// <summary>
        /// Reset value to InitialValue
        /// </summary>
        public void ResetValue()
        {
            Value = InitialValue;
        }
        
        public static implicit operator T(VariableBase<T> variable) => variable.Value;

        public override string ToString() => Value.ToString();

        public override void OnAfterDeserialize()
        {
            InitialValue = _value;
        }
        
#if UNITY_EDITOR
        protected override void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            base.OnPlayModeStateChanged(stateChange);
            if (_autoResetValue && stateChange == PlayModeStateChange.ExitingPlayMode)
            {
                ResetValue();
            }
        }
#endif
    }

    internal enum VariableEventType
    {
        ValueAssign,
        ValueChange
    }
}