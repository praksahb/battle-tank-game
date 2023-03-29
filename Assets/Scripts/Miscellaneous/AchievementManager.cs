using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Services
{
    public class AchievementManager : MonoBehaviour
    {
        [SerializeField] private AchievementTypes allAchievements;

        [SerializeField] private GameObject achievementPanel;

        [SerializeField] private RawImage achievementImage;
        [SerializeField] private TextMeshProUGUI achievementHeading;
        [SerializeField] private TextMeshProUGUI achievementText;
        [SerializeField] private float displayTime = 3f;

        private BulletsFiredAchievementSO bulletAchievementCurrent;
        private EnemiesKilledAchievementSO enemiesKilledAchievementCurrent;
        private CollectibleAchievementsSO ballsCollectedAchievementCurrent;

        private int bulletAchievementsIndex = 0;
        private int enemiesKilledAchievementsIndex = 0;
        private int ballsCollectedAchievementsIndex = 0;

        private Coroutine displayAchievementsCoroutine;
        private WaitForSecondsRealtime displayWaitTimer;

        private void Awake()
        {
            bulletAchievementCurrent = allAchievements.bulletsFiredAchievementList.bulletsFiredAchievementList[bulletAchievementsIndex];

            enemiesKilledAchievementCurrent = allAchievements.enemiesKilledAchievementList.enemiesKilledAchievementList[enemiesKilledAchievementsIndex];

            ballsCollectedAchievementCurrent = allAchievements.ballsCollectedAchievementsList.ballsCollectedAchievementList[ballsCollectedAchievementsIndex];
            displayWaitTimer = new WaitForSecondsRealtime(displayTime);

            EventService.Instance.OnBulletsFired += CheckBulletsFiredCount;
            EventService.Instance.OnEnemyKilled += CheckEnemyKillCount;
            EventService.Instance.OnBallCollected += CheckBallsCollected;
        }

        private void Start()
        {

        }

        private void OnDestroy()
        {
            EventService.Instance.OnBulletsFired -= CheckBulletsFiredCount;
            EventService.Instance.OnEnemyKilled -= CheckEnemyKillCount;
            EventService.Instance.OnBallCollected -= CheckBallsCollected;
        }

        private void CheckBulletsFiredCount(int bulletCount)
        {
            if(bulletCount == bulletAchievementCurrent.requirement)
            {
                achievementImage = bulletAchievementCurrent.AchievementImage;
                achievementHeading.text = bulletAchievementCurrent.AchievementName;
                achievementText.text = bulletAchievementCurrent.AchievementInfo;

                AchievementsDisplay();

                if(bulletAchievementsIndex < allAchievements.bulletsFiredAchievementList.bulletsFiredAchievementList.Length - 1)
                {
                    bulletAchievementsIndex++;
                    bulletAchievementCurrent = allAchievements.bulletsFiredAchievementList.bulletsFiredAchievementList[bulletAchievementsIndex];
                }
            }
        }

        private void CheckEnemyKillCount(int killCount)
        {
            Debug.Log($"Kill count: {killCount}");
            if (killCount == enemiesKilledAchievementCurrent.requirement)
            {
                achievementImage = enemiesKilledAchievementCurrent.AchievementImage;
                achievementHeading.text = enemiesKilledAchievementCurrent.AchievementName;
                achievementText.text = enemiesKilledAchievementCurrent.AchievementInfo;

                AchievementsDisplay();

                if(enemiesKilledAchievementsIndex < allAchievements.enemiesKilledAchievementList.enemiesKilledAchievementList.Length - 1)
                {
                    enemiesKilledAchievementsIndex++;
                    enemiesKilledAchievementCurrent = allAchievements.enemiesKilledAchievementList.enemiesKilledAchievementList[enemiesKilledAchievementsIndex];
                }
            }
        }

        private void CheckBallsCollected(int ballsCollected)
        {
            if (ballsCollected == ballsCollectedAchievementCurrent.requirement)
            {
                achievementImage = ballsCollectedAchievementCurrent.AchievementImage;
                achievementHeading.text = ballsCollectedAchievementCurrent.AchievementName;
                achievementText.text = ballsCollectedAchievementCurrent.AchievementInfo;

                AchievementsDisplay();

                if (ballsCollectedAchievementsIndex < allAchievements.ballsCollectedAchievementsList.ballsCollectedAchievementList.Length - 1)
                {
                    ballsCollectedAchievementsIndex++;
                    ballsCollectedAchievementCurrent = allAchievements.ballsCollectedAchievementsList.ballsCollectedAchievementList[ballsCollectedAchievementsIndex];
                }
            }
        }

        private void AchievementsDisplay()
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
