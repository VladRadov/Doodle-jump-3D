using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementEpicFailFifty", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementEpicFailFifty")]
public class AchievementEpicFailFifty : AchievementEpicFail
{
    public override int CurrentCountSuccess => YandexGame.savesData.EpicFail50CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.EpicFail50CurrentCount)
            ++YandexGame.savesData.EpicFail50CurrentCount;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.EpicFail50CurrentCount)
            YandexGame.savesData.EpicFail50CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.EpicFail50CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
