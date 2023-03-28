using System;
using TankBattle.Tank;

namespace TankBattle.Services
{
    public class EventService
    {
        public static EventService Instance { get; private set; } = new EventService();
        private EventService() { }

        public event Action OnBulletsFired;
        public event Action OnEnemyKilled;
        public event Action<int> OnBallCollected;

        public void InvokeOnBulletFiredEvent()
        {
            OnBulletsFired?.Invoke();
        }

        public void InvokeOnEnemyKilled()
        {
            OnEnemyKilled?.Invoke();
        }

        public void InvokeOnBallCollected(int count)
        {
            OnBallCollected?.Invoke(count);
        }
    };
}
