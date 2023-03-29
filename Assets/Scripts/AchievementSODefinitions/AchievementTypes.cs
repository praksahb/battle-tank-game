using UnityEngine;

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName = "AchievementList", menuName = "ScriptableObjects/Achievements/AchievementList")]
    public class AchievementTypes : ScriptableObject
    {
        public BulletsFiredAchievementList bulletsFiredAchievementList;
        public EnemiesKilledAchievementList enemiesKilledAchievementList;
        public BallsCollectedAchievementsList ballsCollectedAchievementsList;
    }
}
