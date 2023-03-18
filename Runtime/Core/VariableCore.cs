using Kadinche.Kassets.EventSystem;
using UnityEngine;

namespace Kadinche.Kassets.Variable
{
    /// <summary>
    /// Variable System Basics.
    /// </summary>
    /// <typeparam name="T">Type to use on variable system</typeparam>
    public abstract partial class VariableCore<T> : GameEvent<T>, IVariable<T>
    {
        [Tooltip("Set how variable event behave.\nValue Assign: Raise when value is assigned regardless of value.\nValue Changed: Raise only when value is changed.")]
        [SerializeField] protected VariableEventType variableEventType;

        [Tooltip("If true will reset value when play mode end. Otherwise, keep runtime value.")]
        [SerializeField] protected bool autoResetValue;
        
        public virtual T Value
        {
            get => _value;
            set => Raise(value);
        }

        public override void Raise(T value)
        {
            if (variableEventType == VariableEventType.ValueChange && _value.Equals(value))
                return;
            
            base.Raise(value);
        }

        public virtual T InitialValue { get; protected set; }

        /// <summary>
        /// Reset value to InitialValue
        /// </summary>
        public void ResetValue() => ResetInternal();

        protected override void ResetInternal()
        {
            Value = InitialValue;
        }

        public static implicit operator T(VariableCore<T> variable) => variable.Value;

        public override string ToString() => Value.ToString();

#if !UNITY_EDITOR
        protected override void OnEnable()
        {
            base.OnEnable();
            InitialValue = _value;
        }
        
        protected override void OnDisable()
        {
            if (!instanceSettings.autoResetValue)
                return;
            
            base.OnDisable();
        }
#else   
        protected override void OnEnterPlayMode()
        {
            base.OnEnterPlayMode();
            InitialValue = _value;
        }
        
        protected override void OnExitPlayMode()
        {
            if (!autoResetValue)
                return;
            
            base.OnExitPlayMode();
        }
#endif
    }
}