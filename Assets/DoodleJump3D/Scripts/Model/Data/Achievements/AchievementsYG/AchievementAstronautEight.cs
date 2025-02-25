using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementAstronautEight", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementAstronautEight")]
public class AchievementAstronautEight : AchievementAstronaut
{
    public override int CurrentCountSuccess => YandexGame.savesData.Astronaut8CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.Astronaut8CurrentCount)
            ++YandexGame.savesData.Astronaut8CurrentCount;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.Astronaut8CurrentCount)
            YandexGame.savesData.Astronaut8CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.Astronaut8CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
