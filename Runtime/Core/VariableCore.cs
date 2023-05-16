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

        [Tooltip("If true will reset value when play mode end. Otherwise, keep runtime value. Due to shallow copying of class types, it is better avoid using autoResetValue on Class type.")]
        [SerializeField] protected bool autoResetValue;
        
        public virtual T Value
        {
            get => _value;
            set => Raise(value);
        }

        public override void Raise(T value)
        {
            if (variableEventType == VariableEventType.ValueChange && IsValueChanged(value)) return;
            base.Raise(value);
        }

        private bool IsValueChanged(T value) => _value == null && value == null ||
                                                _value != null && value != null && _value.Equals(value);
        
        private T _initialValue;
        private string _initialValueJsonString; // Hack to handle shallow copy of class type. Better to avoid this on class type.

        public virtual T InitialValue
        {
            get
            {
                if (!Type.IsSimpleType())
                {
                    _initialValue = JsonUtility.FromJson<T>(_initialValueJsonString);
                }
                return _initialValue;
            }
            protected set
            {
                if (!Type.IsSimpleType())
                {
                    _initialValueJsonString = JsonUtility.ToJson(value);
                }
                _initialValue = value;
            }
        }

        /// <summary>
        /// Reset value to InitialValue
        /// </summary>
        public void ResetValue() => Value = InitialValue;

        protected override void ResetInternal()
        {
            if (!autoResetValue) return;
            ResetValue();
        }

        public static implicit operator T(VariableCore<T> variable) => variable.Value;

        public override string ToString() => Value.ToString();

        protected override void OnEnable()
        {
            InitialValue = Value;
            base.OnEnable();
        }
    }
}