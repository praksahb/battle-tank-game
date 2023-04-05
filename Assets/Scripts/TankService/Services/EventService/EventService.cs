using System;
using TankBattle.Tank;
using TankBattle.Tank.UI;

namespace TankBattle.Services
{
    public class EventService
    {
        public static EventService Instance { get; private set; } = new EventService();
        private EventService() { }

        public event Action<int, AchievementType> OnAchievementEventTrigger;
        public void InvokeAchievementCallEvent(int count, AchievementType achievementType)
        {
            OnAchievementEventTrigger?.Invoke(count, achievementType);
        }
    };
}
