using System;
using System.Collections;
using TankBattle.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Services
{
    public class AchievementManager : GenericSingleton<AchievementManager>
    {
        [SerializeField] private BulletsFiredAchievementScriptableObject bulletsFiredAchievements;
        [SerializeField] private EnemiesKilledAchievementSO enemiesKilledAchievements;
        [SerializeField] private CollectibleAchievementsSO ballsCollectedAchievements;

        [SerializeField] private GameObject achievementPanel;

        [SerializeField] private RawImage achievementImage;
        [SerializeField] private TextMeshProUGUI achievementHeading;
        [SerializeField] private TextMeshProUGUI achievementText;
        [SerializeField] private float displayTime = 3f;

        private BulletsFiredAchievementScriptableObject.BulletsFiredAchievements bulletAchievementCurrent;
        private EnemiesKilledAchievementSO.EnemiesKilledAchievements enemiesKilledAchievementCurrent;
        private CollectibleAchievementsSO.CollectiblesAchievements ballsCollectedAchievementCurrent;

        private int bulletAchievementsIndex = 0;
        private int enemiesKilledAchievementsIndex = 0;
        private int ballsCollectedAchievementsIndex = 0;

        private Coroutine coroutine;
        private WaitForSecondsRealtime _waitTimer;

        private void OnEnable()
        {
            bulletAchievementCurrent = bulletsFiredAchievements.achievements[bulletAchievementsIndex];
            enemiesKilledAchievementCurrent = enemiesKilledAchievements.achievements[enemiesKilledAchievementsIndex];
            ballsCollectedAchievementCurrent = ballsCollectedAchievements.achievements[ballsCollectedAchievementsIndex];
            _waitTimer = new WaitForSecondsRealtime(displayTime);
        }

        private void Start()
        {
            EventService.Instance.OnBallCollected += CheckBallsCollected;
        }

        private void OnDisable()
        {
            EventService.Instance.OnBallCollected -= CheckBallsCollected;
        }

        private void CheckBallsCollected(int ballsCollected)
        {
            if(ballsCollected == ballsCollectedAchievementCurrent.requirement)
            {
                achievementImage = ballsCollectedAchievementCurrent.AchievementImage;
                achievementHeading.text = ballsCollectedAchievementCurrent.AchievementName;
                achievementText.text = ballsCollectedAchievementCurrent.AchievementInfo;

                AchievementsDisplay();

                if(ballsCollectedAchievementsIndex < ballsCollectedAchievements.achievements.Length - 1)
                {
                    ballsCollectedAchievementsIndex++;
                    ballsCollectedAchievementCurrent = ballsCollectedAchievements.achievements[ballsCollectedAchievementsIndex];
                }
            }
        }

        public void CheckBulletsFiredCount(int bulletCount)
        {
            if(bulletCount == bulletAchievementCurrent.requirement)
            {
                achievementImage = bulletAchievementCurrent.AchievementImage;
                achievementHeading.text = bulletAchievementCurrent.AchievementName;
                achievementText.text = bulletAchievementCurrent.AchievementInfo;

                AchievementsDisplay();

                if(bulletAchievementsIndex < bulletsFiredAchievements.achievements.Length - 1)
                {
                    bulletAchievementsIndex++;
                    bulletAchievementCurrent = bulletsFiredAchievements.achievements[bulletAchievementsIndex];
                }
            }
        }

        public void CheckEnemyKillCount(int killCount)
        {
            if(killCount == enemiesKilledAchievementCurrent.requirement)
            {
                achievementImage = enemiesKilledAchievementCurrent.AchievementImage;
                achievementHeading.text = enemiesKilledAchievementCurrent.AchievementName;
                achievementText.text = enemiesKilledAchievementCurrent.AchievementInfo;

                AchievementsDisplay();

                if(enemiesKilledAchievementsIndex < enemiesKilledAchievements.achievements.Length - 1)
                {
                    enemiesKilledAchievementsIndex++;
                    enemiesKilledAchievementCurrent = enemiesKilledAchievements.achievements[enemiesKilledAchievementsIndex];
                }
            }
        }

        private void AchievementsDisplay()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(DisplayAchievements());
        }

        private IEnumerator DisplayAchievements()
        {
            achievementPanel.SetActive(true);
            yield return _waitTimer;
            achievementPanel.SetActive(false);
        }
    }
}
