using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementEpicFailOneHundred", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementEpicFailOneHundred")]
public class AchievementEpicFailOneHundred : AchievementEpicFail
{
    public override int CurrentCountSuccess => YandexGame.savesData.EpicFail100CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.EpicFail100CurrentCount)
            ++YandexGame.savesData.EpicFail100CurrentCount;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.EpicFail100CurrentCount)
            YandexGame.savesData.EpicFail100CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.EpicFail100CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
