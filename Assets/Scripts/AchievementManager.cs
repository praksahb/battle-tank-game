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

        [SerializeField] private GameObject achievementPanel;

        [SerializeField] private RawImage achievementImage;
        [SerializeField] private TextMeshProUGUI achievementHeading;
        [SerializeField] private TextMeshProUGUI achievementText;
        [SerializeField] private float displayTime = 3f;

        private BulletsFiredAchievementScriptableObject.BulletsFiredAchievements bulletAchievementCurrent;
        private EnemiesKilledAchievementSO.EnemiesKilledAchievements enemiesKilledAchivementCurrent;

        private int bulletAchievementsIndex = 0;
        private int enemiesKilledAchievementsIndex = 0;

        private Coroutine coroutine;
        private WaitForSecondsRealtime _waitTimer;

        private void OnEnable()
        {
            bulletAchievementCurrent = bulletsFiredAchievements.achievements[bulletAchievementsIndex];
            enemiesKilledAchivementCurrent = enemiesKilledAchievements.achievements[enemiesKilledAchievementsIndex];
            _waitTimer = new WaitForSecondsRealtime(displayTime);
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
            if(killCount == enemiesKilledAchivementCurrent.requirement)
            {
                achievementImage = enemiesKilledAchivementCurrent.AchievementImage;
                achievementHeading.text = enemiesKilledAchivementCurrent.AchievementName;
                achievementText.text = enemiesKilledAchivementCurrent.AchievementInfo;

                AchievementsDisplay();

                if(enemiesKilledAchievementsIndex < enemiesKilledAchievements.achievements.Length - 1)
                {
                    enemiesKilledAchievementsIndex++;
                    enemiesKilledAchivementCurrent = enemiesKilledAchievements.achievements[enemiesKilledAchievementsIndex];
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
