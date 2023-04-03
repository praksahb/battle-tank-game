using UnityEngine;

namespace TankBattle.Services
{
    [CreateAssetMenu(fileName = "CollectiblesAchievementList", menuName = "ScriptableObjects/Achievements/BallsCollectedAchievementsList")]
    public class BallsCollectedAchievementsList : ScriptableObject
    {
        public CollectibleAchievementsSO[] ballsCollectedAchievementList;
    }
}