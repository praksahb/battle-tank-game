using System;

namespace TankBattle.Services
{
    public class EventService
    {
        public static EventService Instance { get; private set; } = new EventService();
        private EventService() { }

        public event Action<int, AchievementType> OnAchievementEventTrigger;

        public event Action<float> OnHealthChange;

        public void  InvokeHealthChangeEvent(float value)
        {
            OnHealthChange?.Invoke(value);
        }

        // Function / Event Action name suggestion 
        public void InvokeAchievementCallEvent(int count, AchievementType achievementType)
        {
            OnAchievementEventTrigger?.Invoke(count, achievementType);
        }
    };
}
