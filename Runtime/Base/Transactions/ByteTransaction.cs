using UnityEngine;

namespace Kadinche.Kassets.Transaction
{
    [CreateAssetMenu(fileName = "ByteTransaction", menuName = MenuHelper.DefaultTransactionMenu + "ByteTransaction")]
    public class ByteTransaction : TransactionCore<byte>
    {
    }
}