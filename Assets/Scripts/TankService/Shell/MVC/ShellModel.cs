using UnityEngine;

namespace TankBattle.Tank.Bullets
{
    public class ShellModel
    {
        public ShellModel(ShellScriptableObject bulletShell)
        {
            LayerMask = bulletShell.layerMask;
            ExplosionForce = bulletShell.explosionForce;
            ExplosionRadius = bulletShell.explosionRadius;
            MaxDamage = bulletShell.maxDamage;
            MaxTanksBulletCanDamage = bulletShell.maxTanksBulletCanDamage;
        }

        public LayerMask LayerMask { get; }
        public float ExplosionRadius { get; }
        public float ExplosionForce { get; }
        public float MaxDamage { get; }
        public int MaxTanksBulletCanDamage { get; }
        public TankType SentBy { get; set; }
    }
}
