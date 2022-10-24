using System.Collections.Generic;
using UnityEditor;

namespace Kadinche.Kassets
{
    public static class CustomEditorExtension
    {
        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property)
        {
            property = property.Copy();
            var nextElement = property.Copy();
            if (!nextElement.NextVisible(false))
                nextElement = null;

            if (!property.NextVisible(true)) yield break;

            do
            {
                if (SerializedProperty.EqualContents(property, nextElement))
                    yield break;

                yield return property;
            } while (property.NextVisible(false));
        }
    }
}