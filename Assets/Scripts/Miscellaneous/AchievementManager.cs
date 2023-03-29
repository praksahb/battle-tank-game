using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Services
{
    public class AchievementManager : GenericSingleton<AchievementManager>
    {
        [SerializeField] private BulletsFiredAchievementList bulletsFiredAchievementList;
        [SerializeField] private EnemiesKilledAchievementList enemiesKilledAchievements;
        [SerializeField] private BallsCollectedAchievementsList ballsCollectedAchievements;

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
        private WaitForSecondsRealtime _waitTimer;

        protected override void Awake()
        {
            base.Awake();
            bulletAchievementCurrent = bulletsFiredAchievementList.bulletsFiredAchievementList[bulletAchievementsIndex];

            enemiesKilledAchievementCurrent = enemiesKilledAchievements.enemiesKilledAchievementList[enemiesKilledAchievementsIndex];

            ballsCollectedAchievementCurrent = ballsCollectedAchievements.ballsCollectedAchievementList[ballsCollectedAchievementsIndex];
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

                if(ballsCollectedAchievementsIndex < ballsCollectedAchievements.ballsCollectedAchievementList.Length - 1)
                {
                    ballsCollectedAchievementsIndex++;
                    ballsCollectedAchievementCurrent = ballsCollectedAchievements.ballsCollectedAchievementList[ballsCollectedAchievementsIndex];
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

                if(bulletAchievementsIndex < bulletsFiredAchievementList.bulletsFiredAchievementList.Length - 1)
                {
                    bulletAchievementsIndex++;
                    bulletAchievementCurrent = bulletsFiredAchievementList.bulletsFiredAchievementList[bulletAchievementsIndex];
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

                if(enemiesKilledAchievementsIndex < enemiesKilledAchievements.enemiesKilledAchievementList.Length - 1)
                {
                    enemiesKilledAchievementsIndex++;
                    enemiesKilledAchievementCurrent = enemiesKilledAchievements.enemiesKilledAchievementList[enemiesKilledAchievementsIndex];
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
            yield return _waitTimer;
            achievementPanel.SetActive(false);
        }
    }
}
