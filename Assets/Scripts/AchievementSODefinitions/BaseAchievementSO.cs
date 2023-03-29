using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle.Services
{
    public class BaseAchievementSO : ScriptableObject
    {
            public string AchievementName;
            public string AchievementInfo;
            public RawImage AchievementImage;
            public int requirement;
    }
}
