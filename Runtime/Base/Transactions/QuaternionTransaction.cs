using UnityEngine;

namespace Kadinche.Kassets.Transaction
{
    [CreateAssetMenu(fileName = "QuaternionTransaction", menuName = MenuHelper.DefaultTransactionMenu + "QuaternionTransaction")]
    public class QuaternionTransaction : TransactionCore<Quaternion>
    {
    }
}