using System;

namespace TankBattle.Services
{
    public class EventService
    {
        public static EventService Instance { get; private set; } = new EventService();
        private EventService() { }

        public event Action<int, AchievementType> OnAchievementEventTrigger;


        // Function name suggestion 
        public void InvokeAchievementCallEvent(int count, AchievementType achievementType)
        {
            OnAchievementEventTrigger?.Invoke(count, achievementType);
        }
    };
}
