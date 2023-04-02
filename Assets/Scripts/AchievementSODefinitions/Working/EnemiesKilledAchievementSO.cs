using UnityEngine;

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName = "EnemiesKilledAchievementSO", menuName = "ScriptableObjects/Achievements/EnemiesKilledAchievementSO")]
    public class EnemiesKilledAchievementSO : BaseAchievementSO
    {
        public enum EnemiesAchievementType
        {
            None,
            OneKill,
            ThreeKill,
            FiveKill,
        }
        public EnemiesAchievementType achievementType;
    }
}
