using UnityEngine;

namespace Kadinche.Kassets.Transaction
{
    [CreateAssetMenu(fileName = "Vector3Transaction", menuName = MenuHelper.DefaultTransactionMenu + "Vector3Transaction")]
    public class Vector3Transaction : TransactionCore<Vector3>
    {
    }
}