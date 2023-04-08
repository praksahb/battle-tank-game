using System;
using UnityEngine;

namespace TankBattle.Tank
{
    public class TankModel
    {
        public event Action HealthChanged;

        // read-only properties
        public TankType TankTypes { get; }
        public float Speed { get; }
        public float RotateSpeed { get; }
        public float JumpForce { get; }
        private float health;
        public float Health 
        { 
            get
            {
                return health;
            }
            set
            {
                health = value;
                HealthChanged?.Invoke();
            }
        }
        public Color Color { get; }
        public float bulletLaunchForce { get; }
        public float MaxChargeTime { get; }
        
        // public properties
        public int BulletsFired { get; set; }
        public int EnemiesKilled { get; set; }
        public int BallsCollected { get; set; }
        public bool IsDead { get; set; }

        // public get; private set;
        public int TankIndex { get; private set; }

        public TankModel(TankTypes.TankScriptableObject tankScriptableObject)
        {
            TankTypes = tankScriptableObject.tankType;
            Speed = tankScriptableObject.speed;
            RotateSpeed = tankScriptableObject.rotateSpeed;
            JumpForce = tankScriptableObject.jumpValue;
            Health = tankScriptableObject.health;
            Color = tankScriptableObject.tankColor;
            bulletLaunchForce = tankScriptableObject.bulletLaunchForce;
            MaxChargeTime = tankScriptableObject.maxChargeTime;
            BulletsFired = 0;
            EnemiesKilled = 0;
            BallsCollected = 0;
            IsDead = false;
        }

        public void SetTankIndex(int random_number)
        {
            TankIndex = random_number;
        }
    }
}