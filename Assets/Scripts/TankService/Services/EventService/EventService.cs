using System;
using TankBattle.Tank;

namespace TankBattle.Services
{
    public class EventService
    {
        public static EventService Instance { get; private set; } = new EventService();
        private EventService() { }

        public event Action<int, AchievementType> OnAchievementEventTrigger;

        public event Action OnHealthChange;

        public void  InvokeHealthChangeEvent()
        {
            OnHealthChange?.Invoke();
        }

        // Function / Event Action name suggestion 
        public void InvokeAchievementCallEvent(int count, AchievementType achievementType)
        {
            OnAchievementEventTrigger?.Invoke(count, achievementType);
        }
    };
}
