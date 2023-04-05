using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class ShellModel
    {
        public ShellModel(ShellScriptableObject bulletShell)
        {
            //LayerMask = bulletShell.layerMask;
            ExplosionForce = bulletShell.explosionForce;
            ExplosionRadius = bulletShell.explosionRadius;
            MaxDamage = bulletShell.maxDamage;
            HitColliders = bulletShell.bulletHitColliders;
        }

        //public LayerMask LayerMask { get; }
        public float ExplosionRadius { get; }
        public float ExplosionForce { get; }
        public float MaxDamage { get; }
        public int HitColliders { get; }
        public TankType SentBy { get; set; }
    }
}
