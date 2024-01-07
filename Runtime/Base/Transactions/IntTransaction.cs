using UnityEngine;

namespace Kadinche.Kassets.Transaction
{
    [CreateAssetMenu(fileName = "IntTransaction", menuName = MenuHelper.DefaultTransactionMenu + "IntTransaction")]
    public class IntTransaction : TransactionCore<int>
    {
    }
}