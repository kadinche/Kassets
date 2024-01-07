using UnityEngine;

namespace Kadinche.Kassets.Transaction
{
    [CreateAssetMenu(fileName = "DoubleTransaction", menuName = MenuHelper.DefaultTransactionMenu + "DoubleTransaction")]
    public class DoubleTransaction : TransactionCore<double>
    {
    }
}