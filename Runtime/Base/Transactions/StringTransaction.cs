using UnityEngine;

namespace Kadinche.Kassets.Transaction
{
    [CreateAssetMenu(fileName = "StringTransaction", menuName = MenuHelper.DefaultTransactionMenu + "StringTransaction")]
    public class StringTransaction : TransactionCore<string>
    {
    }
}