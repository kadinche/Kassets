using UnityEngine;

namespace Kassets.Collection
{
    [CreateAssetMenu(fileName = "StringCollection", menuName = MenuHelper.DefaultCollectionMenu + "StringCollection")]
    public class StringCollection : Collection<string>
    {
    }
}