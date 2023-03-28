
using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName = "CollectiblesAchievementSO", menuName = "ScriptableObjects/Achievements/BallsCollected")]
    public class CollectibleAchievementsSO : ScriptableObject
    {
        [Serializable]
        public class CollectiblesAchievements
        {
             public enum  BallsCollectedAchievementType
            {
                None,
                Ballvivaah,
                BallaballWeekly,
                BallHiBallHogaya
            }

            public string AchievementName;
            public string AchievementInfo;
            public RawImage AchievementImage;
            public BallsCollectedAchievementType ballsCollectedAchievementType;
            public int requirement;
        }

        public CollectiblesAchievements[] achievements;
    }
}
