using UnityEngine;
using UnityEngine.UI;

/*
 * * NOT TESTED YET * *
 * main goal
 * a single scriptable object - to access and create all achievements data from
 */

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName = "AchievementType2", menuName = "ScriptableObjects/Achievements/AchievementType2")]
    public class AchievementType2 : ScriptableObject
    {
        public BulletsFiredAchievements[] bulletFiredAchievements;
        public EnemiesKilledAchievements[] enemiesKilledAchievements;
        public BallsCollectedAchievemnt[] ballsCollectedAchievemnts;
    }

    // base class def
    [System.Serializable]
    public class BaseAchievement
    {
        public string AchievementName;
        public string AchievementInfo;
        public RawImage AchievementImage;
        public int requirement;
    }

    [System.Serializable]
    public class BallsCollectedAchievemnt
    {
        public BaseAchievement collectionAchievements;
    }

    [System.Serializable]
    public class BulletsFiredAchievements : BaseAchievement
    {
        public int ExtraValue;
    }

    [System.Serializable]
    public class EnemiesKilledAchievements
    {
        public BaseAchievement enemiesKilledAchievements;
    }
}