using UnityEngine;

using YG;

[CreateAssetMenu(fileName = "AchievementKillMonstersTwenty", menuName = "ScriptableObject/Achievement/AchievementYG/AchievementKillMonstersTwenty")]
public class AchievementKillMonstersTwenty : AchievementKillMonsters
{
    public override int CurrentCountSuccess => YandexGame.savesData.KillMonsters20CurrentCount;

    public override void IncreaseCountSuccess()
    {
        if (MaxCountSuccess != YandexGame.savesData.KillMonsters20CurrentCount)
            ++YandexGame.savesData.KillMonsters20CurrentCount;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void IncreaseCountSuccess(int value)
    {
        if (MaxCountSuccess != YandexGame.savesData.KillMonsters20CurrentCount)
            YandexGame.savesData.KillMonsters20CurrentCount = value;

        YandexGame.SaveProgress();
        base.CheckSuccessAchievement();
    }

    public override void CheckSuccessAchievement()
    {
        if (base.MaxCountSuccess == YandexGame.savesData.KillMonsters20CurrentCount && base.IsAchievementSuccess == false)
            SetAchievementSuccess();
    }
}
