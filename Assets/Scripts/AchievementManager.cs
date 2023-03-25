using System;
using System.Collections;
using TankBattle.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Services
{
    public class AchievementManager : MonoBehaviour
    {
        [SerializeField] private BulletsFiredAchievementScriptableObject bulletsFiredAchievements;

        [SerializeField] private GameObject achievementPanel;
        [SerializeField] private bool achievementActive = false;

        [SerializeField] private RawImage achievementImage;
        [SerializeField] private TextMeshProUGUI achievementHeading;
        [SerializeField] private TextMeshProUGUI achievementText;
        [SerializeField] private float displayTime = 3f;

        private BulletsFiredAchievementScriptableObject.BulletsFiredAchievements achievementSO;
        
        private BulletsFiredAchievementScriptableObject.BulletsFiredAchievements.BulletsFiredAchievementType achievementsIndex = BulletsFiredAchievementScriptableObject.BulletsFiredAchievements.BulletsFiredAchievementType.FireTen;

        private int bulletsFired = 0;
        private Coroutine coroutine;
        private WaitForSecondsRealtime _waitTimer;

        private void Awake()
        {
            _waitTimer = new WaitForSecondsRealtime(displayTime);
        }
        private void OnEnable()
        {
            EventService.Instance.OnBulletsFired += IncrementBulletCount;
            achievementSO = bulletsFiredAchievements.achievements[(int)achievementsIndex];
        }

        private void OnDisable()
        {
            EventService.Instance.OnBulletsFired -= IncrementBulletCount;
        }

        private void IncrementBulletCount()
        {
            bulletsFired++;
            Debug.Log($"Bullets Fired: {bulletsFired}");
            Debug.Log(achievementSO);
            if(bulletsFired == achievementSO.requirement)
            {                
                achievementImage = achievementSO.AchievementImage;
                achievementHeading.text = achievementSO.AchievementName;
                achievementText.text = achievementSO.AchievementInfo;

                //initialize next achievementSO
                achievementsIndex++;
                achievementSO = bulletsFiredAchievements.achievements[(int)achievementsIndex];

                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = StartCoroutine(DisplayAchievements());
            }
        }

        private IEnumerator DisplayAchievements()
        {
            achievementPanel.SetActive(true);
            yield return _waitTimer;
            achievementPanel.SetActive(false);
        }
    }
}
