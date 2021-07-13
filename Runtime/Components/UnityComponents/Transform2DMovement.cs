using UnityEngine;

namespace Kadinche.Kassets.UnityComponents
{
    public class Transform2DMovement : Transform3DMovement
    {
        protected override Vector3 GetDistance(Transform target, Vector2 moveAxis) => target.up * moveAxis.y + target.right * moveAxis.x;
    }
}