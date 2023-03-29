using UnityEngine;

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName = "AchievementType2", menuName = "ScriptableObjects/Achievements/AchievementType2")]
    public class AchievementType2 : ScriptableObject
    {
        [System.Serializable]
        public class AchievementList
        {
            public BulletsFiredAchievementSO[] bulletsAchievementList;
            public EnemiesKilledAchievementSO[] enemiesKilledAchievementList;
            public CollectibleAchievementsSO[] ballsCollectedAchievementList;
        }
        public AchievementList achievements;
    }
}
