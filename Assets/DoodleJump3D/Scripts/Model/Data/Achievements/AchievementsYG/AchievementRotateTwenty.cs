using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementRotateTwenty", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementRotateTwenty")]
public class AchievementRotateTwenty : AchievementRotate
{
    public override int CurrentCountSuccess => YandexGame.savesData.Rotate20CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.Rotate20CurrentCount)
            ++YandexGame.savesData.Rotate20CurrentCount;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.Rotate20CurrentCount)
            YandexGame.savesData.Rotate20CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.Rotate20CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
