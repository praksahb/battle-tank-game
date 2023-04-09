namespace TankBattle.Tank.Bullets
{
    public class BulletModel
    {
        public BulletModel(BulletScriptableObject bulletSO)
        {
            LayerMask = bulletSO.layerMask;
            ExplosionForce = bulletSO.explosionForce;
            ExplosionRadius = bulletSO.explosionRadius;
            MaxDamage = bulletSO.maxDamage;
            HitColliders = bulletSO.bulletHitColliders;
            BulletLaunchForce = bulletSO.bulletLaunchForce;
        }

        public UnityEngine.LayerMask LayerMask { get; }
        public float ExplosionRadius { get; }
        public float ExplosionForce { get; }
        public float MaxDamage { get; }
        public int HitColliders { get; }
        public float BulletLaunchForce { get; }
        public TankType SentBy { get; set; }
    }
}
