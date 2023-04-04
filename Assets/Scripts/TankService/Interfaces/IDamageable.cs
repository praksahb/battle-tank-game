namespace TankBattle.Tank
{
    public interface IDamageable
    {
        public void Damage(UnityEngine.Vector3 impactPosition, float _explosionRadius, float _MaxDamage);
    }
}
