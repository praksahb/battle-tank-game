using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Services
{
    public enum AchievementType
    {
        None,
        BulletFired,
        EnemiesKilled,
        BallsCollected,
    }
    // Achievement System 
    public class AchievementManager : MonoBehaviour
    {
        [SerializeField] private AchievementTypes allAchievements;

        [SerializeField] private GameObject achievementPanel;
        [SerializeField] private RawImage achievementImage;
        [SerializeField] private TextMeshProUGUI achievementHeading;
        [SerializeField] private TextMeshProUGUI achievementText;
        [SerializeField] private float displayTime = 3f;

        private BaseAchievementSO currentAchievement;
        private int bulletAchievementsIndex = 0;
        private int enemiesKilledAchievementsIndex = 0;
        private int ballsCollectedAchievementsIndex = 0;

        private Coroutine displayAchievementsCoroutine;
        private WaitForSecondsRealtime displayWaitTimer;

        private void OnEnable()
        {
            EventService.Instance.OnAchievementEventTrigger += AchievementEventHandler;
            displayWaitTimer = new WaitForSecondsRealtime(displayTime);
        }

        private void OnDisable()
        {
            EventService.Instance.OnAchievementEventTrigger -= AchievementEventHandler;
            displayWaitTimer = null;
        }

        private void AchievementEventHandler(int value, AchievementType achievementType)
        {
            if (!currentAchievement || currentAchievement.type != achievementType)
            {
                SetCurrentAchievement(achievementType);
            }
            if (value == currentAchievement.requirement)
            {
                achievementImage = currentAchievement.AchievementImage;
                achievementHeading.text = currentAchievement.AchievementName;
                achievementText.text = currentAchievement.AchievementInfo;

                DisplayAchievementsCoroutine();
                IncrementAchievementIndexes(achievementType);
            }
        }

        private void SetCurrentAchievement(AchievementType achievementType)
        {
            if(achievementType == AchievementType.BulletFired && 
                bulletAchievementsIndex <= allAchievements.bulletsFiredAchievementList.bulletsFiredAchievementList.Length - 1)
            {
                currentAchievement = allAchievements.bulletsFiredAchievementList.bulletsFiredAchievementList[bulletAchievementsIndex];
            }
            if(achievementType == AchievementType.EnemiesKilled &&
                enemiesKilledAchievementsIndex <= allAchievements.enemiesKilledAchievementList.enemiesKilledAchievementList.Length - 1)
            {
                currentAchievement = allAchievements.enemiesKilledAchievementList.enemiesKilledAchievementList[enemiesKilledAchievementsIndex];
            }
            if (achievementType == AchievementType.BallsCollected && 
                ballsCollectedAchievementsIndex <= allAchievements.ballsCollectedAchievementsList.ballsCollectedAchievementList.Length - 1)
            {
                currentAchievement = allAchievements.ballsCollectedAchievementsList.ballsCollectedAchievementList[ballsCollectedAchievementsIndex];
            }
        }

        private void IncrementAchievementIndexes(AchievementType achievementType)
        {
            if (achievementType == AchievementType.BulletFired)
            {
                bulletAchievementsIndex++;
            }
            if (achievementType == AchievementType.EnemiesKilled)
            {
                enemiesKilledAchievementsIndex++;
            }
            if (achievementType == AchievementType.BallsCollected)
            {
                ballsCollectedAchievementsIndex++;
            }

            //all achievements fulfilled
            if (bulletAchievementsIndex >= allAchievements.bulletsFiredAchievementList.bulletsFiredAchievementList.Length &&
                enemiesKilledAchievementsIndex >= allAchievements.enemiesKilledAchievementList.enemiesKilledAchievementList.Length &&
                ballsCollectedAchievementsIndex >= allAchievements.ballsCollectedAchievementsList.ballsCollectedAchievementList.Length)
            {
                // disable the event
                EventService.Instance.OnAchievementEventTrigger -= AchievementEventHandler;
                currentAchievement = null;
            } 
            else
            {
                SetCurrentAchievement(achievementType);
            }
        }

        private void DisplayAchievementsCoroutine()
        {
            if (displayAchievementsCoroutine != null)
            {
                StopCoroutine(displayAchievementsCoroutine);
            }
            displayAchievementsCoroutine = StartCoroutine(DisplayAchievements());
        }

        private IEnumerator DisplayAchievements()
        {
            achievementPanel.SetActive(true);
            yield return displayWaitTimer;
            achievementPanel.SetActive(false);
        }
    }
}
