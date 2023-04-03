using UnityEngine;

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName = "CollectiblesAchievementSO", menuName = "ScriptableObjects/Achievements/BallsCollected")]
    public class CollectibleAchievementsSO : BaseAchievementSO
    {
        public enum CollectibleAchievementType
        {
            None,
            OneBall,
            TwoBall,
            ThreeBall,
        }
        public CollectibleAchievementType achievementType;
    }
}