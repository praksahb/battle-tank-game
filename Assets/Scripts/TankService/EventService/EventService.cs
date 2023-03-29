using System;

namespace TankBattle.Services
{
    public class EventService
    {
        public static EventService Instance { get; private set; } = new EventService();
        private EventService() { }

        public event Action<int> OnBulletsFired;
        public event Action<int> OnEnemyKilled;
        public event Action<int> OnBallCollected;

        public void InvokeOnBulletFiredEvent(int count)
        {
            OnBulletsFired?.Invoke(count);
        }

        public void InvokeOnEnemyKilled(int count)
        {
            OnEnemyKilled?.Invoke(count);
        }

        public void InvokeOnBallCollected(int count)
        {
            OnBallCollected?.Invoke(count);
        }
    };
}
