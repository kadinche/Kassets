using UnityEngine;

namespace Kadinche.Kassets.Transaction
{
    [CreateAssetMenu(fileName = "PoseTransaction", menuName = MenuHelper.DefaultTransactionMenu + "PoseTransaction")]
    public class PoseTransaction : TransactionCore<Pose>
    {
    }
}