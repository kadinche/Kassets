#if KASSETS_UNIRX && KASSETS_UNITASK

using UnityEngine;

namespace Kadinche.Kassets
{
    public enum LibraryEnum
    {
        UniRx,
        UniTask
    }
    
    public partial class InstanceSettings
    {
        [Tooltip("Default Event Subscription behavior. UniRx for push-based, UniTask for pull-based.")]
        public LibraryEnum defaultSubscribeBehavior;
    }
}

#endif