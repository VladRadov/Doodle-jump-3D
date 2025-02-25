using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementAstronomFifty", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementAstronomFifty")]
public class AchievementAstronomFifty : AchievementAstronom
{
    public override int CurrentCountSuccess => YandexGame.savesData.Astronom50CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.Astronom50CurrentCount)
            ++YandexGame.savesData.Astronom50CurrentCount;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.Astronom50CurrentCount)
            YandexGame.savesData.Astronom50CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.Astronom50CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
