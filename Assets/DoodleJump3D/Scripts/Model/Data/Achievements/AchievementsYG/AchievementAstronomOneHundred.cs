using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementAstronomOneHundred", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementAstronomOneHundred")]
public class AchievementAstronomOneHundred : AchievementAstronom
{
    public override int CurrentCountSuccess => YandexGame.savesData.Astronom100CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.Astronom100CurrentCount)
            ++YandexGame.savesData.Astronom100CurrentCount;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.Astronom100CurrentCount)
            YandexGame.savesData.Astronom100CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.Astronom100CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
