using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementRotateTen", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementRotateTen")]
public class AchievementRotateTen : AchievementRotate
{
    public override int CurrentCountSuccess => YandexGame.savesData.Rotate10CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.Rotate10CurrentCount)
            ++YandexGame.savesData.Rotate10CurrentCount;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.Rotate10CurrentCount)
            YandexGame.savesData.Rotate10CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.Rotate10CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
