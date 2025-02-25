using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementRunnerOneThousand", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementRunnerOneThousand")]
public class AchievementRunnerOneThousand : AchievementRunner
{
    public override int CurrentCountSuccess => YandexGame.savesData.Runner1000CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.Runner1000CurrentCount)
        {
            ++YandexGame.savesData.Runner1000CurrentCount;
        }

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.Runner1000CurrentCount)
            YandexGame.savesData.Runner1000CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.Runner1000CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
