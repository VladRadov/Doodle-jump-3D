using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementRunnerTwoThousand", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementRunnerTwoThousand")]
public class AchievementRunnerTwoThousand : AchievementRunner
{
    public override int CurrentCountSuccess => YandexGame.savesData.Runner2000CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.Runner2000CurrentCount)
            ++YandexGame.savesData.Runner2000CurrentCount;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.Runner2000CurrentCount)
            YandexGame.savesData.Runner2000CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.Runner2000CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
