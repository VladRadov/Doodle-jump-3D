using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementKillMonstersTen", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementKillMonstersTen")]
public class AchievementKillMonstersTen : AchievementKillMonsters
{
    public override int CurrentCountSuccess => YandexGame.savesData.KillMonsters10CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.KillMonsters10CurrentCount)
            ++YandexGame.savesData.KillMonsters10CurrentCount;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.KillMonsters10CurrentCount)
            YandexGame.savesData.KillMonsters10CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.KillMonsters10CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
