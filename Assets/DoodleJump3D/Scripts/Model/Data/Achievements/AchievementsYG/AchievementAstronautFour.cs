using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementAstronautFour", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementAstronautFour")]
public class AchievementAstronautFour : AchievementAstronaut
{
    public override int CurrentCountSuccess => YandexGame.savesData.Astronaut4CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.Astronaut4CurrentCount)
            ++YandexGame.savesData.Astronaut4CurrentCount;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.Astronaut4CurrentCount)
            YandexGame.savesData.Astronaut4CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.Astronaut4CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
