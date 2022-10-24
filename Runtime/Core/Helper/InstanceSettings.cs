using System;
using UnityEngine;

namespace Kadinche.Kassets
{
    [Serializable]
    public partial class InstanceSettings
    {
        [Tooltip("Set how variable event behave.\nValue Assign: Raise when value is assigned regardless of value.\nValue Changed: Raise only when value is changed.")]
        public VariableEventType variableEventType;

        [Tooltip("If true will reset value when play mode end. Otherwise, keep runtime value.")]
        public bool autoResetValue;
    }
    
    public enum VariableEventType
    {
        ValueAssign,
        ValueChange
    }
}