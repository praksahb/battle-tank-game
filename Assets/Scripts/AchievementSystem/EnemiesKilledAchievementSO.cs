using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName = "EnemiesKilledAchievementSO", menuName = "ScriptableObjects/Achievements/EnemiesKilled")]
    public class EnemiesKilledAchievementSO : ScriptableObject
    {

        [Serializable]
        public class EnemiesKilledAchievements
        {
            public enum EnemiesKilledAchievementType
            {
                None,
                Novice,
                Beginner,
                Expert
            }

            public string AchievementName;
            public string AchievementInfo;
            public RawImage AchievementImage;
            public EnemiesKilledAchievementType enemiesKilledAchievementType;
            public int requirement;
        }

        public EnemiesKilledAchievements[] achievements;
    }
}
