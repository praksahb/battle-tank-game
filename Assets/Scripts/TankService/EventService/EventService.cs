using System;

namespace TankBattle.Services
{
    public class EventService
    {
        public static EventService Instance { get; private set; } = new EventService();
        private EventService() { }

        public event Action OnBulletsFired;

        public void InvokeOnBulletFiredEvent()
        {
            OnBulletsFired?.Invoke();
        }

        public event Action OnEnemyKilled;
        public void InvokeOnEnemyKilled()
        {
            OnEnemyKilled?.Invoke();
        }
    };
}
