using UnityEngine;

namespace Kadinche.Kassets.Transaction
{
    [CreateAssetMenu(fileName = "BoolTransaction", menuName = MenuHelper.DefaultTransactionMenu + "BoolTransaction")]
    public class BoolTransaction : TransactionCore<bool>
    {
    }
}