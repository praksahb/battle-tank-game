using System;
using UnityEngine;

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName = "BulletsFiredAchievementSO", menuName = "ScriptableObjects/Achievements/BulletsFiredAchievementSO")]
    public class BulletsFiredAchievementSO : BaseAchievementSO
    {
        public enum BulletsAchievementType
        {
            None,
            Ten,
            Twentyfive,
            Fifty,
        }
        public BulletsAchievementType achievementType;

    }
}