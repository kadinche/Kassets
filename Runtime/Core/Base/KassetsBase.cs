using System;
using UnityEngine;

namespace Kadinche.Kassets
{
    public abstract class KassetsBase : ScriptableObject, ISerializationCallbackReceiver, IDisposable
    {
        public virtual void OnBeforeSerialize() {}
        public virtual void OnAfterDeserialize() {}
        public abstract void Dispose();
        protected virtual void OnDestroy() => Dispose();
    }
}