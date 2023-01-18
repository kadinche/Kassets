using System;

namespace Kadinche.Kassets.Variable.Sample
{
    public class CustomVariable : VariableCore<CustomStruct>
    {
    }

    [Serializable]
    public struct CustomStruct
    {
        public bool boolField;
        public int intField;
        public float floatField;
        public string stringField;
    }
}