using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    [CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/Bullet")]
    public class BulletScriptableObject : ScriptableObject
    {
        public BulletView shellView;
        public float explosionRadius;
        public float explosionForce;
        public LayerMask layerMask;
        public float maxDamage;
        public int bulletHitColliders;
        public float bulletLaunchForce;
    }
}
