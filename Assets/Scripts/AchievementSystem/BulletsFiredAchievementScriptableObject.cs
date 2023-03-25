using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName ="BulletsFiredAchievementSO", menuName="ScriptableObjects/Achievements/BulletsFired")]
    public class BulletsFiredAchievementScriptableObject : ScriptableObject
    {
        public BulletsFiredAchievements[] achievements;

        [Serializable]
        public class BulletsFiredAchievements
        {
            public enum BulletsFiredAchievementType
            {
                None,
                FireTen,
                FireTwentyFive,
                FireFifty,
            }

            public string AchievementName;
            public string AchievementInfo;
            public RawImage AchievementImage;
            public BulletsFiredAchievementType bulletsFiredAchievementType;
            public int requirement;
        }
    }
}