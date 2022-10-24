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
        public virtual T Value
        {
            get => _value;
            set => Raise(value);
        }

        public override void Raise(T value)
        {
            if (instanceSettings.variableEventType == VariableEventType.ValueChange && _value.Equals(value))
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
            if (!instanceSettings.autoResetValue)
                return;
            
            base.OnExitPlayMode();
        }
#endif
    }
    
#if !KASSETS_UNIRX || !KASSETS_UNITASK
    public abstract partial class VariableCore<T>
    {
        [SerializeField] protected InstanceSettings instanceSettings;
    }
#endif
}