using TankBattle.Tank.EnemyTank;
using UnityEngine;

namespace TankBattle.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 switchYAndZValues(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }

        public static Vector3 DirFromAngle(this EnemyStateManager enemyObj, float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += enemyObj.transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public static bool IsInViewingAngle(this Transform self, Vector3 DistFromTarget, float viewAngle)
        {
            return Vector3.Dot(DistFromTarget, self.forward) > Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad);
        }
    }
}