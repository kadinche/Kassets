using Kadinche.Kassets.EventSystem;
using UnityEngine;

namespace Kadinche.Kassets.Variable
{

    /// <summary>
    /// Basic Variable System which value can be handled Asynchronously with UniTask as core system.
    /// </summary>
    /// <typeparam name="T">Type to use on variable system</typeparam>
    public abstract class VariableBase<T> : GameEventBase<T>, IVariable<T>
    {
        /// <summary>
        /// Initial Value of the Variable. Value is initialized on deserialize.
        /// </summary>
        [Tooltip("Value of the Variable.")]
        [SerializeField] private T _value;

        /// <summary>
        /// Set to true to reset value on serialize.
        /// </summary>
        [Tooltip("If true will reset value when play mode end. Otherwise, keep runtime value.")]
        [SerializeField] private bool _autoResetValue;
        
        public virtual T Value
        {
            get => bufferedValue;
            set
            {
                bufferedValue = value;
                if (!_autoResetValue)
                {
                    _value = value;
                }
                Raise();
            }
        }

        private void Raise()
        {
            foreach (var disposable in disposables)
            {
                if (disposable is Subscription<T> valueSubscriber)
                {
                    valueSubscriber.Invoke(bufferedValue);
                }
                else if (disposable is Subscription subscriber)
                {
                    subscriber.Invoke();
                }
            }
        }

        /// <summary>
        /// Get initial value at play mode started.
        /// </summary>
        public T InitialValue { get; private set; }

        /// <summary>
        /// Implicit cast to base type
        /// </summary>
        /// <param name="variable">variable system to cast</param>
        /// <returns>base type of variable system</returns>
        public static implicit operator T(VariableBase<T> variable) => variable.Value;

        public override string ToString() => bufferedValue.ToString();
        
        public override void OnAfterDeserialize()
        {
            bufferedValue = _value;
            InitialValue = _value;
        }
    }
}