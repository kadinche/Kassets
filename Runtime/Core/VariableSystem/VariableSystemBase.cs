using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Kassets.VariableSystem
{

    /// <summary>
    /// Basic Variable System which value can be handled Asynchronously with UniTask as core system.
    /// </summary>
    /// <typeparam name="T">Type to use on variable system</typeparam>
#if ODIN_INSPECTOR
    [InlineEditor()]
#endif
    public abstract class VariableSystemBase<T> : ScriptableObject, IAsyncReactiveProperty<T>, ISerializationCallbackReceiver, IDisposable
    {
        /// <summary>
        /// Initial Value of the Variable. Value is initialized on deserialize.
        /// </summary>
        [Tooltip("Initial Value of the Variable.")]
        [SerializeField] private T _value;
        private T _nonSerializedValue;
        
        /// <summary>
        /// Set to true to set last value as initial value on serialize.
        /// </summary>
        [Tooltip("If true will keep value when play mode end. Otherwise, reset to initial value.")]
        [SerializeField] private bool _keepValue;

        private readonly AsyncReactiveProperty<T> _asyncProperty = new AsyncReactiveProperty<T>(default);
        private CancellationTokenSource _cts = new CancellationTokenSource();
        
        /// <summary>
        /// Public value accessor.
        /// Get returns basic value.
        /// Set to both basic value and reactive value.
        /// </summary>
        public virtual T Value
        {
            get => _asyncProperty.Value;
            set
            {
                _asyncProperty.Value = value;
                if (_keepValue)
                    _value = value;
            }
        }

        /// <summary>
        /// Get initial value at play mode started.
        /// </summary>
        public T InitialValue { get; private set; }

        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken) => _asyncProperty.GetAsyncEnumerator(cancellationToken);
        public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator() => GetAsyncEnumerator(_cts.Token);
        
        public IUniTaskAsyncEnumerable<T> WithoutCurrent() => _asyncProperty.WithoutCurrent();

        public UniTask<T> WaitAsync(CancellationToken cancellationToken = default) => _asyncProperty.WaitAsync(cancellationToken);
        public UniTask<T> ValueAsync(CancellationToken cancellationToken) => _asyncProperty.WaitAsync(cancellationToken);
        public UniTask<T> ValueAsync() => ValueAsync(_cts.Token);
        
        /// <summary>
        /// Implicit cast to base type
        /// </summary>
        /// <param name="variable">variable system to cast</param>
        /// <returns>base type of variable system</returns>
        public static implicit operator T(VariableSystemBase<T> variable) => variable.Value;
        
        public override string ToString() => _asyncProperty.ToString();
        
        public void OnAfterDeserialize()
        {
            Cancel();   
            _asyncProperty.Value = _value;
            InitialValue = _value;
        }
        
        public void OnBeforeSerialize() { }
        
        public void Cancel(bool reset = true)
        {
            try
            {
                _cts?.Cancel();
                _cts?.Dispose();

                if (reset)
                    _cts = new CancellationTokenSource();
            }
            catch (Exception)
            {
                //
            }
        }

        public void Dispose()
        {
            Cancel(false);
            _asyncProperty?.Dispose();
        }

        private void OnDestroy() => Dispose();
    }
    
    public static class VariableSystemBaseAsyncExtension
    {
        public static UniTask<T>.Awaiter GetAwaiter<T>(this VariableSystemBase<T> source)
        {
            return source.ValueAsync().GetAwaiter();
        }
    }
}