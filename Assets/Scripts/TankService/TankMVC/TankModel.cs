using UnityEngine;

namespace TankBattle.Tank
{
    public class TankModel
    {
        // read-only properties
        public TankType TankTypes { get; }
        public float Speed { get; }
        public float RotateSpeed { get; }
        public float JumpForce { get; }
        public float Health { get; set; }
        public Color Color { get; }
        public float MinLaunchForce { get; }
        public float MaxLaunchForce { get; }
        public float MaxChargeTime { get; }
        public int BulletsFired { get; set; }
        public int EnemiesKilled { get; set; }
        public int BallsCollected { get; set; }

        public TankModel(TankTypes.TankScriptableObject tankScriptableObject)
        {
            TankTypes = tankScriptableObject.tankType;
            Speed = tankScriptableObject.speed;
            RotateSpeed = tankScriptableObject.rotateSpeed;
            JumpForce = tankScriptableObject.jumpValue;
            Health = tankScriptableObject.health;
            Color = tankScriptableObject.tankColor;
            MinLaunchForce = tankScriptableObject.minLaunchForce;
            MaxLaunchForce = tankScriptableObject.maxLaunchForce;
            MaxChargeTime = tankScriptableObject.maxChargeTime;
            BulletsFired = 0;
            EnemiesKilled = 0;
            BallsCollected = 0;
        }
    }
}