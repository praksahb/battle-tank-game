using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Services
{
    [System.Serializable]
    public class BaseAchievementSO : ScriptableObject
    {
            public string AchievementName;
            public string AchievementInfo;
            public RawImage AchievementImage;
            public int requirement;
    }
}
