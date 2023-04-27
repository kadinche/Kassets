using System.Collections.Generic;

namespace Kadinche.Kassets
{
    public static class DictionaryHelper
    {
        // source : https://stackoverflow.com/a/15728577
        public static bool TryChangeKey<TKey, TValue>(this IDictionary<TKey, TValue> dict, 
            TKey oldKey, TKey newKey)
        {
            TValue value;
            if (!dict.TryGetValue(oldKey, out value))
                return false;

            dict.Remove(oldKey);
            dict[newKey] = value;  // or dict.Add(newKey, value) depending on ur comfort
            return true;
        }
    }
}