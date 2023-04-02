using UnityEngine;

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName = "BulletsFiredAchievementSO", menuName = "ScriptableObjects/Achievements/BulletsFiredAchievementList")]
    public class BulletsFiredAchievementList : ScriptableObject
    {
        public BulletsFiredAchievementSO[] bulletsFiredAchievementList;
    }
}