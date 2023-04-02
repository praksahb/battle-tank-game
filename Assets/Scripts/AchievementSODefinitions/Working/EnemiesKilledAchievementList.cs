using UnityEngine;

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName = "EnemiesKilledAchievementSO", menuName = "ScriptableObjects/Achievements/EnemiesKilledAchievementList")]
    public class EnemiesKilledAchievementList : ScriptableObject
    {
        public EnemiesKilledAchievementSO[] enemiesKilledAchievementList;
    }
}
